//-----------------------------------------------------------------------
// <copyright file="INativeWindow.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Quasar.Graphics;

using Space.Core;

namespace Quasar.UI
{
    /// <summary>
    /// Represents an operating system provided window object.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public interface INativeWindow : IDisposable
    {
        /// <summary>
        /// Gets the handle.
        /// </summary>
        IntPtr Handle { get; }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        INativeWindow Parent { get; }

        /// <summary>
        /// Gets the operating system platform.
        /// </summary>
        OperatingSystemPlatform Platform { get; }

        /// <summary>
        /// Gets the size.
        /// </summary>
        Size Size { get; }

        /// <summary>
        /// Gets a value indicating whether this window is visible.
        /// </summary>
        bool Visible { get; }


        /// <summary>
        /// Gets the device context for the specified display mode.
        /// </summary>
        /// <param name="displayMode">The display mode.</param>
        /// <returns>The native handle for the device context.</returns>
        internal IntPtr GetDeviceContext(IDisplayMode displayMode);

        /// <summary>
        /// Swaps the rendering buffers.
        /// </summary>
        internal void SwapBuffers();
    }
}
