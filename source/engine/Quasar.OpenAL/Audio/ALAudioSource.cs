//-----------------------------------------------------------------------
// <copyright file="ALAudioSource.cs" company="Space Development">
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

using Space.Core;

namespace Quasar.OpenAL.Internals.Audio
{
    /// <summary>
    /// Open AL audio source implementation.
    /// </summary>
    /// <seealso cref="AudioSourceBase" />
    internal sealed class ALAudioSource : AudioSourceBase, IAttenuationProfile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ALAudioSource" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="audioDevice">The audio device.</param>
        public ALAudioSource(string id, IAudioOutputDevice audioDevice)
            : base(id, audioDevice)
        {
            handle = AL.GenSource();
            AL.Sourcef(handle, SourceProperty.MinimumGain, 0.0f);
            AL.Sourcef(handle, SourceProperty.MaximumGain, 1.0f);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (handle != 0)
            {
                AL.DeleteSource(handle);
                handle = 0;
            }

            direction = position = Vector3.Zero;
            looping = false;
            pitch = volume = 0.0f;
            attenuationRate = maximumDistance = referenceDistance = 0.0f;
        }


        private Vector3 direction;
        /// <inheritdoc/>
        public override Vector3 Direction
        {
            get => direction;
            set
            {
                if (direction == value)
                {
                    return;
                }

                direction = value;
                AL.Source3f(handle, SourceProperty.Direction, value.X, value.Y, value.Z);
            }
        }

        private int handle;
        /// <inheritdoc/>
        public override int Handle => handle;

        private bool looping = false;
        /// <inheritdoc/>
        public override bool Looping
        {
            get => looping;
            set
            {
                if (looping == value)
                {
                    return;
                }

                looping = value;
                AL.Sourcei(handle, SourceProperty.Looping, value ? 1 : 0);
            }
        }

        private float pitch = 1.0f;
        /// <inheritdoc/>
        public override float Pitch
        {
            get => pitch;
            set
            {
                value = Ranges.FloatPositive.Clamp(value);
                if (pitch == value)
                {
                    return;
                }

                pitch = value;
                AL.Sourcef(handle, SourceProperty.Pitch, value);
            }
        }

        private Vector3 position;
        /// <inheritdoc/>
        public override Vector3 Position
        {
            get => position;
            set
            {
                if (position == value)
                {
                    return;
                }

                position = value;
                AL.Source3f(handle, SourceProperty.Position, value.X, value.Y, value.Z);
            }
        }

        /// <inheritdoc/>
        public override IAttenuationProfile Profile => this;

        private float volume = 1.0f;
        /// <inheritdoc/>
        public override float Volume
        {
            get => volume;
            set
            {
                value = Ranges.FloatUnit.Clamp(value);
                if (volume == value)
                {
                    return;
                }

                volume = value;
                AL.Sourcef(handle, SourceProperty.Gain, value);
            }
        }


        #region IAttenuationProfile
        private float attenuationRate = 1.0f;
        /// <inheritdoc/>
        float IAttenuationProfile.AttenuationRate
        {
            get => attenuationRate;
            set
            {
                value = Ranges.FloatPositive.Clamp(value);
                if (attenuationRate == value)
                {
                    return;
                }

                attenuationRate = value;
                AL.Sourcef(handle, SourceProperty.RollOfFactor, value);
            }
        }

        private float maximumDistance = Int32.MaxValue;
        /// <inheritdoc/>
        float IAttenuationProfile.MaximumDistance
        {
            get => maximumDistance;
            set
            {
                value = Ranges.FloatPositive.Clamp(value);
                if (maximumDistance == value)
                {
                    return;
                }

                maximumDistance = value;
                AL.Sourcef(handle, SourceProperty.MaximumDistance, value);
            }
        }

        private float referenceDistance = 1.0f;
        /// <inheritdoc/>
        float IAttenuationProfile.ReferenceDistance
        {
            get => referenceDistance;
            set
            {
                value = Ranges.FloatPositive.Clamp(value);
                if (referenceDistance == value)
                {
                    return;
                }

                referenceDistance = value;
                AL.Sourcef(handle, SourceProperty.ReferenceDistance, value);
            }
        }
        #endregion


        /// <inheritdoc/>
        public override void Play(ISoundEffect soundEffect)
        {
            AL.Sourcei(handle, SourceProperty.Buffer, soundEffect.Handle);
            AL.SourcePlay(handle);
        }

        /// <inheritdoc/>
        public override void Stop()
        {
            AL.SourceStop(handle);
            AL.Sourcei(handle, SourceProperty.Buffer, 0);
        }
    }
}
