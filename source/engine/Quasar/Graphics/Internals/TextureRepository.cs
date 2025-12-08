//-----------------------------------------------------------------------
// <copyright file="TextureRepository.cs" company="Space Development">
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

namespace Quasar.Graphics.Internals
{
    /// <summary>
    /// Texture repository implementation.
    /// </summary>
    /// <seealso cref="ResourceRepositoryBase{String, ITexture, TextureBase}" />
    /// <seealso cref="ITextureRepository" />
    [Export(typeof(ITextureRepository))]
    [Singleton]
    internal sealed class TextureRepository
        : ResourceRepositoryBase<string, ITexture, TextureBase>, ITextureRepository
    {
        private const string BuiltInTextureDirectoryPath = "Resources/Textures";


        private readonly IResourceProvider resourceProvider;
        private readonly ITextureImageDataLoader textureImageDataLoader;
        private readonly ITextureFactory textureFactory;


        /// <summary>
        /// Initializes a new instance of the <see cref="TextureRepository" /> class.
        /// </summary>
        /// <param name="context">The Quasar context.</param>
        /// <param name="textureFactory">The texture factory.</param>
        /// <param name="textureImageDataLoader">The texture image data loader.</param>
        public TextureRepository(
            IQuasarContext context,
            ITextureFactory textureFactory,
            ITextureImageDataLoader textureImageDataLoader)
        {
            this.textureFactory = textureFactory;
            this.textureImageDataLoader = textureImageDataLoader;

            resourceProvider = context.ResourceProvider;
        }


        private TextureBase fallbackNormalMapTexture;
        /// <inheritdoc/>
        public ITexture FallbackNormalMapTexture => fallbackNormalMapTexture;

        private TextureBase fallbackTexture;
        /// <inheritdoc/>
        public ITexture FallbackTexture => fallbackTexture;


        /// <inheritdoc/>
        public ITexture Load(
            string id,
            IImageData imageData,
            string tag = null,
            in TextureDescriptor textureDescriptor = default)
        {
            ValidateIdentifier(id);
            ArgumentNullException.ThrowIfNull(imageData, nameof(imageData));

            try
            {
                RepositoryLock.EnterWriteLock();

                EnsureIdentifierIsAvailable(id);

                return CreateTexture(id, imageData, tag, textureDescriptor);
            }
            finally
            {
                RepositoryLock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        public ITexture Load(
            string id,
            string filePath,
            string tag = null,
            in TextureDescriptor textureDescriptor = default)
        {
            ValidateIdentifier(id);
            ArgumentException.ThrowIfNullOrEmpty(filePath, nameof(filePath));

            Stream stream = null;
            IImageData imageData = null;
            try
            {
                RepositoryLock.EnterWriteLock();

                EnsureIdentifierIsAvailable(id);

                stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                imageData = textureImageDataLoader.Load(stream);

                return CreateTexture(id, imageData, tag, textureDescriptor);
            }
            finally
            {
                imageData?.Dispose();
                stream?.Dispose();

                RepositoryLock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        public ITexture Load(
            string id,
            Stream stream,
            string tag = null,
            in TextureDescriptor textureDescriptor = default)
        {
            ValidateIdentifier(id);
            ArgumentNullException.ThrowIfNull(stream, nameof(stream));

            IImageData imageData = null;
            try
            {
                RepositoryLock.EnterWriteLock();

                EnsureIdentifierIsAvailable(id);

                imageData = textureImageDataLoader.Load(stream);
                return CreateTexture(id, imageData, tag, textureDescriptor);
            }
            finally
            {
                imageData?.Dispose();

                RepositoryLock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        public ITexture Load(
            string id,
            IResourceProvider resourceProvider,
            string resourcePath,
            string tag = null,
            in TextureDescriptor textureDescriptor = default)
        {
            ValidateIdentifier(id);
            ArgumentNullException.ThrowIfNull(resourceProvider, nameof(resourceProvider));

            Stream stream = null;
            IImageData imageData = null;
            try
            {
                RepositoryLock.EnterWriteLock();

                EnsureIdentifierIsAvailable(id);

                stream = resourceProvider.GetResourceStream(resourcePath);
                imageData = textureImageDataLoader.Load(stream);

                return CreateTexture(id, imageData, tag, textureDescriptor);
            }
            finally
            {
                imageData?.Dispose();
                stream?.Dispose();

                RepositoryLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Load the built-in textures into the repository.
        /// </summary>
        public void LoadBuiltInTextures()
        {
            try
            {
                RepositoryLock.EnterWriteLock();

                // loads built-in textures
                LoadBuiltInTexturesInternal();

                // make sure fallback textures are loaded
                fallbackTexture = GetItemById(TextureConstants.FallbackTextureId);
                if (fallbackTexture == null)
                {
                    throw new InvalidOperationException($"Unable to resolve fallback texture '" +
                        $"{TextureConstants.FallbackTextureId}'.");
                }

                fallbackNormalMapTexture = GetItemById(TextureConstants.FallbackNormalMapTextureId);
                if (fallbackNormalMapTexture == null)
                {
                    throw new InvalidOperationException($"Unable to resolve fallback normal texture " +
                        $"{TextureConstants.FallbackNormalMapTextureId}'.");
                }
            }
            finally
            {
                RepositoryLock.ExitWriteLock();
            }
        }


        /// <inheritdoc/>
        protected override void DeleteItem(TextureBase item)
        {
            base.DeleteItem(item);

            item.Dispose();
        }

        /// <inheritdoc/>
        protected override void LoadItems(IResourceProvider resourceProvider, string resourceDirectoryPath, in ICollection<TextureBase> loadedItems, string tag = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Validates the identifier.
        /// Should throw an argument exception if the identifier is not valid.
        /// </summary>
        /// <param name="id">The identifier.</param>
        protected override void ValidateIdentifier(string id)
        {
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));
        }


        private TextureBase CreateTexture(string id, IImageData imageData, string tag, in TextureDescriptor textureDescriptor)
        {
            TextureBase texture = null;
            try
            {
                texture = textureFactory.Create(id, imageData, tag, textureDescriptor);
                AddItem(id, texture);
                return texture;
            }
            catch
            {
                // rollback and propagate error
                texture?.Dispose();

                throw;
            }
        }

        private void LoadBuiltInTexturesInternal()
        {
            var resourcePaths = resourceProvider.EnumerateResources(null, true);
            var textureDescriptor = new TextureDescriptor(
                0,
                TextureRepeatMode.Repeat,
                TextureRepeatMode.Repeat);

            foreach (var resourcePath in resourcePaths)
            {
                // extract texture identifier from resource path
                var id = resourceProvider.GetRelativePathWithoutExtension(resourcePath);

                // create texture
                Stream stream = null;
                IImageData imageData = null;
                try
                {
                    stream = resourceProvider.GetResourceStream(resourcePath);
                    imageData = textureImageDataLoader.Load(stream);
                    CreateTexture(id, imageData, null, textureDescriptor);
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
