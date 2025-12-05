//-----------------------------------------------------------------------
// <copyright file="GLTextureFactory.cs" company="Space Development">
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
using Quasar.OpenGL.Api;
using Quasar.OpenGL.Extensions;

using Space.Core.DependencyInjection;

namespace Quasar.OpenGL.Internals.Graphics.Factories
{
    /// <summary>
    /// OpenGL texture factory implementation.
    /// </summary>
    /// <seealso cref="ITextureFactory" />
    [Export(typeof(ITextureFactory), GraphicsPlatform.OpenGL)]
    [Singleton]
    internal sealed class GLTextureFactory : ITextureFactory
    {
        private const float MipmappingLOD = -1.0f;
        private readonly GraphicsResourceDescriptor textureResourceDescriptor;
        private readonly float maxAnisotropy;


        /// <summary>
        /// Initializes a new instance of the <see cref="GLTextureFactory"/> class.
        /// </summary>
        /// <param name="graphicsContext">The graphics context.</param>
        public unsafe GLTextureFactory(
            [FromKeyedServices(GraphicsPlatform.OpenGL)] IGraphicsDeviceContext graphicsContext)
        {
            textureResourceDescriptor = new GraphicsResourceDescriptor(graphicsContext.Device, GraphicsResourceUsage.Default);

            // get max level of anisotropic filtering
            fixed (float* ptrMaxAnisotropy = &maxAnisotropy)
            {
                GL.GetFloat((int)TextureParameterName.MaxOfMaxAnisotropy, ptrMaxAnisotropy);
            }
        }


        /// <inheritdoc/>
        public TextureBase Create(string key, IImageData imageData, in TextureDescriptor textureDescriptor)
        {
            var id = 0;
            try
            {
                // create and bind texture
                id = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, id);

                // load pixel data from the bitmap
                GL.TexImage2D(
                    TextureTarget.Texture2D,
                    0,
                    PixelInternalFormat.Rgba,
                    imageData.Size.Width,
                    imageData.Size.Height,
                    0,
                    PixelFormat.Bgra,
                    PixelType.UnsignedByte,
                    imageData.Data);

                // set texture wrapping
                GL.TexParameterInteger(
                    TextureTarget.Texture2D,
                    TextureParameterName.TextureWrapS,
                    (int)textureDescriptor.RepeatX.ToTextureWrapMode());
                GL.TexParameterInteger(
                    TextureTarget.Texture2D,
                    TextureParameterName.TextureWrapT,
                    (int)textureDescriptor.RepeatY.ToTextureWrapMode());

                // set texture filtering
                if (textureDescriptor.Filtered)
                {
                    GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
                    GL.TexParameterInteger(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);

                    var anisotropyLevel = MathF.Min(maxAnisotropy, textureDescriptor.AnisotropyLevel);
                    if (anisotropyLevel > 0)
                    {
                        // enable anisotropic filtering instead of mipmapping
                        GL.TexParameterFloat(TextureTarget.Texture2D, TextureParameterName.TextureLodBias, 0.0f);
                        GL.TexParameterFloat(TextureTarget.Texture2D, TextureParameterName.MaxAnisotropy, anisotropyLevel);
                    }
                    else
                    {
                        // standard mipmapping
                        GL.TexParameterFloat(TextureTarget.Texture2D, TextureParameterName.TextureLodBias, MipmappingLOD);
                    }
                }
                else
                {
                    GL.TexParameterInteger(
                        TextureTarget.Texture2D,
                        TextureParameterName.TextureMinFilter,
                        (int)TextureMinFilter.Linear);
                }

                GL.TexParameterInteger(
                    TextureTarget.Texture2D,
                    TextureParameterName.TextureMagFilter,
                    (int)TextureMagFilter.Linear);

                // create texture wrapper object.
                return new GLTexture(id, key, imageData.Size, textureDescriptor, textureResourceDescriptor);
            }
            catch
            {
                // rollback
                if (id > 0)
                {
                    GL.DeleteTexture(id);
                }

                throw;
            }
            finally
            {
                GL.BindTexture(TextureTarget.Texture2D, 0);
            }
        }
    }
}
