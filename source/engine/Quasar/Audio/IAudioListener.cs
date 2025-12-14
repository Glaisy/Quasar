//-----------------------------------------------------------------------
// <copyright file="IAudioListener.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Audio
{
    /// <summary>
    /// Represents the audio listener object.
    /// Defines the position and orientation of the
    /// entity which listens to the played music and sound effects.
    /// </summary>
    /// <seealso cref="IAudioResource" />
    public interface IAudioListener : IAudioResource
    {
        /// <summary>
        /// Gets or sets the attenuation type.
        /// </summary>
        AttenuationType AttenuationType { get; set; }

        /// <summary>
        /// Gets or sets the orientation's forward vector.
        /// </summary>
        Vector3 Forward { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        Vector3 Position { get; set; }

        /// <summary>
        /// Gets the orientation's right vector (Forward x Up).
        /// </summary>
        Vector3 Right { get; }

        /// <summary>
        /// Gets or sets the orientation's up vector.
        /// </summary>
        Vector3 Up { get; set; }

        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        Vector3 Velocity { get; set; }

        /// <summary>
        /// Gets or sets the volume [0...1].
        /// </summary>
        float Volume { get; set; }
    }
}
