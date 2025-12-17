//-----------------------------------------------------------------------
// <copyright file="IUpdateFrameStatistics.cs" company="Space Development">
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
    /// Represents an update frame statistics information.
    /// </summary>
    public interface IUpdateFrameStatistics
    {
        /// <summary>
        /// Gets the frame time [s].
        /// </summary>
        float FrameTime { get; }
    }
}
