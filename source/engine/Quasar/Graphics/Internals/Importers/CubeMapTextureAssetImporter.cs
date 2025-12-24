//-----------------------------------------------------------------------
// <copyright file="CubeMapTextureAssetImporter.cs" company="Space Development">
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
    /// Quasar cube map texture asset importer implementation.
    /// </summary>
    /// <seealso cref="AssetImporterBase{IImageData}" />
    [Export(typeof(IAssetImporter), AssetType.CubeMapTexture)]
    [Singleton]
    internal sealed class CubeMapTextureAssetImporter : AssetImporterBase<IImageData>
    {
        private readonly ICubeMapTextureRepository cubeMapTextureRepository;
        private readonly ITextureImageDataLoader textureImageDataLoader;


        /// <summary>
        /// Initializes a new instance of the <see cref="CubeMapTextureAssetImporter" /> class.
        /// </summary>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="cubeMapTextureRepository">The cube map texture repository.</param>
        /// <param name="textureImageDataLoader">The texture image data loader.</param>
        public CubeMapTextureAssetImporter(
            IDispatcher dispatcher,
            ILoggerFactory loggerFactory,
            ICubeMapTextureRepository cubeMapTextureRepository,
            ITextureImageDataLoader textureImageDataLoader)
            : base(dispatcher, loggerFactory, AssetType.Texture, true)
        {
            this.cubeMapTextureRepository = cubeMapTextureRepository;
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
            cubeMapTextureRepository.Create(state.Id, state.AssetData, state.Tag);
            state.OnCompleted(state.Id, true);
        }
    }
}
