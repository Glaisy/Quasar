//-----------------------------------------------------------------------
// <copyright file="KeyMappingType.cs" company="Space Development">
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
    /// Key mapping type enumeration.
    /// </summary>
    internal enum KeyMappingType
    {
        /// <summary>
        /// The virtual key to scan code mapping type.
        /// </summary>
        VirtualKeyToScanCode = 0,

        /// <summary>
        /// The scan code to virtual key mapping type.
        /// </summary>
        ScanCodeToVirtualKey = 1,

        /// <summary>
        /// The virtual key to character mapping type.
        /// </summary>
        VirtualKeyToCharacter = 2,

        /// <summary>
        /// The scan code to virtual key extended mapping type.
        /// </summary>
        ScanCodeToVirtualKeyEx = 3,

        /// <summary>
        /// The virtual key to scan code extended mapping type.
        /// </summary>
        VirtualKeyToScanCodeEx = 4
    }
}
