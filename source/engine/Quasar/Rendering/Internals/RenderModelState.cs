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

using System.Runtime.CompilerServices;

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
        /// Gets a value indicating whether the model is enabled.
        /// </summary>
        public bool IsEnabled
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Flags.HasFlag(RenderModelStateFlags.Enabled);
        }

        /// <summary>
        /// Gets a value indicating whether the model is double sided.
        /// </summary>
        public bool IsDoubleSided
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Flags.HasFlag(RenderModelStateFlags.DoubleSided);
        }

        /// <summary>
        /// Gets a value indicating whether the model is rendered.
        /// </summary>
        public bool IsRendered
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Flags.HasFlag(RenderModelStateFlags.Rendered);
        }

        /// <summary>
        /// Gets a value indicating whether this model is renderable by properties.
        /// </summary>
        public bool IsRenderableByProperties
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Mesh != null && Material != null && Flags.HasFlag(RenderModelStateFlags.Enabled);
        }


        /// <summary>
        /// Updates the specified flags by the "set" value.
        /// </summary>
        /// <param name="flags">The flags.</param>
        /// <param name="set">The set flag. If true the flags will be set; otherwise they will be cleared.</param>
        public void UpdateFlags(RenderModelStateFlags flags, bool set)
        {
            if (set)
            {
                Flags |= flags;
                return;
            }

            Flags &= ~flags;
        }
    }
}
