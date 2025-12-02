//-----------------------------------------------------------------------
// <copyright file="Gdi32.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;

namespace Quasar.Windows.Interop.Gdi32
{
    /// <summary>
    /// Win32 GDI API wrapper methods in gdi32.dll.
    /// </summary>
    internal static class Gdi32
    {
        private const string DllName = "gdi32.dll";


        /// <summary>
        /// Chooses the pixel format.
        /// </summary>
        /// <param name="deviceContext">The device context.</param>
        /// <param name="pixelFormatDescriptor">The pixel format descriptor.</param>
        /// <returns>The pixel format.</returns>
        [DllImport(DllName)]
        public static extern int ChoosePixelFormat(IntPtr deviceContext, ref PixelFormatDescriptor pixelFormatDescriptor);

        /// <summary>
        /// Sets the pixel format.
        /// </summary>
        /// <param name="deviceContext">The device context.</param>
        /// <param name="pixelFormat">The pixel format.</param>
        /// <param name="pixelFormatDescriptor">The pixel format descriptor.</param>
        /// <returns>
        /// True if the operation was successful otherwise false.
        /// </returns>
        [DllImport(DllName)]
        public static extern int SetPixelFormat(IntPtr deviceContext, int pixelFormat, ref PixelFormatDescriptor pixelFormatDescriptor);

        /// <summary>
        /// Swaps the buffers.
        /// </summary>
        /// <param name="deviceContext">The device context.</param>
        /// <returns>
        /// True if the operation was successful otherwise false.
        /// </returns>
        [DllImport(DllName)]
        public static extern bool SwapBuffers(IntPtr deviceContext);
    }
}
