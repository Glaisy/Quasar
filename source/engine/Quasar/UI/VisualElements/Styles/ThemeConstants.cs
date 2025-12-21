//-----------------------------------------------------------------------
// <copyright file="ThemeConstants.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.UI.VisualElements.Styles
{
    /// <summary>
    /// Theme related constants.
    /// </summary>
    public static class ThemeConstants
    {
        /// <summary>
        /// The base theme variable.
        /// </summary>
        public const string BaseThemeVariable = "--Base-Theme";

        /// <summary>
        /// The default theme identifier.
        /// </summary>
        public const string DefaultId = nameof(Quasar);

        /// <summary>
        /// The (display) name variable.
        /// </summary>
        public const string NameVariable = "--Name";

        /// <summary>
        /// The textures directory path relative to the theme's base directory.
        /// </summary>
        public const string TexturesDirectoryPath = "Textures";

        /// <summary>
        /// The theme texture identifier format string. {0}: theme id, {1}: texture name.
        /// </summary>
        public const string TextureNameFormatString = "Themes/{0}/{1}";
    }
}
