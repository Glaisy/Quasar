//-----------------------------------------------------------------------
// <copyright file="IconInfo.cs" company="Space Development">
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

using Quasar.Graphics;

namespace Quasar.Windows.Interop
{
    /// <summary>
    /// Icon information structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct IconInfo
    {
        /// <summary>
        /// The is icon flag. False = Cursor, True = Icon.
        /// </summary>
        public bool IsIcon;

        /// <summary>
        /// The hotspot.
        /// </summary>
        public Point Hotspot;

        /// <summary>
        /// The mask bitmap handle.
        /// </summary>
        public IntPtr Mask;

        /// <summary>
        /// The color bitmap handle.
        /// </summary>
        public IntPtr Color;
    }
}
