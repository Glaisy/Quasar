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


        private readonly ISettingsService settingsService;
        private readonly ILoggerService loggerService;
        private readonly INativeMessageHandler nativeMessageHandler;


        /// <summary>
        /// Initializes a new instance of the <see cref="QuasarApplication" /> class.
        /// </summary>
        /// <param name="environmentInformation">The environment information.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="nativeWindowFactory">The native window factory.</param>
        /// <param name="nativeMessageHandler">The native message handler.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public QuasarApplication(
            IEnvironmentInformation environmentInformation,
            ISettingsService settingsService,
            ILoggerService loggerService,
            INativeWindowFactory nativeWindowFactory,
            INativeMessageHandler nativeMessageHandler,
            IServiceProvider serviceProvider)
        {
            this.settingsService = settingsService;
            this.loggerService = loggerService;
            this.nativeMessageHandler = nativeMessageHandler;

            ServiceProvider = serviceProvider;

            ApplicationWindow = CreateApplicationWindow(nativeWindowFactory, environmentInformation);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            loggerService?.Stop();
        }


        /// <inheritdoc/>
        public IApplicationWindow ApplicationWindow { get; }

        /// <inheritdoc/>
        public IServiceProvider ServiceProvider { get; }


        /// <inheritdoc/>
        public void Run()
        {
            loggerService.Start();
            settingsService.Load();

            var quasarSettings = settingsService.Get<IQuasarSettings>();
            loggerService.LogLevel = quasarSettings.LogLevel;

            ApplicationWindow.Show();
            while (ApplicationWindow.Visible)
            {
                nativeMessageHandler.ProcessMessages();
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
