//-----------------------------------------------------------------------
// <copyright file="FontDataSettings.cs" company="Space Development">
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

using Quasar.Graphics;

using Space.Core;
using Space.Core.Settings;

namespace Quasar.FontGenerator.Models
{
    /// <summary>
    /// Font data settings object implementation.
    /// </summary>
    /// <seealso cref="SettingsBase{IFontDataSettings}" />
    /// <seealso cref="IFontDataSettings" />
    internal sealed class FontDataSettings : SettingsBase<IFontDataSettings>, IFontDataSettings
    {
        /// <summary>
        /// The mininum base size.
        /// </summary>
        public const int MininumBaseSize = 10;

        /// <summary>
        /// The offset range.
        /// </summary>
        public static readonly Range<int> OffsetRange = new Range<int>(-16, 16);

        /// <summary>
        /// The spacing range.
        /// </summary>
        public static readonly Range<float> SpacingRange = new Range<float>(0.5f, 2.0f);


        /// <summary>
        /// The defaults.
        /// </summary>
        public static readonly IFontDataSettings Defaults = new FontDataSettings
        {
            BaseSize = 32,
            CharacterSpacing = 1.0f,
            FallbackCharacter = ' ',
            FontFamilyName = "Arial",
            FirstCharacter = ' ',
            GeneratedStyles =
            [
                FontStyle.Regular
            ],
            HorizontalScale = 1.0f,
            Padding = 0,
            PageCount = 1
        };


        private int baseSize;
        /// <summary>
        /// Gets or sets the base size.
        /// </summary>
        [JsonRequired]
        public int BaseSize
        {
            get => baseSize;
            set => baseSize = Math.Max(MininumBaseSize, value);
        }

        private float characterSpacing;
        /// <summary>
        /// Gets or sets the character spacing.
        /// </summary>
        [JsonRequired]
        public float CharacterSpacing
        {
            get => characterSpacing;
            set => characterSpacing = SpacingRange.Clamp(value);
        }

        /// <summary>
        /// Gets or sets the fallback character.
        /// </summary>
        [JsonRequired]
        public char FallbackCharacter { get; set; }

        /// <summary>
        /// Gets or sets the font's family name.
        /// </summary>
        [JsonRequired]
        public string FontFamilyName { get; set; }

        /// <summary>
        /// Gets or sets the font familiy name override.
        /// </summary>
        public string FontFamilyNameOverride { get; set; }

        /// <summary>
        /// Gets or sets the first character.
        /// </summary>
        [JsonRequired]
        public char FirstCharacter { get; set; }

        private List<FontStyle> generatedStyles;
        /// <summary>
        /// Gets or sets the generated styles.
        /// </summary>
        [JsonRequired]
        public List<FontStyle> GeneratedStyles
        {
            get => generatedStyles;
            set => generatedStyles = value ?? new List<FontStyle>();
        }

        /// <inheritdoc/>
        IReadOnlyList<FontStyle> IFontDataSettings.GeneratedStyles => GeneratedStyles;

        private int horizontalOffset;
        /// <summary>
        /// Gets or sets the horizontal offset.
        /// </summary>
        public int HorizontalOffset
        {
            get => horizontalOffset;
            set => horizontalOffset = OffsetRange.Clamp(value);
        }

        private float horizontalScale;
        /// <summary>
        /// Gets or sets the horizontal scale.
        /// </summary>
        [JsonRequired]
        public float HorizontalScale
        {
            get => horizontalScale;
            set => horizontalScale = SpacingRange.Clamp(value);
        }

        private int padding;
        /// <summary>
        /// Gets or sets the padding.
        /// </summary>
        [JsonRequired]
        public int Padding
        {
            get => padding;
            set => padding = Math.Max(0, value);
        }

        private int pageCount;
        /// <summary>
        /// Gets or sets the number of character pages.
        /// </summary>
        [JsonRequired]
        public int PageCount
        {
            get => pageCount;
            set => pageCount = Math.Max(1, value);
        }

        private int verticalOffset;
        /// <summary>
        /// Gets or sets the vertical offset.
        /// </summary>
        public int VerticalOffset
        {
            get => verticalOffset;
            set => verticalOffset = OffsetRange.Clamp(value);
        }


        /// <inheritdoc/>
        public override void SetDefaults()
        {
            Copy(Defaults);
        }


        /// <inheritdoc/>
        protected override void CopyProperties(IFontDataSettings source)
        {
            BaseSize = source.BaseSize;
            CharacterSpacing = source.CharacterSpacing;
            FallbackCharacter = source.FallbackCharacter;
            FirstCharacter = source.FirstCharacter;
            FontFamilyName = source.FontFamilyName;
            FontFamilyNameOverride = source.FontFamilyNameOverride;
            HorizontalOffset = source.HorizontalOffset;
            HorizontalScale = source.HorizontalScale;
            Padding = source.Padding;
            PageCount = source.PageCount;
            VerticalOffset = source.VerticalOffset;

            var sourceGeneratedStyles = source.GeneratedStyles ?? Defaults.GeneratedStyles;
            if (GeneratedStyles == null)
            {
                GeneratedStyles = new List<FontStyle>(sourceGeneratedStyles);
            }
            else
            {
                GeneratedStyles.Clear();
                GeneratedStyles.AddRange(sourceGeneratedStyles);
            }
        }
    }
}
