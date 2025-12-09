//-----------------------------------------------------------------------
// <copyright file="IFontSettings.cs" company="Space Development">
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
    /// Represents the settings values for the generated font.
    /// </summary>
    /// <seealso cref="ISettings" />
    internal interface IFontSettings : ISettings
    {
        /// <summary>
        /// Gets a value indicating whether the font is bold.
        /// </summary>
        bool Bold { get; }

        /// <summary>
        /// Gets the font family name.
        /// </summary>
        string FamilyName { get; }

        /// <summary>
        /// Gets a value indicating whether the font is italic.
        /// </summary>
        bool Italic { get; }

        /// <summary>
        /// Gets the font size in pixels.
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Gets a value indicating whether the font is strikeout.
        /// </summary>
        bool Strikeout { get; }

        /// <summary>
        /// Gets a value indicating whether the font is underline.
        /// </summary>
        bool Underline { get; }
    }
}