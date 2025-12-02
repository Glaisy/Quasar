//-----------------------------------------------------------------------
// <copyright file="LayerType.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Windows.Interop.Gdi32
{
    /// <summary>
    /// Rendering layer type enumeration.
    /// </summary>
    internal enum LayerType : byte
    {
        /// <summary>
        /// The main plane.
        /// </summary>
        Main = 0,

        /// <summary>
        /// The overlay plane.
        /// </summary>
        Overlay = 1,

        /// <summary>
        /// The underlay plane.
        /// </summary>
        Underlay = 0xFF
    }
}
