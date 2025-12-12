//-----------------------------------------------------------------------
// <copyright file="ISoundEffectFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Audio.Internals.Factories
{
    /// <summary>
    /// Sound effect factory implementation.
    /// </summary>
    internal interface ISoundEffectFactory
    {
        /// <summary>
        /// Creates a new sound effect.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="format">The format.</param>
        /// <param name="pcmData">The PCM data.</param>
        /// <returns>
        /// The sound effect.
        /// </returns>
        SoundEffectBase Create(string id, in AudioFormat format, byte[] pcmData);
    }
}
