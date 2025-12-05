//-----------------------------------------------------------------------
// <copyright file="ITexture.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Space.Core;

namespace Quasar.Graphics
{
    /// <summary>
    /// Interface definition for 2D texture implementations.
    /// </summary>
    /// <seealso cref="IGraphicsResource" />
    /// <seealso cref="IIdentifierProvider{String}" />
    public interface ITexture : IGraphicsResource, IIdentifierProvider<string>
    {
        /// <summary>
        /// Gets the anisotropic filtering level (if supported).
        /// </summary>
        int AnisotropyLevel { get; }

        /// <summary>
        /// Gets a value indicating whether mipmapping/anitropic filtering is set for the texture.
        /// </summary>
        bool Filtered { get; }

        /// <summary>
        /// Gets the repeat mode in X direction.
        /// </summary>
        TextureRepeatMode RepeatModeX { get; }

        /// <summary>
        /// Gets the repeat mode in Y direction.
        /// </summary>
        TextureRepeatMode RepeatModeY { get; }

        /// <summary>
        /// Gets the size of the texture in pixels.
        /// </summary>
        Size Size { get; }
    }
}
