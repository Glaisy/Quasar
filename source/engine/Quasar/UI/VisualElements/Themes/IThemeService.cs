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
        /// Loads a theme from the path by the resource provider.
        /// Automatic name is set when name is not provided.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="path">The path.</param>
        /// <param name="resourceProvider">The resource provider.</param>
        /// <param name="name">The theme name.</param>
        /// <returns>
        /// The loaded theme instance.
        /// </returns>
        ITheme Load(string id, string path, IResourceProvider resourceProvider, string name = null);

        /// <summary>
        /// Sets the theme by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Set(string id);


        /// <summary>
        /// Loads the built-in themes.
        /// </summary>
        internal void LoadBuiltInThemes();
    }
}
