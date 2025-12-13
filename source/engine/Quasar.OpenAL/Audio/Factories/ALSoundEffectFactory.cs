//-----------------------------------------------------------------------
// <copyright file="ALSoundEffectFactory.cs" company="Space Development">
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
using Quasar.Audio.Internals;
using Quasar.Audio.Internals.Factories;

using Space.Core.DependencyInjection;

namespace Quasar.OpenAL.Internals.Audio.Factories
{
    /// <summary>
    /// OpenAL sound effect factory implementation.
    /// </summary>
    /// <seealso cref="ISoundEffectFactory" />
    [Export(typeof(ISoundEffectFactory), AudioPlatform.OpenAL)]
    [Singleton]
    internal sealed unsafe class ALSoundEffectFactory : ISoundEffectFactory
    {
        private readonly IAudioDevice outputDevice;


        /// <summary>
        /// Initializes a new instance of the <see cref="ALSoundEffectFactory"/> class.
        /// </summary>
        /// <param name="audioDeviceProvider">The audio device provider.</param>
        public ALSoundEffectFactory(IAudioDeviceProvider audioDeviceProvider)
        {
            outputDevice = audioDeviceProvider.GetActiveOutputDevice();
        }


        /// <inheritdoc/>
        public SoundEffectBase Create(string id, in AudioFormat format, byte[] pcmData)
        {
            ALSoundEffect soundEffect = null;
            try
            {
                soundEffect = new ALSoundEffect(id, outputDevice);

                fixed (byte* data = pcmData)
                {
                    soundEffect.LoadData(format, new IntPtr(data), pcmData.Length);
                }

                return soundEffect;
            }
            catch
            {
                soundEffect?.Dispose();

                throw;
            }
        }
    }
}
