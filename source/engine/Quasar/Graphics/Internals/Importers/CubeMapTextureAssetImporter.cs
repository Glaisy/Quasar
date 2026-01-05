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

using Space.Core.DependencyInjection;

namespace Quasar.Graphics.Internals.Importers
{
    /// <summary>
    /// Quasar cube map texture asset importer implementation.
    /// </summary>
    /// <seealso cref="AssetImporterBase" />
    [Export(typeof(IAssetImporter), AssetConstants.Directories.CubeMapTextures)]
    [Singleton]
    internal sealed class CubeMapTextureAssetImporter : AssetImporterBase
    {
        private readonly ICubeMapTextureRepository cubeMapTextureRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="CubeMapTextureAssetImporter" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="cubeMapTextureRepository">The cube map texture repository.</param>
        public CubeMapTextureAssetImporter(
            IQuasarContext context,
            ICubeMapTextureRepository cubeMapTextureRepository)
            : base(context, AssetConstants.Directories.CubeMapTextures)
        {
            this.cubeMapTextureRepository = cubeMapTextureRepository;
        }


        /// <inheritdoc/>
        protected override void OnImport(string id, string tag, Stream stream)
        {
            cubeMapTextureRepository.Create(id, stream, tag);
        }
    }
}
