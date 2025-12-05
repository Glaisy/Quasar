//-----------------------------------------------------------------------
// <copyright file="CubeMapFace.cs" company="Space Development">
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
    /// CubeMap texture face enumeration.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CubeMapFace
    {
        /// <summary>
        /// The positive X.
        /// </summary>
        PositiveX,

        /// <summary>
        /// The negative X.
        /// </summary>
        NegativeX,

        /// <summary>
        /// The positive Y.
        /// </summary>
        PositiveY,

        /// <summary>
        /// The negative Y.
        /// </summary>
        NegativeY,

        /// <summary>
        /// The positive Z.
        /// </summary>
        PositiveZ,

        /// <summary>
        /// The negative Z.
        /// </summary>
        NegativeZ
    }
}
