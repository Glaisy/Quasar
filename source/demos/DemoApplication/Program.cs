//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Quasar;
using Quasar.UI;

using Space.Core.Diagnostics;

namespace DemoApplication
{
    /// <summary>
    /// Application startup class.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        private static void Main()
        {
            var builder = new QuasarApplicationBuilder();

            builder.ServiceLoader.AddExportedServices(Assembly.GetExecutingAssembly());

            builder.ConfigureLoggerService(configuration =>
            {
                configuration.FileNamePattern = "demo_{0}.log";
                configuration.LogLevel = LogLevel.Info;
            });

            builder.ConfigureSettingsService(configuration =>
            {
                configuration.SettingsFilePath = "demo.cfg";
            });

            builder.ConfigureApplication(configuration =>
            {
                configuration.ApplicationWindowType = ApplicationWindowType.Resizable;
                configuration.ScreenRatio = 0.75f;
                configuration.BootstrapperFactory = serviceProvider => serviceProvider.GetRequiredService<Bootstrapper>();
            });

            var application = builder.Build();
            application.Run();
        }
    }
}
