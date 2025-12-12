//-----------------------------------------------------------------------
// <copyright file="KeyModifiers.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Inputs
{
    /// <summary>
    /// Key code enumeration.
    /// </summary>
    [Flags]
    public enum KeyModifiers : short
    {
        /// <summary>
        /// The no modifier keys.
        /// </summary>
        None = 0,

        /// <summary>
        /// The SHIFT key.
        /// </summary>
        Shift = 1,

        /// <summary>
        /// The CTRL key.
        /// </summary>
        Control = 2,

        /// <summary>
        /// The ALT key.
        /// </summary>
        Alt = 4
    }
}
