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
using Quasar.Assets.Importers.Internals;

using Space.Core.DependencyInjection;
using Space.Core.Diagnostics;
using Space.Core.Threading;

namespace Quasar.Graphics.Internals.Importers
{
    /// <summary>
    /// Quasar texture asset importer implementation.
    /// </summary>
    /// <seealso cref="AssetImporterBase{IImageData}" />
    [Export(typeof(IAssetImporter), AssetType.Texture)]
    [Singleton]
    internal sealed class TextureAssetImporter : AssetImporterBase<IImageData>
    {
        private readonly ITextureRepository textureRepository;
        private readonly ITextureImageDataLoader textureImageDataLoader;


        /// <summary>
        /// Initializes a new instance of the <see cref="TextureAssetImporter" /> class.
        /// </summary>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="textureRepository">The texture repository.</param>
        /// <param name="textureImageDataLoader">The texture image data loader.</param>
        public TextureAssetImporter(
            IDispatcher dispatcher,
            ILoggerFactory loggerFactory,
            ITextureRepository textureRepository,
            ITextureImageDataLoader textureImageDataLoader)
            : base(dispatcher, loggerFactory, AssetType.Texture, true)
        {
            this.textureRepository = textureRepository;
            this.textureImageDataLoader = textureImageDataLoader;
        }


        /// <inheritdoc/>
        protected override IImageData OnBeginImport(Stream stream, string id, string tag)
        {
            return textureImageDataLoader.Load(stream);
        }

        /// <inheritdoc/>
        protected override void OnEndImport(in AssetImportState<IImageData> state)
        {
            textureRepository.Create(state.Id, state.AssetData, state.Tag);
            state.OnCompleted(state.Id, true);
        }
    }
}
