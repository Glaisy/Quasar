//-----------------------------------------------------------------------
// <copyright file="IFrameBuffer.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Space.Core;

namespace Quasar.Graphics
{
    /// <summary>
    /// Represents a graphics frame buffer object.
    /// </summary>
    /// <seealso cref="IGraphicsResource" />
    /// <seealso cref="IIdentifierProvider{String}" />
    /// <seealso cref="IDisposable" />
    public interface IFrameBuffer : IGraphicsResource, IIdentifierProvider<string>, IDisposable
    {
        /// <summary>
        /// Gets the color target.
        /// </summary>
        ColorTarget ColorTarget { get; }

        /// <summary>
        /// Gets the color texture.
        /// </summary>
        ITexture ColorTexture { get; }

        /// <summary>
        /// Gets the depth target.
        /// </summary>
        DepthTarget DepthTarget { get; }

        /// <summary>
        /// Gets the depth texture.
        /// </summary>
        ITexture DepthTexture { get; }

        /// <summary>
        /// Gets a value indicating whether this frame buffer is the primary one or not.
        /// </summary>
        bool Primary { get; }

        /// <summary>
        /// Gets or sets the size in pixels.
        /// </summary>
        Size Size { get; set; }


        /// <summary>
        /// Activates the frame buffer.
        /// </summary>
        void Activate();

        /// <summary>
        /// Clears the frame buffer's color buffer with the specified color and resets the depth buffer.
        /// </summary>
        /// <param name="clearColor">The clear color.</param>
        /// <param name="clearDepthBuffer">if set to <c>true</c> the depth buffer is also cleared; otherwise not.</param>
        void Clear(Color clearColor, bool clearDepthBuffer);

        /// <summary>
        /// Clears the depth buffer.
        /// </summary>
        void ClearDepthBuffer();

        /// <summary>
        /// Deactivates the frame buffer.
        /// </summary>
        void Deactivate();
    }
}
