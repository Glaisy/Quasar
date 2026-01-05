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
using System.IO;
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
        public ITheme Create(IResourceProvider resourceProvider, string path)
        {
            ArgumentNullException.ThrowIfNull(resourceProvider, nameof(resourceProvider));
            ArgumentException.ThrowIfNullOrEmpty(path, nameof(path));

            Stream stream = null;
            try
            {
                stream = resourceProvider.GetResourceStream(path);
                if (stream == null)
                {
                    throw new UIException($"Theme stream not found for: {path}");
                }

                return CreateThemeInternal(stream, true);
            }
            finally
            {
                themeLock.ExitWriteLock();
                stream?.Dispose();
            }
        }

        /// <inheritdoc/>
        public ITheme Create(Stream stream, bool leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(stream, nameof(stream));

            return CreateThemeInternal(stream, leaveOpen);
        }

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

        /// <inheritdoc/>
        public void ValidateBuiltInAssets()
        {
            try
            {
                themeLock.EnterWriteLock();

                if (!themes.TryGetValue(ThemeConstants.DefaultId, out var defaultTheme))
                {
                    throw new InvalidOperationException($"Unable to resolve the default UI theme: {ThemeConstants.DefaultId}");
                }

                CurrentTheme = defaultTheme;
            }
            finally
            {
                themeLock.ExitWriteLock();
            }
        }


        private Theme CreateThemeInternal(Stream stream, bool leaveOpen)
        {
            try
            {
                var theme = themeFactory.Create(stream, themes, true);
                themes.Add(theme.Id, theme);
                return theme;
            }
            finally
            {
                if (!leaveOpen)
                {
                    stream.Dispose();
                }
            }
        }
    }
}
