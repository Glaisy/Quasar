//-----------------------------------------------------------------------
// <copyright file="IFrameBufferFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.UI;

namespace Quasar.Graphics.Internals.Factories
{
    /// <summary>
    /// Frame buffer repository interface definition.
    /// </summary>
    internal interface IFrameBufferFactory
    {
        /// <summary>
        /// Creates a frame buffer with the specified parameters.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="colorTarget">The color target.</param>
        /// <param name="depthTarget">The depth target.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The created frame buffer instance.
        /// </returns>
        FrameBufferBase Create(in Size size, ColorTarget colorTarget, DepthTarget depthTarget, string id = null);

        /// <summary>
        /// Creates a frame buffer wrapper for the primary frame buffer.
        /// </summary>
        /// <param name="nativeWindow">The native window.</param>
        /// <returns>
        /// The created frame buffer instance.
        /// </returns>
        FrameBufferBase CreatePrimary(INativeWindow nativeWindow);
    }
}
