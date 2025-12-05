//-----------------------------------------------------------------------
// <copyright file="GLFrameBufferFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Microsoft.Extensions.DependencyInjection;

using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.Graphics.Internals.Factories;
using Quasar.UI;

using Space.Core.DependencyInjection;

namespace Quasar.OpenGL.Graphics.Factories
{
    /// <summary>
    /// OpenGL framebuffer factory implementation.
    /// </summary>
    [Export(typeof(IFrameBufferFactory), nameof(GraphicsPlatform.OpenGL))]
    [Singleton]
    internal sealed class GLFrameBufferFactory : IFrameBufferFactory
    {
        private readonly GraphicsResourceDescriptor frameBufferResourceDescriptor;


        /// <summary>
        /// Initializes a new instance of the <see cref="GLFrameBufferFactory" /> class.
        /// </summary>
        /// <param name="graphicsContext">The graphics context.</param>
        public GLFrameBufferFactory(
            [FromKeyedServices(GraphicsPlatform.OpenGL)] IGraphicsDeviceContext graphicsContext)
        {
            frameBufferResourceDescriptor = new GraphicsResourceDescriptor(
                graphicsContext.Device,
                GraphicsResourceUsage.Default);
        }


        /// <inheritdoc/>
        public FrameBufferBase Create(in Size size, ColorTarget colorTarget, DepthTarget depthTarget, string key = null)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(size.Width, nameof(size.Width));
            ArgumentOutOfRangeException.ThrowIfNegative(size.Height, nameof(size.Height));

            return new GLFrameBuffer(key, size, colorTarget, depthTarget, frameBufferResourceDescriptor);
        }

        /// <inheritdoc/>
        public FrameBufferBase CreatePrimary(INativeWindow nativeWindow)
        {
            ArgumentNullException.ThrowIfNull(nativeWindow, nameof(nativeWindow));

            return new GLPrimaryFrameBuffer(nativeWindow, frameBufferResourceDescriptor);
        }
    }
}
