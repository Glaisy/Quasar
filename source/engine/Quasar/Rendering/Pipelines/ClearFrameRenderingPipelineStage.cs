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

using Quasar.Graphics;
using Quasar.Pipelines;
using Quasar.UI;

using Space.Core.DependencyInjection;

namespace Quasar.Rendering.Pipelines
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


        /// <summary>
        /// Initializes a new instance of the <see cref="ClearFrameRenderingPipelineStage" /> class.
        /// </summary>
        /// <param name="applicationWindow">The application window.</param>
        internal ClearFrameRenderingPipelineStage(IApplicationWindow applicationWindow)
        {
            this.applicationWindow = applicationWindow;
        }


        /// <inheritdoc/>
        protected override void OnApplySettings(IRenderingSettings renderingSettings)
        {
            applicationWindow.FullscreenMode = renderingSettings.FullScreenMode;
        }

        /// <inheritdoc/>
        protected override void OnExecute()
        {
            Context.PrimaryFrameBuffer.Clear(Color.Black, true);
        }

        /// <inheritdoc/>
        protected override void OnSizeChanged(in Size size)
        {
            Context.CommandProcessor.SetViewport(Point.Empty, size);
        }
    }
}
