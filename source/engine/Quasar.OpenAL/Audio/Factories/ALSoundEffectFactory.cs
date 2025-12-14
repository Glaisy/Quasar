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
    [Export]
    [Singleton]
    internal sealed unsafe class ALSoundEffectFactory : ISoundEffectFactory
    {
        private IAudioDeviceContext audioDeviceContext;


        /// <inheritdoc/>
        public SoundEffectBase Create(string id, in AudioFormat format, byte[] pcmData)
        {
            ALSoundEffect soundEffect = null;
            try
            {
                soundEffect = new ALSoundEffect(id, audioDeviceContext.OutputDevice);

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

        /// <summary>
        /// Executes the sound effect factory initialization.
        /// </summary>
        /// <param name="audioDeviceContext">The audio device context.</param>
        public void Initialize(IAudioDeviceContext audioDeviceContext)
        {
            this.audioDeviceContext = audioDeviceContext;
        }
    }
}
