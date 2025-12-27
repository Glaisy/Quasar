//-----------------------------------------------------------------------
// <copyright file="ICubeMapTextureRepository.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.IO;

using Quasar.Collections;
using Quasar.Utilities;

namespace Quasar.Graphics
{
    /// <summary>
    /// Represents the cube map texture resource repository.
    /// </summary>
    /// <seealso cref="IRepository{String, ICubeMapTexture}" />
    public interface ICubeMapTextureRepository : ITaggedRepository<string, ICubeMapTexture>
    {
        /// <summary>
        /// Gets the fallback cube map texture.
        /// </summary>
        ICubeMapTexture FallbackTexture { get; }


        /// <summary>
        /// Creates a 2D texture instance from the image file path.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="filePath">The image file path (The image should contain the cube map faces in vertical layout, in +X, -X, +Y, -Y, +Z, -Z order).</param>
        /// <param name="tag">The tag value.</param>
        /// <returns>
        /// The created texture instance.
        /// </returns>
        ICubeMapTexture Create(
            string id,
            string filePath,
            string tag = null);

        /// <summary>
        /// Creates a cube map texture instance from the image stream.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="stream">The image stream (The image should contain the cube map faces in vertical layout, in +X, -X, +Y, -Y, +Z, -Z order).</param>
        /// <param name="tag">The tag value.</param>
        /// <returns>
        /// The created texture instance.
        /// </returns>
        ICubeMapTexture Create(
            string id,
            Stream stream,
            string tag = null);

        /// <summary>
        /// Creates a cube map texture instance from the resource path.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="resourceProvider">The resource provider.</param>
        /// <param name="resourcePath">The image resource path (The image should contain the cube map faces in vertical layout, in +X, -X, +Y, -Y, +Z, -Z order).</param>
        /// <param name="tag">The tag value.</param>
        /// <returns>
        /// The loaded cube map texture instance.
        /// </returns>
        ICubeMapTexture Create(string id, IResourceProvider resourceProvider, string resourcePath, string tag = null);


        /// <summary>
        /// Validates the built-in assets.
        /// </summary>
        internal void ValidateBuiltInAssets();
    }
}
