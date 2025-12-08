//-----------------------------------------------------------------------
// <copyright file="CubeMapTextureBase.cs" company="Space Development">
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
    /// Abstract base class for CubeMap texture implementations.
    /// </summary>
    /// <seealso cref="ICubeMapTexture" />
    internal abstract class CubeMapTextureBase : GraphicsResourceBase, ICubeMapTexture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CubeMapTextureBase" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="size">The size.</param>
        /// <param name="tag">The tag value.</param>
        /// <param name="graphicsResourceDescriptor">The graphics resource descriptor.</param>
        protected CubeMapTextureBase(
            string id,
            in Size size,
            string tag,
            in GraphicsResourceDescriptor graphicsResourceDescriptor)
            : base(graphicsResourceDescriptor)
        {
            Id = id;
            Size = size;
            Tag = tag;
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            Id = Tag = null;
            Size = Size.Empty;
        }


        /// <inheritdoc/>
        public string Id { get; private set; }

        /// <inheritdoc/>
        public Size Size { get; protected internal set; }

        /// <inheritdoc/>
        public string Tag { get; private set; }
    }
}
