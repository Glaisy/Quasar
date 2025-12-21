//-----------------------------------------------------------------------
// <copyright file="IStyleFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.UI.VisualElements.Themes;

namespace Quasar.UI.VisualElements.Styles.Internals
{
    /// <summary>
    /// Represents an internal style factory component.
    /// </summary>
    internal interface IStyleFactory
    {
        /// <summary>
        /// Create and initializes a style instance by the specified selector and inherited style.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="inheritedStyle">The inherited style.</param>
        Style Create(string selector, IStyle inheritedStyle);

        /// <summary>
        /// Creates an inline style instance by the specifed properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <param name="theme">The theme.</param>
        Style CreateInline(StyleProperties properties, ITheme theme);

        /// <summary>
        /// Creates and initializes a root style instance.
        /// </summary>
        Style CreateRoot();

        /// <summary>
        /// Releases the specified style instance for later re-use.
        /// </summary>
        /// <param name="style">The style.</param>
        void Release(Style style);
    }
}