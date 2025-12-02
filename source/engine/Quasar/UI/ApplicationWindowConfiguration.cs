//-----------------------------------------------------------------------
// <copyright file="ApplicationWindowConfiguration.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.UI
{
    /// <summary>
    /// Application window configuration object to provide customization in application build time.
    /// </summary>
    public sealed class ApplicationWindowConfiguration
    {
        /// <summary>
        /// The default screen ratio.
        /// </summary>
        public const float DefaultScreenRatio = 0.8f;

        /// <summary>
        /// The default application window type.
        /// </summary>
        public const ApplicationWindowType DefaultType = ApplicationWindowType.Default;


        /// <summary>
        /// Gets or sets the ratio of window size relative to the screen.
        /// </summary>
        public float ScreenRatio { get; set; } = DefaultScreenRatio;

        /// <summary>
        /// Gets or sets the application window's title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the application window's type.
        /// </summary>
        public ApplicationWindowType Type { get; set; } = DefaultType;
    }
}
