//-----------------------------------------------------------------------
// <copyright file="ALAudioSourceFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Quasar.Audio;

using Space.Core.DependencyInjection;

namespace Quasar.OpenAL.Internals.Audio.Factories
{
    /// <summary>
    /// OpenAL audio source factory implementation.
    /// </summary>
    /// <seealso cref="IAudioSourceFactory" />
    [Export(typeof(IAudioSourceFactory), AudioPlatform.OpenAL)]
    [Singleton]
    internal sealed class ALAudioSourceFactory : IAudioSourceFactory
    {
        private readonly IAudioDevice outputDevice;


        /// <summary>
        /// Initializes a new instance of the <see cref="ALAudioSourceFactory"/> class.
        /// </summary>
        /// <param name="audioDeviceProvider">The audio device provider.</param>
        public ALAudioSourceFactory(IAudioDeviceProvider audioDeviceProvider)
        {
            outputDevice = audioDeviceProvider.GetActiveOutputDevice();
        }


        /// <inheritdoc/>
        public IAudioSource Create(string id)
        {
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));

            return new ALAudioSource(id, outputDevice);
        }
    }
}
