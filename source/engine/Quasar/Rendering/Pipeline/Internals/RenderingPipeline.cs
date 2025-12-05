//-----------------------------------------------------------------------
// <copyright file="RenderingPipeline.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Quasar.Pipelines.Internals;
using Quasar.Utilities;

using Space.Core.DependencyInjection;

namespace Quasar.Rendering.Pipeline.Internals
{
    /// <summary>
    /// This render pipeline main class provides entry point for all rendering operations.
    /// </summary>
    /// <seealso cref="PipelineBase{RenderPipelineStageBase}" />
    [Export]
    [Singleton]
    internal sealed class RenderingPipeline : PipelineBase<RenderingPipelineStageBase>
    {
        private readonly IRenderingContext renderingContext;
        private readonly ActionBasedObserver<IRenderingSettings> settingsObserver;
        private IDisposable settingsSubscription;


        /// <summary>
        /// Initializes a new instance of the <see cref="RenderingPipeline" /> class.
        /// </summary>
        /// <param name="renderingContext">The rendering context.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public RenderingPipeline(
            IRenderingContext renderingContext,
            IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            this.renderingContext = renderingContext;

            settingsObserver = new ActionBasedObserver<IRenderingSettings>(OnSettingsChanged);
        }


        /// <inheritdoc/>
        protected override void OnExecute()
        {
            foreach (var stage in Stages)
            {
                stage.Execute(renderingContext);
            }
        }

        /// <inheritdoc/>
        protected override void OnStart(IServiceProvider serviceProvider)
        {
            base.OnStart(serviceProvider);

            // subscribe and auto apply settings
            settingsSubscription = SettingsService.Subscribe(settingsObserver);
            var renderingSettings = SettingsService.Get<IRenderingSettings>();
            OnSettingsChanged(renderingSettings);
        }

        /// <inheritdoc/>
        protected override void OnShutdown()
        {
            settingsSubscription?.Dispose();

            base.OnShutdown();
        }


        private void OnSettingsChanged(IRenderingSettings renderingSettings)
        {
            foreach (var stage in Stages)
            {
                stage.ApplySettings(renderingSettings);
            }
        }
    }
}
