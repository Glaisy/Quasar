//-----------------------------------------------------------------------
// <copyright file="UpdateProfiler.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Diagnostics.Profiler;

using Space.Core;
using Space.Core.DependencyInjection;

namespace Quasar.Pipelines.Profiler.Internals
{
    /// <summary>
    /// Update pipeline's profiler implementation.
    /// </summary>
    /// <seealso cref="IUpdateProfiler" />
    /// <seealso cref="IProfilerDataProvider{IUpdateStatistics}" />
    [Export(typeof(IUpdateProfiler))]
    [Export(typeof(IProfilerDataProvider<IUpdateStatistics>))]
    [Singleton]
    internal class UpdateProfiler : IUpdateProfiler, IProfilerDataProvider<IUpdateStatistics>
    {
        private readonly UpdateFrameStatistics frameStatistics = new UpdateFrameStatistics();
        private readonly UpdateStatistics statistics = new UpdateStatistics();


        /// <inheritdoc/>
        IUpdateStatistics IProfilerDataProvider<IUpdateStatistics>.Get()
        {
            return statistics;
        }


        /// <inheritdoc/>
        public void UpdateFrameTimesAndStatistics(float updateTime)
        {
            Assertion.ThrowIfNegative(updateTime, nameof(updateTime));

            frameStatistics.FrameTime = updateTime;

            statistics.FrameStatistics.CopyFrom(frameStatistics);
            frameStatistics.Clear();
        }
    }
}
