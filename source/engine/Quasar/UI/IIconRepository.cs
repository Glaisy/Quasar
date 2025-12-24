//-----------------------------------------------------------------------
// <copyright file="IIconRepository.cs" company="Space Development">
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

namespace Quasar.UI
{
    /// <summary>
    /// Icon repository interface definition.
    /// </summary>
    /// <seealso cref="IRepository{String, Icon}" />
    public interface IIconRepository : IRepository<string, Icon>
    {
        /// <summary>
        /// Gets the list of icons.
        /// </summary>
        /// <returns>The list of icons.</returns>
        List<Icon> List();

        /// <summary>
        /// Creates an icon from the specified file path.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="filePath">The image file path.</param>
        /// <returns>
        /// The loaded icon.
        /// </returns>
        Icon Create(string id, string filePath);

        /// <summary>
        /// Creates an icon from the specified image stream.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="stream">The stream.</param>
        /// <returns>
        /// The loaded icon.
        /// </returns>
        Icon Create(string id, Stream stream);
    }
}
