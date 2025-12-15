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
        private DateTime fixedFrameStart;
        private DateTime frameStart;


        /// <inheritdoc/>
        public float DeltaTime { get; private set; }

        /// <inheritdoc/>
        public float FixedDeltaTime { get; private set; }

        /// <inheritdoc/>
        public float Time { get; private set; }


        /// <summary>
        /// Executes the time service initialization.
        /// </summary>
        public void Initialize()
        {
            fixedFrameStart = frameStart = DateTime.UtcNow;
            DeltaTime = FixedDeltaTime = Time = 0.0f;
        }

        /// <summary>
        /// Updates the fixed time (physics pipeline).
        /// </summary>
        public void UpdateFixedTime()
        {
            var fixedFrameEnd = DateTime.UtcNow;
            var fixedDeltaTime = (float)(fixedFrameEnd - fixedFrameStart).TotalSeconds;
            Assertion.ThrowIfNegativeOrZero(fixedDeltaTime, nameof(fixedDeltaTime));

            FixedDeltaTime = fixedDeltaTime;
            fixedFrameStart = fixedFrameEnd;
        }

        /// <summary>
        /// Updates the time (update and rendering pipeline).
        /// </summary>
        public void UpdateTime()
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
