//-----------------------------------------------------------------------
// <copyright file="DebugRenderingPipelineStage.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

#if DEBUG
using Quasar.Pipelines;
using Quasar.Rendering.Pipelines;
using Quasar.UI.Internals;
using Quasar.UI.Pipelines;

using Space.Core.DependencyInjection;

namespace Quasar.Diagnostics.Pipeline.Internals
{
    /// <summary>
    /// Debug text rendering pipeline stage implementation.
    /// </summary>
    /// <seealso cref="RenderingPipelineStageBase" />
    [Export(typeof(RenderingPipelineStageBase), nameof(DebugRenderingPipelineStage))]
    [Singleton]
    [ExecuteAfter(typeof(UIRenderingPipelineStage))]
    [ExecuteBefore(typeof(FrameBufferSwapperRenderingPipelineStage))]
    public sealed class DebugRenderingPipelineStage : RenderingPipelineStageBase
    {
        private UIElementRenderer uiElementRenderer;
        private DebugTextService debugTextService;


        /// <summary>
        /// Initializes a new instance of the <see cref="DebugRenderingPipelineStage" /> class.
        /// </summary>
        /// <param name="uiElementRenderer">The UI element renderer.</param>
        /// <param name="debugTextService">The debug text service.</param>
        internal DebugRenderingPipelineStage(
            UIElementRenderer uiElementRenderer,
            DebugTextService debugTextService)
        {
            this.uiElementRenderer = uiElementRenderer;
            this.debugTextService = debugTextService;
        }


        /// <inheritdoc/>
        protected override void OnExecute()
        {
            var elements = debugTextService.GetUIElements();
            if (elements.Count == 0)
            {
                return;
            }

            uiElementRenderer.Render(Context.CommandProcessor, elements);
        }
    }
}
#endif