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

using Quasar;
using Quasar.UI;

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

            builder.ConfigureLoggerService(configuration =>
            {
                configuration.LogDirectory = "./logs";
                configuration.FileNamePattern = "demo_{0}.log";
            });

            builder.ConfigureSettingsService(configuration =>
            {
                configuration.SettingsFilePath = "demo.cfg";
            });

            builder.ConfigureApplicationWindow(configuration =>
            {
                configuration.Type = ApplicationWindowType.Resizable;
            });

            using (var application = builder.Build())
            {
                application.Run();
            }
        }
    }
}
