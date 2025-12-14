//-----------------------------------------------------------------------
// <copyright file="AudioOutputDevice.cs" company="Space Development">
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
    /// Generic implementation for audio output devices.
    /// </summary>
    /// <seealso cref="AudioDeviceBase" />
    /// <seealso cref="IAudioOutputDevice" />
    internal sealed class AudioOutputDevice : AudioDeviceBase, IAudioOutputDevice
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioOutputDevice" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="vendor">The vendor.</param>
        public AudioOutputDevice(
            string name,
            string vendor)
            : base(name, vendor)
        {
        }
    }
}
