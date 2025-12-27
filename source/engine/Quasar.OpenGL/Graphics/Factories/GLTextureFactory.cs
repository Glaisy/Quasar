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
using System.IO;
using System.Threading;

using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.Graphics.Internals.Factories;
using Quasar.OpenGL.Api;
using Quasar.OpenGL.Extensions;

using Space.Core;
using Space.Core.DependencyInjection;
using Space.Core.Diagnostics;
using Space.Core.Threading;

namespace Quasar.OpenGL.Graphics.Factories
{
    /// <summary>
    /// OpenGL texture factory implementation.
    /// </summary>
    /// <seealso cref="ITextureFactory" />
    [Export]
    [Singleton]
    internal sealed class GLTextureFactory : ITextureFactory
    {
        private const float MipmappingLOD = -1.0f;


        private readonly IDispatcher dispatcher;
        private readonly IImageDataLoader imageDataLoader;
        private readonly ILogger logger;
        private readonly float maxAnisotropy;
        private GraphicsResourceDescriptor defaultResourceDescriptor;


        /// <summary>
        /// Initializes a new instance of the <see cref="GLTextureFactory" /> class.
        /// </summary>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="imageDataLoader">The image data loader.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public unsafe GLTextureFactory(
            IDispatcher dispatcher,
            IImageDataLoader imageDataLoader,
            ILoggerFactory loggerFactory)
        {
            this.dispatcher = dispatcher;
            this.imageDataLoader = imageDataLoader;

            logger = loggerFactory.Create<GLTextureFactory>();

            // get max level of anisotropic filtering
            fixed (float* ptrMaxAnisotropy = &maxAnisotropy)
            {
                GL.GetFloat((int)TextureParameterName.MaxOfMaxAnisotropy, ptrMaxAnisotropy);
            }
        }


        /// <inheritdoc/>
        public TextureBase Create(string id, Stream stream, string tag, in TextureDescriptor textureDescriptor)
        {
            Assertion.ThrowIfNullOrEmpty(id, nameof(id));
            Assertion.ThrowIfNull(stream, nameof(stream));

            IImageData imageData = null;
            try
            {
                imageData = imageDataLoader.Load(stream, true);
                var createData = new TextureCreateData<TextureBase>(imageData, id, tag, textureDescriptor);
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
        /// Executes the texture factory initialization.
        /// </summary>
        /// <param name="graphicsContext">The graphics context.</param>
        public void Initialize(IGraphicsContext graphicsContext)
        {
            defaultResourceDescriptor = new GraphicsResourceDescriptor(graphicsContext.Device, GraphicsResourceUsage.Default);
        }


        private void CreateTexture(TextureCreateData<TextureBase> createData)
        {
            var id = 0;
            var textureSize = createData.ImageData.Size;

            try
            {
                createData.Texture = null;

                // create and bind texture
                id = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, id);

                // load pixel data from the bitmap
                GL.TexImage2D(
                    TextureTarget.Texture2D,
                    0,
                    PixelInternalFormat.Rgba,
                    textureSize.Width,
                    textureSize.Height,
                    0,
                    PixelFormat.Bgra,
                    PixelType.UnsignedByte,
                    createData.ImageData.Data);

                // set texture wrapping
                GL.TexParameterInteger(
                    TextureTarget.Texture2D,
                    TextureParameterName.TextureWrapS,
                    (int)createData.TextureDescriptor.RepeatX.ToTextureWrapMode());
                GL.TexParameterInteger(
                    TextureTarget.Texture2D,
                    TextureParameterName.TextureWrapT,
                    (int)createData.TextureDescriptor.RepeatY.ToTextureWrapMode());

                // set texture filtering
                if (createData.TextureDescriptor.Filtered)
                {
                    GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
                    GL.TexParameterInteger(
                        TextureTarget.Texture2D,
                        TextureParameterName.TextureMinFilter,
                        (int)TextureMinFilter.LinearMipmapLinear);

                    var anisotropyLevel = MathF.Min(maxAnisotropy, createData.TextureDescriptor.AnisotropyLevel);
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
                createData.Texture =
                    new GLTexture(id, createData.Id, textureSize, createData.TextureDescriptor, createData.Tag, defaultResourceDescriptor);

                // send completed signal
                lock (createData.ImageData)
                {
                    Monitor.PulseAll(createData.ImageData);
                }
            }
            catch (Exception exception)
            {
                logger.Error(exception, $"Unable to load texture #{createData.Id}");

                if (id > 0)
                {
                    GL.DeleteTexture(id);
                }
            }
            finally
            {
                GL.BindTexture(TextureTarget.Texture2D, 0);
            }
        }
    }
}
