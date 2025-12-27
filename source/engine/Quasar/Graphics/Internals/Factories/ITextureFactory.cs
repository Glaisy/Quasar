//-----------------------------------------------------------------------
// <copyright file="ITextureFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.IO;

namespace Quasar.Graphics.Internals.Factories
{
    /// <summary>
    /// Represents a 2D texture factory component.
    /// </summary>
    internal interface ITextureFactory
    {
        /// <summary>
        /// Creates a texture by the specified identifier from the image stream.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="stream">The image stream.</param>
        /// <param name="tag">The tag value.</param>
        /// <param name="textureDescriptor">The texture descriptor.</param>
        /// <returns>
        /// The created texture instance.
        /// </returns>
        TextureBase Create(string id, Stream stream, string tag, in TextureDescriptor textureDescriptor);
    }
}