//-----------------------------------------------------------------------
// <copyright file="InitializeRenderingPipelineStage.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;

using Quasar.Graphics.Internals;
using Quasar.UI;

using Space.Core.DependencyInjection;

namespace Quasar.Rendering.Pipeline
{
    /// <summary>
    /// Render pipeline's frame initialization stage implementation.
    /// </summary>
    /// <seealso cref="RenderingPipelineStageBase" />
    [Export(typeof(RenderingPipelineStageBase), nameof(InitializeRenderingPipelineStage))]
    public sealed class InitializeRenderingPipelineStage : RenderingPipelineStageBase
    {
        private readonly IApplicationWindow applicationWindow;
        private readonly IRenderingContext renderingContext;
        private bool isRenderingContextInitialized;


        /// <summary>
        /// Initializes a new instance of the <see cref="InitializeRenderingPipelineStage" /> class.
        /// </summary>
        /// <param name="applicationWindow">The application window.</param>
        /// <param name="renderingContext">The rendering context.</param>
        internal InitializeRenderingPipelineStage(
            IApplicationWindow applicationWindow,
            IRenderingContext renderingContext)
        {
            this.applicationWindow = applicationWindow;
            this.renderingContext = renderingContext;
        }


        /// <inheritdoc/>
        protected override void OnApplySettings(IRenderingSettings renderingSettings)
        {
            EnsureRenderingContextIsInitialized(renderingSettings);

            applicationWindow.FullscreenMode = renderingSettings.FullScreenMode;
        }

        /// <inheritdoc/>
        protected override void OnExecute(IRenderingContext renderingContext)
        {
            renderingContext.CommandProcessor.ResetState();
        }


        private void EnsureRenderingContextIsInitialized(IRenderingSettings renderingSettings)
        {
            if (isRenderingContextInitialized)
            {
                return;
            }

            var graphicsDeviceContextFactory = ServiceProvider.GetRequiredService<IGraphicsDeviceContextFactory>();
            var graphicsDeviceContext = graphicsDeviceContextFactory.Create(renderingSettings.Platform);

            var serviceLoader = ServiceProvider.GetRequiredService<IServiceLoader>();
            serviceLoader.AddSingleton(graphicsDeviceContext);

            graphicsDeviceContext.Initialize(applicationWindow);
            renderingContext.Initialize(graphicsDeviceContext);

            isRenderingContextInitialized = true;
        }
    }
}
