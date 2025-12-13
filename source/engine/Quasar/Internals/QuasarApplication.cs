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
using System.Threading;

using Microsoft.Extensions.DependencyInjection;

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
    /// <seealso cref="DisposableBase" />
    /// <seealso cref="IQuasarApplication" />
    [Export]
    [Singleton]
    internal sealed class QuasarApplication : DisposableBase, IQuasarApplication
    {
        private static readonly Range<float> ScreenRatioRange = new Range<float>(0.1f, 1.0f);


        private ILoggerService loggerService;
        private UpdatePipeline updatePipeline;
        private RenderingPipeline renderingPipeline;


        /// <summary>
        /// Initializes a new instance of the <see cref="QuasarApplication" /> class.
        /// </summary>
        /// <param name="environmentInformation">The environment information.</param>
        /// <param name="nativeWindowFactory">The native window factory.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="serviceLoader">The service loader.</param>
        public QuasarApplication(
            IEnvironmentInformation environmentInformation,
            INativeWindowFactory nativeWindowFactory,
            IServiceProvider serviceProvider,
            IServiceLoader serviceLoader)
        {
            ServiceProvider = serviceProvider;

            ApplicationWindow = CreateApplicationWindow(nativeWindowFactory, environmentInformation);
            serviceLoader.AddSingleton(ApplicationWindow);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            renderingPipeline?.Shutdown();
            loggerService?.Stop();
        }


        /// <inheritdoc/>
        public IApplicationWindow ApplicationWindow { get; }

        /// <inheritdoc/>
        public IServiceProvider ServiceProvider { get; }


        /// <inheritdoc/>
        public void Run()
        {
            // initialize internal services
            loggerService = ServiceProvider.GetRequiredService<ILoggerService>();
            loggerService.Start();

            var settingsService = ServiceProvider.GetRequiredService<ISettingsService>();
            settingsService.Load();

            // initialize pipelines
            updatePipeline = ServiceProvider.GetRequiredService<UpdatePipeline>();
            updatePipeline.Start();
            renderingPipeline = ServiceProvider.GetRequiredService<RenderingPipeline>();
            renderingPipeline.Start();

            // show application window and execute application loop
            ApplicationWindow.Show();
            while (ApplicationWindow.Visible)
            {
                updatePipeline.Execute();
                renderingPipeline.Execute();
                Thread.Sleep(20);
            }
        }


        private IApplicationWindow CreateApplicationWindow(
            INativeWindowFactory nativeWindowFactory,
            IEnvironmentInformation environmentInformation)
        {
            string title = null;
            ApplicationWindowType applicationWindowType;
            float screenRatio;
            var applicationWindowConfiguration = ServiceProvider.GetService<ApplicationWindowConfiguration>();
            if (applicationWindowConfiguration == null)
            {
                applicationWindowType = ApplicationWindowConfiguration.DefaultType;
                screenRatio = ApplicationWindowConfiguration.DefaultScreenRatio;
            }
            else
            {
                applicationWindowType = applicationWindowConfiguration.Type;
                screenRatio = ScreenRatioRange.Clamp(applicationWindowConfiguration.ScreenRatio);
                title = applicationWindowConfiguration.Title;
            }

            if (String.IsNullOrEmpty(title))
            {
                title = environmentInformation.Title;
            }

            return nativeWindowFactory.CreateApplicationWindow(applicationWindowType, title, screenRatio);
        }
    }
}
