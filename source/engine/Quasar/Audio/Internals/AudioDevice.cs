//-----------------------------------------------------------------------
// <copyright file="AudioDevice.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Audio.Internals
{
    /// <summary>
    /// Represents a generic audio device.
    /// </summary>
    /// <seealso cref="IAudioDevice" />
    internal sealed class AudioDevice : IAudioDevice
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioDevice" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="vendor">The vendor.</param>
        public AudioDevice(
            string id,
            string name,
            string vendor)
        {
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));

            Id = id;
            Name = name;
            Vendor = vendor;
        }


        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the vendor.
        /// </summary>
        public string Vendor { get; }
    }
}
