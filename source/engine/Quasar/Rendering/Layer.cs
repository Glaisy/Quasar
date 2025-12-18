//-----------------------------------------------------------------------
// <copyright file="Layer.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Rendering
{
    /// <summary>
    /// Render layer enumeration.
    /// </summary>
    public enum Layer
    {
        /// <summary>
        /// The opaque 0 layer.
        /// </summary>
        Opaque0 = 0,

        /// <summary>
        /// The opaque 1 layer.
        /// </summary>
        Opaque1 = 1,

        /// <summary>
        /// The opaque 2 layer.
        /// </summary>
        Opaque2 = 2,

        /// <summary>
        /// The opaque 3 layer.
        /// </summary>
        Opaque3 = 3,

        /// <summary>
        /// The opaque layer (alias for Opaque0)
        /// </summary>
        Opaque = Opaque0,

        /// <summary>
        /// The last opaque layer.
        /// </summary>
        LastOpaque = Opaque3,

        /// <summary>
        /// The transparent 0 layer.
        /// </summary>
        Transparent0 = 4,

        /// <summary>
        /// The transparent 1 layer.
        /// </summary>
        Transparent1 = 5,

        /// <summary>
        /// The transparent 2 layer.
        /// </summary>
        Transparent2 = 6,

        /// <summary>
        /// The transparent 3 layerW.
        /// </summary>
        Transparent3 = 7,

        /// <summary>
        /// The transparent layer (alias for Transparent0)
        /// </summary>
        Transparent = Transparent0,

        /// <summary>
        /// The last transparent layer.
        /// </summary>
        LastTransparent = Transparent3,

        /// <summary>
        /// The default layer.
        /// </summary>
        Default = Opaque0,

        /// <summary>
        /// The first layer.
        /// </summary>
        First = Opaque0,

        /// <summary>
        /// The last layer.
        /// </summary>
        Last = Transparent3
    }
}
