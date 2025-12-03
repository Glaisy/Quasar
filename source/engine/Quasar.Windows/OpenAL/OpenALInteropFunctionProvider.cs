//-----------------------------------------------------------------------
// <copyright file="OpenALInteropFunctionProvider.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.IO;
using System.Runtime.InteropServices;

using Quasar.Audio;
using Quasar.Core.Utilities;
using Quasar.Windows.Interop;

using Space.Core.DependencyInjection;

namespace Quasar.Windows.OpenAL
{
    /// <summary>
    /// OpenAL function provider for Windows platform.
    /// </summary>
    /// <seealso cref="IInteropFunctionProvider" />
    [Export(typeof(IInteropFunctionProvider), AudioPlatform.OpenAL)]
    [Singleton]
    internal sealed class OpenALInteropFunctionProvider : IInteropFunctionProvider
    {
        private const string OpenAL32Dll = "OpenAL32.dll";
        private readonly IntPtr openAL32Module;


        /// <summary>
        /// Initializes a new instance of the <see cref="OpenALInteropFunctionProvider"/> class.
        /// </summary>
        public OpenALInteropFunctionProvider()
        {
            var dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, OpenAL32Dll);
            openAL32Module = Kernel32.LoadLibrary(OpenAL32Dll);
        }


        /// <inheritdoc/>
        public T GetFunction<T>()
            where T : Delegate
        {
            var functionName = typeof(T).Name;

            // try to get standard OpenAL function
            var functionAddress = Kernel32.GetProcAddress(openAL32Module, functionName);
            if (functionAddress == IntPtr.Zero)
            {
                throw new Exception($"Unable to find OpenAL function: '{functionName}'.");
            }

            return Marshal.GetDelegateForFunctionPointer<T>(functionAddress);
        }
    }
}
