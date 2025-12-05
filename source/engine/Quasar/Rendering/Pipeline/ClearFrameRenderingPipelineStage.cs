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

using System;

using Quasar.Graphics;
using Quasar.Pipelines;
using Quasar.UI;
using Quasar.Utilities;

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
        private readonly IApplicationWindow applicationWindow;
        private readonly IRenderingContext renderingContext;
        private readonly ActionBasedObserver<Size> applicationWindowSizeChangedObserver;
        private IDisposable applicationWindowSizeChangedSubscription;


        /// <summary>
        /// Initializes a new instance of the <see cref="ClearFrameRenderingPipelineStage" /> class.
        /// </summary>
        /// <param name="applicationWindow">The application window.</param>
        /// <param name="renderingContext">The rendering context.</param>
        public ClearFrameRenderingPipelineStage(
            IApplicationWindow applicationWindow,
            IRenderingContext renderingContext)
        {
            this.applicationWindow = applicationWindow;
            this.renderingContext = renderingContext;

            applicationWindowSizeChangedObserver = new ActionBasedObserver<Size>(OnApplicationWindowSizeChanged);
        }


        /// <inheritdoc/>
        protected override void OnExecute()
        {
        }

        /// <inheritdoc/>
        protected override void OnStart()
        {
            applicationWindowSizeChangedSubscription =
                applicationWindow.SizeChanged.Subscribe(applicationWindowSizeChangedObserver);
        }

        /// <inheritdoc/>
        protected override void OnShutdown()
        {
            applicationWindowSizeChangedSubscription?.Dispose();
            applicationWindowSizeChangedSubscription = null;
        }


        private void OnApplicationWindowSizeChanged(Size size)
        {
            renderingContext.CommandProcessor.SetViewport(Point.Empty, size);
        }
    }
}
