//-----------------------------------------------------------------------
// <copyright file="ITextureImageDataLoader.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.IO;

namespace Quasar.Graphics
{
    /// <summary>
    /// Represents a texture image data loader component.
    /// </summary>
    public interface ITextureImageDataLoader
    {
        /// <summary>
        /// Loads image data of a texture specified by the file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>
        /// The loaded texture image data.
        /// </returns>
        IImageData Load(string filePath);

        /// <summary>
        /// Loads image data of a texture specified by the stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>
        /// The loaded texture image data.
        /// </returns>
        IImageData Load(Stream stream);
    }
}
