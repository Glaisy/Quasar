//-----------------------------------------------------------------------
// <copyright file="GLCubeMapTextureFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;

using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.Graphics.Internals.Factories;
using Quasar.OpenGL.Api;

using Space.Core.DependencyInjection;

namespace Quasar.OpenGL.Internals.Graphics.Factories
{
    /// <summary>
    /// OpenGL cubemap texture factory implementation.
    /// </summary>
    /// <seealso cref="ICubeMapTextureFactory" />
    [Export(typeof(ICubeMapTextureFactory), nameof(GraphicsPlatform.OpenGL))]
    [Singleton]
    internal sealed class GLCubeMapTextureFactory : ICubeMapTextureFactory
    {
        private readonly GraphicsResourceDescriptor textureResourceDescriptor;


        /// <summary>
        /// Initializes a new instance of the <see cref="GLCubeMapTextureFactory"/> class.
        /// </summary>
        /// <param name="graphicsContext">The graphics context.</param>
        public GLCubeMapTextureFactory(
            [FromKeyedServices(GraphicsPlatform.OpenGL)] IGraphicsDeviceContext graphicsContext)
        {
            textureResourceDescriptor = new GraphicsResourceDescriptor(graphicsContext.Device, GraphicsResourceUsage.Default);
        }


        /// <inheritdoc/>
        public CubeMapTextureBase Create(string key, IImageData imageData)
        {
            // create cubemap texture
            var texture = new GLCubeMapTexture(key, Size.Empty, textureResourceDescriptor);
            texture.Activate();

            // set texture parameters
            GL.TexParameterInteger(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMagFilter.Linear);
            GL.TexParameterInteger(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameterInteger(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameterInteger(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            texture.SetFaces(imageData);

            // unbind texture for safety reason
            texture.Deactivate();

            return texture;
        }
    }
}
