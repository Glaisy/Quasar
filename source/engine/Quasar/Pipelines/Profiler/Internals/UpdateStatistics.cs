//-----------------------------------------------------------------------
// <copyright file="UpdateStatistics.cs" company="Space Development">
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
    /// Update statistics data implementation.
    /// </summary>
    /// <seealso cref="IUpdateStatistics" />
    internal sealed class UpdateStatistics : IUpdateStatistics
    {
        /// <summary>
        /// The most recent update frame's statistics.
        /// </summary>
        public readonly UpdateFrameStatistics FrameStatistics = new UpdateFrameStatistics();

        /// <summary>
        /// Gets the last update frame's statistics.
        /// </summary>
        IUpdateFrameStatistics IUpdateStatistics.FrameStatistics => FrameStatistics;
    }
}
