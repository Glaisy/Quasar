//-----------------------------------------------------------------------
// <copyright file="ISkyBox.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics;

namespace Quasar.Rendering
{
    /// <summary>
    /// Represents a cube map texture based skybox.
    /// </summary>
    public interface ISkyBox
    {
        /// <summary>
        /// Gets the cube map texture.
        /// </summary>
        ICubeMapTexture CubeMapTexture { get; }

        /// <summary>
        /// Gets the exposure.
        /// </summary>
        float Exposure { get; }

        /// <summary>
        /// Gets the tint color.
        /// </summary>
        public Color TintColor { get; }
    }
}