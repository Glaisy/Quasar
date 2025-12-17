//-----------------------------------------------------------------------
// <copyright file="RenderingStatistics.cs" company="Space Development">
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
    /// Statistics object for rendering pipeline's profiler.
    /// </summary>
    /// <seealso cref="IRenderingStatistics" />
    internal sealed class RenderingStatistics : IRenderingStatistics
    {
        /// <inheritdoc/>
        public float FramesPerSecond { get; set; }

        /// <summary>
        /// The most recent render frame's statistics.
        /// </summary>
        public readonly RenderFrameStatistics FrameStatistics = new RenderFrameStatistics();


        /// <inheritdoc/>
        IRenderFrameStatistics IRenderingStatistics.FrameStatistics => FrameStatistics;
    }
}
