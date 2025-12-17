//-----------------------------------------------------------------------
// <copyright file="IRenderFrameStatistics.cs" company="Space Development">
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
    /// Represents a render frame statistics information.
    /// </summary>
    public interface IRenderFrameStatistics
    {
        /// <summary>
        /// Gets the number of draw calls.
        /// </summary>
        int DrawCalls { get; }

        /// <summary>
        /// Gets the total frame time [s].
        /// </summary>
        float FrameTime { get; }

        /// <summary>
        /// Gets the rendering time [s].
        /// </summary>
        float RenderingTime { get; }

        /// <summary>
        /// Gets the waiting time [s].
        /// </summary>
        float WaitingTime { get; }
    }
}
