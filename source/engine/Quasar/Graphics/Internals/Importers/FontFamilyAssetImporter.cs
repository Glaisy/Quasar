//-----------------------------------------------------------------------
// <copyright file="FontFamilyAssetImporter.cs" company="Space Development">
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
    /// Quasar font asset importer implementation..
    /// </summary>
    /// <seealso cref="AssetImporterBase" />
    [Export(typeof(IAssetImporter), AssetType.FontFamily)]
    internal sealed class FontFamilyAssetImporter : AssetImporterBase
    {
        private readonly IFontFamilyRepository fontFamilyRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="FontFamilyAssetImporter" /> class.
        /// </summary>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="fontFamilyRepository">The font family repository.</param>
        public FontFamilyAssetImporter(
            ILoggerFactory loggerFactory,
            IFontFamilyRepository fontFamilyRepository)
            : base(loggerFactory, AssetType.FontFamily)
        {
            this.fontFamilyRepository = fontFamilyRepository;
        }


        /// <inheritdoc/>
        protected override void OnImport(string id, string tag, Stream stream)
        {
            fontFamilyRepository.Create(stream);
        }
    }
}
