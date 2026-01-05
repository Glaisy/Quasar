//-----------------------------------------------------------------------
// <copyright file="StyleSheetParser.Types.cs" company="Space Development">
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

namespace Quasar.UI.VisualElements.Styles.Internals.Parsers
{
    /// <summary>
    /// Quasar style sheet (qss) parser implementation - Context.
    /// </summary>
    internal sealed partial class StyleSheetParser : IStyleSheetParser
    {
        [Flags]
        private enum ParsedToken
        {
            Default = 0,
            Comment = 1,
            Style = 2
        }

        private readonly struct ParsedProperty
        {
            public ParsedProperty(string name, string value)
            {
                Name = name;
                Value = value;
            }


            public readonly string Name;

            public readonly string Value;
        }

        private struct ParserContext
        {
            public ParserContext(IReadOnlyDictionary<string, string> styleSheets)
            {
                StyleSheets = styleSheets;
            }

            public ParserContext(ParserContext source)
            {
                StyleSheets = source.StyleSheets;
            }

            public readonly IReadOnlyDictionary<string, string> StyleSheets;

            public string ImportBaseUrl;

            public ParsedToken ParsedToken;
            public StyleProperties StyleProperties;
            public StyleTable Styles;

            public string StyleSheetUrl;
            public int LineIndex;
        }
    }
}
