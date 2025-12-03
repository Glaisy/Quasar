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


        /// <summary>
        /// Initializes a new instance of the <see cref="InitializeRenderingPipelineStage" /> class.
        /// </summary>
        /// <param name="applicationWindow">The application window.</param>
        internal InitializeRenderingPipelineStage(IApplicationWindow applicationWindow)
        {
            this.applicationWindow = applicationWindow;
        }


        /// <inheritdoc/>
        protected override void OnExecute(IRenderingContext renderingContext)
        {
            renderingContext.CommandProcessor.ResetState();
        }


        /// <inheritdoc/>
        protected override void OnApplySettings(IRenderingSettings settings)
        {
            applicationWindow.FullscreenMode = settings.FullScreenMode;
        }
    }
}
