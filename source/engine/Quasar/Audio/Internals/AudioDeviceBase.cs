//-----------------------------------------------------------------------
// <copyright file="AudioDeviceBase.cs" company="Space Development">
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
    /// Abstract base class for audio device implementations.
    /// </summary>
    /// <seealso cref="IAudioDevice" />
    internal abstract class AudioDeviceBase : IAudioDevice
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioDeviceBase" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="vendor">The vendor.</param>
        protected AudioDeviceBase(
            string name,
            string vendor)
        {
            Name = name;
            Vendor = vendor;
        }


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
