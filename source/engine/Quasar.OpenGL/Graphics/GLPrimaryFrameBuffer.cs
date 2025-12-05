//-----------------------------------------------------------------------
// <copyright file="GLPrimaryFrameBuffer.cs" company="Space Development">
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
using Quasar.OpenGL.Api;
using Quasar.UI;

namespace Quasar.OpenGL.Graphics
{
    /// <summary>
    /// Primary frame buffer implementation.
    /// </summary>
    /// <seealso cref="FrameBufferBase" />
    internal sealed class GLPrimaryFrameBuffer : FrameBufferBase
    {
        private readonly INativeWindow nativeWindow;


        /// <summary>
        /// Initializes a new instance of the <see cref="GLPrimaryFrameBuffer" /> class.
        /// </summary>
        /// <param name="nativeWindow">The native window.</param>
        /// <param name="descriptor">The descriptor.</param>
        public GLPrimaryFrameBuffer(INativeWindow nativeWindow, in GraphicsResourceDescriptor descriptor)
            : base(null, ColorTarget.None, DepthTarget.None, descriptor)
        {
            this.nativeWindow = nativeWindow;
        }


        /// <inheritdoc/>
        public override ITexture ColorTexture => null;

        /// <inheritdoc/>
        public override ITexture DepthTexture => null;

        /// <inheritdoc/>
        public override int Handle => 0;

        /// <inheritdoc/>
        public override bool Primary => true;


        /// <inheritdoc/>
        public override Size Size
        {
            get => nativeWindow.Size;
            set => throw new NotSupportedException("Primary framebuffers are not resizable from code.");
        }


        /// <inheritdoc/>
        public override void Clear(Color clearColor, bool clearDepthBuffer)
        {
            GL.ClearColor(clearColor.R, clearColor.G, clearColor.B, clearColor.A);

            var clearMask = BufferClearMask.ColorBuffer;
            if (clearDepthBuffer)
            {
                clearMask |= BufferClearMask.DepthBuffer;
            }

            GL.Clear(clearMask);
        }

        /// <inheritdoc/>
        public override void ClearDepthBuffer()
        {
            GL.Clear(BufferClearMask.DepthBuffer);
        }


        /// <inheritdoc/>
        internal override void Activate()
        {
            GL.BindFrameBuffer(FrameBufferTarget.Framebuffer, 0);
            GL.Viewport(0, 0, Size.Width, Size.Height);
        }

        /// <inheritdoc/>
        internal override void Deactivate()
        {
        }
    }
}
