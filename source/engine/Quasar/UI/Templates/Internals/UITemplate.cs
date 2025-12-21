//-----------------------------------------------------------------------
// <copyright file="UITemplate.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Xml;

namespace Quasar.UI.Templates.Internals
{
    /// <summary>
    /// UI template data structure.
    /// </summary>
    internal readonly struct UITemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UITemplate" /> struct.
        /// </summary>
        /// <param name="path">The template path.</param>
        /// <param name="rootNode">The QXml root node.</param>
        /// <param name="namespaces">The namespaces.</param>
        public UITemplate(string path, XmlNode rootNode, IReadOnlyDictionary<string, string> namespaces)
        {
            Path = path;
            RootNode = rootNode;
            Namespaces = namespaces;
        }


        /// <summary>
        /// The namespaces.
        /// </summary>
        public readonly IReadOnlyDictionary<string, string> Namespaces;

        /// <summary>
        /// The template path.
        /// </summary>
        public readonly string Path;

        /// <summary>
        /// The QXml root node.
        /// </summary>
        public readonly XmlNode RootNode;
    }
}
