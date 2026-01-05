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
using System.Timers;

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
        private Timer timer;


        /// <summary>
        /// Initializes a new instance of the <see cref="PhysicsPipeline" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="physicsContext">The physics context.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="timeService">The time service.</param>
        public PhysicsPipeline(
            IQuasarContext context,
            IPhysicsContext physicsContext,
            IServiceProvider serviceProvider,
            TimeService timeService)
            : base(context, serviceProvider)
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

            // initialize timer
            timer = new Timer
            {
                Enabled = false,
                AutoReset = true
            };
            timer.Elapsed += TimerCallback;

            // subscribe for settings changes and auto apply settings
            var physicsSettings = SettingsProvider.Get<IPhysicsSettings>();
            settingsSubscription = SettingsProvider.Subscribe(settingsObserver);
            OnSettingsChanged(physicsSettings);

            // enable timer
            timer.Enabled = true;
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
            UpdateTimer(physicsSettings.TimeStepMs);

            foreach (var stage in Stages)
            {
                stage.ApplySettings(physicsSettings);
            }
        }

        private void TimerCallback(object sender, ElapsedEventArgs e)
        {
            timeService.UpdatePhysicsDeltaTime();
            Execute();
        }

        private void UpdateTimer(float intervalSeconds)
        {
            var intervalMs = intervalSeconds * 1000.0;
            if (timer.Interval == intervalMs)
            {
                return;
            }

            timer.Interval = intervalMs;
        }
    }
}
