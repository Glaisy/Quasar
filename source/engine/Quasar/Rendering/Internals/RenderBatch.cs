//-----------------------------------------------------------------------
// <copyright file="RenderBatch.cs" company="Space Development">
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
    /// Render batch object.
    /// </summary>
    internal sealed class RenderBatch
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderBatch" /> class.
        /// </summary>
        /// <param name="shader">The shader.</param>
        public RenderBatch(ShaderBase shader)
        {
            Shader = shader;
        }


        /// <summary>
        /// The double sided render model objects.
        /// </summary>
        public readonly HashSet<RenderModel> DoubleSidedModels = new HashSet<RenderModel>();

        /// <summary>
        /// The render model objects.
        /// </summary>
        public readonly HashSet<RenderModel> Models = new HashSet<RenderModel>();

        /// <summary>
        /// The shader.
        /// </summary>
        public readonly ShaderBase Shader;


        /// <summary>
        /// Adds the model to the batch.
        /// </summary>
        /// <param name="renderModel">The render model.</param>
        public void AddModel(RenderModel renderModel)
        {
            if (renderModel.State.IsDoubleSided)
            {
                DoubleSidedModels.Add(renderModel);
                return;
            }

            Models.Add(renderModel);
        }

        /// <summary>
        /// Removes all model from the batch.
        /// </summary>
        public void Clear()
        {
            foreach (var renderModel in DoubleSidedModels)
            {
                renderModel.State.IsRenderable = false;
                renderModel.State.RenderBatch = null;
            }

            foreach (var renderModel in Models)
            {
                renderModel.State.IsRenderable = false;
                renderModel.State.RenderBatch = null;
            }

            DoubleSidedModels.Clear();
            Models.Clear();
        }

        /// <summary>
        /// Moves the model by the double sided flag.
        /// </summary>
        /// <param name="renderModel">The render model.</param>
        public void MoveModel(RenderModel renderModel)
        {
            if (renderModel.State.IsDoubleSided)
            {
                Models.Remove(renderModel);
                DoubleSidedModels.Add(renderModel);
                return;
            }

            DoubleSidedModels.Remove(renderModel);
            Models.Add(renderModel);
        }

        /// <summary>
        /// Removes the model from the batch.
        /// </summary>
        /// <param name="renderModel">The render model.</param>
        public void RemoveModel(RenderModel renderModel)
        {
            if (renderModel.State.IsDoubleSided)
            {
                DoubleSidedModels.Remove(renderModel);
                return;
            }

            Models.Remove(renderModel);
        }
    }
}
