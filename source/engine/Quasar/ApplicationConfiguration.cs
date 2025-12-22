//-----------------------------------------------------------------------
// <copyright file="ApplicationConfiguration.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Quasar.UI;

namespace Quasar
{
    /// <summary>
    /// Application configuration object to provide customization during the application build process.
    /// </summary>
    public sealed class ApplicationConfiguration
    {
        /// <summary>
        /// The default screen ratio.
        /// </summary>
        public const float DefaultScreenRatio = 0.75f;


        /// <summary>
        /// Gets or sets the application window's type.
        /// </summary>
        public ApplicationWindowType ApplicationWindowType { get; set; } = ApplicationWindowType.Default;

        /// <summary>
        /// Gets or sets the ratio of application window's size relative to the screen.
        /// </summary>
        public float ScreenRatio { get; set; } = DefaultScreenRatio;

        /// <summary>
        /// Gets or sets the application window's title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the bootstrapper factory function.
        /// </summary>
        public Func<IServiceProvider, IBootstrapper> BootstrapperFactory { get; set; }
    }
}
