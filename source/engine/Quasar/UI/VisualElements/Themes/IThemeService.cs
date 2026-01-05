//-----------------------------------------------------------------------
// <copyright file="IThemeService.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;

using Quasar.Utilities;

namespace Quasar.UI.VisualElements.Themes
{
    /// <summary>
    /// Represents the Quasar theme service for manipulating UI themes.
    /// </summary>
    /// <seealso cref="IThemeProvider" />
    public interface IThemeService : IThemeProvider
    {
        /// <summary>
        /// Creates a theme from the path by the resource provider.
        /// </summary>
        /// <param name="resourceProvider">The resource provider.</param>
        /// <param name="path">The path.</param>
        /// <returns>
        /// The loaded theme instance.
        /// </returns>
        ITheme Create(IResourceProvider resourceProvider, string path);

        /// <summary>
        /// Creates a theme from the stream.
        /// </summary>
        /// <param name="stream">The theme stream.</param>
        /// <param name="leaveOpen">The stream is kept open if set to true; otherwise the stream is disposed after loading.</param>
        /// <returns>
        /// The loaded theme instance.
        /// </returns>
        ITheme Create(Stream stream, bool leaveOpen = false);

        /// <summary>
        /// Gets the theme by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The theme instance or null if not exists.</returns>
        ITheme Get(string id);

        /// <summary>
        /// Gets the list of the avaiable themes.
        /// </summary>
        IReadOnlyList<ITheme> List();

        /// <summary>
        /// Sets the theme by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Set(string id);


        /// <summary>
        /// Validates the built-in assets.
        /// </summary>
        internal void ValidateBuiltInAssets();
    }
}
