//-----------------------------------------------------------------------
// <copyright file="PixelType.cs" company="Space Development">
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
    /// Pixel type enumeration.
    /// </summary>
    internal enum PixelType : byte
    {
        /// <summary>
        /// The RBGA type.
        /// </summary>
        RGBA = 0,

        /// <summary>
        /// The color index type.
        /// </summary>
        ColorIndex = 1
    }
}
