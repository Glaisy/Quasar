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
        /// Adds the render model to this layer.
        /// </summary>
        /// <param name="renderModel">The render model.</param>
        public void AddModel(RenderModel renderModel)
        {
            AddModelInternal(renderModel.State.Shader, renderModel);
        }

        /// <summary>
        /// Clears  the layer.
        /// </summary>
        public void Clear()
        {
            ////    foreach (var renderBatch in renderBatches.Values)
            ////    {
            ////        foreach (var renderModel in renderBatch.Models)
            ////        {
            ////            renderModel.State.Activated = false;
            ////        }

            ////        foreach (var renderModel in renderBatch.DoubleSidedModels)
            ////        {
            ////            renderModel.State.Activated = false;
            ////        }

            ////        renderBatch.Models.Clear();
            ////        renderBatch.DoubleSidedModels.Clear();
            ////    }
        }

        /// <summary>
        /// Gets the render batch enumerator.
        /// </summary>
        public Dictionary<int, RenderBatch>.ValueCollection.Enumerator GetEnumerator()
        {
            return renderBatches.Values.GetEnumerator();
        }

        /////// <summary>
        /////// Gets the render batch for the render model.
        /////// </summary>
        /////// <param name="renderModel">The render model.</param>
        ////public RenderBatch GetBatch(RenderModel renderModel)
        ////{
        ////    var shader = renderModel.State.Shader;
        ////    renderBatches.TryGetValue(shader.Handle, out var renderBatch);
        ////    return renderBatch;
        ////}

        /////// <summary>
        /////// Moves the render model between render batches.
        /////// </summary>
        /////// <param name="shader">The shader.</param>
        /////// <param name="renderModel">The render model.</param>
        ////public void MoveModel(ShaderBase shader, RenderModel renderModel)
        ////{
        ////    RemoveModelInternal(renderModel.State.Shader, renderModel);
        ////    AddModelInternal(shader, renderModel);
        ////}

        /////// <summary>
        /////// Removes the render model from this layer.
        /////// </summary>
        /////// <param name="renderModel">The render model.</param>
        ////public void RemoveModel(RenderModel renderModel)
        ////{
        ////    RemoveModelInternal(renderModel.State.Shader, renderModel);
        ////}

        /////// <summary>
        /////// Updates the double sided state for the render model.
        /////// </summary>
        /////// <param name="renderModel">The render model.</param>
        /////// <param name="doubleSided">The double sided flag.</param>
        ////public void UpdateDoubleSidedState(RenderModel renderModel, bool doubleSided)
        ////{
        ////    var shader = renderModel.State.Shader;
        ////    var renderBatch = renderBatches[shader.Handle];
        ////    if (doubleSided)
        ////    {
        ////        renderBatch.Models.Remove(renderModel);
        ////        renderBatch.DoubleSidedModels.Add(renderModel);
        ////    }
        ////    else
        ////    {
        ////        renderBatch.DoubleSidedModels.Remove(renderModel);
        ////        renderBatch.Models.Add(renderModel);
        ////    }
        ////}


        private void AddModelInternal(ShaderBase shader, RenderModel renderModel)
        {
            if (!renderBatches.TryGetValue(shader.Handle, out var renderBatch))
            {
                renderBatch = new RenderBatch(shader);
                renderBatches.Add(shader.Handle, renderBatch);
            }

            if (renderModel.State.DoubleSided)
            {
                renderBatch.DoubleSidedModels.Add(renderModel);
            }
            else
            {
                renderBatch.Models.Add(renderModel);
            }
        }

        ////private void RemoveModelInternal(ShaderBase shader, RenderModel renderModel)
        ////{
        ////    if (!renderBatches.TryGetValue(shader.Handle, out var renderBatch))
        ////    {
        ////        return;
        ////    }

        ////    if (renderModel.State.DoubleSided)
        ////    {
        ////        renderBatch.DoubleSidedModels.Remove(renderModel);
        ////    }
        ////    else
        ////    {
        ////        renderBatch.Models.Remove(renderModel);
        ////    }
        ////}
    }
}
