//-----------------------------------------------------------------------
// <copyright file="IRenderingSettings.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics;
using Quasar.Settings;

namespace Quasar.Rendering
{
    /// <summary>
    /// Represents the Quasar rendering settings.
    /// </summary>
    /// <seealso cref="ISettings" />
    public interface IRenderingSettings : ISettings
    {
        /// <summary>
        /// Gets the display mode identifier.
        /// </summary>
        string DisplayMode { get; }

        /// <summary>
        /// Gets a value indicating whether full screen mode is active.
        /// </summary>
        bool FullScreenMode { get; }

        /// <summary>
        /// Gets the graphics platform.
        /// </summary>
        GraphicsPlatform Platform { get; }

        /// <summary>
        /// Gets the V-Sync mode.
        /// </summary>
        VSyncMode VSyncMode { get; }
    }
}
