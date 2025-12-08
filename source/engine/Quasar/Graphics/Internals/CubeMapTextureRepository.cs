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
using System.Collections.Generic;
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
    /// <seealso cref="ResourceRepositoryBase{String, ICubeMapTexture, CubeMapTextureBase}" />
    /// <seealso cref="ICubeMapTextureRepository" />
    [Export(typeof(ICubeMapTextureRepository))]
    [Singleton]
    internal sealed class CubeMapTextureRepository :
        ResourceRepositoryBase<string, ICubeMapTexture, CubeMapTextureBase>, ICubeMapTextureRepository
    {
        private const string BuiltInCubemapTextureDirectoryPath = "./Cubemaps";
        private const string FallbackTexturePath = TextureConstants.FallbackTextureId + ".png";


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
        public ICubeMapTexture Load(string id, IImageData imageData, string tag = null)
        {
            ValidateIdentifier(id);
            ArgumentNullException.ThrowIfNull(imageData, nameof(imageData));

            try
            {
                RepositoryLock.EnterWriteLock();

                EnsureIdentifierIsAvailable(id);

                return LoadCubeMapTextureInternal(id, imageData, tag);
            }
            finally
            {
                RepositoryLock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        public ICubeMapTexture Load(string id, IResourceProvider resourceProvider, string resourcePath, string tag = null)
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

                return LoadCubeMapTextureInternal(id, imageData, tag);
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

                // loads built-in cubemap textures
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
        protected override void DeleteItem(CubeMapTextureBase item)
        {
            base.DeleteItem(item);

            item.Dispose();
        }

        /// <inheritdoc/>
        protected override void LoadItems(
            IResourceProvider resourceProvider,
            string resourceDirectoryPath,
            in ICollection<CubeMapTextureBase> loadedItems,
            string tag = null)
        {
            throw new NotImplementedException();
        }


        /// <inheritdoc/>
        protected override void ValidateIdentifier(string id)
        {
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));
        }


        private CubeMapTextureBase LoadCubeMapTextureInternal(string id, IImageData imageData, string tag)
        {
            ArgumentOutOfRangeException.ThrowIfNotEqual(true, MathematicsHelper.IsPowerOf2(imageData.Size.Width), nameof(imageData.Size.Width));
            ArgumentOutOfRangeException.ThrowIfNotEqual(imageData.Size.Width * 6, imageData.Size.Height, nameof(imageData.Size.Height));

            CubeMapTextureBase cubeMapTexture = null;
            try
            {
                cubeMapTexture = cubeMapTextureFactory.Create(id, imageData, tag);
                AddItem(id, cubeMapTexture);
                return cubeMapTexture;
            }
            catch
            {
                cubeMapTexture?.Dispose();
                throw;
            }
        }

        private void LoadBuiltInCubeMapTexturesInternal()
        {
            var resourcePaths = resourceProvider.EnumerateResources(null, true);
            foreach (var resourcePath in resourcePaths)
            {
                // extract texture identifier from resource path
                var id = resourceProvider.GetRelativePathWithoutExtension(resourcePath);

                // create texture
                Stream stream = null;
                CubeMapTextureBase texture = null;
                IImageData imageData = null;
                try
                {
                    stream = resourceProvider.GetResourceStream(resourcePath);
                    imageData = textureImageDataLoader.Load(stream);

                    LoadCubeMapTextureInternal(id, imageData, null);
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
    }
}
