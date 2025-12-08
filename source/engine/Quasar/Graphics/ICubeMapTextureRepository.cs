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

using Quasar.Collections;
using Quasar.Core.IO;

namespace Quasar.Graphics
{
    /// <summary>
    /// Represents the cube map texture resource repository.
    /// </summary>
    /// <seealso cref="IRepository{String, ICubeMapTexture}" />
    public interface ICubeMapTextureRepository : IResourceRepository<string, ICubeMapTexture>
    {
        /// <summary>
        /// Gets the fallback cube map texture.
        /// </summary>
        ICubeMapTexture FallbackTexture { get; }


        /// <summary>
        /// Loads a cube map texture instance from the image data.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="imageData">The image data(The image should contain the cube map faces in vertical layout, in +X, -X, +Y, -Y, +Z, -Z order).</param>
        /// <param name="tag">The tag value.</param>
        /// <returns>
        /// The loaded cube map texture instance.
        /// </returns>
        ICubeMapTexture Load(string id, IImageData imageData, string tag = null);

        /// <summary>
        /// Loads a cube map texture instance from the resource path.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="resourceProvider">The resource provider.</param>
        /// <param name="resourcePath">The image resource path (The image should contain the cube map faces in vertical layout, in +X, -X, +Y, -Y, +Z, -Z order).</param>
        /// <param name="tag">The tag value.</param>
        /// <returns>
        /// The loaded cube map texture instance.
        /// </returns>
        ICubeMapTexture Load(string id, IResourceProvider resourceProvider, string resourcePath, string tag = null);


        /// <summary>
        /// Load the built-in cube map textures into the repository.
        /// </summary>
        internal void LoadBuiltInCubeMapTextures();
    }
}
