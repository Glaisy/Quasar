//-----------------------------------------------------------------------
// <copyright file="RenderFrameStatistics.cs" company="Space Development">
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
    /// Render frame statistics object implementation.
    /// </summary>
    /// <seealso cref="IRenderFrameStatistics" />
    internal sealed class RenderFrameStatistics : IRenderFrameStatistics
    {
        /// <inheritdoc/>
        public int DrawCalls { get; set; }

        /// <inheritdoc/>
        public float FrameTime { get; set; }

        /// <inheritdoc/>
        public float RenderingTime { get; set; }

        /// <inheritdoc/>
        public float WaitingTime { get; set; }


        /// <summary>
        /// Copies the statistics values from the source.
        /// </summary>
        /// <param name="source">The source.</param>
        public void CopyFrom(IRenderFrameStatistics source)
        {
            DrawCalls = source.DrawCalls;
            FrameTime = source.FrameTime;
            RenderingTime = source.RenderingTime;
            WaitingTime = source.WaitingTime;
        }

        /// <summary>
        /// Clears the statistics values.
        /// </summary>
        public void Clear()
        {
            DrawCalls = 0;
            FrameTime = RenderingTime = WaitingTime = 0.0f;
        }
    }
}
