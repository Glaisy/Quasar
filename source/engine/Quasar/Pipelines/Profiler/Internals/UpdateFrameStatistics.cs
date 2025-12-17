//-----------------------------------------------------------------------
// <copyright file="UpdateFrameStatistics.cs" company="Space Development">
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
    /// Update frame statistics data object implementation.
    /// </summary>
    /// <seealso cref="IUpdateFrameStatistics" />
    internal sealed class UpdateFrameStatistics : IUpdateFrameStatistics
    {
        /// <summary>
        /// Gets or sets the frame time [s].
        /// </summary>
        public float FrameTime { get; set; }

        /// <summary>
        /// Copies the statistics values from the source.
        /// </summary>
        /// <param name="source">The source.</param>
        public void CopyFrom(IUpdateFrameStatistics source)
        {
            FrameTime = source.FrameTime;
        }

        /// <summary>
        /// Clears the statistics values.
        /// </summary>
        public void Clear()
        {
            FrameTime = 0.0f;
        }
    }
}
