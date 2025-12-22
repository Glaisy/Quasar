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

using Microsoft.Extensions.DependencyInjection;

using Quasar.Physics.Pipelines.Internals;
using Quasar.Pipelines.Internals;
using Quasar.Rendering.Pipelines.Internals;
using Quasar.Settings;
using Quasar.UI;
using Quasar.UI.Internals;

using Space.Core;
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
        private readonly ApplicationConfiguration applicationConfiguration;
        private ILoggerService loggerService;
        private ISettingsService settingsService;
        private TimeService timeService;
        private UpdatePipeline updatePipeline;
        private PhysicsPipeline physicsPipeline;
        private RenderingPipeline renderingPipeline;
        private ILogger logger;


        /// <summary>
        /// Initializes a new instance of the <see cref="QuasarApplication" /> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="criticalErrorHandler">The critical error handler.</param>
        public QuasarApplication(
            IServiceProvider serviceProvider,
            ICriticalErrorHandler criticalErrorHandler)
        {
            this.criticalErrorHandler = criticalErrorHandler;
            ServiceProvider = serviceProvider;

            applicationConfiguration = serviceProvider.GetService<ApplicationConfiguration>();
            ApplicationWindow = CreateApplicationWindow();
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
                    timeService.UpdateDeltaTime();
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


        private IApplicationWindow CreateApplicationWindow()
        {
            string title = null;
            ApplicationWindowType applicationWindowType;
            float screenRatio;
            if (applicationConfiguration == null)
            {
                applicationWindowType = ApplicationWindowType.Default;
                screenRatio = ApplicationConfiguration.DefaultScreenRatio;
            }
            else
            {
                applicationWindowType = applicationConfiguration.ApplicationWindowType;
                screenRatio = Ranges.FloatUnit.Clamp(applicationConfiguration.ScreenRatio);
                title = applicationConfiguration.Title;
            }

            if (String.IsNullOrEmpty(title))
            {
                var environmentInformation = ServiceProvider.GetRequiredService<IEnvironmentInformation>();
                title = environmentInformation.Title;
            }

            var nativeWindowFactory = ServiceProvider.GetRequiredService<INativeWindowFactory>();
            var applicationWindow = nativeWindowFactory.CreateApplicationWindow(applicationWindowType, title, screenRatio);

            var serviceLoader = ServiceProvider.GetRequiredService<IServiceLoader>();
            serviceLoader.AddSingleton(applicationWindow);
            return applicationWindow;
        }

        private void StartApplicationInternals()
        {
            // initialize internal services
            loggerService = ServiceProvider.GetRequiredService<ILoggerService>();
            loggerService.Start();
            logger = loggerService.Factory.Create<QuasarApplication>();
            logger.Info("Initializing the application.");

            settingsService = ServiceProvider.GetRequiredService<ISettingsService>();
            settingsService.Load();

            timeService = ServiceProvider.GetRequiredService<TimeService>();
            timeService.Initialize();

            // initialize pipelines
            renderingPipeline = ServiceProvider.GetRequiredService<RenderingPipeline>();
            renderingPipeline.Start();
            updatePipeline = ServiceProvider.GetRequiredService<UpdatePipeline>();
            updatePipeline.Start();
            physicsPipeline = ServiceProvider.GetRequiredService<PhysicsPipeline>();
            physicsPipeline.Start();

            // execute custom bootstrapper process
            var bootstrapperFactory = applicationConfiguration?.BootstrapperFactory;
            if (bootstrapperFactory != null)
            {
                var bootstrapper = bootstrapperFactory(ServiceProvider);
                bootstrapper?.Execute();
            }

            logger?.Info("Application initialization is completed.");
        }

        private void ShutdownApplicationInternals(Exception unhandledException)
        {
            try
            {
                logger?.Info("Shutting down the application.");
                physicsPipeline.Shutdown();
                updatePipeline.Shutdown();
                renderingPipeline.Shutdown();

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
