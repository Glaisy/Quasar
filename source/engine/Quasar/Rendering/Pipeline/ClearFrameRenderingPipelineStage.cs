//-----------------------------------------------------------------------
// <copyright file="ClearFrameRenderingPipelineStage.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics.Internals.Factories;
using Quasar.Pipelines;

using Space.Core.DependencyInjection;

namespace Quasar.Rendering.Pipeline
{
    /// <summary>
    /// Render pipeline's clear stage implementation.
    /// </summary>
    /// <seealso cref="RenderingPipelineStageBase" />
    [Export(typeof(RenderingPipelineStageBase), nameof(ClearFrameRenderingPipelineStage))]
    [ExecuteAfter(typeof(InitializeRenderingPipelineStage))]
    public sealed class ClearFrameRenderingPipelineStage : RenderingPipelineStageBase
    {
        private readonly IShaderFactory shaderFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClearFrameRenderingPipelineStage"/> class.
        /// </summary>
        /// <param name="shaderFactory">The shader factory.</param>
        internal ClearFrameRenderingPipelineStage(IShaderFactory shaderFactory)
        {
            this.shaderFactory = shaderFactory;
        }


        /// <inheritdoc/>
        protected override void OnExecute(IRenderingContext renderingContext)
        {
            // TODO: implement rendering framebuffer size by the application window size
        }

        /// <summary>
        /// Start event handler.
        /// </summary>
        protected override void OnStart()
        {
            base.OnStart();

            var shaders = shaderFactory.LoadBuiltInShaders();
        }
    }
}
