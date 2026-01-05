//-----------------------------------------------------------------------
// <copyright file="ThemeAssetImporter.cs" company="Space Development">
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
using Quasar.UI.VisualElements.Themes;

using Space.Core.DependencyInjection;
using Space.Core.Diagnostics;

namespace Quasar.UI.Internals.Importers
{
    /// <summary>
    /// Quasar UI theme asset importer implementation.
    /// </summary>
    /// <seealso cref="AssetImporterBase" />
    [Export(typeof(IAssetImporter), AssetConstants.Directories.Themes)]
    internal sealed class ThemeAssetImporter : AssetImporterBase
    {
        private readonly IThemeService themeService;


        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeAssetImporter" /> class.
        /// </summary>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="themeService">The theme service.</param>
        public ThemeAssetImporter(
            ILoggerFactory loggerFactory,
            IThemeService themeService)
            : base(loggerFactory, AssetConstants.Directories.Themes)
        {
            this.themeService = themeService;
        }

        /// <inheritdoc/>
        protected override void OnImport(string id, string tag, Stream stream)
        {
            themeService.Create(stream);
        }
    }
}
