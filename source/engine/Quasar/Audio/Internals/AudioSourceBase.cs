//-----------------------------------------------------------------------
// <copyright file="AudioSourceBase.cs" company="Space Development">
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
    /// Abstract base class for audio source implementations.
    /// </summary>
    /// <seealso cref="AudioResourceBase" />
    /// <seealso cref="IAudioSource" />
    internal abstract class AudioSourceBase : AudioResourceBase, IAudioSource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioSourceBase" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="audioDevice">The audio device.</param>
        protected AudioSourceBase(string id, IAudioDevice audioDevice)
            : base(audioDevice)
        {
            Id = id;
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Id = null;
        }


        /// <inheritdoc/>
        public abstract Vector3 Direction { get; set; }

        /// <inheritdoc/>
        public string Id { get; private set; }

        /// <inheritdoc/>
        public abstract bool Looping { get; set; }

        /// <inheritdoc/>
        public abstract float Pitch { get; set; }

        /// <inheritdoc/>
        public abstract Vector3 Position { get; set; }

        /// <inheritdoc/>
        public abstract IAttenuationProfile Profile { get; }

        /// <inheritdoc/>
        public abstract float Volume { get; set; }


        /// <inheritdoc/>
        public abstract void Play(ISoundEffect soundEffect);

        /// <inheritdoc/>
        public abstract void Stop();
    }
}
