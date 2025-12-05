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

using Quasar.Graphics.Internals;
using Quasar.Pipelines.Internals;
using Quasar.UI;
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
        private readonly IApplicationWindow applicationWindow;
        private readonly IGraphicsDeviceContextFactory graphicsDeviceContextFactory;
        private readonly IRenderingContext renderingContext;
        private readonly IServiceLoader serviceLoader;
        private readonly ActionBasedObserver<IRenderingSettings> settingsObserver;
        private IDisposable settingsSubscription;


        /// <summary>
        /// Initializes a new instance of the <see cref="RenderingPipeline" /> class.
        /// </summary>
        /// <param name="applicationWindow">The application window.</param>
        /// <param name="graphicsDeviceContextFactory">The graphics device context factory.</param>
        /// <param name="renderingContext">The rendering context.</param>
        /// <param name="serviceLoader">The service loader.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public RenderingPipeline(
            IApplicationWindow applicationWindow,
            IGraphicsDeviceContextFactory graphicsDeviceContextFactory,
            IRenderingContext renderingContext,
            IServiceLoader serviceLoader,
            IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            this.applicationWindow = applicationWindow;
            this.graphicsDeviceContextFactory = graphicsDeviceContextFactory;
            this.renderingContext = renderingContext;
            this.serviceLoader = serviceLoader;

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
        protected override void OnStart()
        {
            // initialize rendering context by the actual settings
            var renderingSettings = SettingsService.Get<IRenderingSettings>();
            InitializeRenderingContext(renderingSettings);

            // collect rendering stages
            base.OnStart();

            // subscribe for settings changes and auto apply settings
            settingsSubscription = SettingsService.Subscribe(settingsObserver);
            OnSettingsChanged(renderingSettings);
        }

        /// <inheritdoc/>
        protected override void OnShutdown()
        {
            settingsSubscription?.Dispose();

            base.OnShutdown();
        }


        private void InitializeRenderingContext(IRenderingSettings renderingSettings)
        {
            var graphicsDeviceContext = graphicsDeviceContextFactory.Create(renderingSettings.Platform);
            serviceLoader.AddSingleton(graphicsDeviceContext);

            graphicsDeviceContext.Initialize(applicationWindow);
            renderingContext.Initialize(graphicsDeviceContext);
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
