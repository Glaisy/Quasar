//-----------------------------------------------------------------------
// <copyright file="DisplayDevice.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Runtime.InteropServices;

namespace Quasar.Windows.Interop.User32
{
    /// <summary>
    /// Win32 data structure which contains information about a display device.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    internal unsafe struct DisplayDevice
    {
        /// <summary>
        /// The size of this structure in bytes.
        /// </summary>
        public int Size;

        /// <summary>
        /// The device name.
        /// </summary>
        public fixed byte DeviceName[32];

        /// <summary>
        /// The device string.
        /// </summary>
        public fixed byte DeviceString[128];

        /// <summary>
        /// The state flags.
        /// </summary>
        public DisplayDeviceStateFlags StateFlags;

        /// <summary>
        /// The device identifier.
        /// </summary>
        public fixed byte DeviceID[128];

        /// <summary>
        /// The device key.
        /// </summary>
        public fixed byte DeviceKey[128];


        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Init()
        {
            Size = (ushort)Marshal.SizeOf<DisplayDevice>();
        }
    }
}
