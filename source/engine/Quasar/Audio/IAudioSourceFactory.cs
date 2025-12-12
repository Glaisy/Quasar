//-----------------------------------------------------------------------
// <copyright file="IAudioSourceFactory.cs" company="Space Development">
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
    /// Represents the audio source factory component.
    /// </summary>
    public interface IAudioSourceFactory
    {
        /// <summary>
        /// Creates a new audio source instance by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The created audio source instance.
        /// </returns>
        IAudioSource Create(string id);
    }
}
