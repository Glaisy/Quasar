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

using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.Graphics.Internals.Factories;
using Quasar.UI;

using Space.Core.DependencyInjection;

namespace Quasar.OpenGL.Graphics.Factories
{
    /// <summary>
    /// OpenGL frame buffer factory implementation.
    /// </summary>
    [Export]
    [Singleton]
    internal sealed class GLFrameBufferFactory : IFrameBufferFactory
    {
        private GraphicsResourceDescriptor defaultResourceDescriptor;


        /// <inheritdoc/>
        public FrameBufferBase Create(in Size size, ColorTarget colorTarget, DepthTarget depthTarget, string key = null)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(size.Width, nameof(size.Width));
            ArgumentOutOfRangeException.ThrowIfNegative(size.Height, nameof(size.Height));

            return new GLFrameBuffer(key, size, colorTarget, depthTarget, defaultResourceDescriptor);
        }

        /// <inheritdoc/>
        public FrameBufferBase CreatePrimary(INativeWindow nativeWindow)
        {
            ArgumentNullException.ThrowIfNull(nativeWindow, nameof(nativeWindow));

            return new GLPrimaryFrameBuffer(nativeWindow, defaultResourceDescriptor);
        }


        /// <summary>
        /// Executes the frame buffer initialization.
        /// </summary>
        /// <param name="graphicsContext">The graphics context.</param>
        public void Initialize(IGraphicsContext graphicsContext)
        {
            defaultResourceDescriptor = new GraphicsResourceDescriptor(graphicsContext.Device, GraphicsResourceUsage.Default);
        }
    }
}
