//-----------------------------------------------------------------------
// <copyright file="ITextureRepository.cs" company="Space Development">
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
using Quasar.Core.IO;

namespace Quasar.Graphics
{
    /// <summary>
    /// Represents the texture resource repository.
    /// </summary>
    /// <seealso cref="IRepository{String, ITexture}" />
    public interface ITextureRepository : IResourceRepository<string, ITexture>
    {
        /// <summary>
        /// Gets the fallback normal texture.
        /// </summary>
        ITexture FallbackNormalMapTexture { get; }

        /// <summary>
        /// Gets the fallback texture.
        /// </summary>
        ITexture FallbackTexture { get; }


        /// <summary>
        /// Loads a 2D texture from the image data.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="imageData">The image data.</param>
        /// <param name="tag">The tag value.</param>
        /// <param name="textureDescriptor">The texture descriptor.</param>
        /// <returns>
        /// The loaded texture instance.
        /// </returns>
        ITexture Load(
            string id,
            IImageData imageData,
            string tag = null,
            in TextureDescriptor textureDescriptor = default);

        /// <summary>
        /// Loads a 2D texture instance from the file path.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="tag">The tag value.</param>
        /// <param name="textureDescriptor">The texture descriptor.</param>
        /// <returns>
        /// The loaded texture instance.
        /// </returns>
        ITexture Load(
            string id,
            string filePath,
            string tag = null,
            in TextureDescriptor textureDescriptor = default);

        /// <summary>
        /// Loads a 2D texture instance from the stream.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="stream">The stream.</param>
        /// <param name="tag">The tag value.</param>
        /// <param name="textureDescriptor">The texture descriptor.</param>
        /// <returns>
        /// The loaded texture instance.
        /// </returns>
        ITexture Load(
            string id,
            Stream stream,
            string tag = null,
            in TextureDescriptor textureDescriptor = default);

        /// <summary>
        /// Loads a 2D texture instance from the resource path.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="resourceProvider">The resource provider.</param>
        /// <param name="resourcePath">The texture resource path.</param>
        /// <param name="tag">The tag value.</param>
        /// <param name="textureDescriptor">The texture descriptor.</param>
        /// <returns>
        /// The loaded texture instance.
        /// </returns>
        ITexture Load(
            string id,
            IResourceProvider resourceProvider,
            string resourcePath,
            string tag = null,
            in TextureDescriptor textureDescriptor = default);


        /// <summary>
        /// Load the built-in textures into the repository.
        /// </summary>
        internal void LoadBuiltInTextures();
    }
}
