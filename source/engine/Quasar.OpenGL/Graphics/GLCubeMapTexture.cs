//-----------------------------------------------------------------------
// <copyright file="GLCubeMapTexture.cs" company="Space Development">
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
using Quasar.OpenGL.Extensions;

using Space.Core;
using Space.Core.Mathematics;

namespace Quasar.OpenGL.Internals.Graphics
{
    /// <summary>
    /// OpenGL cube map texture implementation.
    /// </summary>
    /// <seealso cref="CubeMapTextureBase" />
    internal class GLCubeMapTexture : CubeMapTextureBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GLCubeMapTexture" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="size">The size.</param>
        /// <param name="graphicsResourceDescriptor">The graphics resource descriptor.</param>
        public GLCubeMapTexture(
            string id,
            in Size size,
            GraphicsResourceDescriptor graphicsResourceDescriptor)
            : base(id, size, graphicsResourceDescriptor)
        {
            handle = GL.GenTexture();
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
            GL.BindTexture(TextureTarget.TextureCubeMap, handle);
        }

        /// <inheritdoc/>
        internal override void Deactivate()
        {
            GL.BindTexture(TextureTarget.TextureCubeMap, 0);
        }

        /// <summary>
        /// Sets the cube map texture faces.
        /// </summary>
        /// <param name="imageData">The image data.</param>
        internal void SetFaces(IImageData imageData)
        {
            Assertion.ThrowIfNotEqual(imageData.Size != Size.Empty, true, nameof(imageData.Size));
            Assertion.ThrowIfNotEqual(MathematicsHelper.IsPowerOf2(imageData.Size.Width), true, nameof(imageData.Size.Width));
            Assertion.ThrowIfNotEqual(imageData.Size.Width * 6, imageData.Size.Height, nameof(imageData.Size.Height));

            try
            {
                Activate();

                var size = imageData.Size.Width;
                var faceStride = size * imageData.Stride;
                var dataPointer = imageData.Data;
                for (var i = 0; i < 6; i++, dataPointer += faceStride)
                {
                    var cubeMapFace = (CubeMapFace)i;
                    GL.TexImage2D(
                        cubeMapFace.ToCubeMapTextureTarget(),
                        0,
                        PixelInternalFormat.Rgba,
                        size,
                        size,
                        0,
                        PixelFormat.Bgra,
                        PixelType.UnsignedByte,
                        dataPointer);
                }
            }
            finally
            {
                Deactivate();
            }
        }
    }
}
