//-----------------------------------------------------------------------
// <copyright file="FrameBufferSwapperRenderingPipelineStage.cs" company="Space Development">
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
    /// Rendering pipeline stage which swaps the front and back buffers.
    /// </summary>
    /// <seealso cref="RenderingPipelineStageBase" />
    [Export(typeof(RenderingPipelineStageBase), nameof(FrameBufferSwapperRenderingPipelineStage))]
    public sealed class FrameBufferSwapperRenderingPipelineStage : RenderingPipelineStageBase
    {
        private readonly IApplicationWindow applicationWindow;


        /// <summary>
        /// Initializes a new instance of the <see cref="FrameBufferSwapperRenderingPipelineStage"/> class.
        /// </summary>
        /// <param name="applicationWindow">The application window.</param>
        internal FrameBufferSwapperRenderingPipelineStage(IApplicationWindow applicationWindow)
        {
            this.applicationWindow = applicationWindow;
        }


        /// <inheritdoc/>
        protected override void OnExecute()
        {
            applicationWindow.SwapBuffers();
        }
    }
}
