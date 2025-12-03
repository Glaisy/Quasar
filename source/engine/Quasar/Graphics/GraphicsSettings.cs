//-----------------------------------------------------------------------
// <copyright file="GraphicsSettings.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Settings;

namespace Quasar.Graphics
{
    /// <summary>
    /// Quasar graphics settings implementation.
    /// </summary>
    /// <seealso cref="SettingsBase{IGraphicsSettings}" />
    /// <seealso cref="IGraphicsSettings" />
    [Settings(typeof(IGraphicsSettings))]
    public sealed class GraphicsSettings : SettingsBase<IGraphicsSettings>, IGraphicsSettings
    {
        /// <summary>
        /// The defaults.
        /// </summary>
        public static readonly IGraphicsSettings Defaults = new GraphicsSettings
        {
            FullScreenMode = false,
            Platform = GraphicsPlatform.OpenGL
        };


        /// <summary>
        /// Gets or sets a value indicating whether full screen mode is active.
        /// </summary>
        public bool FullScreenMode { get; set; }

        /// <summary>
        /// Gets or sets the graphics platform.
        /// </summary>
        public GraphicsPlatform Platform { get; set; }


        /// <inheritdoc/>
        public override void SetDefaults()
        {
            Copy(Defaults);
        }

        /// <inheritdoc/>
        protected override void CopyProperties(IGraphicsSettings source)
        {
            FullScreenMode = source.FullScreenMode;
            Platform = source.Platform;
        }
    }
}
