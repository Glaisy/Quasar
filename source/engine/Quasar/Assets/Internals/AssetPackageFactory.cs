//-----------------------------------------------------------------------
// <copyright file="AssetPackageFactory.cs" company="Space Development">
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
using System.IO.Compression;

using Microsoft.Extensions.DependencyInjection;

using Quasar.Utilities;

using Space.Core.DependencyInjection;

namespace Quasar.Assets.Internals
{
    /// <summary>
    /// Asset package factory component implementation.
    /// </summary>
    /// <seealso cref="IAssetPackageFactory" />
    [Export(typeof(IAssetPackageFactory))]
    [Singleton]
    internal sealed class AssetPackageFactory : IAssetPackageFactory
    {
        private readonly IServiceProvider serviceProvider;


        /// <summary>
        /// Initializes a new instance of the <see cref="AssetPackageFactory"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public AssetPackageFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;

            AssetPackage.InitializeStaticServices(serviceProvider);
        }


        /// <inheritdoc/>
        public IAssetPackage Create(string filePath)
        {
            ArgumentException.ThrowIfNullOrEmpty(filePath, nameof(filePath));

            var stream = File.OpenRead(filePath);
            if (stream == null)
            {
                throw new AssetException($"Asset package not found: {filePath}");
            }

            return CreateInternal(stream, false);
        }

        /// <inheritdoc/>
        public IAssetPackage Create(Stream stream, bool leaveOpen = true)
        {
            ArgumentNullException.ThrowIfNull(stream, nameof(stream));

            return CreateInternal(stream, leaveOpen);
        }

        /// <inheritdoc/>
        public IAssetPackage Create(IResourceProvider resourceProvider, string resourcePath)
        {
            ArgumentNullException.ThrowIfNull(resourceProvider, nameof(resourceProvider));
            ArgumentException.ThrowIfNullOrEmpty(resourcePath, nameof(resourcePath));

            var stream = resourceProvider.GetResourceStream(resourcePath);
            if (stream == null)
            {
                throw new AssetException($"Asset package not found: {resourcePath}");
            }

            return CreateInternal(stream, false);
        }


        private AssetPackage CreateInternal(Stream stream, bool leaveOpen)
        {
            ZipArchive zipArchive = null;
            AssetPackage assetPackage = null;
            try
            {
                zipArchive = new ZipArchive(stream, ZipArchiveMode.Read, leaveOpen);
                assetPackage = serviceProvider.GetRequiredService<AssetPackage>();
                assetPackage.Initialize(zipArchive);
                return assetPackage;
            }
            catch
            {
                assetPackage?.Dispose();
                zipArchive?.Dispose();

                throw;
            }
        }
    }
}
