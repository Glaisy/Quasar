//-----------------------------------------------------------------------
// <copyright file="ProfilerData.cs" company="Space Development">
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

namespace Quasar.Diagnostics.Profiler.Internals
{
    /// <summary>
    /// Quasar profiler data object implementation.
    /// </summary>
    /// <seealso cref="IProfilerData" />
    internal sealed class ProfilerData : IProfilerData
    {
        /// <summary>
        /// Gets or sets the rendering pipeline's statistics.
        /// </summary>
        public IRenderingStatistics RenderingStatistics { get; set; }

        /// <summary>
        /// Gets or sets the update pipeline's statistics.
        /// </summary>
        public IUpdateStatistics UpdateStatistics { get; set; }
    }
}
