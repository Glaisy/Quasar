//-----------------------------------------------------------------------
// <copyright file="SoundEffectBase.cs" company="Space Development">
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
    /// Abstract base class implementation for sound effects.
    /// </summary>
    /// <seealso cref="AudioResourceBase" />
    /// <seealso cref="ISoundEffect" />
    internal abstract class SoundEffectBase : AudioResourceBase, ISoundEffect
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SoundEffectBase" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="audioDevice">The audio device.</param>
        protected SoundEffectBase(string id, IAudioDevice audioDevice)
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
        public abstract AudioFormat Format { get; }

        /// <inheritdoc/>
        public string Id { get; private set; }

        /// <inheritdoc/>
        public abstract int SampleCount { get; }
    }
}
