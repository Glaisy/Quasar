//-----------------------------------------------------------------------
// <copyright file="ThemeFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

using Quasar.Graphics;
using Quasar.UI.VisualElements.Styles;
using Quasar.UI.VisualElements.Styles.Internals;
using Quasar.UI.VisualElements.Styles.Internals.Parsers;
using Quasar.Utilities;

using Space.Core;
using Space.Core.DependencyInjection;

namespace Quasar.UI.VisualElements.Themes.Internals
{
    /// <summary>
    /// Theme factory implementation.
    /// </summary>
    [Export]
    [Singleton]
    internal sealed class ThemeFactory
    {
        private static readonly TextureDescriptor textureDescriptor =
            new TextureDescriptor(0, TextureRepeatMode.Clamped, TextureRepeatMode.Clamped);


        private readonly IIdentifierExtractor identifierExtractor;
        private readonly IStyleFactory styleFactory;
        private readonly IStyleBuilder styleBuilder;
        private readonly IStyleSheetParser styleSheetParser;
        private readonly ITextureRepository textureRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeFactory" /> class.
        /// </summary>
        /// <param name="identifierExtractor">The identifier extractor.</param>
        /// <param name="styleFactory">The style factory.</param>
        /// <param name="styleBuilder">The style builder.</param>
        /// <param name="styleSheetParser">The style sheet parser.</param>
        /// <param name="textureRepository">The texture repository.</param>
        public ThemeFactory(
            IIdentifierExtractor identifierExtractor,
            IStyleFactory styleFactory,
            IStyleBuilder styleBuilder,
            IStyleSheetParser styleSheetParser,
            ITextureRepository textureRepository)
        {
            this.identifierExtractor = identifierExtractor;
            this.styleFactory = styleFactory;
            this.styleBuilder = styleBuilder;
            this.styleSheetParser = styleSheetParser;
            this.textureRepository = textureRepository;
        }


        /// <summary>
        /// Creates a new UI theme object instance from the specified zip stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="themes">The themes.</param>
        /// <param name="leaveOpen">if set to <c>true</c> [leave open].</param>
        /// <returns>
        /// The theme object.
        /// </returns>
        public Theme Create(
            Stream stream,
            IReadOnlyDictionary<string, Theme> themes,
            bool leaveOpen)
        {
            Assertion.ThrowIfNull(stream, nameof(stream));
            Assertion.ThrowIfNull(themes, nameof(themes));

            Theme theme = null;
            ZipArchive zipArchive = null;
            try
            {
                zipArchive = new ZipArchive(stream, ZipArchiveMode.Read, leaveOpen);

                // collect and parse styles
                var stylesheets = CollectStylesSheets(zipArchive);
                var styleTable = styleSheetParser.Parse(stylesheets, ThemeConstants.ThemeRootStyleSheetPath);

                // extract basic theme properties
                ExtractThemeProperties(themes, styleTable, out var id, out var name, out var baseTheme);

                // load theme associated textures
                LoadTextures(id, zipArchive);

                // initialize theme
                if (baseTheme == null)
                {
                    var rootStyle = styleFactory.CreateRoot();
                    theme = new Theme(id, name, rootStyle);
                }
                else
                {
                    theme = new Theme(id, name, baseTheme, styleFactory);
                }

                // initialize theme styles
                foreach (var styleEntries in styleTable)
                {
                    var selector = styleEntries.Key;
                    if (!theme.TryGetStyle(selector, out var style))
                    {
                        style = styleFactory.Create(selector, theme.RootStyle);
                        theme.AddStyle(style);
                    }

                    styleBuilder.Update(style, styleEntries.Value, theme);
                }

                return theme;
            }
            finally
            {
                zipArchive?.Dispose();
            }
        }


        private static Dictionary<string, string> CollectStylesSheets(ZipArchive zipArchive)
        {
            // parse style sheets
            var stylesheets = new Dictionary<string, string>();
            foreach (var zipEntry in zipArchive.Entries)
            {
                if (zipEntry.FullName.StartsWith(ThemeConstants.TexturesDirectoryPath) ||
                    zipEntry.Length == 0)
                {
                    continue;
                }

                using (var reader = new StreamReader(zipEntry.Open(), leaveOpen: false))
                {
                    var styleSheet = reader.ReadToEnd();
                    stylesheets.Add(zipEntry.FullName, styleSheet);
                }
            }

            return stylesheets;
        }

        private void ExtractThemeProperties(
            IReadOnlyDictionary<string, Theme> themes,
            StyleTable styleTable,
            out string id,
            out string name,
            out Theme baseTheme)
        {
            if (!styleTable.TryGetValue(StyleConstants.ThemeStyleName, out var themeStyle))
            {
                throw new UIException($"Theme root style not found. ({ThemeConstants.ThemeRootStyleSheetPath})");
            }

            if (!themeStyle.TryGetValue(ThemeConstants.IdVariable, out id) ||
                String.IsNullOrEmpty(id))
            {
                throw new UIException($"Theme identifier ({ThemeConstants.IdVariable}) is not found in root style ({ThemeConstants.ThemeRootStyleSheetPath}).");
            }

            if (!themeStyle.TryGetValue(ThemeConstants.NameVariable, out name) ||
                String.IsNullOrEmpty(name))
            {
                name = id;
            }

            themeStyle.TryGetValue(ThemeConstants.BaseThemeVariable, out var baseThemeId);
            if (String.IsNullOrEmpty(baseThemeId))
            {
                baseTheme = null;
                return;
            }

            themes.TryGetValue(baseThemeId, out baseTheme);
        }

        private void LoadTextures(string themeId, ZipArchive zipArchive)
        {
            foreach (var zipEntry in zipArchive.Entries)
            {
                if (!zipEntry.FullName.StartsWith(ThemeConstants.TexturesDirectoryPath) ||
                    zipEntry.Length == 0)
                {
                    continue;
                }

                var assetId = identifierExtractor.GetIdentifier(
                    zipEntry.FullName,
                    ThemeConstants.TexturesDirectoryPath.Length + 1,
                    true);
                var textureId = String.Format(ThemeConstants.TextureNameFormatString, themeId, assetId);
                using (var stream = zipEntry.Open())
                {
                    textureRepository.Create(textureId, stream, null, textureDescriptor);
                }
            }
        }
    }
}
