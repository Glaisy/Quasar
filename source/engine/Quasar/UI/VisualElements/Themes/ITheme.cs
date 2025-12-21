//-----------------------------------------------------------------------
// <copyright file="ITheme.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.UI.VisualElements.Styles;

using Space.Core;

namespace Quasar.UI.VisualElements.Themes
{
    /// <summary>
    /// Represents a Quasar UI theme object.
    /// </summary>
    /// <seealso cref="IIdentifierProvider{String}" />
    /// <seealso cref="INameProvider" />
    public interface ITheme : IIdentifierProvider<string>, INameProvider
    {
        /// <summary>
        /// Gets the root style.
        /// </summary>
        IStyle RootStyle { get; }


        /// <summary>
        /// Gets the style by the class selector.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pseudoClass">The pseudo class.</param>
        /// <returns>
        /// The style instance or null if not exists.
        /// </returns>
        IStyle GetStyleByClass(string selector, PseudoClass pseudoClass);

        /// <summary>
        /// Gets the style by the name selector.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pseudoClass">The pseudo class.</param>
        /// <returns>
        /// The style instance or null if not exists.
        /// </returns>
        IStyle GetStyleByName(string selector, PseudoClass pseudoClass);

        /// <summary>
        /// Gets the style by the tag selector.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pseudoClass">The pseudo class.</param>
        /// <returns>
        /// The style instance or null if not exists.
        /// </returns>
        IStyle GetStyleByTag(string selector, PseudoClass pseudoClass);
    }
}
