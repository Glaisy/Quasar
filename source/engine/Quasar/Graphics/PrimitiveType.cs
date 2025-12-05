//-----------------------------------------------------------------------
// <copyright file="PrimitiveType.cs" company="Space Development">
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
    /// Graphics primitive type enumeration.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PrimitiveType
    {
        /// <summary>
        /// The point type.
        /// </summary>
        Point = 0,

        /// <summary>
        /// The line type.
        /// </summary>
        Line = 1,

        /// <summary>
        /// The line with adjacency type.
        /// </summary>
        LineWithAdjacency = 2,

        /// <summary>
        /// The line strip type.
        /// </summary>
        LineStrip = 3,

        /// <summary>
        /// The line strip with adjacency type.
        /// </summary>
        LineStripWithAdjacency = 4,

        /// <summary>
        /// The line loop type.
        /// </summary>
        LineLoop = 5,

        /// <summary>
        /// The triangle type.
        /// </summary>
        Triangle = 6,

        /// <summary>
        /// The triangle strip type.
        /// </summary>
        TriangleStrip = 7,
    }
}
