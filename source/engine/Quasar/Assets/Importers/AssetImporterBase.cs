//-----------------------------------------------------------------------
// <copyright file="AssetImporterBase.cs" company="Space Development">
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

using Quasar.Utilities;

using Space.Core.Diagnostics;

namespace Quasar.Assets.Importers
{
    /// <summary>
    /// Abstract base class for Quasar asset importers.
    /// </summary>
    /// <seealso cref="IAssetImporter" />
    public abstract class AssetImporterBase : IAssetImporter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetImporterBase" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="directory">The asset directory name.</param>
        protected AssetImporterBase(
            IQuasarContext context,
            string directory)
        {
            Logger = context.Logger;
            Directory = directory;
        }


        /// <inheritdoc/>
        public string Directory { get; }


        /// <inheritdoc/>
        public virtual string GetIdentifier(IIdentifierExtractor identifierExtractor, string assetName)
        {
            return identifierExtractor.GetIdentifier(assetName, Directory.Length + 1, true);
        }

        /// <inheritdoc/>
        public void Import(string id, string tag, Stream stream)
        {
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));
            ArgumentNullException.ThrowIfNull(stream, nameof(stream));

            try
            {
                OnImport(id, tag, stream);
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
        }


        /// <summary>
        /// The logger.
        /// </summary>
        protected readonly ILogger Logger;


        /// <summary>
        /// Asset importing event handler.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="stream">The stream.</param>
        protected abstract void OnImport(string id, string tag, Stream stream);
    }
}
