//-----------------------------------------------------------------------
// <copyright file="IAttenuationProfile.cs" company="Space Development">
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
    /// Represents an attenuation profile for audio sources.
    /// </summary>
    public interface IAttenuationProfile
    {
        /// <summary>
        /// Gets or sets the attenuation rate [0...+Inf].
        /// </summary>
        float AttenuationRate { get; set; }

        /// <summary>
        /// Gets or sets the maximum distance [0...+Inf].
        /// </summary>
        float MaximumDistance { get; set; }

        /// <summary>
        /// Gets or sets the reference distance [0...+Inf].
        /// </summary>
        float ReferenceDistance { get; set; }
    }
}
