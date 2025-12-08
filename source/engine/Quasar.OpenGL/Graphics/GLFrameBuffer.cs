//-----------------------------------------------------------------------
// <copyright file="GLFrameBuffer.cs" company="Space Development">
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

namespace Quasar.OpenGL.Graphics
{
    /// <summary>
    /// OpenGL framebuffer implementation.
    /// </summary>
    /// <seealso cref="FrameBufferBase" />
    internal class GLFrameBuffer : FrameBufferBase
    {
        private static readonly TextureDescriptor textureDescriptor = new TextureDescriptor(
            0,
            TextureRepeatMode.Clamped,
            TextureRepeatMode.Clamped);


        private int depthRenderBufferHandle;


        /// <summary>
        /// Initializes a new instance of the <see cref="GLFrameBuffer" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="size">The size in pixels.</param>
        /// <param name="colorTarget">The color target.</param>
        /// <param name="depthTarget">The depth target.</param>
        /// <param name="resourceDescriptor">The resource descriptor.</param>
        public GLFrameBuffer(
            string id,
            in Size size,
            ColorTarget colorTarget,
            DepthTarget depthTarget,
            in GraphicsResourceDescriptor resourceDescriptor)
            : base(id, colorTarget, depthTarget, resourceDescriptor)
        {
            this.size = size;

            handle = GL.GenFrameBuffer();
            GL.BindFrameBuffer(FrameBufferTarget.Framebuffer, handle);

            // color target
            if (colorTarget == ColorTarget.Texture)
            {
                GL.DrawBuffer(DrawBufferMode.ColorAttachment0);
                colorTexture = CreateColorBufferTextureAttachment(size, FrameBufferAttachment.ColorAttachment0);
            }
            else
            {
                GL.DrawBuffer(DrawBufferMode.None);
                GL.ReadBuffer(ReadBufferMode.None);
            }

            // depth target
            switch (depthTarget)
            {
                case DepthTarget.Texture:
                    depthTexture = CreateDepthBufferTextureAttachment(size);
                    break;
                case DepthTarget.RenderBuffer:
                    depthRenderBufferHandle = CreateDepthRenderBuffer(size);
                    break;
                default:
                    break;
            }

            GL.BindFrameBuffer(FrameBufferTarget.Framebuffer, 0);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (handle == 0)
            {
                return;
            }

            colorTexture?.Dispose();
            colorTexture = null;
            depthTexture?.Dispose();
            depthTexture = null;
            size = Size.Empty;

            if (depthRenderBufferHandle > 0)
            {
                GL.DeleteRenderBuffer(depthRenderBufferHandle);
                depthRenderBufferHandle = 0;
            }

            GL.DeleteFrameBuffer(handle);
        }


        private GLTexture colorTexture;
        /// <inheritdoc/>
        public override TextureBase ColorTexture => colorTexture;

        private GLTexture depthTexture;
        /// <inheritdoc/>
        public override TextureBase DepthTexture => depthTexture;

        private int handle;
        /// <inheritdoc/>
        public override int Handle => handle;

        /// <inheritdoc/>
        public override bool Primary => false;

        private Size size;
        /// <inheritdoc/>
        public override Size Size
        {
            get => size;
            set
            {
                if (size != value)
                {
                    return;
                }

                size = value;
                ResizeInternals(value);
            }
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
            GL.BindFrameBuffer(FrameBufferTarget.Framebuffer, handle);
            GL.Viewport(0, 0, size.Width, size.Height);
        }


        /// <inheritdoc/>
        internal override void Deactivate()
        {
            GL.BindFrameBuffer(FrameBufferTarget.Framebuffer, 0);
        }


        private GLTexture CreateTexture(int textureId, string id, in Size size)
        {
            var resourceDescriptor = new GraphicsResourceDescriptor(GraphicsDevice, GraphicsResourceUsage.Default);
            return new GLTexture(textureId, id, size, textureDescriptor, null, resourceDescriptor);
        }

        private GLTexture CreateColorBufferTextureAttachment(in Size size, FrameBufferAttachment attachmentId)
        {
            var id = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, id);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, size.Width, size.Height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameterInteger(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameterInteger(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.TexParameterInteger(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameterInteger(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.FrameBufferTexture2D(FrameBufferTarget.Framebuffer, attachmentId, TextureTarget.Texture2D, id, 0);

            return CreateTexture(id, "ColorBuffer", size);
        }

        private static int CreateDepthRenderBuffer(in Size size)
        {
            var id = GL.GenRenderBuffer();
            GL.BindRenderBuffer(RenderBufferTarget.Renderbuffer, id);
            GL.RenderBufferStorage(RenderBufferTarget.Renderbuffer, RenderBufferStorage.DepthComponent, size.Width, size.Height);
            GL.FrameBufferRenderBuffer(FrameBufferTarget.Framebuffer, FrameBufferAttachment.DepthAttachment, RenderBufferTarget.Renderbuffer, id);
            return id;
        }

        private GLTexture CreateDepthBufferTextureAttachment(Size size)
        {
            var id = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, id);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.DepthComponent, size.Width, size.Height, 0, PixelFormat.DepthComponent, PixelType.Float, IntPtr.Zero);
            GL.TexParameterInteger(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameterInteger(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.TexParameterInteger(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToBorder);
            GL.TexParameterInteger(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToBorder);
            GL.TexParameterVector4(TextureTarget.Texture2D, TextureParameterName.TextureBorderColor, new Vector4(1));
            GL.FrameBufferTexture2D(FrameBufferTarget.Framebuffer, FrameBufferAttachment.DepthAttachment, TextureTarget.Texture2D, id, 0);
            return CreateTexture(id, "DepthBuffer", size);
        }

        private void ResizeInternals(in Size size)
        {
            // resize color texture
            if (colorTexture != null)
            {
                GL.BindTexture(TextureTarget.Texture2D, colorTexture.Handle);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, size.Width, size.Height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, IntPtr.Zero);
                colorTexture.SetSize(size);
            }

            // resize depth texture
            if (depthTexture != null)
            {
                GL.BindTexture(TextureTarget.Texture2D, depthTexture.Handle);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.DepthComponent, size.Width, size.Height, 0, PixelFormat.DepthComponent, PixelType.Float, IntPtr.Zero);
                depthTexture.SetSize(size);
            }

            // resize depth render buffer
            if (depthRenderBufferHandle > 0)
            {
                GL.BindRenderBuffer(RenderBufferTarget.Renderbuffer, depthRenderBufferHandle);
                GL.RenderBufferStorage(RenderBufferTarget.Renderbuffer, RenderBufferStorage.DepthComponent, size.Width, size.Height);
            }
        }
    }
}
