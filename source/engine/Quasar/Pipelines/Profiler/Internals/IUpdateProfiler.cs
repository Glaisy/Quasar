//-----------------------------------------------------------------------
// <copyright file="IUpdateProfiler.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Pipelines.Profiler.Internals
{
    /// <summary>
    /// Represetns the update pipeline's internal data profiler.
    /// </summary>
    internal interface IUpdateProfiler
    {
        /// <summary>
        /// Updates the frame time and statistics.
        /// </summary>
        /// <param name="updateTime">The update time [s].</param>
        void UpdateFrameTimesAndStatistics(float updateTime);
    }
}
