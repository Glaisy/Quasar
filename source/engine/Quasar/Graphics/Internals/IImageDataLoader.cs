//-----------------------------------------------------------------------
// <copyright file="IImageDataLoader.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.IO;

namespace Quasar.Graphics.Internals
{
    /// <summary>
    /// Represents a native image data loader component.
    /// </summary>
    internal interface IImageDataLoader
    {
        /// <summary>
        /// Loads the image data from the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="flipVerticalAxis">Vertical axis flipping flag.</param>
        /// <returns>
        /// The loaded image data.
        /// </returns>
        IImageData Load(Stream stream, bool flipVerticalAxis);

        /// <summary>
        /// Loads the image data from the specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="flipVerticalAxis">Vertical axis flipping flag.</param>
        /// <returns>
        /// The loaded image data.
        /// </returns>
        IImageData Load(string filePath, bool flipVerticalAxis);
    }
}
