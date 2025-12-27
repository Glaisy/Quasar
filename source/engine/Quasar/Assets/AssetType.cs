//-----------------------------------------------------------------------
// <copyright file="AssetType.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Assets
{
    /// <summary>
    /// Asset type enumeration.
    /// </summary>
    public enum AssetType
    {
        /// <summary>
        /// The texture asset type.
        /// </summary>
        Texture,

        /// <summary>
        /// The cube map texture asset type.
        /// </summary>
        CubeMapTexture,

        /// <summary>
        /// The font family asset type.
        /// </summary>
        FontFamily,

        /// <summary>
        /// The icon asset type.
        /// </summary>
        Icon,

        /// <summary>
        /// The cursor asset type.
        /// </summary>
        Cursor,

        /// <summary>
        /// The 3D model asset type.
        /// </summary>
        Model,

        /// <summary>
        /// The UI theme asset type.
        /// </summary>
        Theme,

        /// <summary>
        /// The UI template asset type.
        /// </summary>
        UITemplate,

        /// <summary>
        /// The custom asset type.
        /// </summary>
        Custom
    }
}
