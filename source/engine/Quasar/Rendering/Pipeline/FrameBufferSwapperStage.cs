//-----------------------------------------------------------------------
// <copyright file="FrameBufferSwapperStage.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.UI;

namespace Quasar.Rendering.Pipeline
{
    /// <summary>
    /// Rendering pipeline stage which swaps the forward and back buffers.
    /// </summary>
    /// <seealso cref="RenderingPipelineStageBase" />
    internal sealed class FrameBufferSwapperStage : RenderingPipelineStageBase
    {
        private readonly IApplicationWindow applicationWindow;


        /// <summary>
        /// Initializes a new instance of the <see cref="FrameBufferSwapperStage"/> class.
        /// </summary>
        /// <param name="applicationWindow">The application window.</param>
        public FrameBufferSwapperStage(IApplicationWindow applicationWindow)
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
