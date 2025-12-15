//-----------------------------------------------------------------------
// <copyright file="QuasarApplication.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Quasar.Physics.Pipelines.Internals;
using Quasar.Pipelines.Internals;
using Quasar.Rendering.Pipelines.Internals;
using Quasar.Settings;
using Quasar.UI;

using Space.Core.DependencyInjection;
using Space.Core.Diagnostics;

namespace Quasar.Internals
{
    /// <summary>
    /// Quasar application implementation.
    /// </summary>
    /// <seealso cref="IQuasarApplication" />
    [Export]
    [Singleton]
    internal sealed class QuasarApplication : IQuasarApplication
    {
        private readonly ICriticalErrorHandler criticalErrorHandler;
        private readonly ILoggerService loggerService;
        private readonly ISettingsService settingsService;
        private readonly TimeService timeService;
        private readonly UpdatePipeline updatePipeline;
        private readonly PhysicsPipeline physicsPipeline;
        private readonly RenderingPipeline renderingPipeline;
        private ILogger logger;


        /// <summary>
        /// Initializes a new instance of the <see cref="QuasarApplication" /> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="criticalErrorHandler">The critical error handler.</param>
        /// <param name="applicationWindow">The application window.</param>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="timeService">The time service.</param>
        /// <param name="updatePipeline">The update pipeline.</param>
        /// <param name="physicsPipeline">The physics pipeline.</param>
        /// <param name="renderingPipeline">The rendering pipeline.</param>
        public QuasarApplication(
            IServiceProvider serviceProvider,
            ICriticalErrorHandler criticalErrorHandler,
            IApplicationWindow applicationWindow,
            ILoggerService loggerService,
            ISettingsService settingsService,
            TimeService timeService,
            UpdatePipeline updatePipeline,
            PhysicsPipeline physicsPipeline,
            RenderingPipeline renderingPipeline)
        {
            this.criticalErrorHandler = criticalErrorHandler;
            this.loggerService = loggerService;
            this.settingsService = settingsService;
            this.timeService = timeService;
            this.updatePipeline = updatePipeline;
            this.physicsPipeline = physicsPipeline;
            this.renderingPipeline = renderingPipeline;

            ServiceProvider = serviceProvider;
            ApplicationWindow = applicationWindow;
        }


        /// <inheritdoc/>
        public IApplicationWindow ApplicationWindow { get; }

        /// <inheritdoc/>
        public IServiceProvider ServiceProvider { get; }


        /// <inheritdoc/>
        public void Run()
        {
            Exception unhandledException = null;
            try
            {
                StartApplicationInternals();

                // run the main application loop
                ApplicationWindow.Show();
                while (ApplicationWindow.Visible)
                {
                    updatePipeline.Execute();
                    renderingPipeline.Execute();
                }
            }
            catch (Exception exception)
            {
                unhandledException = exception;
            }
            finally
            {
                ShutdownApplicationInternals(unhandledException);
            }
        }

        private void StartApplicationInternals()
        {
            // initialize internal services
            loggerService.Start();
            logger = loggerService.Factory.Create<QuasarApplication>();
            logger.Info("Initializing the application.");

            logger.Info("Loading application settings.");
            settingsService.Load();

            // initialize pipelines
            updatePipeline.Start();
            physicsPipeline.Start();
            renderingPipeline.Start();

            logger?.Info("Application initialization is completed.");
        }

        private void ShutdownApplicationInternals(Exception unhandledException)
        {
            try
            {
                logger?.Info("Shutting down the application.");
                renderingPipeline.Shutdown();
                physicsPipeline.Shutdown();
                updatePipeline.Shutdown();

                logger?.Info("Application shutdown is completed.");
                loggerService?.Stop();

                if (unhandledException != null)
                {
                    criticalErrorHandler.Handle(ApplicationWindow.Title, unhandledException);
                }
            }
            catch (Exception exception)
            {
                criticalErrorHandler.Handle(ApplicationWindow.Title, exception);
            }
        }
    }
}
