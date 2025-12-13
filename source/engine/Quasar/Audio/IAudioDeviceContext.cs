//-----------------------------------------------------------------------
// <copyright file="IAudioDeviceContext.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Audio
{
    /// <summary>
    /// Generic audio device context interface definition.
    /// </summary>
    public interface IAudioDeviceContext
    {
        /// <summary>
        /// Gets the audio output device.
        /// </summary>
        IAudioDevice OutputDevice { get; }

        /// <summary>
        /// Gets the audio platform.
        /// </summary>
        AudioPlatform Platform { get; }

        /// <summary>
        /// Gets the audio platform version.
        /// </summary>
        Version Version { get; }


        /// <summary>
        /// Initializes tha context.
        /// </summary>
        internal void Initialize();
    }
}
