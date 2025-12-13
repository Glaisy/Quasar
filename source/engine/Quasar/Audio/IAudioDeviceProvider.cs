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
        /// Gets all available output devices in the system.
        /// </summary>
        IReadOnlyList<IAudioDevice> GetOutputDevices();

        /// <summary>
        /// Gets the active output device.
        /// </summary>
        IAudioDevice GetActiveOutputDevice();


        /// <summary>
        /// Initializes tha audio device provider.
        /// </summary>
        internal void Initialize();
    }
}
