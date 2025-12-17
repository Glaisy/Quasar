//-----------------------------------------------------------------------
// <copyright file="IProfilerDataProvider.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Diagnostics.Profiler
{
    /// <summary>
    /// Represents the Quasar profiler data provider component.
    /// </summary>
    public interface IProfilerDataProvider
    {
        /// <summary>
        /// Gets the profiler data.
        /// </summary>
        IProfilerData ProfilerData { get; }
    }
}
