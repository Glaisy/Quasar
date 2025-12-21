//-----------------------------------------------------------------------
// <copyright file="IStyleSheetParser.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.UI.VisualElements.Styles.Internals.Parsers;
using Quasar.Utilities;

namespace Quasar.UI.VisualElements.Styles.Internals
{
    /// <summary>
    /// Quasat style sheet (qss) parser interface definition.
    /// </summary>
    internal interface IStyleSheetParser
    {
        /// <summary>
        /// Parses a style sheet by the specified style sheet resource path and resource provider.
        /// </summary>
        /// <param name="path">The resource path.</param>
        /// <param name="resourceProvider">The resource provider.</param>
        /// <returns>
        /// The dictionary of non-parsed attribute sets by selectors.
        /// </returns>
        StyleTable Parse(string path, IResourceProvider resourceProvider);

        /// <summary>
        /// Parses the inline style string to style properties.
        /// </summary>
        /// <param name="inlineStyle">The inline style.</param>
        /// <param name="templateId">The UI template identifier.</param>
        StyleProperties ParseInline(string inlineStyle, string templateId);
    }
}
