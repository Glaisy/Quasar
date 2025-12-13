//-----------------------------------------------------------------------
// <copyright file="ALAudioListenerProvider.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;

using Quasar.Audio;
using Quasar.Audio.Internals;
using Quasar.OpenAL.Internals.Audio;

using Space.Core.DependencyInjection;

namespace Quasar.OpenAL.Audio
{
    /// <summary>
    /// OpenAL audio listener provider implementation.
    /// </summary>
    /// <seealso cref="IAudioListenerProvider" />
    [Export(typeof(IAudioListenerProvider), AudioPlatform.OpenAL)]
    internal sealed class ALAudioListenerProvider : IAudioListenerProvider
    {
        private readonly ALAudioListener audioListener;


        /// <summary>
        /// Initializes a new instance of the <see cref="ALAudioListenerProvider"/> class.
        /// </summary>
        /// <param name="audioDeviceProvider">The audio device provider.</param>
        public ALAudioListenerProvider([FromKeyedServices(AudioPlatform.OpenAL)] IAudioDeviceProvider audioDeviceProvider)
        {
            var outputDevice = audioDeviceProvider.GetActiveOutputDevice();
            audioListener = new ALAudioListener(outputDevice);
        }


        /// <inheritdoc/>
        public AudioListenerBase GetListener()
        {
            return audioListener;
        }
    }
}
