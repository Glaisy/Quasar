//-----------------------------------------------------------------------
// <copyright file="IRenderingStatistics.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Rendering.Profiler
{
    /// <summary>
    /// Represents the statistics data for the rendering pipeline's profiler.
    /// </summary>
    public interface IRenderingStatistics
    {
        /// <summary>
        /// Gets the number of rendered frames per second.
        /// </summary>
        float FramesPerSecond { get; }

        /// <summary>
        /// Gets the most recent render frame's statistics.
        /// </summary>
        IRenderFrameStatistics FrameStatistics { get; }
    }
}
