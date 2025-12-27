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

using System;
using System.IO;
using System.Threading;

using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.Graphics.Internals.Factories;
using Quasar.OpenGL.Api;

using Space.Core;
using Space.Core.DependencyInjection;
using Space.Core.Mathematics;
using Space.Core.Threading;

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
        private readonly IDispatcher dispatcher;
        private readonly IImageDataLoader imageDataLoader;
        private GraphicsResourceDescriptor defaultResourceDescriptor;


        /// <summary>
        /// Initializes a new instance of the <see cref="GLCubeMapTextureFactory"/> class.
        /// </summary>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="imageDataLoader">The image data loader.</param>
        public GLCubeMapTextureFactory(
            IDispatcher dispatcher,
            IImageDataLoader imageDataLoader)
        {
            this.dispatcher = dispatcher;
            this.imageDataLoader = imageDataLoader;
        }


        /// <inheritdoc/>
        public CubeMapTextureBase Create(string id, Stream stream, string tag)
        {
            Assertion.ThrowIfNullOrEmpty(id, nameof(id));
            Assertion.ThrowIfNull(stream, nameof(stream));

            IImageData imageData = null;
            try
            {
                imageData = imageDataLoader.Load(stream, true);

                ArgumentOutOfRangeException.ThrowIfNotEqual(
                    imageData.Size != Size.Empty,
                    true,
                    nameof(stream));
                ArgumentOutOfRangeException.ThrowIfNotEqual(
                    MathematicsHelper.IsPowerOf2(imageData.Size.Width),
                    true,
                    nameof(stream));
                ArgumentOutOfRangeException.ThrowIfNotEqual(
                    imageData.Size.Width * 6,
                    imageData.Size.Height,
                    nameof(stream));

                var createData = new TextureCreateData<CubeMapTextureBase>(imageData, id, tag);
                lock (imageData)
                {
                    var taskId = dispatcher.Dispatch(CreateTexture, createData);
                    if (taskId > 0)
                    {
                        Monitor.Wait(imageData);
                    }
                }

                return createData.Texture;
            }
            finally
            {
                imageData?.Dispose();
            }
        }


        /// <summary>
        /// Executes the cube map texture factory initialization.
        /// </summary>
        /// <param name="graphicsContext">The graphics context.</param>
        public void Initialize(IGraphicsContext graphicsContext)
        {
            defaultResourceDescriptor = new GraphicsResourceDescriptor(graphicsContext.Device, GraphicsResourceUsage.Default);
        }


        private void CreateTexture(TextureCreateData<CubeMapTextureBase> createData)
        {
            GLCubeMapTexture texture = null;
            createData.Texture = null;
            try
            {
                texture = new GLCubeMapTexture(createData.Id, Size.Empty, createData.Tag, defaultResourceDescriptor);
                texture.Activate();

                // set texture parameters
                GL.TexParameterInteger(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMagFilter.Linear);
                GL.TexParameterInteger(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                GL.TexParameterInteger(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameterInteger(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

                texture.SetFaces(createData.ImageData);

                createData.Texture = texture;
            }
            catch
            {
                if (texture != null)
                {
                    texture.Deactivate();
                    texture.Dispose();
                    texture = null;
                }

                throw;
            }
            finally
            {
                texture?.Deactivate();
            }
        }
    }
}
