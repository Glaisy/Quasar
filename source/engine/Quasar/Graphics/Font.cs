//-----------------------------------------------------------------------
// <copyright file="Font.cs" company="Space Development">
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
using System.Runtime.CompilerServices;

using Microsoft.Extensions.DependencyInjection;

using Quasar.Graphics.Internals;

using Space.Core;

namespace Quasar.Graphics
{
    /// <summary>
    /// Quasar Font implementation.
    /// </summary>
    /// <seealso cref="IIdentifierProvider{Int32}" />
    public sealed class Font : IIdentifierProvider<long>
    {
        private const int FamilyShift = 32;
        private const int StyleMask = 0xFF;
        private const int SizeShift = 16;


        private static ITextureRepository textureRepository;
        private static IFontFamilyRepository fontFamilyRepository;
        private readonly float scale;
        private readonly float characterSpacing;
        private readonly int firstIndex;
        private readonly int fallbackIndex;
        private readonly int lastIndex;
        private readonly float scaledLineHeight;
        private readonly IReadOnlyList<float> characterWidths;
        private readonly IReadOnlyList<Vector2> uvs;


        /// <summary>
        /// Initializes a new instance of the <see cref="Font"/> class.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <param name="size">The size.</param>
        /// <param name="style">The style.</param>
        public Font(Font font, int size, FontStyle style = FontStyle.Regular)
            : this(font?.Family, size, style)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Font"/> class.
        /// </summary>
        /// <param name="fontFamilyName">The font family name.</param>
        /// <param name="size">The size.</param>
        /// <param name="style">The style.</param>
        public Font(string fontFamilyName, int size, FontStyle style = FontStyle.Regular)
            : this(fontFamilyRepository.Get(fontFamilyName), size, style)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Font"/> class.
        /// </summary>
        /// <param name="fontFamily">The font family.</param>
        /// <param name="size">The size.</param>
        /// <param name="style">The style.</param>
        public Font(IFontFamily fontFamily, int size, FontStyle style = FontStyle.Regular)
        {
            ArgumentNullException.ThrowIfNull(fontFamily, nameof(fontFamily));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(size, nameof(size));

            // texture
            var textureName = String.Format(FontFamilyConstants.TextureNamePatternP2, fontFamily.Id, style);
            var texture = textureRepository.Get(textureName);
            if (texture == null)
            {
                throw new ArgumentOutOfRangeException($"Unable to create font '{fontFamily.Id}/{size}/{style}'. Missing or invalid internal texture.");
            }

            Texture = texture;

            // font style information
            var fontStyleInformation = fontFamily[style];

            // set basic properties
            Id = GenerateId(fontFamily, style, size);
            Family = fontFamily;
            Style = style;
            Size = size;

            // set font parameters
            characterSpacing = fontFamily.CharacterSpacing;
            firstIndex = fontFamily.FirstCharacter;
            lastIndex = firstIndex + fontFamily.CharacterCount - 1;
            fallbackIndex = fontFamily.FallbackCharacter - firstIndex;
            scale = size / fontFamily.BaseSize;
            scaledLineHeight = scale * fontStyleInformation.LineSpacing;
            characterWidths = fontStyleInformation.CharacterWidths;
            uvs = fontStyleInformation.UVs;
        }


        /// <summary>
        /// Gets the family.
        /// </summary>
        public IFontFamily Family { get; }

        /// <inheritdoc/>
        public long Id { get; }

        /// <summary>
        /// Gets the size.
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Gets the style.
        /// </summary>
        public FontStyle Style { get; }

        /// <summary>
        /// Gets the texture assigned to this font.
        /// </summary>
        public ITexture Texture { get; private set; }


        /// <summary>
        /// Finds the character index at position.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="position">The position.</param>
        /// <param name="characterPosition">The character position (left side).</param>
        public int FindCharacterIndexAtPosition(string value, int position, out int characterPosition)
        {
            if (String.IsNullOrEmpty(value) ||
                position <= 0)
            {
                characterPosition = 0;
                return 0;
            }

            var index = 0;
            var stringPosition = 0.0f;
            foreach (var c in value)
            {
                // skip control characters
                if (c < ' ')
                {
                    index++;
                    continue;
                }

                // get character width
                var characterIndex = GetCharacterIndex(c);
                var characterWidth = scale * characterWidths[characterIndex];

                // check if current character is at position
                if (stringPosition + characterWidth >= position)
                {
                    characterPosition = (int)stringPosition;
                    return index;
                }

                // next character
                stringPosition += characterWidth * characterSpacing;
                index++;
            }

            characterPosition = (int)stringPosition;
            return index;
        }

        /// <summary>
        /// Measures the string dimensions.
        /// </summary>
        /// <param name="value">The value.</param>
        public Vector2 MeasureString(string value)
        {
            // check empty string
            if (String.IsNullOrEmpty(value))
            {
                return Vector2.Zero;
            }

            return MeasureStringInternal(value, 0, value.Length);
        }

        /// <summary>
        /// Measures the string dimensions.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        public Vector2 MeasureString(string value, int startIndex, int count)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(startIndex, nameof(startIndex));
            if (String.IsNullOrEmpty(value))
            {
                return Vector2.Zero;
            }

            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex + count, value.Length, nameof(count));

            return MeasureStringInternal(value, startIndex, count);
        }


        /// <summary>
        /// Generates the mesh data for the specified range (start, length) of the string value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="start">The start.</param>
        /// <param name="length">The length.</param>
        /// <param name="vertices">The vertices.</param>
        /// <param name="indices">The index contrainer.</param>
        internal void GenerateMeshData(string value, int start, int length, Span<VertexPositionUV> vertices, Span<int> indices)
        {
            // add vertices, uvs. indices
            var bottom = 0.0f;
            var left = 0.0f;
            var top = scaledLineHeight;
            var vertexIndex = 0;
            var index = 0;
            for (int i = start, j = 0; j < length; i++, j++)
            {
                var c = value[i];

                // new line character?
                if (c == '\n')
                {
                    top = bottom;
                    bottom -= scaledLineHeight;
                    left = 0.0f;
                    continue;
                }

                // other control characters
                if (c < ' ')
                {
                    // skip ASCII control characters
                    continue;
                }

                // get character index
                var characterIndex = GetCharacterIndex(c);
                var uvIndex = 4 * characterIndex;

                // right x
                var characterWidth = MathF.Round(scale * characterWidths[characterIndex]);
                var right = left + characterWidth;

                // add new indices for the 2 triangles of the character quad
                indices[index++] = vertexIndex;
                indices[index++] = vertexIndex + 1;
                indices[index++] = vertexIndex + 2;

                indices[index++] = vertexIndex;
                indices[index++] = vertexIndex + 2;
                indices[index++] = vertexIndex + 3;

                // create new positions and uvs
                vertices[vertexIndex].Position = new Vector3(left, bottom, 0.0f);             // bottom left
                vertices[vertexIndex++].UV = uvs[uvIndex++];

                vertices[vertexIndex].Position = new Vector3(left, top, 0.0f);                // top left
                vertices[vertexIndex++].UV = uvs[uvIndex++];

                vertices[vertexIndex].Position = new Vector3(right, top, 0.0f);               // top right
                vertices[vertexIndex++].UV = uvs[uvIndex++];

                vertices[vertexIndex].Position = new Vector3(right, bottom, 0.0f);            // bottom right
                vertices[vertexIndex++].UV = uvs[uvIndex];

                // next character position (letter spacing)
                left += MathF.Round(characterWidth * characterSpacing);
            }
        }

        /// <summary>
        /// Initializes the static services.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        internal static void InitializeStaticServices(IServiceProvider serviceProvider)
        {
            textureRepository = serviceProvider.GetRequiredService<ITextureRepository>();
            fontFamilyRepository = serviceProvider.GetRequiredService<IFontFamilyRepository>();
        }


        private static long GenerateId(IFontFamily family, FontStyle style, int size)
        {
            long id = family.Id.GetHashCode();
            id <<= FamilyShift;
            id += (int)style & StyleMask;
            id <<= SizeShift;
            id += size & FontFamilyConstants.MaximumFontSize;
            return id;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetCharacterIndex(int characterCode)
        {
            if (characterCode < firstIndex || characterCode > lastIndex)
            {
                return fallbackIndex;
            }

            return characterCode - firstIndex;
        }

        /// <summary>
        /// Measures the string dimensions.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        private Vector2 MeasureStringInternal(string value, int startIndex, int count)
        {
            // calculate width
            var maxWidth = 0.0f;
            var width = 0.0f;
            for (int i = startIndex, j = 0; j < count; i++, j++)
            {
                var c = value[i];

                // new line character?
                if (c == '\n')
                {
                    maxWidth = MathF.Max(width, maxWidth);
                    width = 0.0f;
                    continue;
                }

                // other control characters
                if (c < ' ')
                {
                    // skip ASCII control characters
                    continue;
                }

                // add character width
                var characterIndex = GetCharacterIndex(c);
                var characterWidth = characterWidths[characterIndex];
                width += characterWidth * characterSpacing;
            }

            // last line
            if (width > 0.0f)
            {
                maxWidth = MathF.Max(width, maxWidth);
            }

            // return scaled size
            return new Vector2(maxWidth * scale, scaledLineHeight);
        }
    }
}
