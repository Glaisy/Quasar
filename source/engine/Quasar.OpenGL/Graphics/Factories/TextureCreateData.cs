//-----------------------------------------------------------------------
// <copyright file="TextureCreateData.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics;

namespace Quasar.OpenGL.Graphics.Factories
{
    /// <summary>
    /// Represents a data structure to provide creation data of OpenGL texture resources.
    /// </summary>
    /// <typeparam name="T">The resource data type.</typeparam>
    internal sealed class TextureCreateData<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextureCreateData{T}"/> class.
        /// </summary>
        /// <param name="imageData">The image data.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="textureDescriptor">The texture descriptor.</param>
        public TextureCreateData(IImageData imageData, string id, string tag, in TextureDescriptor textureDescriptor = default)
        {
            ImageData = imageData;
            Id = id;
            Tag = tag;
            TextureDescriptor = textureDescriptor;
        }


        /// <summary>
        /// The image data.
        /// </summary>
        public readonly IImageData ImageData;

        /// <summary>
        /// The identifier.
        /// </summary>
        public readonly string Id;

        /// <summary>
        /// The tag.
        /// </summary>
        public readonly string Tag;

        /// <summary>
        /// The texture descriptor.
        /// </summary>
        public readonly TextureDescriptor TextureDescriptor;

        /// <summary>
        /// The texture resource.
        /// </summary>
        public T Texture;
    }
}
