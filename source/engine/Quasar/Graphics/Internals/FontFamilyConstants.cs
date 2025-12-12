//-----------------------------------------------------------------------
// <copyright file="FontFamilyConstants.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Graphics.Internals
{
    /// <summary>
    /// Font family related constants.
    /// </summary>
    internal static class FontFamilyConstants
    {
        /// <summary>
        /// The default font family name.
        /// </summary>
        public const string DefaultName = "Conthrax";

        /// <summary>
        /// The default font size.
        /// </summary>
        public const int DefaultFontSize = 12;

        /// <summary>
        /// The maximum font size.
        /// </summary>
        public const int MaximumFontSize = 255;

        /// <summary>
        /// The texture name pattern (2 parameters: family name, font style).
        /// </summary>
        public const string TextureNamePatternP2 = "FontFamily/{0}/{1}";

        /// <summary>
        /// The resource file zip entry name.
        /// </summary>
        public const string ZipEntryName = "FontFamily.json";
    }
}
