//-----------------------------------------------------------------------
// <copyright file="ALAudioListener.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Audio;
using Quasar.Audio.Internals;
using Quasar.OpenAL.Api;
using Quasar.OpenAL.Extensions;

using Space.Core;

namespace Quasar.OpenAL.Internals.Audio
{
    /// <summary>
    /// Open AL audio listener implementation.
    /// </summary>
    /// <seealso cref="AudioListenerBase" />
    internal sealed class ALAudioListener : AudioListenerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ALAudioListener" /> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <param name="audioDevice">The audio device.</param>
        public ALAudioListener(int handle, IAudioOutputDevice audioDevice)
            : base(audioDevice)
        {
            this.handle = handle;

            AttenuationType = AttenuationType.Exponential;
            SetOrientation();
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            handle = 0;
            attenuationType = AttenuationType.None;
            forward = right = up = position = velocity = Vector3.Zero;
            volume = 0.0f;
        }


        private AttenuationType attenuationType = AttenuationType.None;
        /// <inheritdoc/>
        public override AttenuationType AttenuationType
        {
            get => attenuationType;
            set
            {
                if (attenuationType == value)
                {
                    return;
                }

                attenuationType = value;
                AL.DistanceModel(attenuationType.ToDistanceModel());
            }
        }

        private Vector3 forward = Vector3.PositiveZ;
        /// <inheritdoc/>
        public override Vector3 Forward
        {
            get => forward;
            set
            {
                if (forward == value)
                {
                    return;
                }

                forward = value;
                SetOrientation();
            }
        }

        private int handle;
        /// <inheritdoc/>
        public override int Handle => handle;

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
                AL.Listener3f(ListenerProperty.Position, value.X, value.Y, value.Z);
            }
        }

        private Vector3 right = Vector3.PositiveX;
        /// <inheritdoc/>
        public override Vector3 Right => right;

        private Vector3 up = Vector3.PositiveY;
        /// <inheritdoc/>
        public override Vector3 Up
        {
            get => up;
            set
            {
                if (up == value)
                {
                    return;
                }

                up = value;
                SetOrientation();
            }
        }

        private Vector3 velocity;
        /// <inheritdoc/>
        public override Vector3 Velocity
        {
            get => velocity;
            set
            {
                if (velocity == value)
                {
                    return;
                }

                velocity = value;
                AL.Listener3f(ListenerProperty.Velocity, value.X, value.Y, value.Z);
            }
        }

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
                AL.Listenerf(ListenerProperty.Gain, value);
            }
        }


        private void SetOrientation()
        {
            right = forward.Cross(up);

            AL.Listenerfv(
                ListenerProperty.Orientation,
                forward.X,
                forward.Y,
                forward.Z,
                up.X,
                up.Y,
                up.Z);
        }
    }
}
