//-----------------------------------------------------------------------
// <copyright file="IFontDataSettings.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;

using Quasar.Graphics;

using Space.Core.Settings;

namespace Quasar.FontGenerator.Models
{
    /// <summary>
    /// Represents the settings values for the generated font data/preview image.
    /// </summary>
    internal interface IFontDataSettings : ISettings
    {
        /// <summary>
        /// Gets the font base size.
        /// </summary>
        int BaseSize { get; }

        /// <summary>
        /// Gets the character spacing.
        /// </summary>
        float CharacterSpacing { get; }

        /// <summary>
        /// Gets the fallback character.
        /// </summary>
        char FallbackCharacter { get; }

        /// <summary>
        /// Gets the first character.
        /// </summary>
        char FirstCharacter { get; }

        /// <summary>
        /// Gets the name of the font family.
        /// </summary>
        string FontFamilyName { get; }

        /// <summary>
        /// Gets the font familiy name override.
        /// </summary>
        string FontFamilyNameOverride { get; }

        /// <summary>
        /// Gets the generated styles.
        /// </summary>
        IReadOnlyList<FontStyle> GeneratedStyles { get; }

        /// <summary>
        /// Gets the horizontal scale.
        /// </summary>
        float HorizontalScale { get; }

        /// <summary>
        /// Gets the horizontal offset.
        /// </summary>
        int HorizontalOffset { get; }

        /// <summary>
        /// Gets the padding.
        /// </summary>
        int Padding { get; }

        /// <summary>
        /// Gets the number of character pages.
        /// </summary>
        int PageCount { get; }

        /// <summary>
        /// Gets the vertical offset.
        /// </summary>
        int VerticalOffset { get; }
    }
}
