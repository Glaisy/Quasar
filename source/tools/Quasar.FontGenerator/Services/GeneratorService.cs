//-----------------------------------------------------------------------
// <copyright file="GeneratorService.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Windows.Forms;

using Quasar.FontGenerator.Extensions;
using Quasar.FontGenerator.Models;
using Quasar.Graphics.Internals;

namespace Quasar.FontGenerator.Services
{
    /// <summary>
    /// Represents a service to generate font data and preview images.
    /// </summary>
    internal sealed class GeneratorService
    {
        /// <summary>
        /// The number of characters per page.
        /// </summary>
        public const int CharactersPerPage = 256;

        /// <summary>
        /// The font texture's width in pixels.
        /// </summary>
        public const int FontTextureWidth = 768;


        private static readonly Size proposedCharacterSize = new Size(Int32.MaxValue, Int32.MaxValue);

        /// <summary>
        /// Exports the font to the specified file.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="filePath">The file path.</param>
        public bool ExportFont(
            IFontDataSettings settings,
            string filePath)
        {
            var fontData = GenerateFontData(settings);
            if (fontData == null)
            {
                return false;
            }

            return ExportFontData(fontData.Value, filePath);
        }

        /// <summary>
        /// Generates a preview bitmap by the specified settings, font and colors.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="fontStyle">The font style.</param>
        /// <param name="foregroundColor">The foreground color.</param>
        /// <param name="backgroundColor">The background color.</param>
        /// <returns>
        /// The generated bitmap.
        /// </returns>
        public Bitmap GeneratePreviewBitmap(
            IFontDataSettings settings,
            FontStyle fontStyle,
            in Color foregroundColor,
            in Color backgroundColor)
        {
            using (var font = CreateFont(settings, fontStyle))
            {
                var fontFamilyData = CreateFontFamilyData(settings);
                var fontStyleData = CreateFontStyleData(font, settings, fontFamilyData);
                return GenerateBitmapInternal(
                    font,
                    settings,
                    fontFamilyData,
                    fontStyleData,
                    foregroundColor,
                    backgroundColor,
                    false,
                    false);
            }
        }


        private static float ConvertValueToUV(int value, float size)
        {
            return value / size;
        }

        private static Font CreateFont(IFontDataSettings settings, FontStyle fontStyle)
        {
            return new Font(settings.FontFamilyName, settings.BaseSize, fontStyle, GraphicsUnit.Pixel);
        }

        private static FontFamilyData CreateFontFamilyData(IFontDataSettings settings)
        {
            var characterCount = CharactersPerPage * settings.PageCount - settings.FirstCharacter;
            return new FontFamilyData(settings.BaseSize, characterCount);
        }

        private static FontStyleInformation CreateFontStyleInformation(
            IFontDataSettings settings,
            FontStyle fontStyle,
            in FontFamilyData fontFamilyData,
            in FontStyleData fontStyleData)
        {
            // calculate uvs and widths
            var offsetX = settings.Padding + settings.HorizontalOffset;
            var offsetY = settings.Padding + settings.VerticalOffset;
            var sizeX = (float)fontStyleData.TextureSize.Width;
            var sizeY = (float)fontStyleData.TextureSize.Height;
            var fontStyleInformation = new FontStyleInformation
            {
                Id = fontStyle.ToFontStyle(),
                Ascent = fontStyleData.Ascent,
                Descent = fontStyleData.Descent,
                LineSpacing = fontStyleData.LineSpacing,
                CharacterWidths = fontStyleData.CharacterWidths,
                UVs = new List<Vector2>()
            };

            for (int i = settings.FirstCharacter, j = 0; j < fontFamilyData.CharacterCount; i++, j++)
            {
                var columIndex = j % fontStyleData.ColumnCount;
                var rowIndex = j / fontStyleData.ColumnCount;

                // create uvs
                var characterWidth = (int)MathF.Ceiling(fontStyleData.CharacterWidths[j]);
                var x0 = offsetX + columIndex * fontStyleData.CellDistance.Width;
                var x1 = x0 + characterWidth;
                var y1 = offsetY + rowIndex * fontStyleData.CellDistance.Height;
                var y0 = y1 + fontStyleData.CellSize.Height;

                var u0 = ConvertValueToUV(x0, sizeX);
                var u1 = ConvertValueToUV(x1, sizeX);
                var v0 = 1.0f - ConvertValueToUV(y0, sizeY);
                var v1 = 1.0f - ConvertValueToUV(y1, sizeY);

                // add uvs (bottom left, top left, top right, bottom right)
                fontStyleInformation.UVs.Add(new Vector2(u0, v0));
                fontStyleInformation.UVs.Add(new Vector2(u0, v1));
                fontStyleInformation.UVs.Add(new Vector2(u1, v1));
                fontStyleInformation.UVs.Add(new Vector2(u1, v0));
            }

            return fontStyleInformation;
        }

        private static FontStyleData CreateFontStyleData(Font font, IFontDataSettings settings, in FontFamilyData fontFamilyData)
        {
            // calculate font metrics
            var fontFamily = font.FontFamily;
            var fontStyle = font.Style;

            var emHeight = fontFamily.GetEmHeight(fontStyle);
            var emAscent = fontFamily.GetCellAscent(fontStyle);
            var emDescent = fontFamily.GetCellDescent(fontStyle);
            var emLineSpacing = fontFamily.GetLineSpacing(fontStyle);

            var ascentPixels = font.ConvertEmToPixels(emHeight, emAscent);
            var descentPixels = font.ConvertEmToPixels(emHeight, emDescent);
            var lineSpacingPixels = ascentPixels + descentPixels;

            // measure glyphs
            var fontMeasurementData = MeasureFont(font, settings, fontFamilyData);
            var cellSize = new Graphics.Size((int)MathF.Ceiling(fontMeasurementData.MaxWidth), lineSpacingPixels);
            var cellDistance = new Graphics.Size(cellSize.Width + settings.Padding, cellSize.Height + settings.Padding);

            // calculate texture details
            var columnCount = (FontTextureWidth - settings.Padding) / cellDistance.Width;
            var rowCount = (fontFamilyData.CharacterCount + columnCount - 1) / columnCount;
            var fontTextureHeight = settings.Padding + rowCount * cellDistance.Height;

            // return font style data
            return new FontStyleData
            {
                Ascent = ascentPixels,
                Descent = descentPixels,
                CellSize = cellSize,
                CellDistance = cellDistance,
                LineSpacing = lineSpacingPixels,

                ColumnCount = columnCount,
                TextureSize = new Graphics.Size(FontTextureWidth, fontTextureHeight),
                CharacterWidths = fontMeasurementData.Widths
            };
        }

        private static bool ExportFontData(in FontData fontData, string filePath)
        {
            Stream stream = null;
            try
            {
                stream = File.Create(filePath);

                using (var zipArchive = new ZipArchive(stream, ZipArchiveMode.Create))
                {
                    var zipEntry = zipArchive.CreateEntry(FontFamilyConstants.ZipEntryName);
                    using (var zipStream = zipEntry.Open())
                    {
                        JsonSerializer.Serialize(zipStream, fontData.FontFamily);
                    }

                    foreach (var pair in fontData.Bitmaps)
                    {
                        var entryName = $"{(int)pair.Key}.png";
                        zipEntry = zipArchive.CreateEntry(entryName);
                        using (var zipStream = zipEntry.Open())
                        {
                            pair.Value.Save(zipStream, ImageFormat.Png);
                        }
                    }

                    return true;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                foreach (var bitmap in fontData.Bitmaps.Values)
                {
                    bitmap.Dispose();
                }

                stream?.Dispose();
            }
        }

        private static unsafe void FilterBitmap(Bitmap bitmap, Color foregroundColor)
        {
            BitmapData bitmapData = null;
            try
            {
                var bitmapRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                bitmapData = bitmap.LockBits(bitmapRect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                var foregroundColorArgb = foregroundColor.ToArgb();
                var ptrLineStart = (int*)bitmapData.Scan0.ToPointer();
                var stride = bitmapData.Stride >> 2;
                for (var y = 0; y < bitmapData.Height; y++, ptrLineStart += stride)
                {
                    var ptrPixel = ptrLineStart;
                    for (var x = 0; x < bitmapData.Width; x++, ptrPixel++)
                    {
                        if (*ptrPixel == 0)
                        {
                            continue;
                        }

                        if (*ptrPixel != foregroundColorArgb)
                        {
                        }

                        *ptrPixel = foregroundColorArgb;
                    }
                }
            }
            finally
            {
                if (bitmapData != null)
                {
                    bitmap.UnlockBits(bitmapData);
                }
            }
        }

        private static unsafe Bitmap GenerateBitmapInternal(
            Font font,
            IFontDataSettings settings,
            in FontFamilyData fontFamilyData,
            in FontStyleData fontStyleData,
            in Color foregroundColor,
            in Color backgroundColor,
            bool flipVertically,
            bool filterImage)
        {
            Bitmap bitmap = null;
            System.Drawing.Graphics graphics = null;
            Brush brush = null;
            Brush cellBrush = null;
            var shouldFillCells = backgroundColor != Color.Transparent;
            try
            {
                // create image
                bitmap = new Bitmap(fontStyleData.TextureSize.Width, fontStyleData.TextureSize.Height, PixelFormat.Format32bppArgb);

                // create GDI graphics for the image
                graphics = System.Drawing.Graphics.FromImage(bitmap);
                graphics.PixelOffsetMode = PixelOffsetMode.Half;
                graphics.SmoothingMode = SmoothingMode.Default;
                graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                if (shouldFillCells)
                {
                    graphics.Clear(Color.Magenta);
                    cellBrush = new SolidBrush(backgroundColor);
                }
                else
                {
                    graphics.Clear(backgroundColor);
                }

                brush = new SolidBrush(foregroundColor);

                // iterate characters
                var padding = settings.Padding;
                var offsetX = settings.HorizontalOffset;
                var offsetY = settings.VerticalOffset;
                var characterArray = stackalloc char[1];
                var characterSpan = new Span<char>(characterArray, 1);
                for (int i = settings.FirstCharacter, j = 0; j < fontFamilyData.CharacterCount; i++, j++)
                {
                    // get character string
                    var c = (char)i;
                    if (Char.IsWhiteSpace(c))
                    {
                        continue;
                    }

                    characterArray[0] = c;

                    // get column and row indices
                    var columIndex = j % fontStyleData.ColumnCount;
                    var rowIndex = j / fontStyleData.ColumnCount;

                    // cell top left
                    var x = padding + columIndex * fontStyleData.CellDistance.Width;
                    var y = padding + rowIndex * fontStyleData.CellDistance.Height;

                    // draw cell
                    if (shouldFillCells)
                    {
                        graphics.FillRectangle(cellBrush, x, y, fontStyleData.CharacterWidths[j], fontStyleData.CellSize.Height);
                    }

                    // draw character
                    graphics.DrawString(
                        characterSpan,
                        font,
                        brush,
                        offsetX + x,
                        offsetY + y,
                        StringFormat.GenericTypographic);
                }

                // dispose GDI graphics
                graphics.Dispose();
                brush.Dispose();

                // post process bitmap
                if (filterImage)
                {
                    FilterBitmap(bitmap, foregroundColor);
                }

                // flip bitmap
                if (flipVertically)
                {
                    bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                }

                return bitmap;
            }
            catch
            {
                cellBrush?.Dispose();
                brush?.Dispose();
                graphics?.Dispose();
                bitmap?.Dispose();

                throw;
            }
        }

        private static FontData? GenerateFontData(IFontDataSettings settings)
        {
            var fontData = new FontData
            {
                Bitmaps = new Dictionary<Quasar.Graphics.FontStyle, Bitmap>(),
                FontFamily = new Graphics.Internals.FontFamily
                {
                    Id = String.IsNullOrEmpty(settings.FontFamilyNameOverride) ?
                        settings.FontFamilyName :
                        settings.FontFamilyNameOverride,
                    CharacterSpacing = settings.CharacterSpacing,
                    FallbackCharacter = settings.FallbackCharacter,
                    FirstCharacter = settings.FirstCharacter,
                }
            };

            try
            {
                var fontFamilyData = CreateFontFamilyData(settings);
                fontData.FontFamily.BaseSize = fontFamilyData.BaseSize;
                fontData.FontFamily.CharacterCount = fontFamilyData.CharacterCount;

                var fontStyleInformations = new List<FontStyleInformation>();
                foreach (var fontStyle in settings.GeneratedStyles)
                {
                    using (var styleFont = CreateFont(settings, (FontStyle)fontStyle))
                    {
                        var fontStyleData = CreateFontStyleData(styleFont, settings, fontFamilyData);

                        var bitmap = GenerateBitmapInternal(
                            styleFont,
                            settings,
                            fontFamilyData,
                            fontStyleData,
                            Color.White,
                            Color.Transparent,
                            false,
                            false);
                        fontData.Bitmaps.Add(fontStyle, bitmap);

                        // generate font style descriptor
                        var fontStyleInformation = CreateFontStyleInformation(settings, styleFont.Style, fontFamilyData, fontStyleData);
                        fontStyleInformations.Add(fontStyleInformation);
                    }
                }

                fontData.FontFamily.SetFontStyleInformations(fontStyleInformations);

                return fontData;
            }
            catch
            {
                foreach (var bitmap in fontData.Bitmaps.Values)
                {
                    bitmap?.Dispose();
                }

                return null;
            }
        }

        private static FontMeasurementData MeasureFont(Font font, IFontDataSettings settings, in FontFamilyData fontFamilyData)
        {
            // create character array from used characters
            var characters = new char[fontFamilyData.CharacterCount];
            for (int i = settings.FirstCharacter, j = 0; j < fontFamilyData.CharacterCount; i++, j++)
            {
                characters[j] = (char)i;
            }

            // calculate character widths
            var maxWidth = 0.0f;
            var maxHeight = 0.0f;
            var widths = new List<float>(fontFamilyData.CharacterCount);
            var flags = TextFormatFlags.Top | TextFormatFlags.Left | TextFormatFlags.NoPrefix;
            for (int i = settings.FirstCharacter, j = 0; j < fontFamilyData.CharacterCount; i++, j++)
            {
                // get character dimensions
                var span = new Span<char>(characters, j, 1);
                var size = TextRenderer.MeasureText(span, font, proposedCharacterSize, flags);
                var width = size.Width * settings.HorizontalScale;
                var height = size.Height;

                // update max size
                if (maxWidth < width)
                {
                    maxWidth = width;
                }

                if (maxHeight < height)
                {
                    maxHeight = height;
                }

                // add width
                widths.Add(width);
            }

            return new FontMeasurementData(widths, maxWidth, maxHeight);
        }
    }
}