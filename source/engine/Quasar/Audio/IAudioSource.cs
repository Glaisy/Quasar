//-----------------------------------------------------------------------
// <copyright file="IAudioSource.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Space.Core;

namespace Quasar.Audio
{
    /// <summary>
    /// Represents an audio source object.
    /// </summary>
    /// <seealso cref="IAudioResource" />
    /// <seealso cref="IIdentifierProvider{String}" />
    /// <seealso cref="IDisposable" />
    public interface IAudioSource : IAudioResource, IIdentifierProvider<string>, IDisposable
    {
        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        Vector3 Direction { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the looping is active or not.
        /// </summary>
        bool Looping { get; set; }

        /// <summary>
        /// Gets or sets the pitch [0...+Inf] (Default 1.0).
        /// </summary>
        float Pitch { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        Vector3 Position { get; set; }

        /// <summary>
        /// Gets the attenuation profile.
        /// </summary>
        IAttenuationProfile Profile { get; }

        /// <summary>
        /// Gets or sets the volume [0...1].
        /// </summary>
        float Volume { get; set; }


        /// <summary>
        /// Plays the specified sound effect.
        /// </summary>
        /// <param name="soundEffect">The sound effect.</param>
        void Play(ISoundEffect soundEffect);

        /// <summary>
        /// Stops playing the currently played sound effect.
        /// </summary>
        void Stop();
    }
}
