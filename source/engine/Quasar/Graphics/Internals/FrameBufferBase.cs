//-----------------------------------------------------------------------
// <copyright file="FrameBufferBase.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Graphics.Internals
{
    /// <summary>
    /// Abstract base class for frame buffer implementations.
    /// </summary>
    /// <seealso cref="GraphicsResourceBase" />
    /// <seealso cref="IFrameBuffer" />
    internal abstract class FrameBufferBase : GraphicsResourceBase, IFrameBuffer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FrameBufferBase" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="colorTarget">The color target.</param>
        /// <param name="depthTarget">The depth target.</param>
        /// <param name="resourceDescriptor">The resource descriptor.</param>
        protected FrameBufferBase(
            string id,
            ColorTarget colorTarget,
            DepthTarget depthTarget,
            in GraphicsResourceDescriptor resourceDescriptor)
            : base(resourceDescriptor)
        {
            Id = id;
            ColorTarget = colorTarget;
            DepthTarget = depthTarget;
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            Id = null;
            ColorTarget = ColorTarget.None;
            DepthTarget = DepthTarget.None;
        }


        /// <inheritdoc/>
        public ColorTarget ColorTarget { get; private set; }

        /// <inheritdoc/>
        public abstract ITexture ColorTexture { get; }

        /// <inheritdoc/>
        public DepthTarget DepthTarget { get; private set; }

        /// <inheritdoc/>
        public abstract ITexture DepthTexture { get; }

        /// <inheritdoc/>
        public string Id { get; private set; }

        /// <inheritdoc/>
        public abstract bool Primary { get; }

        /// <inheritdoc/>
        public abstract Size Size { get; set; }


        /// <inheritdoc/>
        public abstract void Clear(Color clearColor, bool clearDepthBuffer);

        /// <inheritdoc/>
        public abstract void ClearDepthBuffer();

        /// <inheritdoc/>
        void IFrameBuffer.Activate()
        {
            Activate();
        }

        /// <inheritdoc/>
        void IFrameBuffer.Deactivate()
        {
            Deactivate();
        }
    }
}
