//-----------------------------------------------------------------------
// <copyright file="IProfilerDataProvider.Generic.cs" company="Space Development">
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
    /// Represents a generic profiler data provider.
    /// </summary>
    /// <typeparam name="T">The profiler data type.</typeparam>
    public interface IProfilerDataProvider<T>
    {
        /// <summary>
        /// Gets the most recent profiler data.
        /// </summary>
        T Get();
    }
}
