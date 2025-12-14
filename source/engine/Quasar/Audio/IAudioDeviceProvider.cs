//-----------------------------------------------------------------------
// <copyright file="IAudioDeviceProvider.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace Quasar.Audio
{
    /// <summary>
    /// Represents the audio device provider.
    /// </summary>
    public interface IAudioDeviceProvider
    {
        /// <summary>
        /// Gets the default audio input device.
        /// </summary>
        IAudioInputDevice DefaultInputDevice { get; }

        /// <summary>
        /// Gets the default audio output device.
        /// </summary>
        IAudioOutputDevice DefaultOutputDevice { get; }

        /// <summary>
        /// Gets the list of available audio input devices.
        /// </summary>
        IReadOnlyList<IAudioInputDevice> InputDevices { get; }

        /// <summary>
        /// Gets the list of available audio output devices.
        /// </summary>
        IReadOnlyList<IAudioOutputDevice> OutputDevices { get; }
    }
}
