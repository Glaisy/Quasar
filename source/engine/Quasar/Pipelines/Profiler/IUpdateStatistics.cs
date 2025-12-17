//-----------------------------------------------------------------------
// <copyright file="IUpdateStatistics.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Pipelines.Profiler
{
    /// <summary>
    /// Represents the statistics data for the updating pipeline's profiler.
    /// </summary>
    public interface IUpdateStatistics
    {
        /// <summary>
        /// Gets the most recent update frame's statistics.
        /// </summary>
        IUpdateFrameStatistics FrameStatistics { get; }
    }
}
