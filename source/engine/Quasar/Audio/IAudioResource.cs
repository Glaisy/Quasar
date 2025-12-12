//-----------------------------------------------------------------------
// <copyright file="IAudioResource.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Audio
{
    /// <summary>
    /// Represents an audio resource.
    /// </summary>
    /// <seealso cref="IEquatable{IAudioResource}" />
    public interface IAudioResource : IEquatable<IAudioResource>
    {
        /// <summary>
        /// Gets the audio device.
        /// </summary>
        IAudioDevice AudioDevice { get; }

        /// <summary>
        /// Gets the internal resource handle.
        /// </summary>
        int Handle { get; }
    }
}
