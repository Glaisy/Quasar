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
using System.Text;
using System.Windows.Forms;

namespace Quasar.Windows.Interop.User32
{
    /// <summary>
    /// Win32 User API wrapper methods in user32.dll.
    /// </summary>
    internal static class User32
    {
        private const string DllName = "user32.dll";


        /// <summary>
        /// Creates the icon indirect.
        /// </summary>
        /// <param name="iconInfo">The icon information.</param>
        /// <returns>
        /// The icon handle if the operation was successful otherwise zero.
        /// </returns>
        [DllImport(DllName)]
        public static extern IntPtr CreateIconIndirect([In] ref IconInfo iconInfo);

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
        /// Gets the icon information.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <param name="iconInfo">The icon information.</param>
        /// <returns>
        /// True if the operation was successful otherwise false.
        /// </returns>
        [DllImport(DllName)]
        public static extern bool GetIconInfo(IntPtr handle, out IconInfo iconInfo);

        /// <summary>
        /// Maps the virtual key to character or scan code.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="mappingType">Ther key mapping type.</param>
        /// <returns>The mapped value.</returns>
        [DllImport(DllName)]
        public static extern int MapVirtualKey(Keys key, KeyMappingType mappingType);

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

        /// <summary>
        /// Translates the specified virtual-key code and keyboard state to the corresponding Unicode character or characters.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="scanCode">The scan code.</param>
        /// <param name="keyboardState">The keyboard state buffer.</param>
        /// <param name="receivingBuffer">The receiving buffer.</param>
        /// <param name="bufferSize">The buffer size.</param>
        /// <param name="flags">The flags.</param>
        [DllImport(DllName, CharSet = CharSet.Unicode)]
        public static extern bool ToUnicode(
            Keys key,
            uint scanCode,
            byte[] keyboardState,
            StringBuilder receivingBuffer,
            int bufferSize,
            uint flags);
    }
}
