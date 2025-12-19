//-----------------------------------------------------------------------
// <copyright file="RenderingLayer.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;

using Quasar.Graphics.Internals;

namespace Quasar.Rendering.Internals
{
    /// <summary>
    /// Rendering layer object implementation.
    /// </summary>
    internal sealed partial class RenderingLayer
    {
        private readonly Dictionary<int, RenderBatch> renderBatches = new Dictionary<int, RenderBatch>();


        /// <summary>
        /// Initializes a new instance of the <see cref="RenderingLayer" /> class.
        /// </summary>
        /// <param name="layer">The layer.</param>
        public RenderingLayer(Layer layer)
        {
            Layer = layer;
            LayerMask = (LayerMask)(1 << (int)layer);
        }


        /// <summary>
        /// The layer.
        /// </summary>
        public readonly Layer Layer;

        /// <summary>
        /// The layer mask.
        /// </summary>
        public readonly LayerMask LayerMask;


        /// <summary>
        /// Clears the layer.
        /// </summary>
        public void Clear()
        {
            foreach (var renderBatch in renderBatches.Values)
            {
                renderBatch.Clear();
            }

            renderBatches.Clear();
        }

        /// <summary>
        /// Gets the render batch enumerator.
        /// </summary>
        public Dictionary<int, RenderBatch>.ValueCollection.Enumerator GetEnumerator()
        {
            return renderBatches.Values.GetEnumerator();
        }

        /// <summary>
        /// Gets the render batch by th specified shader.
        /// </summary>
        /// <param name="shader">The shader.</param>
        public RenderBatch GetRenderBatch(ShaderBase shader)
        {
            if (!renderBatches.TryGetValue(shader.Handle, out var renderBatch))
            {
                renderBatch = new RenderBatch(shader);
                renderBatches.Add(shader.Handle, renderBatch);
            }

            return renderBatch;
        }
    }
}
