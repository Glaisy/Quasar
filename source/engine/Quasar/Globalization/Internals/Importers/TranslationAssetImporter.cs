//-----------------------------------------------------------------------
// <copyright file="TranslationAssetImporter.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.IO;
using System.Text.Json;

using Quasar.Assets;
using Quasar.Assets.Importers;

using Space.Core.DependencyInjection;
using Space.Core.Globalization;

namespace Quasar.Globalization.Internals.Importers
{
    /// <summary>
    /// The Quasar translation table asset importer implementation.
    /// </summary>
    [Export(typeof(IAssetImporter), AssetConstants.Directories.Translations)]
    [Singleton]
    internal sealed class TranslationAssetImporter : AssetImporterBase
    {
        private readonly IGlobalizationService globalizationService;


        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationAssetImporter" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="globalizationService">The globalization service.</param>
        public TranslationAssetImporter(
            IQuasarContext context,
            IGlobalizationService globalizationService)
            : base(context, AssetConstants.Directories.Translations)
        {
            this.globalizationService = globalizationService;
        }


        /// <inheritdoc/>
        protected override void OnImport(string id, string tag, Stream stream)
        {
            var stringTable = JsonSerializer.Deserialize<StringTable>(stream);
            globalizationService.AddStringTable(stringTable);
        }
    }
}
