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

using System.Collections.Generic;

using Quasar.UI.VisualElements.Styles.Internals.Parsers;

namespace Quasar.UI.VisualElements.Styles.Internals
{
    /// <summary>
    /// Quasat style sheet (qss) parser interface definition.
    /// </summary>
    internal interface IStyleSheetParser
    {
        /// <summary>
        /// Parses a style sheet by the specified style sheet table and root identifier.
        /// </summary>
        /// <param name="styleSheets">The style sheets.</param>
        /// <param name="rootStyleSheetId">The root style sheet identifier.</param>
        /// <returns>
        /// The dictionary of non-parsed attribute sets by selectors.
        /// </returns>
        StyleTable Parse(IReadOnlyDictionary<string, string> styleSheets, string rootStyleSheetId);

        /// <summary>
        /// Parses the inline style string to style properties.
        /// </summary>
        /// <param name="inlineStyle">The inline style.</param>
        /// <param name="templateId">The UI template identifier.</param>
        StyleProperties ParseInline(string inlineStyle, string templateId);
    }
}
