//-----------------------------------------------------------------------
// <copyright file="AudioDeviceContextBase.cs" company="Space Development">
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

namespace Quasar.Audio.Internals
{
    /// <summary>
    /// Abstract base class for audio device context implementations.
    /// </summary>
    /// <seealso cref="DisposableBase" />
    /// <seealso cref="IAudioDeviceContext" />
    internal abstract class AudioDeviceContextBase : DisposableBase, IAudioDeviceContext
    {
        /// <inheritdoc/>
        public abstract IAudioInputDevice InputDevice { get; }

        /// <inheritdoc/>
        public abstract IAudioOutputDevice OutputDevice { get; }

        /// <inheritdoc/>
        public abstract AudioPlatform Platform { get; }

        /// <inheritdoc/>
        public abstract Version Version { get; }


        /// <summary>
        /// Executes the audio device context initialization.
        /// </summary>
        public abstract void Initialize();
    }
}
