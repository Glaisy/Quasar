//-----------------------------------------------------------------------
// <copyright file="RenderingProfiler.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Diagnostics;

using Quasar.Diagnostics.Profiler;

using Space.Core.DependencyInjection;

namespace Quasar.Rendering.Profiler.Internals
{
    /// <summary>
    /// Render pipeline's profiler implementation.
    /// </summary>
    /// <seealso cref="IRenderingProfiler" />
    /// <seealso cref="IProfilerDataProvider{IRenderingStatistics}" />
    [Export(typeof(IRenderingProfiler))]
    [Export(typeof(IProfilerDataProvider<IRenderingStatistics>))]
    [Singleton]
    internal sealed class RenderingProfiler : IRenderingProfiler, IProfilerDataProvider<IRenderingStatistics>
    {
        private const float fpsFilteringRate = 5;
        private readonly Stopwatch stopwatch = new Stopwatch();
        private readonly RenderFrameStatistics frameStatistics = new RenderFrameStatistics();
        private readonly RenderingStatistics statistics = new RenderingStatistics();


        /// <inheritdoc/>
        IRenderingStatistics IProfilerDataProvider<IRenderingStatistics>.Get()
        {
            return statistics;
        }


        /// <inheritdoc/>
        public void IncrementDrawCalls(int increment = 1)
        {
            frameStatistics.DrawCalls += increment;
        }


        /// <inheritdoc/>
        public void BeginFrame()
        {
            frameStatistics.Clear();
            stopwatch.Restart();
        }

        /// <inheritdoc/>
        public void EndFrame()
        {
            frameStatistics.FrameTime = (float)stopwatch.Elapsed.TotalSeconds;
            frameStatistics.RenderingTime = frameStatistics.FrameTime - frameStatistics.WaitingTime;

            var fps = frameStatistics.FrameTime > 0.0f ? 1.0f / frameStatistics.FrameTime : 0.0f;
            statistics.FramesPerSecond = (fpsFilteringRate * statistics.FramesPerSecond + fps) / (1.0f + fpsFilteringRate);

            statistics.FrameStatistics.CopyFrom(frameStatistics);
            frameStatistics.Clear();
        }

        /// <inheritdoc/>
        public void UpdateWaitingTime(float value)
        {
            frameStatistics.WaitingTime = value;
        }
    }
}
