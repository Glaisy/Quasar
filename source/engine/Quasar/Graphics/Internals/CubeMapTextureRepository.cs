//-----------------------------------------------------------------------
// <copyright file="CubeMapTextureRepository.cs" company="Space Development">
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

using Quasar.Collections;
using Quasar.Core.IO;
using Quasar.Graphics.Internals.Factories;

using Space.Core.DependencyInjection;
using Space.Core.Mathematics;

namespace Quasar.Graphics.Internals
{
    /// <summary>
    /// Cube map texture repository implementation.
    /// </summary>
    /// <seealso cref="TaggedRepositoryBase{String, ICubeMapTexture, CubeMapTextureBase}" />
    /// <seealso cref="ICubeMapTextureRepository" />
    [Export(typeof(ICubeMapTextureRepository))]
    [Singleton]
    internal sealed class CubeMapTextureRepository :
        TaggedRepositoryBase<string, ICubeMapTexture, CubeMapTextureBase>, ICubeMapTextureRepository
    {
        private const string BuiltInCubemapIdPrefix = "Cubemaps/";
        private const string BuiltInCubemapTextureSearchPath = "./" + BuiltInCubemapIdPrefix;


        private readonly IResourceProvider resourceProvider;
        private readonly ITextureImageDataLoader textureImageDataLoader;
        private readonly ICubeMapTextureFactory cubeMapTextureFactory;


        /// <summary>
        /// Initializes a new instance of the <see cref="CubeMapTextureRepository" /> class.
        /// </summary>
        /// <param name="context">The Quasar context.</param>
        /// <param name="cubeMapTextureFactory">The cube map texture factory.</param>
        /// <param name="textureImageDataLoader">The texture image data loader.</param>
        public CubeMapTextureRepository(
            IQuasarContext context,
            ICubeMapTextureFactory cubeMapTextureFactory,
            ITextureImageDataLoader textureImageDataLoader)
        {
            this.cubeMapTextureFactory = cubeMapTextureFactory;
            this.textureImageDataLoader = textureImageDataLoader;

            resourceProvider = context.ResourceProvider;
        }


        private CubeMapTextureBase fallbackTexture;
        /// <inheritdoc/>
        public ICubeMapTexture FallbackTexture => fallbackTexture;


        /// <inheritdoc/>
        public ICubeMapTexture Create(
            string id,
            IImageData imageData,
            string tag = null)
        {
            ValidateIdentifier(id);
            ArgumentNullException.ThrowIfNull(imageData, nameof(imageData));

            try
            {
                RepositoryLock.EnterWriteLock();

                EnsureIdentifierIsAvailable(id);

                return CreateCubeMapTexture(id, imageData, tag);
            }
            finally
            {
                RepositoryLock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        public ICubeMapTexture Create(
            string id,
            string filePath,
            string tag = null)
        {
            ValidateIdentifier(id);
            ArgumentException.ThrowIfNullOrEmpty(filePath, nameof(filePath));

            Stream stream = null;
            IImageData imageData = null;
            try
            {
                RepositoryLock.EnterWriteLock();

                EnsureIdentifierIsAvailable(id);

                stream = File.Open(filePath, FileMode.Open);
                imageData = textureImageDataLoader.Load(stream);

                return CreateCubeMapTexture(id, imageData, tag);
            }
            finally
            {
                RepositoryLock.ExitWriteLock();

                stream?.Dispose();
                imageData?.Dispose();
            }
        }

        /// <inheritdoc/>
        public ICubeMapTexture Create(
            string id,
            Stream stream,
            string tag = null)
        {
            ValidateIdentifier(id);
            ArgumentNullException.ThrowIfNull(stream, nameof(stream));

            IImageData imageData = null;
            try
            {
                RepositoryLock.EnterWriteLock();

                EnsureIdentifierIsAvailable(id);

                imageData = textureImageDataLoader.Load(stream);

                return CreateCubeMapTexture(id, imageData, tag);
            }
            finally
            {
                RepositoryLock.ExitWriteLock();

                imageData?.Dispose();
            }
        }

        /// <inheritdoc/>
        public ICubeMapTexture Create(
            string id,
            IResourceProvider resourceProvider,
            string resourcePath,
            string tag = null)
        {
            ValidateIdentifier(id);
            ArgumentNullException.ThrowIfNull(resourceProvider, nameof(resourceProvider));
            ArgumentException.ThrowIfNullOrEmpty(resourcePath, nameof(resourcePath));

            Stream stream = null;
            IImageData imageData = null;
            try
            {
                RepositoryLock.EnterWriteLock();

                EnsureIdentifierIsAvailable(id);

                stream = resourceProvider.GetResourceStream(resourcePath);
                imageData = textureImageDataLoader.Load(stream);

                return CreateCubeMapTexture(id, imageData, tag);
            }
            finally
            {
                RepositoryLock.ExitWriteLock();

                stream?.Dispose();
                imageData?.Dispose();
            }
        }

        /// <inheritdoc/>
        public void LoadBuiltInCubeMapTextures()
        {
            try
            {
                RepositoryLock.EnterWriteLock();

                LoadBuiltInCubeMapTexturesInternal();

                // make sure fallback textures are loaded
                fallbackTexture = GetItemById(TextureConstants.FallbackTextureId);
                if (fallbackTexture == null)
                {
                    throw new InvalidOperationException($"Unable to resolve fallback cube map texture '{TextureConstants.FallbackTextureId}'.");
                }
            }
            finally
            {
                RepositoryLock.ExitWriteLock();
            }
        }


        /// <inheritdoc/>
        protected override bool DeleteItem(CubeMapTextureBase item)
        {
            var isDeleted = base.DeleteItem(item);
            if (!isDeleted)
            {
                return false;
            }

            item.Dispose();
            return true;
        }

        /// <inheritdoc/>
        protected override void ValidateIdentifier(string id)
        {
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));
        }


        private void LoadBuiltInCubeMapTexturesInternal()
        {
            var resourcePaths = resourceProvider.EnumerateResources(BuiltInCubemapTextureSearchPath, true);
            foreach (var resourcePath in resourcePaths)
            {
                var id = resourceProvider.GetRelativePathWithoutExtension(resourcePath);
                id = id.Substring(BuiltInCubemapIdPrefix.Length);

                // create texture
                Stream stream = null;
                CubeMapTextureBase texture = null;
                IImageData imageData = null;
                try
                {
                    stream = resourceProvider.GetResourceStream(resourcePath);
                    imageData = textureImageDataLoader.Load(stream);

                    CreateCubeMapTexture(id, imageData, null);
                }
                catch
                {
                    texture?.Dispose();
                    throw;
                }
                finally
                {
                    imageData?.Dispose();
                    stream?.Dispose();
                }
            }
        }

        private CubeMapTextureBase CreateCubeMapTexture(string id, IImageData imageData, string tag)
        {
            ArgumentOutOfRangeException.ThrowIfNotEqual(
                true,
                MathematicsHelper.IsPowerOf2(imageData.Size.Width),
                nameof(imageData.Size.Width));
            ArgumentOutOfRangeException.ThrowIfNotEqual(
                imageData.Size.Width * 6,
                imageData.Size.Height,
                nameof(imageData.Size.Height));

            CubeMapTextureBase cubeMapTexture = null;
            try
            {
                cubeMapTexture = cubeMapTextureFactory.Create(id, imageData, tag);
                AddItem(cubeMapTexture);
                return cubeMapTexture;
            }
            catch
            {
                cubeMapTexture?.Dispose();
                throw;
            }
        }
    }
}
