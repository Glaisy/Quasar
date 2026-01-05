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

using Space.Core.DependencyInjection;

namespace Quasar.UI.Internals.Importers
{
    /// <summary>
    /// Quasar cursor asset importer implementation.
    /// </summary>
    /// <seealso cref="AssetImporterBase" />
    [Export(typeof(IAssetImporter), AssetConstants.Directories.Cursors)]
    [Singleton]
    internal sealed class CursorAssetImporter : AssetImporterBase
    {
        private readonly ICursorRepository cursorRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="CursorAssetImporter" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="cursorRepository">The cursor repository.</param>
        public CursorAssetImporter(
            IQuasarContext context,
            ICursorRepository cursorRepository)
            : base(context, AssetConstants.Directories.Cursors)
        {
            this.cursorRepository = cursorRepository;
        }

        /// <inheritdoc/>
        protected override void OnImport(string id, string tag, Stream stream)
        {
            cursorRepository.Create(id, stream, default);
        }
    }
}
