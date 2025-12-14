//-----------------------------------------------------------------------
// <copyright file="ALSoundEffect.cs" company="Space Development">
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
using Quasar.OpenAL.Api;

namespace Quasar.OpenAL.Internals.Audio
{
    /// <summary>
    /// OpenAL sound effect implementation.
    /// </summary>
    /// <seealso cref="SoundEffectBase" />
    internal sealed class ALSoundEffect : SoundEffectBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ALSoundEffect" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="audioOutputDevice">The audio output device.</param>
        public ALSoundEffect(string id, IAudioOutputDevice audioOutputDevice)
            : base(id, audioOutputDevice)
        {
            handle = AL.GenBuffer();
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (handle > 0)
            {
                AL.DeleteBuffer(handle);
                handle = 0;
            }

            format = default;
            sampleCount = 0;
        }


        private AudioFormat format;
        /// <inheritdoc/>
        public override AudioFormat Format => format;

        private int handle;
        /// <inheritdoc/>
        public override int Handle => handle;

        private int sampleCount;
        /// <inheritdoc/>
        public override int SampleCount => sampleCount;


        /// <summary>
        /// Loads the PCM data to the buffer.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="pcmData">The PCM data.</param>
        /// <param name="size">The size.</param>
        public void LoadData(in AudioFormat format, IntPtr pcmData, int size)
        {
            BufferFormat bufferFormat;
            if (format.BitsPerSample == 8)
            {
                bufferFormat = format.Channels == 1 ? BufferFormat.Mono8Bits : BufferFormat.Stereo8Bits;
            }
            else
            {
                bufferFormat = format.Channels == 1 ? BufferFormat.Mono16Bits : BufferFormat.Stereo16Bits;
            }

            AL.BufferData(handle, bufferFormat, pcmData, size, format.SampleRate);
        }
    }
}
