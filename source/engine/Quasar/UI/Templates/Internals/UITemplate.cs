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

using Space.Core;

namespace Quasar.UI.Templates.Internals
{
    /// <summary>
    /// UI template object.
    /// </summary>
    /// <seealso cref="IIdentifierProvider{String}" />
    internal class UITemplate : IIdentifierProvider<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UITemplate" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="rootNode">The QXml root node.</param>
        /// <param name="namespaces">The namespaces.</param>
        public UITemplate(string id, XmlNode rootNode, IReadOnlyDictionary<string, string> namespaces)
        {
            Id = id;
            RootNode = rootNode;
            Namespaces = namespaces;
        }


        /// <inheritdoc/>
        public string Id { get; }

        /// <summary>
        /// The namespaces.
        /// </summary>
        public readonly IReadOnlyDictionary<string, string> Namespaces;

        /// <summary>
        /// The QXml root node.
        /// </summary>
        public readonly XmlNode RootNode;
    }
}
