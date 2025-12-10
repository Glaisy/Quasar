//-----------------------------------------------------------------------
// <copyright file="ICursorRepository.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;

using Quasar.Collections;
using Quasar.Graphics;

namespace Quasar.UI
{
    /// <summary>
    /// Cursor repository interface definition.
    /// </summary>
    /// <seealso cref="IRepository{String, Cursor}" />
    public interface ICursorRepository : IRepository<string, Cursor>
    {
        /// <summary>
        /// Gets the list of cursors.
        /// </summary>
        /// <returns>The list of cursors.</returns>
        List<Cursor> List();

        /// <summary>
        /// Loads a cursor from the specified file path.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="filePath">The image file path.</param>
        /// <param name="hotspot">The hotspot position.</param>
        /// <returns>
        /// The loaded cursor.
        /// </returns>
        Cursor Load(string id, string filePath, in Point hotspot);

        /// <summary>
        /// Loads a cursor from the specified image stream.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="stream">The stream.</param>
        /// <param name="hotspot">The hotspot.</param>
        /// <returns>
        /// The loaded cursor.
        /// </returns>
        Cursor Load(string id, Stream stream, in Point hotspot);
    }
}
