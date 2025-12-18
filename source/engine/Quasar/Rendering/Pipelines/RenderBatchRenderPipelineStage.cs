//-----------------------------------------------------------------------
// <copyright file="RenderBatchRenderPipelineStage.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Pipelines;
using Quasar.Rendering.Internals;
using Quasar.Rendering.Internals.Renderers;
using Quasar.Rendering.Internals.Services;

using Space.Core.DependencyInjection;

namespace Quasar.Rendering.Pipelines
{
    /// <summary>
    /// RenderBatch processing rendering pipeline stage implementation.
    /// </summary>
    /// <seealso cref="RenderingPipelineStageBase"/>
    [Export(typeof(RenderingPipelineStageBase), nameof(RenderBatchRenderPipelineStage))]
    [Singleton]
    [ExecuteAfter(typeof(CommandProcessorRenderPipelineStage))]
    [ExecuteBefore(typeof(FrameBufferSwapperRenderingPipelineStage))]
    public sealed class RenderBatchRenderPipelineStage : RenderingPipelineStageBase
    {
        private readonly CameraService cameraService;
        private readonly RenderBatchRenderer renderBatchRenderer;
        private readonly RenderingLayerService renderingLayerService;


        /// <summary>
        /// Initializes a new instance of the <see cref="RenderBatchRenderPipelineStage" /> class.
        /// </summary>
        /// <param name="cameraService">The camera service.</param>
        /// <param name="renderBatchRenderer">The render batch rendererer.</param>
        /// <param name="renderingLayerService">The rendering layer service.</param>
        internal RenderBatchRenderPipelineStage(
            CameraService cameraService,
            RenderBatchRenderer renderBatchRenderer,
            RenderingLayerService renderingLayerService)
        {
            this.renderBatchRenderer = renderBatchRenderer;
            this.cameraService = cameraService;
            this.renderingLayerService = renderingLayerService;
        }


        /// <summary>
        /// Execution event handler.
        /// </summary>
        protected override void OnExecute()
        {
            var cameraEnumerator = cameraService.GetEnumerator();
            while (cameraEnumerator.MoveNext())
            {
                var camera = cameraEnumerator.Current;
                var renderingView = new RenderingView(camera);

                camera.FrameBuffer.Activate();

                var renderingLayerEnumerator = renderingLayerService.GetEnumerator(camera.LayerMask);
                while (renderingLayerEnumerator.MoveNext())
                {
                    var renderingLayer = renderingLayerEnumerator.Current;
                    foreach (var renderBatch in renderingLayer)
                    {
                        if (renderBatch.DoubleSidedModels.Count + renderBatch.Models.Count == 0)
                        {
                            continue;
                        }

                        renderBatchRenderer.Activate(renderingView, renderBatch.Shader);
                        renderBatchRenderer.Render(Context, renderingView, renderBatch);
                        renderBatchRenderer.Deactivate();
                    }
                }
            }
        }
    }
}
