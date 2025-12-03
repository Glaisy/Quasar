//-----------------------------------------------------------------------
// <copyright file="OpenGLInteropFunctionProvider.cs" company="Space Development">
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

using Quasar.Core.Utilities;
using Quasar.Graphics;
using Quasar.Windows.Interop;

using Space.Core.DependencyInjection;

namespace Quasar.Windows.OpenGL
{
    /// <summary>
    /// OpenGL function provider for Windows platform.
    /// </summary>
    /// <seealso cref="IInteropFunctionProvider" />
    [Export(typeof(IInteropFunctionProvider), GraphicsPlatform.OpenGL)]
    [Singleton]
    internal sealed class OpenGLInteropFunctionProvider : IInteropFunctionProvider
    {
        private const string OpenGL32Dll = "opengl32.dll";
        private readonly IntPtr openGL32Module;


        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGLInteropFunctionProvider"/> class.
        /// </summary>
        public OpenGLInteropFunctionProvider()
        {
            openGL32Module = Kernel32.LoadLibrary(OpenGL32Dll);
        }


        /// <summary>
        /// Gets the function delegate by the specified name.
        /// </summary>
        /// <typeparam name="T">The delegate type.</typeparam>
        /// <returns>
        /// The delegate instance.
        /// </returns>
        public T GetFunction<T>()
            where T : Delegate
        {
            var functionName = typeof(T).Name;
            switch (functionName)
            {
                case "MakeCurrent":
                    functionName = "wglMakeCurrent";
                    break;
                case "CreateContext":
                    functionName = "wglCreateContext";
                    break;
                case "glSwapIntervalEXT":
                    functionName = "wglSwapIntervalEXT";
                    break;
            }

            // try to get standard OpenGL function
            var functionAddress = Kernel32.GetProcAddress(openGL32Module, functionName);
            if (functionAddress == IntPtr.Zero)
            {
                // try to load OpenGL extension function
                functionAddress = WglGetProcAddress(functionName);
                if (functionAddress == IntPtr.Zero)
                {
                    throw new Exception($"Unable to find OpenGL function: '{functionName}'.");
                }
            }

            return Marshal.GetDelegateForFunctionPointer<T>(functionAddress);
        }


        /// <summary>
        /// Gets a proc address.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <returns>The address of the function.</returns>
        [DllImport(OpenGL32Dll, EntryPoint = "wglGetProcAddress")]
        private static extern IntPtr WglGetProcAddress(string name);
    }
}
