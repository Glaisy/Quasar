//-----------------------------------------------------------------------
// <copyright file="SettingsEnumerationType.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Windows.Interop.User32
{
    /// <summary>
    /// Settings enumeration types.
    /// </summary>
    internal enum SettingsEnumerationType : int
    {
        /// <summary>
        /// The current settings.
        /// </summary>
        Current = -1,

        /// <summary>
        /// The registry settings.
        /// </summary>
        Registry = -2
    }
}
