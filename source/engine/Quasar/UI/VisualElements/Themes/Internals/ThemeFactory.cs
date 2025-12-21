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

using Quasar.Graphics;
using Quasar.UI.VisualElements.Styles;
using Quasar.UI.VisualElements.Styles.Internals;
using Quasar.UI.VisualElements.Styles.Internals.Parsers;
using Quasar.Utilities;

using Space.Core;
using Space.Core.DependencyInjection;
using Space.Core.Diagnostics;

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


        private readonly IStyleFactory styleFactory;
        private readonly IStyleBuilder styleBuilder;
        private readonly IStyleSheetParser styleSheetParser;
        private readonly IStyleSheetValueParser valueParser;
        private readonly ITextureRepository textureRepository;
        private readonly ILogger logger;


        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeFactory" /> class.
        /// </summary>
        /// <param name="styleFactory">The style factory.</param>
        /// <param name="styleBuilder">The style builder.</param>
        /// <param name="styleSheetParser">The style sheet parser.</param>
        /// <param name="valueParser">The style sheet value parser.</param>
        /// <param name="textureRepository">The texture repository.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public ThemeFactory(
            IStyleFactory styleFactory,
            IStyleBuilder styleBuilder,
            IStyleSheetParser styleSheetParser,
            IStyleSheetValueParser valueParser,
            ITextureRepository textureRepository,
            ILoggerFactory loggerFactory)
        {
            this.styleFactory = styleFactory;
            this.styleBuilder = styleBuilder;
            this.styleSheetParser = styleSheetParser;
            this.valueParser = valueParser;
            this.textureRepository = textureRepository;

            logger = loggerFactory.Create<ThemeFactory>();
        }


        /// <summary>
        /// Creates a new UI theme object instance by the specified path and resource provider.
        /// Automatic name is set when name is not provided.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name [optional].</param>
        /// <param name="themeResourcePath">The theme resource path.</param>
        /// <param name="resourceProvider">The resource provider.</param>
        /// <param name="themes">The themes.</param>
        /// <returns>
        /// The theme object.
        /// </returns>
        /// Automatic name is set when name is not provided.
        public Theme Create(
            string id,
            string name,
            string themeResourcePath,
            IResourceProvider resourceProvider,
            IReadOnlyDictionary<string, Theme> themes)
        {
            Assertion.ThrowIfNullOrEmpty(id, nameof(id));
            Assertion.ThrowIfNullOrEmpty(themeResourcePath, nameof(themeResourcePath));
            Assertion.ThrowIfNull(resourceProvider, nameof(resourceProvider));

            Theme theme = null;
            try
            {
                // parse style sheet(s)
                var styleSheetParserResult = styleSheetParser.Parse(themeResourcePath, resourceProvider);

                // load theme textures
                LoadTextures(id, themeResourcePath, resourceProvider);

                // initialize theme
                if (String.IsNullOrEmpty(name))
                {
                    name = CalculateThemeName(styleSheetParserResult, id);
                }

                // get base theme
                var baseTheme = GetBaseTheme(styleSheetParserResult, themes);

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

                // parse styles
                foreach (var resultPair in styleSheetParserResult)
                {
                    // get or create new style
                    var selector = resultPair.Key;
                    if (!theme.TryGetStyle(selector, out var style))
                    {
                        style = styleFactory.Create(selector, theme.RootStyle);
                        theme.AddStyle(style);
                    }

                    // update the style
                    styleBuilder.Update(style, resultPair.Value, theme);
                }
            }
            catch (Exception exception)
            {
                logger.Error(exception, $"Unable to complete theme '{themeResourcePath}'.");
            }

            return theme;
        }

        private Theme GetBaseTheme(StyleTable styleSheetParserResult, IReadOnlyDictionary<string, Theme> themes)
        {
            // try to get :theme var(--Base-Theme) variable
            if (!styleSheetParserResult.TryGetValue(StyleConstants.ThemeStyleName, out var themeProperties) ||
                !themeProperties.TryGetValue(ThemeConstants.BaseThemeVariable, out var baseThemeId) ||
                String.IsNullOrEmpty(baseThemeId))
            {
                // no base theme.
                return null;
            }

            // try to get the base theme
            themes.TryGetValue(baseThemeId, out var baseTheme);
            return baseTheme;
        }

        private static string CalculateThemeName(StyleTable styleSheetParserResult, string id)
        {
            // try to get :theme var(--Name) variable
            if (!styleSheetParserResult.TryGetValue(StyleConstants.ThemeStyleName, out var themeProperties) ||
                !themeProperties.TryGetValue(ThemeConstants.NameVariable, out var value) ||
                String.IsNullOrEmpty(value))
            {
                // fallback to the identifier.
                return id;
            }

            return value;
        }

        private void LoadTextures(string themeId, string themeResourcePath, IResourceProvider resourceProvider)
        {
            var baseResourcePath = resourceProvider.GetDirectoryPath(themeResourcePath);
            var textureDirectoryPath = String.Concat(baseResourcePath, resourceProvider.PathResolver.PathSeparator, ThemeConstants.TexturesDirectoryPath);
            var texturePaths = resourceProvider.EnumerateResources(textureDirectoryPath, true);
            foreach (var texturePath in texturePaths)
            {
                // calculate texture name
                var textureName = resourceProvider.GetResourceName(texturePath);
                var relativePathIndex = texturePath.IndexOf(textureDirectoryPath) + textureDirectoryPath.Length + 1;
                var textureRelativePath = texturePath.Substring(relativePathIndex, texturePath.Length - textureName.Length - relativePathIndex);
                if (!String.IsNullOrEmpty(textureRelativePath) &&
                    resourceProvider.PathResolver.PathSeparator != StyleConstants.UrlSeparator)
                {
                    textureRelativePath = textureRelativePath.Replace(resourceProvider.PathResolver.PathSeparator, StyleConstants.UrlSeparator);
                }

                textureName = String.Format(ThemeConstants.TextureNameFormatString, themeId, textureRelativePath + textureName);

                // load texture
                using (var stream = resourceProvider.GetResourceStream(texturePath))
                {
                    textureRepository.Create(textureName, stream, themeId, textureDescriptor);
                }
            }
        }
    }
}
