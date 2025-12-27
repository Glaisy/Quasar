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
using System.IO;

using Quasar.Collections;
using Quasar.Graphics.Internals.Factories;
using Quasar.Utilities;

using Space.Core.DependencyInjection;

namespace Quasar.Graphics.Internals
{
    /// <summary>
    /// Texture repository implementation.
    /// </summary>
    /// <seealso cref="TaggedRepositoryBase{String, ITexture, TextureBase}" />
    /// <seealso cref="ITextureRepository" />
    [Export(typeof(ITextureRepository))]
    [Singleton]
    internal sealed class TextureRepository
        : TaggedRepositoryBase<string, ITexture, TextureBase>, ITextureRepository
    {
        private const string BuiltInTextureIdPrefix = "Textures/";
        private const string BuiltInTextureSearchPath = "./" + BuiltInTextureIdPrefix;


        private readonly IResourceProvider resourceProvider;
        private readonly ITextureFactory textureFactory;


        /// <summary>
        /// Initializes a new instance of the <see cref="TextureRepository" /> class.
        /// </summary>
        /// <param name="context">The Quasar context.</param>
        /// <param name="textureFactory">The texture factory.</param>
        public TextureRepository(
            IQuasarContext context,
            ITextureFactory textureFactory)
        {
            this.textureFactory = textureFactory;

            resourceProvider = context.ResourceProvider;
        }


        private TextureBase fallbackNormalMapTexture;
        /// <inheritdoc/>
        public ITexture FallbackNormalMapTexture => fallbackNormalMapTexture;

        private TextureBase fallbackTexture;
        /// <inheritdoc/>
        public ITexture FallbackTexture => fallbackTexture;


        /// <inheritdoc/>
        public ITexture Create(
            string id,
            string filePath,
            string tag = null,
            in TextureDescriptor textureDescriptor = default)
        {
            ValidateIdentifier(id);
            ArgumentException.ThrowIfNullOrEmpty(filePath, nameof(filePath));

            Stream stream = null;
            try
            {
                RepositoryLock.EnterWriteLock();

                EnsureIdentifierIsAvailable(id);

                stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                return CreateTexture(id, stream, tag, textureDescriptor);
            }
            finally
            {
                stream?.Dispose();

                RepositoryLock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        public ITexture Create(
            string id,
            Stream stream,
            string tag = null,
            in TextureDescriptor textureDescriptor = default)
        {
            ValidateIdentifier(id);
            ArgumentNullException.ThrowIfNull(stream, nameof(stream));

            try
            {
                RepositoryLock.EnterWriteLock();

                EnsureIdentifierIsAvailable(id);

                return CreateTexture(id, stream, tag, textureDescriptor);
            }
            finally
            {
                RepositoryLock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        public ITexture Create(
            string id,
            IResourceProvider resourceProvider,
            string resourcePath,
            string tag = null,
            in TextureDescriptor textureDescriptor = default)
        {
            ValidateIdentifier(id);
            ArgumentNullException.ThrowIfNull(resourceProvider, nameof(resourceProvider));

            Stream stream = null;
            try
            {
                RepositoryLock.EnterWriteLock();

                EnsureIdentifierIsAvailable(id);

                stream = resourceProvider.GetResourceStream(resourcePath);

                return CreateTexture(id, stream, tag, textureDescriptor);
            }
            finally
            {
                stream?.Dispose();

                RepositoryLock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        public void ValidateBuiltInAssets()
        {
            try
            {
                RepositoryLock.EnterWriteLock();

                fallbackTexture = GetItemById(TextureConstants.FallbackTextureId);
                if (fallbackTexture == null)
                {
                    throw new InvalidOperationException($"Unable to resolve fallback texture '{TextureConstants.FallbackTextureId}'.");
                }

                fallbackNormalMapTexture = GetItemById(TextureConstants.FallbackNormalMapTextureId);
                if (fallbackNormalMapTexture == null)
                {
                    throw new InvalidOperationException($"Unable to resolve fallback normal map texture '{TextureConstants.FallbackNormalMapTextureId}'.");
                }
            }
            finally
            {
                RepositoryLock.ExitWriteLock();
            }
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


        private TextureBase CreateTexture(string id, Stream stream, string tag, in TextureDescriptor textureDescriptor)
        {
            TextureBase texture = null;
            try
            {
                texture = textureFactory.Create(id, stream, tag, textureDescriptor);
                if (texture == null)
                {
                    throw new GraphicsException($"Unable to load texture: {id}");
                }

                if (id == TextureConstants.FallbackNormalMapTextureId)
                {
                    fallbackNormalMapTexture = texture;
                }
                else if (id == TextureConstants.FallbackTextureId)
                {
                    fallbackTexture = texture;
                }

                AddItem(texture);
                return texture;
            }
            catch
            {
                // rollback and propagate error
                texture?.Dispose();

                throw;
            }
        }
    }
}
