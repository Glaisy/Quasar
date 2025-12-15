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

using System;

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
        private DateTime phyisicsFrameStart;
        private DateTime frameStart;


        /// <inheritdoc/>
        public float DeltaTime { get; private set; }

        /// <inheritdoc/>
        public float PhysicsDeltaTime { get; private set; }

        /// <inheritdoc/>
        public float Time { get; private set; }


        /// <summary>
        /// Executes the time service initialization.
        /// </summary>
        public void Initialize()
        {
            phyisicsFrameStart = frameStart = DateTime.UtcNow;
            DeltaTime = PhysicsDeltaTime = Time = 0.0f;
        }

        /// <summary>
        /// Updates the delta time for the physics pipeline.
        /// </summary>
        public void UpdatePhysicsDeltaTime()
        {
            var physicsFrameStart = DateTime.UtcNow;
            var physicsDeltaTime = (float)(physicsFrameStart - phyisicsFrameStart).TotalSeconds;
            Assertion.ThrowIfNegativeOrZero(physicsDeltaTime, nameof(physicsDeltaTime));

            PhysicsDeltaTime = physicsDeltaTime;
            phyisicsFrameStart = physicsFrameStart;
        }

        /// <summary>
        /// Updates the delta time for the update and rendering pipeline.
        /// </summary>
        public void UpdateDeltaTime()
        {
            var frameEnd = DateTime.UtcNow;
            var deltaTime = (float)(frameEnd - frameStart).TotalSeconds;
            Assertion.ThrowIfNegativeOrZero(deltaTime, nameof(deltaTime));

            DeltaTime = deltaTime;
            Time += deltaTime;
            frameStart = frameEnd;
        }
    }
}
