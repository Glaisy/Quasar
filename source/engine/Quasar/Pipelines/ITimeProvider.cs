//-----------------------------------------------------------------------
// <copyright file="ITimeProvider.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Pipelines
{
    /// <summary>
    /// Represents a provider to access the internal time counters.
    /// </summary>
    public interface ITimeProvider
    {
        /// <summary>
        /// Gets the delta time since last update [s].
        /// </summary>
        float DeltaTime { get; }

        /// <summary>
        /// Gets the delta time since last fixed update [s].
        /// </summary>
        float FixedDeltaTime { get; }

        /// <summary>
        /// Gets the time since application start [s].
        /// </summary>
        float Time { get; }
    }
}
