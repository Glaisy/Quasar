//-----------------------------------------------------------------------
// <copyright file="RenderingLayerService.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Space.Core.DependencyInjection;

namespace Quasar.Rendering.Internals.Services
{
    /// <summary>
    /// Rendering layer service implementation.
    /// </summary>
    [Export]
    [Singleton]
    internal sealed class RenderingLayerService
    {
        private readonly RenderingLayer[] renderingLayers = new RenderingLayer[(int)Layer.Last + 1];


        /// <summary>
        /// Initializes a new instance of the <see cref="RenderingLayerService" /> class.
        /// </summary>
        public RenderingLayerService()
        {
            for (var i = 0; i < renderingLayers.Length; i++)
            {
                renderingLayers[i] = new RenderingLayer((Layer)i);
            }
        }


        /// <summary>
        /// Gets the <see cref="RenderingLayer"/> with the specified layer.
        /// </summary>
        /// <param name="layer">The layer.</param>
        public RenderingLayer this[Layer layer] => renderingLayers[(int)layer];

        /// <summary>
        /// Gets the <see cref="RenderingLayer" /> with the specified layer index.
        /// </summary>
        /// <param name="layerIndex">The layer index.</param>
        public RenderingLayer this[int layerIndex] => renderingLayers[layerIndex];


        /// <summary>
        /// Clears rendermodel on all layers.
        /// </summary>
        public void Clear()
        {
            foreach (var renderingLayer in renderingLayers)
            {
                renderingLayer.Clear();
            }
        }

        /// <summary>
        /// Gets a rendering layer enumerator by the specified mask.
        /// </summary>
        /// <param name="layerMask">The layer mask.</param>
        public RenderingLayer.Enumerator GetEnumerator(LayerMask layerMask)
        {
            return new RenderingLayer.Enumerator(renderingLayers, layerMask);
        }
    }
}
