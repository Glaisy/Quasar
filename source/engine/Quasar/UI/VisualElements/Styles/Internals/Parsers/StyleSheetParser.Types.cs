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

using Quasar.Utilities;

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

        private struct Context
        {
            public Context(IResourceProvider resourceProvider, string rootResourcePath)
            {
                ResourceProvider = resourceProvider;
                RootResourcePath = rootResourcePath;
            }

            public Context(Context source)
            {
                ResourceProvider = source.ResourceProvider;
                RootResourcePath = source.RootResourcePath;
            }

            public readonly IResourceProvider ResourceProvider;
            public readonly string RootResourcePath;

            public string ImportBaseUrl;

            public ParsedToken ParsedToken;
            public StyleProperties StyleProperties;
            public StyleTable Styles;

            public string StyleSheetUrl;
            public int LineIndex;
        }
    }
}
