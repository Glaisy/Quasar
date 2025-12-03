//-----------------------------------------------------------------------
// <copyright file="PixelFormatFlags.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Windows.Interop.Gdi32
{
    /// <summary>
    /// Pixel format flags.
    /// </summary>
    [Flags]
    internal enum PixelFormatFlags : uint
    {
        /// <summary>
        /// The double buffering.
        /// </summary>
        DoubleBuffer = 1,

        /// <summary>
        /// The stereo display.
        /// </summary>
        Stereo = 2,

        /// <summary>
        /// The draw to window.
        /// </summary>
        DrawToWindow = 4,

        /// <summary>
        /// The draw to bitmap.
        /// </summary>
        DrawToBitmap = 8,

        /// <summary>
        /// The supports GDI.
        /// </summary>
        SupportsGDI = 16,

        /// <summary>
        /// The supports OpenGL.
        /// </summary>
        SupportsOpenGL = 32,

        /// <summary>
        /// The generic format.
        /// </summary>
        GenericFormat = 64,

        /// <summary>
        /// The need palette.
        /// </summary>
        NeedPalette = 128,

        /// <summary>
        /// The needs system palette.
        /// </summary>
        NeedsSystemPalette = 256,

        /// <summary>
        /// The swap exchange.
        /// </summary>
        SwapExchange = 512,

        /// <summary>
        /// The swap copy.
        /// </summary>
        SwapCopy = 1024,

        /// <summary>
        /// The swap layer buffers.
        /// </summary>
        SwapLayerBuffers = 2048,

        /// <summary>
        /// The generic accelerated.
        /// </summary>
        GenericAccelerated = 4096,

        /// <summary>
        /// The supports direct draw.
        /// </summary>
        SupportsDirectDraw = 8192,
    }
}
