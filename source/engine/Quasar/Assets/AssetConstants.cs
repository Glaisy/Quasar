//-----------------------------------------------------------------------
// <copyright file="AssetConstants.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Assets
{
    /// <summary>
    /// Asset related constant definitions.
    /// </summary>
    public static class AssetConstants
    {
        /// <summary>
        /// The meta data asset name.
        /// </summary>
        public const string MetaDataAssetName = "pack.json";

        /// <summary>
        /// The latest version.
        /// </summary>
        public static readonly Version LatestVersion = new Version(1, 0);

        /// <summary>
        /// The asset directory constants.
        /// </summary>
        public static class Directories
        {
            /// <summary>
            /// The cube map textures directory.
            /// </summary>
            public const string CubeMapTextures = nameof(CubeMapTextures);

            /// <summary>
            /// The cursors directory.
            /// </summary>
            public const string Cursors = nameof(Cursors);

            /// <summary>
            /// The font families directory.
            /// </summary>
            public const string FontFamilies = nameof(FontFamilies);

            /// <summary>
            /// The icons directory.
            /// </summary>
            public const string Icons = nameof(Icons);

            /// <summary>
            /// The textures directory.
            /// </summary>
            public const string Textures = nameof(Textures);
        }
    }
}
