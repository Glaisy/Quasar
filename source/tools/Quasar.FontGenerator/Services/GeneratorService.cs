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
using System.Windows.Forms;

using Quasar.FontGenerator.Extensions;
using Quasar.FontGenerator.Models;

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
        public void ExportFont(FontDataSettings settings, string filePath)
        {
            throw new NotImplementedException();
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
            FontDataSettings settings,
            FontStyle fontStyle,
            in Color foregroundColor,
            in Color backgroundColor)
        {
            using (var font = CreateFont(settings, fontStyle))
            {
                var fontFamilyData = CreateFontFamilyData(font, settings);
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


        private static Font CreateFont(FontDataSettings settings, FontStyle fontStyle)
        {
            return new Font(settings.FontFamilyName, settings.BaseSize, fontStyle);
        }

        private static FontFamilyData CreateFontFamilyData(Font font, FontDataSettings settings)
        {
            var baseSize = (int)MathF.Ceiling(font.Size);
            var characterCount = CharactersPerPage * settings.PageCount - settings.FirstCharacter;
            return new FontFamilyData(baseSize, characterCount);
        }

        private static FontStyleData CreateFontStyleData(Font font, FontDataSettings settings, in FontFamilyData fontFamilyData)
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
            FontDataSettings settings,
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

        private static FontMeasurementData MeasureFont(Font font, FontDataSettings settings, in FontFamilyData fontFamilyData)
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