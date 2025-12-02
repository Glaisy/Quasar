//-----------------------------------------------------------------------
// <copyright file="User32.cs" company="Space Development">
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

namespace Quasar.Windows.Interop.User32
{
    /// <summary>
    /// Win32 User API wrapper methods in user32.dll.
    /// </summary>
    internal static class User32
    {
        private const string DllName = "user32.dll";


        /// <summary>
        /// Enumerates the display devices.
        /// </summary>
        /// <param name="deviceName">Name of the device or null.</param>
        /// <param name="deviceNumber">The device number.</param>
        /// <param name="device">The device structure.</param>
        /// <param name="flags">The flags.</param>
        /// <returns>
        /// True if the operation was successful otherwise false.
        /// </returns>
        [DllImport(DllName)]
        public static extern bool EnumDisplayDevices(string deviceName, int deviceNumber, ref DisplayDevice device, int flags = 0);

        /// <summary>
        /// Enumerates the display settings.
        /// </summary>
        /// <param name="deviceName">Name of the device.</param>
        /// <param name="displayModeNumber">The display mode number.</param>
        /// <param name="devMode">The device mode structure.</param>
        /// <returns>
        /// True if the operation was successful otherwise false.
        /// </returns>
        [DllImport(DllName)]
        public static extern bool EnumDisplaySettings(string deviceName, int displayModeNumber, ref DevMode devMode);

        /// <summary>
        /// Gets a window's device context.
        /// </summary>
        /// <param name="handle">The window handle.</param>
        /// <returns>The device context or zero if not available.</returns>
        [DllImport(DllName)]
        public static extern IntPtr GetDC(IntPtr handle);

        /// <summary>
        /// Releases a window's device context.
        /// </summary>
        /// <param name="handle">The window handle.</param>
        /// <param name="deviceContext">The device context.</param>
        /// <returns>
        /// True if the operation was successful otherwise false.
        /// </returns>
        [DllImport(DllName)]
        public static extern bool ReleaseDC(IntPtr handle, IntPtr deviceContext);
    }
}
