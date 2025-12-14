//-----------------------------------------------------------------------
// <copyright file="FontStyleInformation.cs" company="Space Development">
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
using System.Text.Json.Serialization;

namespace Quasar.Graphics.Internals
{
    /// <summary>
    /// Font style information object implementation.
    /// </summary>
    /// <seealso cref="IFontStyleInformation" />
    internal sealed class FontStyleInformation : IFontStyleInformation
    {
        private float ascent;
        /// <summary>
        /// Gets or sets the ascent of the font.
        /// </summary>
        public float Ascent
        {
            get => ascent;
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value, nameof(Ascent));
                ascent = value;
            }
        }

        private List<float> characterWidths;
        /// <summary>
        /// Gets or sets the character widths.
        /// </summary>
        [JsonRequired]
        public List<float> CharacterWidths
        {
            get => characterWidths;
            set
            {
                ArgumentNullException.ThrowIfNull(value, nameof(CharacterWidths));
                characterWidths = value;
            }
        }

        /// <inheritdoc/>
        [JsonIgnore]
        IReadOnlyList<float> IFontStyleInformation.CharacterWidths => characterWidths;

        private float descent;
        /// <summary>
        /// Gets or sets the descent of the font.
        /// </summary>
        public float Descent
        {
            get => descent;
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value, nameof(Descent));
                descent = value;
            }
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [JsonPropertyOrder(-1)]
        public FontStyle Id { get; set; }

        private float lineSpacing;
        /// <summary>
        /// Gets or sets the line spacing of the font.
        /// </summary>
        public float LineSpacing
        {
            get => lineSpacing;
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value, nameof(LineSpacing));
                lineSpacing = value;
            }
        }

        private List<Vector2> uvs;
        /// <summary>
        /// Gets or sets the character UV texture coordinates. (4 per characters).
        /// </summary>
        [JsonRequired]
        public List<Vector2> UVs
        {
            get => uvs;
            set
            {
                ArgumentNullException.ThrowIfNull(value, nameof(UVs));
                uvs = value;
            }
        }

        /// <summary>
        /// Gets the list of texture coordinates.
        /// </summary>
        [JsonIgnore]
        IReadOnlyList<Vector2> IFontStyleInformation.UVs => uvs;
    }
}
