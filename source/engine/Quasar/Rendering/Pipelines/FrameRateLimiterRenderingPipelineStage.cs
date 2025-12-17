//-----------------------------------------------------------------------
// <copyright file="FrameRateLimiterRenderingPipelineStage.cs" company="Space Development">
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
using Quasar.Rendering.Profiler.Internals;

using Space.Core.DependencyInjection;

namespace Quasar.Rendering.Pipelines
{
    /// <summary>
    /// Rendering pipeline stage which determines the update/rendering frame rates.
    /// </summary>
    /// <seealso cref="RenderingPipelineStageBase" />
    [Export(typeof(RenderingPipelineStageBase), nameof(FrameRateLimiterRenderingPipelineStage))]
    [ExecuteAfter(typeof(FrameBufferSwapperRenderingPipelineStage))]
    public sealed class FrameRateLimiterRenderingPipelineStage : RenderingPipelineStageBase
    {
        private readonly IRenderingProfiler renderingProfiler;
        private DateTime lastFrameEndTime;
        private TimeSpan expectedFrameTime;
        private bool limitFrameRate;


        /// <summary>
        /// Initializes a new instance of the <see cref="FrameRateLimiterRenderingPipelineStage"/> class.
        /// </summary>
        /// <param name="renderingProfiler">The rendering profiler.</param>
        internal FrameRateLimiterRenderingPipelineStage(IRenderingProfiler renderingProfiler)
        {
            this.renderingProfiler = renderingProfiler;
        }


        /// <inheritdoc/>
        protected override void OnApplySettings(IRenderingSettings renderingSettings)
        {
            Context.CommandProcessor.SetVSyncMode(renderingSettings.VSyncMode);
            limitFrameRate = renderingSettings.VSyncMode == VSyncMode.Off && renderingSettings.FPSLimit > 0;
            if (limitFrameRate)
            {
                expectedFrameTime = TimeSpan.FromSeconds(1.0 / renderingSettings.FPSLimit);
            }
        }

        /// <inheritdoc/>
        protected override void OnStart()
        {
            lastFrameEndTime = DateTime.UtcNow;
        }

        /// <inheritdoc/>
        protected override void OnExecute()
        {
            if (!limitFrameRate)
            {
                return;
            }

            var frameTime = DateTime.UtcNow.Subtract(lastFrameEndTime);
            var waitTimeTicks = expectedFrameTime.Ticks - frameTime.Ticks;
            if (waitTimeTicks > 0)
            {
                var waitingTime = TimeSpan.FromTicks(waitTimeTicks);
                renderingProfiler.UpdateWaitingTime((float)waitingTime.TotalSeconds);

                Thread.Sleep(waitingTime);
            }

            lastFrameEndTime = DateTime.UtcNow;
        }
    }
}
