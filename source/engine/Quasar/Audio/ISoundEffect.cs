//-----------------------------------------------------------------------
// <copyright file="ISoundEffect.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Space.Core;

namespace Quasar.Audio
{
    /// <summary>
    /// Represents a sound effect.
    /// </summary>
    /// <seealso cref="IAudioResource" />
    /// <seealso cref="IIdentifierProvider{String}" />
    public interface ISoundEffect : IAudioResource, IIdentifierProvider<string>
    {
        /// <summary>
        /// Gets the audio format.
        /// </summary>
        AudioFormat Format { get; }

        /// <summary>
        /// Gets the sample count.
        /// </summary>
        int SampleCount { get; }
    }
}
