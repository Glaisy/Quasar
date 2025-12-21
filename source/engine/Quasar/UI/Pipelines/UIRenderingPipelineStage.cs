//-----------------------------------------------------------------------
// <copyright file="UIRenderingPipelineStage.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics;
using Quasar.Pipelines;
using Quasar.Rendering.Pipelines;
using Quasar.UI.Internals.Renderers;
using Quasar.UI.VisualElements;
using Quasar.UI.VisualElements.Internals;

using Space.Core.DependencyInjection;

namespace Quasar.UI.Pipelines
{
    /// <summary>
    /// Render pipeline's UI visual element rendering stage implementation.
    /// </summary>
    /// <seealso cref="RenderingPipelineStageBase" />
    [Export(typeof(RenderingPipelineStageBase), nameof(UIRenderingPipelineStage))]
    [ExecuteAfter(typeof(RenderBatchRenderPipelineStage))]
    [ExecuteBefore(typeof(FrameBufferSwapperRenderingPipelineStage))]
    public sealed class UIRenderingPipelineStage : RenderingPipelineStageBase
    {
        private readonly IApplicationWindow applicationWindow;
        private readonly UIElementRenderer uiElementRenderer;


        /// <summary>
        /// Initializes a new instance of the <see cref="UIRenderingPipelineStage" /> class.
        /// </summary>
        /// <param name="applicationWindow">The application window.</param>
        /// <param name="uiElementRenderer">The UI element renderer.</param>
        internal UIRenderingPipelineStage(
            IApplicationWindow applicationWindow,
            UIElementRenderer uiElementRenderer)
        {
            this.applicationWindow = applicationWindow;
            this.uiElementRenderer = uiElementRenderer;
        }


        private VisualElement testElement = new VisualElement();

        /// <inheritdoc/>
        protected override void OnExecute()
        {
            testElement.ProcessRenderEvent(Context);
        }

        /// <inheritdoc/>
        protected override void OnSizeChanged(in Size size)
        {
            uiElementRenderer.Update(size);
        }

        /// <inheritdoc/>
        protected override void OnStart()
        {
            // initialize internal components
            Canvas.InitializeStaticServices(ServiceProvider);
            VisualElement.InitializeStaticServices(ServiceProvider);
            uiElementRenderer.Initalize();
        }
    }
}
