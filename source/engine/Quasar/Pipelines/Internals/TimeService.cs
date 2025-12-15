//-----------------------------------------------------------------------
// <copyright file="TimeService.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Space.Core;
using Space.Core.DependencyInjection;

namespace Quasar.Pipelines.Internals
{
    /// <summary>
    /// The internal time service implementation.
    /// </summary>
    /// <seealso cref="ITimeProvider" />
    [Export(typeof(ITimeProvider))]
    [Export]
    [Singleton]
    internal sealed class TimeService : ITimeProvider
    {
        /// <inheritdoc/>
        public float DeltaTime { get; private set; }

        /// <inheritdoc/>
        public float FixedDeltaTime { get; private set; }

        /// <inheritdoc/>
        public float Time { get; private set; }


        /// <summary>
        /// Increments the fixed time counter by the specified delta time.
        /// </summary>
        /// <param name="deltaTime">The delta time [s].</param>
        public void IncrementFixedTime(float deltaTime)
        {
            Assertion.ThrowIfNegativeOrZero(deltaTime, nameof(deltaTime));

            FixedDeltaTime = deltaTime;
        }

        /// <summary>
        /// Increments the time counter by the specified delta time.
        /// </summary>
        /// <param name="deltaTime">The delta time [s].</param>
        public void IncrementTime(float deltaTime)
        {
            Assertion.ThrowIfNegative(deltaTime, nameof(deltaTime));

            DeltaTime = deltaTime;
            Time += deltaTime;
        }
    }
}
