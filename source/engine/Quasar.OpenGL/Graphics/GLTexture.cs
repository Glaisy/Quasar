//-----------------------------------------------------------------------
// <copyright file="GLTexture.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.OpenGL.Api;

namespace Quasar.OpenGL.Graphics
{
    /// <summary>
    /// OpenGL 2D texture implementation.
    /// </summary>
    /// <seealso cref="TextureBase" />
    internal sealed class GLTexture : TextureBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GLTexture" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="size">The size.</param>
        /// <param name="textureDescriptor">The texture descriptor.</param>
        /// <param name="tag">The tag value.</param>
        /// <param name="resourceDescriptor">The resource descriptor.</param>
        public GLTexture(
            string id,
            in Size size,
            in TextureDescriptor textureDescriptor,
            string tag,
            in GraphicsResourceDescriptor resourceDescriptor)
            : this(GL.GenTexture(), id, size, textureDescriptor, tag, resourceDescriptor)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GLTexture" /> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="size">The size.</param>
        /// <param name="textureDescriptor">The texture descriptor.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="resourceDescriptor">The resource descriptor.</param>
        public GLTexture(
            int handle,
            string id,
            in Size size,
            in TextureDescriptor textureDescriptor,
            string tag,
            in GraphicsResourceDescriptor resourceDescriptor)
            : base(id, size, textureDescriptor, tag, resourceDescriptor)
        {
            this.handle = handle;
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (handle != 0)
            {
                GL.DeleteTexture(handle);
                handle = 0;
            }

            base.Dispose(disposing);
        }


        private int handle;
        /// <inheritdoc/>
        public override int Handle => handle;


        /// <inheritdoc/>
        internal override void Activate()
        {
            GL.BindTexture(TextureTarget.Texture2D, handle);
        }

        /// <inheritdoc/>
        internal override void Deactivate()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        /// <summary>
        /// Sets the size.
        /// </summary>
        /// <param name="size">The size.</param>
        internal void SetSize(in Size size)
        {
            Size = size;
        }
    }
}
