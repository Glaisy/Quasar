//-----------------------------------------------------------------------
// <copyright file="AssetPackage.cs" company="Space Development">
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
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Quasar.Assets.Importers;
using Quasar.Utilities;

using Space.Core;
using Space.Core.DependencyInjection;
using Space.Core.Diagnostics;
using Space.Core.Utilities;

namespace Quasar.Assets.Internals
{
    /// <summary>
    /// Quasar asset package implementation.
    /// </summary>
    /// <seealso cref="DisposableBase" />
    /// <seealso cref="IAssetPackage" />
    [Export]
    internal sealed class AssetPackage : DisposableBase, IAssetPackage
    {
        private static readonly List<string> emptyCustomAssets = new List<string>();
        private static IQuasarContext context;
        private static Dictionary<string, IAssetImporter> assetImportersByDirectory;
        private static ILogger logger;
        private static IIdentifierExtractor identifierExtractor;
        private readonly List<string> customAssets = emptyCustomAssets;
        private ZipArchive zipArchive;


        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            zipArchive?.Dispose();
        }


        /// <inheritdoc/>
        public string Name { get; private set; }

        /// <inheritdoc/>
        public string Tag { get; private set; }

        /// <inheritdoc/>
        public Version Version { get; private set; }


        /// <inheritdoc/>
        public IEnumerable<string> EnumerateAssets()
        {
            foreach (var zipEntry in zipArchive.Entries)
            {
                yield return zipEntry.Name;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<string> EnumerateCustomAssets()
        {
            return customAssets;
        }

        /// <inheritdoc/>
        public Stream GetAssetStream(string assetName)
        {
            ArgumentException.ThrowIfNullOrEmpty(assetName, nameof(assetName));

            var zipEntry = zipArchive.GetEntry(assetName);
            if (zipEntry == null)
            {
                throw new AssetException($"Asset '{assetName}' not found in '{Name}' package.");
            }

            return zipEntry.Open();
        }

        /// <inheritdoc/>
        public void ImportAssets()
        {
            foreach (var zipEntry in zipArchive.Entries.OrderBy(x => x.FullName))
            {
                ImportAssetFromEntry(zipEntry);
            }
        }

        /// <inheritdoc/>
        public Task ImportAssetsAsync(Action<IProgressInformation> progressUpdate = null)
        {
            var task = new Task(ImportAssetsTaskFunction, progressUpdate);
            task.Start();
            return task;
        }


        /// <summary>
        /// Initializes the package by the specified zip archive.
        /// </summary>
        /// <param name="zipArchive">The zip archive.</param>
        /// <param name="tag">The tag.</param>
        internal void Initialize(ZipArchive zipArchive, string tag)
        {
            this.zipArchive = zipArchive;
            Tag = tag;

            LoadMetadata();
        }

        /// <summary>
        /// Initializes the static services.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        internal static void InitializeStaticServices(IServiceProvider serviceProvider)
        {
            context = serviceProvider.GetRequiredService<IQuasarContext>();
            identifierExtractor = serviceProvider.GetRequiredService<IIdentifierExtractor>();

            logger = context.Logger;
            assetImportersByDirectory = serviceProvider
                .GetServices<IAssetImporter>()
                .ToDictionary(x => x.Directory, x => x);
        }


        private static IAssetImporter GetAssetImporter(string assetDirectoryName)
        {
            if (assetImportersByDirectory.TryGetValue(assetDirectoryName, out var assetImporter))
            {
                return assetImporter;
            }

            return null;
        }

        private void ImportAssetFromEntry(ZipArchiveEntry zipEntry)
        {
            if (zipEntry.Name == AssetConstants.MetaDataAssetName ||
                String.IsNullOrEmpty(zipEntry.Name))
            {
                return;
            }

            var fullName = zipEntry.FullName;
            var directoryIndex = fullName.IndexOf(context.ResourceProvider.PathResolver.PathSeparator);
            if (directoryIndex <= 1)
            {
                logger.Warning($"Unable to determine asset type for '{zipEntry.Name}'. Skipped.");
                return;
            }

            var directoryName = fullName.Substring(0, directoryIndex);

            var assetImporter = GetAssetImporter(directoryName);
            if (assetImporter == null)
            {
                logger.Warning($"Unhandled asset type for '{zipEntry.Name}'. Skipped.");
                return;
            }

            var assetId = assetImporter.GetIdentifier(identifierExtractor, fullName);
            using (var stream = zipEntry.Open())
            {
                assetImporter.Import(assetId, Tag, stream);
            }
        }

        private void ImportAssetsTaskFunction(object state)
        {
            var progressUpdate = state as Action<IProgressInformation>;

            try
            {
                logger.TraceMethodStart();
                logger.Info($"Importing assets from package: {Name}");

                var progressInformation = new ProgressInformation();

                var delta = 1.0f / (zipArchive.Entries.Count + 1.0f);
                foreach (var zipEntry in zipArchive.Entries)
                {
                    if (progressUpdate != null)
                    {
                        progressInformation.Value += delta;
                        progressInformation.Tag = zipEntry.Name;
                        progressUpdate(progressInformation);
                    }

                    ImportAssetFromEntry(zipEntry);
                }

                if (progressUpdate != null)
                {
                    progressInformation.Value += delta;
                    progressInformation.Tag = null;

                    progressUpdate(progressInformation);
                }
            }
            catch (Exception exception)
            {
                logger.Error(exception, $"Unable import asset(s) from package: {Name}");
            }
            finally
            {
                logger.TraceMethodEnd();
            }
        }

        private void LoadMetadata()
        {
            var metadataEntry = zipArchive.GetEntry(AssetConstants.MetaDataAssetName);
            if (metadataEntry == null)
            {
                throw new AssetException($"Invalid package. Metadata asset is not found: '{AssetConstants.MetaDataAssetName}'");
            }

            using (var stream = metadataEntry.Open())
            {
                var assetMetadata = JsonSerializer.Deserialize<AssetPackageMetaData>(stream);
                Name = assetMetadata.Name;
                Version = assetMetadata.Version ?? AssetConstants.LatestVersion;
            }
        }
    }
}
