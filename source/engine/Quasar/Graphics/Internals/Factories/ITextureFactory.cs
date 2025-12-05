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

namespace Quasar.Graphics.Internals.Factories
{
    /// <summary>
    /// Represents a 2D texture factory component.
    /// </summary>
    internal interface ITextureFactory
    {
        /// <summary>
        /// Creates a texture by the specified identifier from the image data.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="imageData">The image data.</param>
        /// <param name="textureDescriptor">The texture descriptor.</param>
        /// <returns>
        /// The created texture instance.
        /// </returns>
        TextureBase Create(string id, IImageData imageData, in TextureDescriptor textureDescriptor);
    }
}