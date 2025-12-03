//-----------------------------------------------------------------------
// <copyright file="AudioPlatform.cs" company="Space Development">
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
    /// Audio platform type enumeration.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AudioPlatform
    {
        /// <summary>
        /// The unknown audio platform.
        /// </summary>
        Unknown,

        /// <summary>
        /// The OpenAL audio platform.
        /// </summary>
        OpenAL
    }
}
