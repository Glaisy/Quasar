//-----------------------------------------------------------------------
// <copyright file="QuasarApplicationBuilder.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.IO;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Quasar.Internals;
using Quasar.Settings;
using Quasar.UI;
using Quasar.UI.Internals;

using Space.Core;
using Space.Core.DependencyInjection;
using Space.Core.Diagnostics;

namespace Quasar
{
    /// <summary>
    /// Quasar application builder component.
    /// </summary>
    public sealed class QuasarApplicationBuilder
    {
        private const string CriticalErrorTitle = $"{nameof(Quasar)} critical error...";
        private const string PlatformSpecificAssemblyNameFormatStringP1 = $"{nameof(Quasar)}.{{0}}.dll";
        private static readonly Range<float> ScreenRatioRange = new Range<float>(0.1f, 1.0f);


        private readonly OperatingSystemPlatform operatingSystemPlatform;
        private readonly IServiceProvider serviceProvider;
        private readonly ICriticalErrorHandler criticalErrorHandler;


        /// <summary>
        /// Initializes a new instance of the <see cref="QuasarApplicationBuilder"/> class.
        /// </summary>
        public QuasarApplicationBuilder()
        {
            try
            {
                operatingSystemPlatform = DetectOperatingSystemPlatform();

                serviceProvider = new DynamicServiceProvider()
                    .InitializeStaticServices();

                ServiceLoader = serviceProvider.GetRequiredService<IServiceLoader>()
                    .AddExportedServices(typeof(IEnvironmentInformation).Assembly)
                    .AddExportedServices(typeof(IQuasarApplication).Assembly)
                    .ConfigureEnvironmentInformation();

                AddPlatformSpecificServices(operatingSystemPlatform);

                criticalErrorHandler = serviceProvider.GetRequiredService<ICriticalErrorHandler>();
            }
            catch (Exception exception)
            {
                HandleCriticalError(exception);
            }
        }


        /// <summary>
        /// Gets the service loader.
        /// </summary>
        public IServiceLoader ServiceLoader { get; }


        /// <summary>
        /// Builds a new Quasar application instance.
        /// </summary>
        /// <returns>The application instance.</returns>
        public IQuasarApplication Build()
        {
            try
            {
                serviceProvider.InitializeStaticServices();
                CreateApplicationWindow();
                return serviceProvider.GetRequiredService<QuasarApplication>();
            }
            catch (Exception exception)
            {
                HandleCriticalError(exception);

                return null;
            }
        }

        /// <summary>
        /// Configures the application window.
        /// </summary>
        /// <param name="configureAction">The configure action.</param>
        public QuasarApplicationBuilder ConfigureApplicationWindow(Action<ApplicationWindowConfiguration> configureAction)
        {
            var applicationWindowConfiguration = new ApplicationWindowConfiguration();
            configureAction(applicationWindowConfiguration);
            ServiceLoader.AddSingleton(applicationWindowConfiguration);

            return this;
        }

        /// <summary>
        /// Configures the logger service.
        /// </summary>
        /// <param name="configureAction">The configure action.</param>
        public QuasarApplicationBuilder ConfigureLoggerService(Action<LoggerServiceConfiguration> configureAction)
        {
            ArgumentNullException.ThrowIfNull(configureAction, nameof(configureAction));

            var loggerServiceConfiguration = new LoggerServiceConfiguration();
            configureAction(loggerServiceConfiguration);
            ServiceLoader.AddSingleton(loggerServiceConfiguration);

            return this;
        }

        /// <summary>
        /// Configures the settings service.
        /// </summary>
        /// <param name="configureAction">The configure action.</param>
        public QuasarApplicationBuilder ConfigureSettingsService(Action<SettingsServiceConfiguration> configureAction)
        {
            ArgumentNullException.ThrowIfNull(configureAction, nameof(configureAction));

            var settingsOptions = new SettingsServiceConfiguration();
            configureAction(settingsOptions);
            ServiceLoader.AddSingleton(settingsOptions);
            return this;
        }


        private void AddPlatformSpecificServices(OperatingSystemPlatform operatingSystemPlatform)
        {
            var platformSpecificAssemblyName = String.Format(PlatformSpecificAssemblyNameFormatStringP1, operatingSystemPlatform);
            var platformSpecificAssemblyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, platformSpecificAssemblyName);
            var platformSpecificAssembly = Assembly.LoadFile(platformSpecificAssemblyPath);
            if (platformSpecificAssembly == null)
            {
                throw new ApplicationException($"Platform specific assembly not found: {platformSpecificAssemblyPath}");
            }

            ServiceLoader.AddExportedServices(platformSpecificAssembly);
        }

        private void CreateApplicationWindow()
        {
            string title = null;
            ApplicationWindowType applicationWindowType;
            float screenRatio;
            var applicationWindowConfiguration = serviceProvider.GetService<ApplicationWindowConfiguration>();
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
                var environmentInformation = serviceProvider.GetRequiredService<IEnvironmentInformation>();
                title = environmentInformation.Title;
            }

            var nativeWindowFactory = serviceProvider.GetRequiredService<INativeWindowFactory>();
            var applicationWindow = nativeWindowFactory.CreateApplicationWindow(applicationWindowType, title, screenRatio);
            ServiceLoader.AddSingleton(applicationWindow);
        }

        private static OperatingSystemPlatform DetectOperatingSystemPlatform()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                    return OperatingSystemPlatform.Windows;

                default:
                    throw new NotSupportedException(Environment.OSVersion.ToString());
            }
        }

        private void HandleCriticalError(Exception exception)
        {
            if (criticalErrorHandler != null)
            {
                criticalErrorHandler.Handle(CriticalErrorTitle, exception);
                return;
            }

#if DEBUG
            var errorMessage = exception.ToString();
#else
            var errorMessage = exception.Message;
#endif

            Console.WriteLine($"Critical error detected: {errorMessage}");
            Environment.Exit(exception.HResult);
        }
    }
}
