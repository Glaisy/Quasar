//-----------------------------------------------------------------------
// <copyright file="TextureRepeatMode.cs" company="Space Development">
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
    /// Texture repeat mode.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TextureRepeatMode
    {
        /// <summary>
        /// The clamped mode (default).
        /// </summary>
        Clamped,

        /// <summary>
        /// The repeat mode.
        /// </summary>
        Repeat,

        /// <summary>
        /// The mirror mode.
        /// </summary>
        Mirror
    }
}
