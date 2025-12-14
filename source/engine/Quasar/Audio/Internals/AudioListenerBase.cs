//-----------------------------------------------------------------------
// <copyright file="AudioListenerBase.cs" company="Space Development">
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
    /// Abstract base class for audio listener implementations.
    /// </summary>
    /// <seealso cref="AudioResourceBase" />
    /// <seealso cref="IAudioListener" />
    internal abstract class AudioListenerBase : AudioResourceBase, IAudioListener
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioListenerBase"/> class.
        /// </summary>
        /// <param name="audioDevice">The audio device.</param>
        protected AudioListenerBase(IAudioOutputDevice audioDevice)
            : base(audioDevice)
        {
        }


        /// <inheritdoc/>
        public abstract AttenuationType AttenuationType { get; set; }

        /// <inheritdoc/>
        public abstract Vector3 Forward { get; set; }

        /// <inheritdoc/>
        public abstract Vector3 Position { get; set; }

        /// <inheritdoc/>
        public abstract Vector3 Right { get; }

        /// <inheritdoc/>
        public abstract Vector3 Up { get; set; }

        /// <inheritdoc/>
        public abstract Vector3 Velocity { get; set; }

        /// <inheritdoc/>
        public abstract float Volume { get; set; }
    }
}
