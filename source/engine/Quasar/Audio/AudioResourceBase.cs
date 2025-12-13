//-----------------------------------------------------------------------
// <copyright file="AudioResourceBase.cs" company="Space Development">
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
    /// Abstract base class for audio resource implementations.
    /// </summary>
    /// <seealso cref="DisposableBase" />
    /// <seealso cref="IAudioResource" />
    public abstract class AudioResourceBase : DisposableBase, IAudioResource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioResourceBase" /> class.
        /// </summary>
        /// <param name="audioDevice">The audio device.</param>
        protected AudioResourceBase(IAudioDevice audioDevice)
        {
            AudioDevice = audioDevice;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            AudioDevice = null;
        }


        /// <summary>
        /// Gets the audio device.
        /// </summary>
        public IAudioDevice AudioDevice { get; private set; }

        /// <summary>
        /// Gets the internal resource handle.
        /// </summary>
        public abstract int Handle { get; }


        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is not IAudioResource other)
            {
                return false;
            }

            return other != null && Handle == other.Handle;
        }

        /// <inheritdoc/>
        public bool Equals(IAudioResource other)
        {
            return other != null && Handle == other.Handle;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            return Handle;
        }
    }
}
