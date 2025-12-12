//-----------------------------------------------------------------------
// <copyright file="FontFamily.cs" company="Space Development">
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
using System.Linq;
using System.Text.Json.Serialization;

namespace Quasar.Graphics.Internals
{
    /// <summary>
    /// Quasar font family implementation.
    /// </summary>
    public sealed class FontFamily : IFontFamily
    {
        private IFontStyleInformation fallbackFontStyleInformation;

        [JsonPropertyName("FontStyles")]
        [JsonRequired]
        private List<FontStyleInformation> fontStyleInformations;


        /// <summary>
        /// Gets the <see cref="IFontStyleInformation" /> with the specified font style.
        /// </summary>
        /// <param name="fontStyle">The font style.</param>
        /// <returns>
        /// The font style information.
        /// </returns>
        [JsonIgnore]
        public IFontStyleInformation this[FontStyle fontStyle]
        {
            get
            {
                var fontStyleInformation = fontStyleInformations.FirstOrDefault(x => x.Id == fontStyle);
                return fontStyleInformation ?? fallbackFontStyleInformation;
            }
        }


        private float baseSize;
        /// <summary>
        /// Gets the base size of the font.
        /// </summary>
        [JsonRequired]
        public float BaseSize
        {
            get => baseSize;
            internal set
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value, nameof(BaseSize));
                baseSize = value;
            }
        }

        private int characterCount;
        /// <summary>
        /// Gets the character count.
        /// </summary>
        [JsonRequired]
        public int CharacterCount
        {
            get => characterCount;
            internal set
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value, nameof(CharacterCount));
                characterCount = value;
            }
        }

        private float characterSpacing;
        /// <summary>
        /// Gets the character spacing. (relative to character width).
        /// </summary>
        [JsonRequired]
        public float CharacterSpacing
        {
            get => characterSpacing;
            internal set
            {
                ArgumentOutOfRangeException.ThrowIfNegative(value, nameof(CharacterSpacing));
                characterSpacing = value;
            }
        }

        private char fallbackCharacter = ' ';
        /// <summary>
        /// Gets or sets the fabllback character.
        /// </summary>
        public char FallbackCharacter
        {
            get => fallbackCharacter;
            set
            {
                ArgumentOutOfRangeException.ThrowIfLessThan(value, ' ', nameof(FallbackCharacter));
                fallbackCharacter = value;
            }
        }

        private char firstCharacter = ' ';
        /// <summary>
        /// Gets or sets the first character.
        /// </summary>
        public char FirstCharacter
        {
            get => firstCharacter;
            set
            {
                ArgumentOutOfRangeException.ThrowIfLessThan(value, ' ', nameof(FirstCharacter));
                firstCharacter = value;
            }
        }

        private string id;
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [JsonRequired]
        public string Id
        {
            get => id;
            set
            {
                ArgumentException.ThrowIfNullOrEmpty(value, nameof(Id));
                id = value;
            }
        }


        /// <summary>
        /// Initializes this instance by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        internal void Initialize(string id)
        {
            if (fontStyleInformations == null ||
                fontStyleInformations.Count == 0)
            {
                throw new InvalidOperationException("Font styles are not yet initialized.");
            }

            foreach (var fontStyleInformation in fontStyleInformations)
            {
                ValidateFontStyleInformation(id, fontStyleInformation);
            }

            SetFallbackFontStyleInformation();
        }

        /// <summary>
        /// Adds the font style informations.
        /// </summary>
        /// <param name="fontStyleInformations">The font style informations.</param>
        internal void SetFontStyleInformations(List<FontStyleInformation> fontStyleInformations)
        {
            this.fontStyleInformations = fontStyleInformations;

            SetFallbackFontStyleInformation();
        }


        private void SetFallbackFontStyleInformation()
        {
            fallbackFontStyleInformation = fontStyleInformations?.FirstOrDefault(x => x.Id == FontStyle.Regular);
            if (fallbackFontStyleInformation == null)
            {
                throw new FormatException($"Regular font style is not found for {Id} font.");
            }
        }

        private void ValidateFontStyleInformation(string fontFamilyId, FontStyleInformation fontStyleInformation)
        {
            if (fontStyleInformation.CharacterWidths.Count != characterCount)
            {
                throw new FormatException($"Character count mismatch at {fontStyleInformation.Id} font style for {fontFamilyId} font family.");
            }

            if (fontStyleInformation.CharacterWidths.Count * 4 != fontStyleInformation.UVs.Count)
            {
                throw new FormatException($"CharacterWidths - UV count mismatch at {fontStyleInformation.Id} font style for {fontFamilyId} font family.");
            }
        }
    }
}
