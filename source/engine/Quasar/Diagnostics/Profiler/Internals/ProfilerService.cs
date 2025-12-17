//-----------------------------------------------------------------------
// <copyright file="ProfilerService.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Pipelines.Profiler;
using Quasar.Rendering.Profiler;

using Space.Core.DependencyInjection;

namespace Quasar.Diagnostics.Profiler.Internals
{
    /// <summary>
    /// Quasar's profiler service implementation.
    /// </summary>
    /// <seealso cref="IProfilerDataProvider" />
    [Export(typeof(IProfilerDataProvider))]
    [Export]
    [Singleton]
    internal sealed class ProfilerService : IProfilerDataProvider
    {
        private readonly ProfilerData profilerData = new ProfilerData();


        /// <summary>
        /// Initializes a new instance of the <see cref="ProfilerService" /> class.
        /// </summary>
        /// <param name="renderingStatisticsDataProvider">The rendering statistics data provider.</param>
        /// <param name="updateStatisticsDataProvider">The update statistics data provider.</param>
        public ProfilerService(
            IProfilerDataProvider<IRenderingStatistics> renderingStatisticsDataProvider,
            IProfilerDataProvider<IUpdateStatistics> updateStatisticsDataProvider)
        {
            profilerData.RenderingStatistics = renderingStatisticsDataProvider.Get();
            profilerData.UpdateStatistics = updateStatisticsDataProvider.Get();
        }


        /// <summary>
        /// Gets the profiler data.
        /// </summary>
        public IProfilerData ProfilerData => profilerData;
    }
}
