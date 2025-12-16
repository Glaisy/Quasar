//-----------------------------------------------------------------------
// <copyright file="LayerMask.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Rendering
{
    /// <summary>
    /// Render layer mask enumeration.
    /// </summary>
    [Flags]
    public enum LayerMask
    {
        /// <summary>
        /// The no layer mask.
        /// </summary>
        None = 0,

        /// <summary>
        /// The opaque 0 layer mask.
        /// </summary>
        Opaque0 = 1,

        /// <summary>
        /// The opaque 1 layer mask.
        /// </summary>
        Opaque1 = 2,

        /// <summary>
        /// The opaque 2 layer mask.
        /// </summary>
        Opaque2 = 4,

        /// <summary>
        /// The opaque 3 layer mask.
        /// </summary>
        Opaque3 = 8,

        /// <summary>
        /// The opaque layer mask (alias for Opaque0).
        /// </summary>
        Opaque = Opaque0,

        /// <summary>
        /// The transparent 0 layer mask.
        /// </summary>
        Transparent0 = 16,

        /// <summary>
        /// The transparent 0 layer mask.
        /// </summary>
        Transparent1 = 32,

        /// <summary>
        /// The transparent 0 layer mask.
        /// </summary>
        Transparent2 = 64,

        /// <summary>
        /// The transparent 0 layer mask.
        /// </summary>
        Transparent3 = 128,

        /// <summary>
        /// The transparent layer mask (alias for Transparent0).
        /// </summary>
        Transparent = Transparent0,

        /// <summary>
        /// All opaque layer mask.
        /// </summary>
        AllOpaque = Opaque0 | Opaque1 | Opaque2 | Opaque3,

        /// <summary>
        /// All transparent layer mask.
        /// </summary>
        AllTransparent = Transparent0 | Transparent1 | Transparent2 | Transparent3,

        /// <summary>
        /// All layer mask.
        /// </summary>
        All = Opaque0 | Opaque1 | Opaque2 | Opaque3 | Transparent0 | Transparent1 | Transparent2 | Transparent3
    }
}
