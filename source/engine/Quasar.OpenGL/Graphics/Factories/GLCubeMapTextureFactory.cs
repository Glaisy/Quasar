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

using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.Graphics.Internals.Factories;
using Quasar.OpenGL.Api;

using Space.Core.DependencyInjection;

namespace Quasar.OpenGL.Graphics.Factories
{
    /// <summary>
    /// OpenGL cubemap texture factory implementation.
    /// </summary>
    /// <seealso cref="ICubeMapTextureFactory" />
    [Export]
    [Singleton]
    internal sealed class GLCubeMapTextureFactory : ICubeMapTextureFactory
    {
        private GraphicsResourceDescriptor defaultResourceDescriptor;


        /// <inheritdoc/>
        public CubeMapTextureBase Create(string key, IImageData imageData, string tag)
        {
            // create cubemap texture
            var texture = new GLCubeMapTexture(key, Size.Empty, tag, defaultResourceDescriptor);
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


        /// <summary>
        /// Executes the cube map texture factory initialization.
        /// </summary>
        /// <param name="graphicsContext">The graphics context.</param>
        public void Initialize(IGraphicsContext graphicsContext)
        {
            defaultResourceDescriptor = new GraphicsResourceDescriptor(graphicsContext.Device, GraphicsResourceUsage.Default);
        }
    }
}
