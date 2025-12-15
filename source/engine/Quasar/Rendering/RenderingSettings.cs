//-----------------------------------------------------------------------
// <copyright file="RenderingSettings.cs" company="Space Development">
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
using Quasar.Settings;

using Space.Core.Settings;

namespace Quasar.Rendering
{
    /// <summary>
    /// Quasar's rendering settings object implementation.
    /// </summary>
    /// <seealso cref="SettingsBase{IRenderingSettings}" />
    /// <seealso cref="IRenderingSettings" />
    [Settings(typeof(IRenderingSettings))]
    public sealed class RenderingSettings : SettingsBase<IRenderingSettings>, IRenderingSettings
    {
        /// <summary>
        /// The defaults.
        /// </summary>
        public static readonly IRenderingSettings Defaults = new RenderingSettings
        {
            Platform = GraphicsPlatform.OpenGL
        };


        /// <summary>
        /// Gets or sets the display mode identifier.
        /// </summary>
        public string DisplayMode { get; set; }

        private int fpsLimit;
        /// <summary>
        /// Gets or sets the FPS limit [1/s].
        /// </summary>
        public int FPSLimit
        {
            get => fpsLimit;
            set => fpsLimit = Math.Max(0, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether full screen mode is active.
        /// </summary>
        public bool FullScreenMode { get; set; }

        /// <summary>
        /// Gets or sets the graphics platform.
        /// </summary>
        public GraphicsPlatform Platform { get; set; }

        /// <summary>
        /// Gets or sets the V-Sync mode.
        /// </summary>
        public VSyncMode VSyncMode { get; set; }


        /// <inheritdoc/>
        public override void SetDefaults()
        {
            Copy(Defaults);
        }


        /// <inheritdoc/>
        protected override void CopyProperties(IRenderingSettings source)
        {
            DisplayMode = source.DisplayMode;
            FPSLimit = source.FPSLimit;
            FullScreenMode = source.FullScreenMode;
            Platform = source.Platform;
            VSyncMode = source.VSyncMode;
        }
    }
}
