//-----------------------------------------------------------------------
// <copyright file="SkyBox.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics;
using Space.Core;

namespace Quasar.Rendering
{
    /// <summary>
    /// Cube map texture based skybox object implementation.
    /// </summary>
    /// <seealso cref="ISkyBox" />
    public sealed class SkyBox : ISkyBox
    {
        /// <inheritdoc/>
        public ICubeMapTexture CubeMapTexture { get; set; }

        private float exposure = 1.0f;
        /// <inheritdoc/>
        public float Exposure
        {
            get => exposure;
            set => exposure = Ranges.FloatUnit.Clamp(value);
        }

        /// <inheritdoc/>
        public Color TintColor { get; set; } = Color.White;
    }
}
