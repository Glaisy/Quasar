//-----------------------------------------------------------------------
// <copyright file="TextureBase.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Graphics.Internals
{
    /// <summary>
    /// Abstract base class for 2D texture implementations.
    /// </summary>
    /// <seealso cref="GraphicsResourceBase" />
    /// <seealso cref="ITexture" />
    internal abstract class TextureBase : GraphicsResourceBase, ITexture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextureBase" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="size">The size.</param>
        /// <param name="textureDescriptor">The texture descriptor.</param>
        /// <param name="tag">The tag value.</param>
        /// <param name="resourceDescriptor">The resource descriptor.</param>
        protected TextureBase(
            string id,
            in Size size,
            in TextureDescriptor textureDescriptor,
            string tag,
            in GraphicsResourceDescriptor resourceDescriptor)
            : base(resourceDescriptor)
        {
            Id = id;
            Size = size;

            AnisotropyLevel = textureDescriptor.AnisotropyLevel;
            Filtered = textureDescriptor.Filtered;
            RepeatModeX = textureDescriptor.RepeatX;
            RepeatModeY = textureDescriptor.RepeatX;

            Tag = tag;
        }


        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            Id = Tag = null;
            AnisotropyLevel = 0;
            Filtered = false;
            RepeatModeX = RepeatModeY = TextureRepeatMode.Clamped;
            Size = Size.Empty;
        }


        /// <inheritdoc/>
        public int AnisotropyLevel { get; private set; }

        /// <inheritdoc/>
        public bool Filtered { get; private set; }

        /// <inheritdoc/>
        public string Id { get; private set; }

        /// <inheritdoc/>
        public TextureRepeatMode RepeatModeX { get; private set; }

        /// <inheritdoc/>
        public TextureRepeatMode RepeatModeY { get; private set; }

        /// <inheritdoc/>
        public Size Size { get; protected set; }

        /// <inheritdoc/>
        public string Tag { get; private set; }
    }
}
