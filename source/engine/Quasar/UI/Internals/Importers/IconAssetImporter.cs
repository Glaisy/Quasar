//-----------------------------------------------------------------------
// <copyright file="IconAssetImporter.cs" company="Space Development">
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

namespace Quasar.UI.Internals.Importers
{
    /// <summary>
    /// Quasar icon asset importer implementation.
    /// </summary>
    /// <seealso cref="AssetImporterBase" />
    [Export(typeof(IAssetImporter), AssetConstants.Directories.Icons)]
    [Singleton]
    internal sealed class IconAssetImporter : AssetImporterBase
    {
        private readonly IIconRepository iconRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="IconAssetImporter" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="iconRepository">The icon repository.</param>
        public IconAssetImporter(
            IQuasarContext context,
            IIconRepository iconRepository)
            : base(context, AssetConstants.Directories.Icons)
        {
            this.iconRepository = iconRepository;
        }

        /// <inheritdoc/>
        protected override void OnImport(string id, string tag, Stream stream)
        {
            iconRepository.Create(id, stream);
        }
    }
}
