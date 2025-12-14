//-----------------------------------------------------------------------
// <copyright file="IAudioDeviceContextFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Audio.Internals
{
    /// <summary>
    /// Represents the audio device context factory component.
    /// </summary>
    internal interface IAudioDeviceContextFactory
    {
        /// <summary>
        /// Creates the audio device context by the specified audio platform.
        /// </summary>
        /// <param name="audioPlatform">The audio platform.</param>
        /// <returns>The created audio device context instance.</returns>
        AudioDeviceContextBase Create(AudioPlatform audioPlatform);
    }
}
