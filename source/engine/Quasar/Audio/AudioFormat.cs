//-----------------------------------------------------------------------
// <copyright file="AudioFormat.cs" company="Space Development">
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
    /// Audio format structure.
    /// </summary>
    public readonly struct AudioFormat
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFormat"/> struct.
        /// </summary>
        /// <param name="sampleRate">The sample rate.</param>
        /// <param name="channels">The channels.</param>
        /// <param name="bitsPerSample">The bits per sample.</param>
        public AudioFormat(int sampleRate, int channels, int bitsPerSample)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(sampleRate, nameof(sampleRate));

            if (channels != 1 && channels != 2)
            {
                throw new ArgumentOutOfRangeException(nameof(channels), "The value should be 1 or 2.");
            }

            if (bitsPerSample != 8 && bitsPerSample != 16)
            {
                throw new ArgumentOutOfRangeException(nameof(bitsPerSample), "The value should be 8 or 16.");
            }

            SampleRate = sampleRate;
            Channels = channels;
            BitsPerSample = bitsPerSample;
        }


        /// <summary>
        /// The bits per sample.
        /// </summary>
        public readonly int BitsPerSample;

        /// <summary>
        /// The channels.
        /// </summary>
        public readonly int Channels;

        /// <summary>
        /// The sample rate.
        /// </summary>
        public readonly int SampleRate;
    }
}
