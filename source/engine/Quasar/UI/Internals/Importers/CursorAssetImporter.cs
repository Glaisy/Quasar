//-----------------------------------------------------------------------
// <copyright file="CursorAssetImporter.cs" company="Space Development">
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
    /// Quasar cursor asset importer implementation.
    /// </summary>
    /// <seealso cref="AssetImporterBase{Object}" />
    [Export(typeof(IAssetImporter), AssetType.Cursor)]
    [Singleton]
    internal sealed class CursorAssetImporter : AssetImporterBase<object>
    {
        private readonly ICursorRepository cursorRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="CursorAssetImporter" /> class.
        /// </summary>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="cursorRepository">The cursor repository.</param>
        public CursorAssetImporter(
            IDispatcher dispatcher,
            ILoggerFactory loggerFactory,
            ICursorRepository cursorRepository)
            : base(dispatcher, loggerFactory, AssetType.Icon, false)
        {
            this.cursorRepository = cursorRepository;
        }

        /// <inheritdoc/>
        protected override object OnBeginImport(Stream stream, string id, string tag)
        {
            cursorRepository.Create(id, stream, default);

            return null;
        }
    }
}
