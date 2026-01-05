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

using Quasar.Graphics;
using Quasar.Graphics.Internals.Factories;
using Quasar.Pipelines.Internals;
using Quasar.Rendering.Profiler.Internals;
using Quasar.UI;
using Quasar.Utilities;

using Space.Core.DependencyInjection;

namespace Quasar.Rendering.Pipelines.Internals
{
    /// <summary>
    /// The Quasar rendering pipeline's main class provides entry point for all rendering operations.
    /// </summary>
    /// <seealso cref="PipelineBase{RenderPipelineStageBase, IRenderingContext}" />
    [Export]
    [Singleton]
    internal sealed class RenderingPipeline : PipelineBase<RenderingPipelineStageBase, IRenderingContext>
    {
        private readonly GraphicsContextFactory graphicsContextFactory;
        private readonly IApplicationWindow applicationWindow;
        private readonly IRenderingProfiler renderingProfiler;
        private readonly IServiceLoader serviceLoader;
        private readonly ActionBasedObserver<IRenderingSettings> settingsObserver;
        private readonly ActionBasedObserver<Size> sizeChangedObserver;
        private IDisposable settingsSubscription;
        private IDisposable sizeChangedSubscription;


        /// <summary>
        /// Initializes a new instance of the <see cref="RenderingPipeline" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="applicationWindow">The application window.</param>
        /// <param name="renderingContext">The rendering context.</param>
        /// <param name="renderingProfiler">The rendering profiler.</param>
        /// <param name="serviceLoader">The service loader.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="graphicsContextFactory">The graphics context factory.</param>
        internal RenderingPipeline(
            IQuasarContext context,
            IApplicationWindow applicationWindow,
            IRenderingContext renderingContext,
            IRenderingProfiler renderingProfiler,
            IServiceLoader serviceLoader,
            IServiceProvider serviceProvider,
            GraphicsContextFactory graphicsContextFactory)
            : base(context, serviceProvider)
        {
            this.applicationWindow = applicationWindow;
            this.renderingProfiler = renderingProfiler;
            this.serviceLoader = serviceLoader;
            this.graphicsContextFactory = graphicsContextFactory;

            Context = renderingContext;

            settingsObserver = new ActionBasedObserver<IRenderingSettings>(OnSettingsChanged);
            sizeChangedObserver = new ActionBasedObserver<Size>(OnSizeChanged);
        }


        /// <inheritdoc/>
        protected override IRenderingContext Context { get; }


        /// <inheritdoc/>
        protected override void OnStart()
        {
            // initialize rendering context by the actual settings
            var renderingSettings = SettingsProvider.Get<IRenderingSettings>();
            InitializeRenderingContext(renderingSettings);

            // collect pipeline stages
            base.OnStart();

            // subscribe for settings changes and auto apply settings
            settingsSubscription = SettingsProvider.Subscribe(settingsObserver);
            OnSettingsChanged(renderingSettings);

            // subscribe for application window size change event
            sizeChangedSubscription = applicationWindow.SizeChanged.Subscribe(sizeChangedObserver);
            OnSizeChanged(applicationWindow.Size);
        }

        /// <inheritdoc/>
        protected override void OnShutdown()
        {
            settingsSubscription?.Dispose();
            sizeChangedSubscription?.Dispose();

            base.OnShutdown();
        }

        /// <inheritdoc/>
        protected override void OnExecute()
        {
            renderingProfiler.BeginFrame();

            base.OnExecute();

            renderingProfiler.EndFrame();
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
                stage.InvokeApplySettings(renderingSettings);
            }
        }

        private void OnSizeChanged(Size size)
        {
            foreach (var stage in Stages)
            {
                stage.InvokeSizeChanged(size);
            }
        }
    }
}
