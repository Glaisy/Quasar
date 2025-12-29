//-----------------------------------------------------------------------
// <copyright file="TextureAssetImporter.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.IO;

using Quasar.Assets;
using Quasar.Assets.Importers;

using Space.Core.DependencyInjection;
using Space.Core.Diagnostics;

namespace Quasar.Graphics.Internals.Importers
{
    /// <summary>
    /// Quasar texture asset importer implementation.
    /// </summary>
    /// <seealso cref="AssetImporterBase" />
    [Export(typeof(IAssetImporter), AssetConstants.Directories.Textures)]
    [Singleton]
    internal sealed class TextureAssetImporter : AssetImporterBase
    {
        private readonly ITextureRepository textureRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="TextureAssetImporter" /> class.
        /// </summary>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="textureRepository">The texture repository.</param>
        public TextureAssetImporter(
            ILoggerFactory loggerFactory,
            ITextureRepository textureRepository)
            : base(loggerFactory, AssetConstants.Directories.Textures)
        {
            this.textureRepository = textureRepository;
        }


        /// <inheritdoc/>
        protected override void OnImport(string id, string tag, Stream stream)
        {
            textureRepository.Create(id, stream, tag);
        }
    }
}
