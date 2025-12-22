//-----------------------------------------------------------------------
// <copyright file="IAudioSettings.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Space.Core.Settings;

namespace Quasar.Settings
{
    /// <summary>
    /// Audio settings interface definition.
    /// </summary>
    /// <seealso cref="ISettings" />
    public interface IAudioSettings : ISettings
    {
        /// <summary>
        /// Gets the master volume [0...1].
        /// </summary>
        float MasterVolume { get; }

        /// <summary>
        /// Gets the music volume [0...1].
        /// </summary>
        float MusicVolume { get; }

        /// <summary>
        /// Gets the UI volume [0...1].
        /// </summary>
        float UIVolume { get; }
    }
}
