//-----------------------------------------------------------------------
// <copyright file="IGraphicsSettings.cs" company="Space Development">
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
    /// Represents the Quasar graphics settings.
    /// </summary>
    /// <seealso cref="ISettings" />
    public interface IGraphicsSettings : ISettings
    {
        /// <summary>
        /// Gets a value indicating whether full screen mode is active.
        /// </summary>
        bool FullScreenMode { get; }

        /// <summary>
        /// Gets the graphics platform.
        /// </summary>
        GraphicsPlatform Platform { get; }
    }
}
