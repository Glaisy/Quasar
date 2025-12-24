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
using Quasar.Assets.Importers.Internals;

using Space.Core.DependencyInjection;
using Space.Core.Diagnostics;
using Space.Core.Threading;

namespace Quasar.UI.Internals.Importers
{
    /// <summary>
    /// Quasar icon asset importer implementation.
    /// </summary>
    /// <seealso cref="AssetImporterBase{Object}" />
    [Export(typeof(IAssetImporter), AssetType.Icon)]
    [Singleton]
    internal sealed class IconAssetImporter : AssetImporterBase<object>
    {
        private readonly IIconRepository iconRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="IconAssetImporter"/> class.
        /// </summary>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="iconRepository">The icon repository.</param>
        public IconAssetImporter(
            IDispatcher dispatcher,
            ILoggerFactory loggerFactory,
            IIconRepository iconRepository)
            : base(dispatcher, loggerFactory, AssetType.Icon, false)
        {
            this.iconRepository = iconRepository;
        }

        /// <inheritdoc/>
        protected override object OnBeginImport(Stream stream, string id, string tag)
        {
            iconRepository.Create(id, stream);

            return null;
        }
    }
}
