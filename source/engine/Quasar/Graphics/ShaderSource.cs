//-----------------------------------------------------------------------
// <copyright file="ShaderSource.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Graphics
{
    /// <summary>
    /// Represents an object which stores sub-shader source codes for a render shader.
    /// </summary>
    public class ShaderSource
    {
        /// <summary>
        /// The fragment shader source code.
        /// </summary>
        public string FragmentShader;

        /// <summary>
        /// The geometry shader source code.
        /// </summary>
        public string GeometryShader;

        /// <summary>
        /// The tesselation shader source code.
        /// </summary>
        public string TesselationShader;

        /// <summary>
        /// The vertex shader source code.
        /// </summary>
        public string VertexShader;
    }
}
