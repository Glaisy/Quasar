//-----------------------------------------------------------------------
// <copyright file="Kernel32.cs" company="Space Development">
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

namespace Quasar.Windows.Interop
{
    /// <summary>
    /// WinAPI Kernel API wrapper methods in kernel32.dll.
    /// </summary>
    internal static class Kernel32
    {
        private const string DllName = "kernel32.dll";


        /// <summary>
        /// Gets the address of a procedure.
        /// </summary>
        /// <param name="module">The module handle.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <returns>The address of the specified processor or zero if not found.</returns>
        [DllImport(DllName)]
        public static extern IntPtr GetProcAddress(IntPtr module, string procedureName);

        /// <summary>
        /// Gets the module handle by the specified name.
        /// </summary>
        /// <param name="moduleName">The module nme.</param>
        /// <returns>The module handle or zero if not found.</returns>
        [DllImport(DllName)]
        public static extern IntPtr GetModuleHandle(string moduleName);

        /// <summary>
        /// Loads the library.
        /// </summary>
        /// <param name="dllName">The library name.</param>
        /// <returns>The loaded dll module handle.</returns>
        [DllImport(DllName)]
        public static extern IntPtr LoadLibrary(string dllName);
    }
}
