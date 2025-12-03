//-----------------------------------------------------------------------
// <copyright file="VSyncMode.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Quasar.Graphics
{
    /// <summary>
    /// V-Sync mode enumeration.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum VSyncMode
    {
        /// <summary>
        /// The V-Sync is turned off.
        /// </summary>
        Off,

        /// <summary>
        /// The V-Sync is turned on.
        /// </summary>
        On
    }
}
