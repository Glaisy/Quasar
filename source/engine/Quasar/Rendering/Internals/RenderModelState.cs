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
using Quasar.Rendering.Internals;

namespace Quasar.Rendering.Processors.Internals
{
    /// <summary>
    /// Render model object's processed state.
    /// </summary>
    internal struct RenderModelState
    {
        /// <summary>
        /// The flags.
        /// </summary>
        public RenderModelStateFlags Flags;

        /// <summary>
        /// The material.
        /// </summary>
        public Material Material;

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
        /// Gets a value indicating whether the model is active.
        /// </summary>
        public bool IsActive => RenderBatch != null && Flags.HasFlag(RenderModelStateFlags.Enabled | RenderModelStateFlags.Renderable);

        /// <summary>
        /// Gets a value indicating whether the model is enabled.
        /// </summary>
        public bool IsEnabled => Flags.HasFlag(RenderModelStateFlags.Enabled);

        /// <summary>
        /// Gets a value indicating whether the model is double sided.
        /// </summary>
        public bool IsDoubleSided => Flags.HasFlag(RenderModelStateFlags.DoubleSided);

        /// <summary>
        /// Gets a value indicating whether the model is renderable.
        /// </summary>
        public bool IsRenderable => Flags.HasFlag(RenderModelStateFlags.Renderable);

        /// <summary>
        /// Gets a value indicating whether this model is renderable by properties.
        /// </summary>
        public bool IsRenderableByProperties => Mesh != null && Material != null;
    }
}
