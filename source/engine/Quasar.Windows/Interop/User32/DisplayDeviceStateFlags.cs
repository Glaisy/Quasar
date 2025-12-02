//-----------------------------------------------------------------------
// <copyright file="DisplayDeviceStateFlags.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Windows.Interop.User32
{
    /// <summary>
    /// Display device state flags.
    /// </summary>
    [Flags]
    internal enum DisplayDeviceStateFlags : int
    {
        /// <summary>
        /// The attached to desktop flag.
        /// </summary>
        AttachedToDesktop = 1,

        /// <summary>
        /// The multi driver flag.
        /// </summary>
        MultiDriver = 2,

        /// <summary>
        /// The primary device flag.
        /// </summary>
        PrimaryDevice = 4,

        /// <summary>
        /// The mirroring driver flag.
        /// </summary>
        MirroringDriver = 8,

        /// <summary>
        /// The VGA compatible flag.
        /// </summary>
        VGACompatible = 16,

        /// <summary>
        /// The removable flag.
        /// </summary>
        Removable = 32,

        /// <summary>
        /// The ACC driver flag.
        /// </summary>
        ACCDriver = 64,

        /// <summary>
        /// The unsafe modes on flag
        /// </summary>
        UnsafeModesOn = 0x00080000,

        /// <summary>
        /// The compatible flag.
        /// </summary>
        Compatible = 0x00200000,

        /// <summary>
        /// The RDP UDD flag.
        /// </summary>
        RDPUDD = 0x01000000,

        /// <summary>
        /// The disconnect flag.
        /// </summary>
        Disconnect = 0x02000000,

        /// <summary>
        /// The remote flag.
        /// </summary>
        Remote = 0x04000000,

        /// <summary>
        /// The modes pruned flag.
        /// </summary>
        ModesPruned = 0x08000000
    }
}
