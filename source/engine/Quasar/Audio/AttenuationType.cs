//-----------------------------------------------------------------------
// <copyright file="AttenuationType.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Quasar.Audio
{
    /// <summary>
    /// Audio attenuation type enumeration.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AttenuationType
    {
        /// <summary>
        /// The no attenuation type.
        /// </summary>
        None,

        /// <summary>
        /// The exponential attenuation type.
        /// </summary>
        Exponential,

        /// <summary>
        /// The exponential clamped attenuation type.
        /// </summary>
        ExponentialClamped,

        /// <summary>
        /// The linear attenuation type.
        /// </summary>
        Linear,

        /// <summary>
        /// The linear clamped attenuation type.
        /// </summary>
        LinearClamped
    }
}
