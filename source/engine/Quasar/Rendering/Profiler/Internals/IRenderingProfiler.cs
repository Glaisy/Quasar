//-----------------------------------------------------------------------
// <copyright file="IRenderingProfiler.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Rendering.Profiler.Internals
{
    /// <summary>
    /// Represetns the rendering pipeline's internal data profiler.
    /// </summary>
    internal interface IRenderingProfiler
    {
        /// <summary>
        /// Increments the draw call counter.
        /// </summary>
        /// <param name="increment">The increment value.</param>
        void IncrementDrawCalls(int increment = 1);

        /// <summary>
        /// Initializes the rendering frame related counters.
        /// </summary>
        void BeginFrame();

        /// <summary>
        /// Stops the rendering frame counters and updates the statistics.
        /// </summary>
        void EndFrame();

        /// <summary>
        /// Updates the waiting time [s].
        /// </summary>
        /// <param name="value">The value.</param>
        void UpdateWaitingTime(float value);
    }
}
