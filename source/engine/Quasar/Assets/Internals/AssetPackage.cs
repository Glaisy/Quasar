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
        private static Dictionary<AssetType, IAssetImporter> assetImporters;
        private readonly ILogger logger;
        private readonly List<string> customAssets = emptyCustomAssets;
        private ZipArchive zipArchive;


        /// <summary>
        /// Initializes a new instance of the <see cref="AssetPackage"/> class.
        /// </summary>
        /// <param name="loggerFactory">The logger factory.</param>
        public AssetPackage(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.Create<AssetPackage>();
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            zipArchive?.Dispose();
        }


        /// <inheritdoc/>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the version.
        /// </summary>
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
            foreach (var zipEntry in zipArchive.Entries)
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
        internal void Initialize(ZipArchive zipArchive)
        {
            this.zipArchive = zipArchive;

            LoadMetadata();
        }

        /// <summary>
        /// Initializes the static services.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        internal static void InitializeStaticServices(IServiceProvider serviceProvider)
        {
            context = serviceProvider.GetRequiredService<IQuasarContext>();

            assetImporters = serviceProvider
                .GetServices<IAssetImporter>()
                .ToDictionary(x => x.Type, x => x);
        }


        private static AssetType DetermineAssetType(string name)
        {
            throw new NotImplementedException();
        }

        private void ImportAssetFromEntry(ZipArchiveEntry zipEntry)
        {
            var assetType = DetermineAssetType(zipEntry.Name);

            throw new NotImplementedException();
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
