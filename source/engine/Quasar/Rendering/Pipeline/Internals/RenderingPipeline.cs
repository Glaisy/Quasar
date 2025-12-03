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
        private readonly IGraphicsDeviceContext graphicsDeviceContext;
        private readonly IRenderingContext renderingContext;
        private readonly ActionBasedObserver<IRenderingSettings> settingsObserver;
        private IDisposable settingsSubscription;



        /// <summary>
        /// Initializes a new instance of the <see cref="RenderingPipeline" /> class.
        /// </summary>
        /// <param name="applicationWindow">The application window.</param>
        /// <param name="graphicsDeviceContext">The graphics device context.</param>
        /// <param name="renderingContext">The rendering context.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public RenderingPipeline(
            IApplicationWindow applicationWindow,
            IGraphicsDeviceContext graphicsDeviceContext,
            IRenderingContext renderingContext,
            IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            this.applicationWindow = applicationWindow;
            this.graphicsDeviceContext = graphicsDeviceContext;
            this.renderingContext = renderingContext;

            settingsObserver = new ActionBasedObserver<IRenderingSettings>(OnSettingsChanged);
        }


        /// <summary>
        /// Execute event handler.
        /// </summary>
        protected override void OnExecute()
        {
            foreach (var stage in Stages)
            {
                stage.Execute(renderingContext);
            }
        }

        /// <summary>
        /// Start event handler.
        /// </summary>
        protected override void OnStart()
        {
            base.OnStart();

            renderingContext.Initialize(graphicsDeviceContext);

            // subscribe and apply settings
            settingsSubscription = SettingsService.Subscribe(settingsObserver);
            var settings = SettingsService.Get<IRenderingSettings>();
            OnSettingsChanged(settings);
        }

        /// <summary>
        /// Shutdown event handler.
        /// </summary>
        protected override void OnShutdown()
        {
            settingsSubscription?.Dispose();

            base.OnShutdown();
        }


        private void OnSettingsChanged(IRenderingSettings settings)
        {
            foreach (var stage in Stages)
            {
                stage.ApplySettings(settings);
            }
        }
    }
}
