//-----------------------------------------------------------------------
// <copyright file="FrameLimiterRenderingPipelineStage.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Threading;

using Quasar.Graphics;
using Quasar.Pipelines;

using Space.Core.DependencyInjection;

namespace Quasar.Rendering.Pipelines
{
    /// <summary>
    /// Rendering pipeline stage which swaps the front and back buffers.
    /// </summary>
    /// <seealso cref="RenderingPipelineStageBase" />
    [Export(typeof(RenderingPipelineStageBase), nameof(FrameLimiterRenderingPipelineStage))]
    [ExecuteAfter(typeof(FrameBufferSwapperRenderingPipelineStage))]
    public sealed class FrameLimiterRenderingPipelineStage : RenderingPipelineStageBase
    {
        private DateTime lastFrameEndTime;
        private TimeSpan expectedFrameTime;


        /// <inheritdoc/>
        protected override void OnApplySettings(IRenderingSettings renderingSettings)
        {
            Context.CommandProcessor.SetVSyncMode(renderingSettings.VSyncMode);
            expectedFrameTime = renderingSettings.VSyncMode == VSyncMode.On || renderingSettings.FPSLimit == 0
                ? TimeSpan.Zero
                : TimeSpan.FromSeconds(1.0 / renderingSettings.FPSLimit);
        }

        /// <inheritdoc/>
        protected override void OnStart()
        {
            lastFrameEndTime = DateTime.UtcNow;
        }

        /// <inheritdoc/>
        protected override void OnExecute()
        {
            if (expectedFrameTime.Ticks > 0)
            {
                var frameTime = DateTime.UtcNow.Subtract(lastFrameEndTime);
                var waitTimeTicks = expectedFrameTime.Ticks - frameTime.Ticks;
                if (waitTimeTicks > 0)
                {
                    Thread.Sleep(TimeSpan.FromTicks(waitTimeTicks));
                }
            }

            lastFrameEndTime = DateTime.UtcNow;
        }
    }
}
