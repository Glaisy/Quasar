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

using Space.Core.Settings;

namespace Quasar.FontGenerator.Models
{
    /// <summary>
    /// Represents the settings values for the generated font data/preview image.
    /// </summary>
    internal interface IFontDataSettings : ISettings
    {
        /////// <summary>
        /////// Gets or sets the character spacing.
        /////// </summary>
        ////public float CharacterSpacing { get; set; }

        /////// <summary>
        /////// Gets or sets the fallback character.
        /////// </summary>
        ////public char FallbackCharacter { get; set; }

        /////// <summary>
        /////// Gets or sets the first character.
        /////// </summary>
        ////public char FirstCharacter { get; set; }

        /////// <summary>
        /////// Gets or sets the font name override.
        /////// </summary>
        ////public string FontNameOverride { get; set; }

        /////// <summary>
        /////// Gets or sets the horizontal scale.
        /////// </summary>
        ////public float HorizontalScale { get; set; }

        /////// <summary>
        /////// Gets or sets the generated styles.
        /////// </summary>
        ////public List<FontStyle> GeneratedStyles { get; set; }

        /////// <summary>
        /////// Gets or sets the X offset.
        /////// </summary>
        ////public int OffsetX { get; set; }

        /////// <summary>
        /////// Gets or sets the Y offset.
        /////// </summary>
        ////public int OffsetY { get; set; }

        /////// <summary>
        /////// Gets or sets the padding.
        /////// </summary>
        ////public int Padding { get; set; }

        /////// <summary>
        /////// Gets or sets the number of character pages.
        /////// </summary>
        ////public int PageCount { get; set; }
    }
}
