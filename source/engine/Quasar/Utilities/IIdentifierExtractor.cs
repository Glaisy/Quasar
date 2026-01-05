//-----------------------------------------------------------------------
// <copyright file="IIdentifierExtractor.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Utilities
{
    /// <summary>
    /// Represents an component to extract identifiers from composed resource paths, names or urls.
    /// </summary>
    public interface IIdentifierExtractor
    {
        /// <summary>
        /// Gets the identifier from the specified composed identifier.
        /// Removes the extension suffix and the characters before the start index.
        /// </summary>
        /// <param name="composedIdentifier">The composed identifier.</param>
        /// <param name="startIndex">The start index.</param>
        string GetIdentifier(string composedIdentifier, int startIndex = 0);
    }
}
