//-----------------------------------------------------------------------
// <copyright file="UITemplateAssetImporter.cs" company="Space Development">
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
using Quasar.UI.Templates.Internals;

using Space.Core.DependencyInjection;

namespace Quasar.UI.Internals.Importers
{
    /// <summary>
    /// Quasar UI template importer implementation.
    /// </summary>
    /// <seealso cref="AssetImporterBase" />
    [Export(typeof(IAssetImporter), AssetConstants.Directories.UITemplates)]
    internal sealed class UITemplateAssetImporter : AssetImporterBase
    {
        private readonly IUITemplateRepository templateRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="UITemplateAssetImporter" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="templateRepository">The UI template repository.</param>
        public UITemplateAssetImporter(
            IQuasarContext context,
            IUITemplateRepository templateRepository)
            : base(context, AssetConstants.Directories.UITemplates)
        {
            this.templateRepository = templateRepository;
        }


        /// <inheritdoc/>
        protected override void OnImport(string id, string tag, Stream stream)
        {
            templateRepository.Create(stream, id, false);
        }
    }
}
