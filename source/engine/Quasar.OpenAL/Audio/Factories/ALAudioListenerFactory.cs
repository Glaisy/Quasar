//-----------------------------------------------------------------------
// <copyright file="ALAudioListenerFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Threading;

using Quasar.Audio;
using Quasar.Audio.Internals;
using Quasar.Audio.Internals.Factories;
using Quasar.OpenAL.Internals.Audio;

using Space.Core.DependencyInjection;

namespace Quasar.OpenAL.Audio.Factories
{
    /// <summary>
    /// OpenAL audio listener factory implementation.
    /// </summary>
    /// <seealso cref="IAudioListenerFactory" />
    [Export]
    [Singleton]
    internal sealed class ALAudioListenerFactory : IAudioListenerFactory
    {
        private IAudioDeviceContext audioDeviceContext;
        private int currentHandle = 0;


        /// <inheritdoc/>
        public AudioListenerBase Create()
        {
            var handle = Interlocked.Increment(ref currentHandle);
            return new ALAudioListener(handle, audioDeviceContext.OutputDevice);
        }

        /// <summary>
        /// Executes the audio listener initialization.
        /// </summary>
        /// <param name="audioDeviceContext">The audio device context.</param>
        public void Initialize(IAudioDeviceContext audioDeviceContext)
        {
            this.audioDeviceContext = audioDeviceContext;
        }
    }
}
