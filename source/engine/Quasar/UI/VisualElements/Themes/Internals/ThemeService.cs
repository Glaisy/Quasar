//-----------------------------------------------------------------------
// <copyright file="ThemeService.cs" company="Space Development">
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
using System.Linq;
using System.Threading;

using Quasar.Utilities;

using Space.Core.DependencyInjection;

namespace Quasar.UI.VisualElements.Themes.Internals
{
    /// <summary>
    /// Quasar theme service implementation.
    /// </summary>
    /// <seealso cref="IThemeService" />
    [Export(typeof(IThemeService))]
    [Export(typeof(IThemeProvider))]
    [Singleton]
    internal sealed class ThemeService : IThemeService
    {
        private const string BuiltInThemesDirectoryPath = $"{nameof(Quasar)}/Resources/Themes";
        private const string DefaultThemePath = $"{ThemeConstants.DefaultId}/Theme.tss";


        private readonly ThemeFactory themeFactory;
        private readonly ReaderWriterLockSlim themeLock = new ReaderWriterLockSlim();
        private readonly Dictionary<string, Theme> themes = new Dictionary<string, Theme>();
        private readonly IResourceProvider resourceProvider;


        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeService" /> class.
        /// </summary>
        /// <param name="resourceProviderFactory">The resource provider factory.</param>
        /// <param name="themeFactory">The theme factory.</param>
        public ThemeService(
            IResourceProviderFactory resourceProviderFactory,
            ThemeFactory themeFactory)
        {
            this.themeFactory = themeFactory;

            resourceProvider = resourceProviderFactory
                .Create(GetType().Assembly, BuiltInThemesDirectoryPath);
        }


        /// <inheritdoc/>
        public ITheme CurrentTheme { get; private set; }


        /// <inheritdoc/>
        public ITheme Get(string id)
        {
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));

            try
            {
                themeLock.EnterReadLock();

                themes.TryGetValue(id, out var theme);
                return theme;
            }
            finally
            {
                themeLock.ExitReadLock();
            }
        }

        /// <inheritdoc/>
        public IReadOnlyList<ITheme> List()
        {
            try
            {
                themeLock.EnterReadLock();

                return themes.Values.ToList();
            }
            finally
            {
                themeLock.ExitReadLock();
            }
        }

        /// <inheritdoc/>
        public ITheme Load(string id, string path, IResourceProvider resourceProvider, string name = null)
        {
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));
            ArgumentException.ThrowIfNullOrEmpty(path, nameof(path));
            ArgumentNullException.ThrowIfNull(resourceProvider, nameof(resourceProvider));

            try
            {
                themeLock.EnterWriteLock();

                return LoadInternal(id, name, path, resourceProvider);
            }
            finally
            {
                themeLock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        public void LoadBuiltInThemes()
        {
            try
            {
                themeLock.EnterWriteLock();

                CurrentTheme = LoadInternal(ThemeConstants.DefaultId, null, DefaultThemePath, resourceProvider);
            }
            finally
            {
                themeLock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        public void Set(string id)
        {
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));

            try
            {
                themeLock.EnterWriteLock();

                if (!themes.TryGetValue(id, out var theme))
                {
                    return;
                }

                CurrentTheme = theme;
            }
            finally
            {
                themeLock.ExitWriteLock();
            }
        }


        private Theme LoadInternal(string id, string name, string path, IResourceProvider resourceProvider)
        {
            // try to load theme from the resource path
            var theme = themeFactory.Create(id, name, path, resourceProvider, themes);
            if (theme == null)
            {
                return null;
            }

            // add to themes
            themes.Add(theme.Id, theme);
            return theme;
        }
    }
}
