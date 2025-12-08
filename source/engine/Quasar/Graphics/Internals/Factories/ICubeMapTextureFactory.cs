//-----------------------------------------------------------------------
// <copyright file="ICubeMapTextureFactory.cs" company="Space Development">
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
    /// Represents a cube map texture factory component.
    /// </summary>
    internal interface ICubeMapTextureFactory
    {
        /// <summary>
        /// Creates a cube map texture by the specified identifier from the image data.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="imageData">The cube map texture image data.</param>
        /// <param name="tag">The tag value.</param>
        /// <returns>
        /// The created cube map texture instance.
        /// </returns>
        CubeMapTextureBase Create(string id, IImageData imageData, string tag);
    }
}