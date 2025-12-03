//-----------------------------------------------------------------------
// <copyright file="GraphicsPlatform.cs" company="Space Development">
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
    /// Graphics platform type enumeration.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GraphicsPlatform
    {
        /// <summary>
        /// The unknown graphics platform.
        /// </summary>
        Unknown,

        /// <summary>
        /// The OpenGL graphics platform.
        /// </summary>
        OpenGL,

        /// <summary>
        /// The Vulkan graphics platform.
        /// </summary>
        Vulkan
    }
}
