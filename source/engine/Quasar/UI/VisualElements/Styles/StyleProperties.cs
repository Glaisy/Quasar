//-----------------------------------------------------------------------
// <copyright file="StyleProperties.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace Quasar.UI.VisualElements.Styles
{
    /// <summary>
    /// Represents a set of raw style properties by key and value pair.
    /// </summary>
    /// <seealso cref="Dictionary{String,String}" />
    public sealed class StyleProperties : Dictionary<string, string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StyleProperties"/> class.
        /// </summary>
        public StyleProperties()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleProperties"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public StyleProperties(IReadOnlyDictionary<string, string> source)
            : base(source)
        {
        }
    }
}
