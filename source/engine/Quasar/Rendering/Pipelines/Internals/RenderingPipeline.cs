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

using Quasar.Graphics.Internals.Factories;
using Quasar.Pipelines.Internals;
using Quasar.UI;
using Quasar.Utilities;

using Space.Core.DependencyInjection;

namespace Quasar.Rendering.Pipelines.Internals
{
    /// <summary>
    /// This render pipeline main class provides entry point for all rendering operations.
    /// </summary>
    /// <seealso cref="PipelineBase{RenderPipelineStageBase, IRenderingContext}" />
    [Export]
    [Singleton]
    internal sealed class RenderingPipeline : PipelineBase<RenderingPipelineStageBase, IRenderingContext>
    {
        private readonly GraphicsContextFactory graphicsContextFactory;
        private readonly IApplicationWindow applicationWindow;
        private readonly IServiceLoader serviceLoader;
        private readonly ActionBasedObserver<IRenderingSettings> settingsObserver;
        private IDisposable settingsSubscription;


        /// <summary>
        /// Initializes a new instance of the <see cref="RenderingPipeline" /> class.
        /// </summary>
        /// <param name="graphicsContextFactory">The graphics context factory.</param>
        /// <param name="applicationWindow">The application window.</param>
        /// <param name="renderingContext">The rendering context.</param>
        /// <param name="serviceLoader">The service loader.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public RenderingPipeline(
            IApplicationWindow applicationWindow,
            IRenderingContext renderingContext,
            IServiceLoader serviceLoader,
            IServiceProvider serviceProvider,
            GraphicsContextFactory graphicsContextFactory)
            : base(serviceProvider)
        {
            this.applicationWindow = applicationWindow;
            this.serviceLoader = serviceLoader;
            this.graphicsContextFactory = graphicsContextFactory;

            Context = renderingContext;

            settingsObserver = new ActionBasedObserver<IRenderingSettings>(OnSettingsChanged);
        }


        /// <inheritdoc/>
        protected override IRenderingContext Context { get; }


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
            var graphicsDeviceContext = graphicsContextFactory.Create(renderingSettings.Platform, applicationWindow);
            serviceLoader.AddSingleton(graphicsDeviceContext);

            Context.Initialize(graphicsDeviceContext);
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
