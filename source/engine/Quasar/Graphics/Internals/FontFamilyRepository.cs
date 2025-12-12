//-----------------------------------------------------------------------
// <copyright file="FontFamilyRepository.cs" company="Space Development">
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
using System.IO.Compression;
using System.Linq;
using System.Text.Json;

using Quasar.Collections;
using Quasar.Utilities;

using Space.Core.DependencyInjection;

namespace Quasar.Graphics.Internals
{
    /// <summary>
    /// Font family repository implementation.
    /// </summary>
    /// <seealso cref="RepositoryBase{String, IFontFamily, FontFamily}" />
    /// <seealso cref="IFontFamilyRepository" />
    [Export(typeof(IFontFamilyRepository))]
    [Singleton]
    internal sealed class FontFamilyRepository : RepositoryBase<string, IFontFamily, FontFamily>, IFontFamilyRepository
    {
        private const string BuiltInFontFamilySearchPath = "FontFamilies";


        private readonly IResourceProvider resourceProvider;
        private readonly ITextureRepository textureRepository;
        private readonly TextureDescriptor textureDescriptor = new TextureDescriptor(0, TextureRepeatMode.Clamped, TextureRepeatMode.Clamped);


        /// <summary>
        /// Initializes a new instance of the <see cref="FontFamilyRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="textureRepository">The texture repository.</param>
        public FontFamilyRepository(
            IQuasarContext context,
            ITextureRepository textureRepository)
        {
            this.textureRepository = textureRepository;

            resourceProvider = context.ResourceProvider;
        }


        /// <inheritdoc/>
        public List<IFontFamily> List()
        {
            try
            {
                RepositoryLock.EnterReadLock();

                return FindItems(fontFamily => true).OfType<IFontFamily>().ToList();
            }
            finally
            {
                RepositoryLock.ExitReadLock();
            }
        }

        /// <inheritdoc/>
        public IFontFamily Load(Stream stream)
        {
            ArgumentNullException.ThrowIfNull(stream, nameof(stream));

            try
            {
                RepositoryLock.EnterWriteLock();

                return LoadFontFamilyInternal(stream);
            }
            finally
            {
                RepositoryLock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        public IFontFamily Load(IResourceProvider resourceProvider, string resourcePath)
        {
            ArgumentNullException.ThrowIfNull(resourceProvider, nameof(resourceProvider));
            ArgumentException.ThrowIfNullOrEmpty(resourcePath, nameof(resourcePath));

            using (var stream = resourceProvider.GetResourceStream(resourcePath))
            {
                if (stream == null)
                {
                    throw new ArgumentOutOfRangeException($"Unable to find resource stream for '{resourceProvider.GetAbsolutePath(resourcePath)}'.");
                }

                try
                {
                    RepositoryLock.EnterWriteLock();

                    return LoadFontFamilyInternal(stream);
                }
                finally
                {
                    RepositoryLock.ExitWriteLock();
                }
            }
        }

        /// <inheritdoc/>
        public void LoadBuiltInFontFamilies()
        {
            try
            {
                RepositoryLock.EnterWriteLock();

                var resourcePaths = resourceProvider.EnumerateResources(BuiltInFontFamilySearchPath, true);
                foreach (var resourcePath in resourcePaths)
                {
                    using (var stream = resourceProvider.GetResourceStream(resourcePath))
                    {
                        LoadFontFamilyInternal(stream);
                    }
                }
            }
            finally
            {
                RepositoryLock.ExitWriteLock();
            }
        }


        /// <inheritdoc/>
        protected override void ValidateIdentifier(string id)
        {
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));
        }


        private IFontFamily LoadFontFamilyInternal(Stream stream)
        {
            using (var zipArchive = new ZipArchive(stream, ZipArchiveMode.Read))
            {
                // load font family object
                FontFamily fontFamily = null;
                var fontFamilyEntry = zipArchive.GetEntry(FontFamilyConstants.ZipEntryName);
                if (fontFamilyEntry == null)
                {
                    throw new InvalidOperationException($"Unable to locate font family entry.");
                }

                using (var zipStream = fontFamilyEntry.Open())
                {
                    fontFamily = JsonSerializer.Deserialize<FontFamily>(zipStream);
                    fontFamily.Initialize(fontFamilyEntry.Name);
                }

                // load font textures
                foreach (var zipEntry in zipArchive.Entries)
                {
                    // skip font family entry
                    if (zipEntry.Name == FontFamilyConstants.ZipEntryName)
                    {
                        continue;
                    }

                    using (var zipStream = zipEntry.Open())
                    {
                        // font family texture
                        var fontStyle = (FontStyle)Int32.Parse(Path.GetFileNameWithoutExtension(zipEntry.Name));
                        var textureName = String.Format(
                            FontFamilyConstants.TextureNamePatternP2,
                            fontFamily.Id,
                            fontStyle);
                        textureRepository.Create(textureName, zipStream, null, textureDescriptor);
                    }
                }

                AddItem(fontFamily);

                return fontFamily;
            }
        }
    }
}
