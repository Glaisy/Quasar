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

using System;

using Quasar.Collections;
using Quasar.Graphics;
using Quasar.Pipelines;
using Quasar.Rendering.Pipelines;
using Quasar.UI.Internals;
using Quasar.Utilities;

using Space.Core.DependencyInjection;

namespace Quasar.UI.Pipelines
{
    /// <summary>
    /// Render pipeline's UI visual element rendering stage implementation.
    /// </summary>
    /// <seealso cref="RenderingPipelineStageBase" />
    [Export(typeof(RenderingPipelineStageBase), nameof(UIRenderingPipelineStage))]
    [ExecuteAfter(typeof(ClearFrameRenderingPipelineStage))]
    [ExecuteBefore(typeof(FrameBufferSwapperRenderingPipelineStage))]
    public sealed class UIRenderingPipelineStage : RenderingPipelineStageBase
    {
        private readonly IApplicationWindow applicationWindow;
        private readonly UIElementRenderer uiElementRenderer;
        private readonly ActionBasedObserver<Size> applicationWindowSizeChangedObserver;
        private IDisposable applicationWindowSizeChangedSubscription;


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

            applicationWindowSizeChangedObserver = new ActionBasedObserver<Size>(OnApplicationWindowSizeChanged);
        }


        // TODO: remove this
        private readonly ValueTypeCollection<UIElement> uiElements = new ValueTypeCollection<UIElement>();


        /// <inheritdoc/>
        protected override void OnExecute()
        {
            uiElementRenderer.Render(Context.CommandProcessor, uiElements);
        }

        /// <inheritdoc/>
        protected override void OnStart()
        {
            uiElementRenderer.Initalize();

            applicationWindowSizeChangedSubscription =
                applicationWindow.SizeChanged.Subscribe(applicationWindowSizeChangedObserver);

            OnApplicationWindowSizeChanged(applicationWindow.Size);
        }

        /// <inheritdoc/>
        protected override void OnShutdown()
        {
            applicationWindowSizeChangedSubscription?.Dispose();
            applicationWindowSizeChangedSubscription = null;
        }


        private void OnApplicationWindowSizeChanged(Size size)
        {
            uiElementRenderer.Update(size);
        }
    }
}
