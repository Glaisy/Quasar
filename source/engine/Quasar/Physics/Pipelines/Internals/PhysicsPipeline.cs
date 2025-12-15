//-----------------------------------------------------------------------
// <copyright file="PhysicsPipeline.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Threading;

using Quasar.Pipelines.Internals;
using Quasar.Utilities;

using Space.Core.DependencyInjection;

namespace Quasar.Physics.Pipelines.Internals
{
    /// <summary>
    /// The Quasar physics pipeline's main class provides entry point for all physics operations.
    /// </summary>
    /// <seealso cref="PipelineBase{PhysicsPipelineStageBase,IPhysicsContext}" />
    [Export]
    [Singleton]
    internal sealed class PhysicsPipeline : PipelineBase<PhysicsPipelineStageBase, IPhysicsContext>
    {
        private readonly TimeService timeService;
        private readonly ActionBasedObserver<IPhysicsSettings> settingsObserver;
        private IDisposable settingsSubscription;
        private float timerIntervalMs;
        private Timer timer;


        /// <summary>
        /// Initializes a new instance of the <see cref="PhysicsPipeline" /> class.
        /// </summary>
        /// <param name="physicsContext">The physics context.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="timeService">The time service.</param>
        public PhysicsPipeline(
            IPhysicsContext physicsContext,
            IServiceProvider serviceProvider,
            TimeService timeService)
            : base(serviceProvider)
        {
            this.timeService = timeService;

            Context = physicsContext;

            settingsObserver = new ActionBasedObserver<IPhysicsSettings>(OnSettingsChanged);
        }


        /// <inheritdoc/>
        protected override IPhysicsContext Context { get; }

        /// <inheritdoc/>
        protected override void OnStart()
        {
            // collect pipeline stages
            base.OnStart();

            // subscribe for settings changes and auto apply settings
            var physicsSettings = SettingsProvider.Get<IPhysicsSettings>();
            settingsSubscription = SettingsProvider.Subscribe(settingsObserver);
            OnSettingsChanged(physicsSettings);
            UpdateTimer(0.04f);
        }

        /// <inheritdoc/>
        protected override void OnShutdown()
        {
            settingsSubscription?.Dispose();

            timer?.Dispose();
            timer = null;

            base.OnShutdown();
        }


        private void OnSettingsChanged(IPhysicsSettings physicsSettings)
        {
            foreach (var stage in Stages)
            {
                stage.ApplySettings(physicsSettings);
            }
        }

        private void TimerCallback(object state)
        {
            timeService.UpdatePhysicsDeltaTime();
            Execute();
        }

        private void UpdateTimer(float intervalSeconds)
        {
            var intervalMs = (int)MathF.Round(intervalSeconds * 1000.0f);
            if (timerIntervalMs == intervalSeconds)
            {
                return;
            }

            timer?.Dispose();
            timer = new Timer(TimerCallback, null, 0, intervalMs);
            timerIntervalMs = intervalSeconds;
        }
    }
}
