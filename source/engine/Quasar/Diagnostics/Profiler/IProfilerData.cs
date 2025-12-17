//-----------------------------------------------------------------------
// <copyright file="IProfilerData.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Pipelines.Profiler;
using Quasar.Rendering.Profiler;

namespace Quasar.Diagnostics.Profiler
{
    /// <summary>
    /// Represents Quasar's profiler data object.
    /// </summary>
    public interface IProfilerData
    {
        /// <summary>
        /// Gets the rendering pipeline's statistics.
        /// </summary>
        IRenderingStatistics RenderingStatistics { get; }

        /// <summary>
        /// Gets the update pipeline's statistics.
        /// </summary>
        IUpdateStatistics UpdateStatistics { get; }
    }
}
