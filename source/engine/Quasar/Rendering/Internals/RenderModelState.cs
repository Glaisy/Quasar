//-----------------------------------------------------------------------
// <copyright file="RenderModelState.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.Rendering.Internals;

namespace Quasar.Rendering.Processors.Internals
{
    /// <summary>
    /// Render model object's processed state.
    /// </summary>
    internal struct RenderModelState
    {
        /// <summary>
        /// The double sided flag.
        /// </summary>
        public bool IsDoubleSided;

        /// <summary>
        /// The enabled flag.
        /// </summary>
        public bool IsEnabled;

        /// <summary>
        /// The renderable flag.
        /// </summary>
        public bool IsRenderable;

        /// <summary>
        /// The mesh.
        /// </summary>
        public IMesh Mesh;

        /// <summary>
        /// The render batch.
        /// </summary>
        public RenderBatch RenderBatch;

        /// <summary>
        /// The render layer.
        /// </summary>
        public RenderingLayer RenderingLayer;

        /// <summary>
        /// The shader.
        /// </summary>
        public ShaderBase Shader;

        /// <summary>
        /// The shared mesh flag.
        /// </summary>
        public bool SharedMesh;
    }
}
