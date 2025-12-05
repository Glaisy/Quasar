//-----------------------------------------------------------------------
// <copyright file="DepthTestMode.cs" company="Space Development">
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
    /// Depth test mode enumeration.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DepthTestMode
    {
        /// <summary>
        /// The none mode.
        /// </summary>
        None,

        /// <summary>
        /// The fragment always passes.
        /// </summary>
        Always,

        /// <summary>
        /// The fragment never passes.
        /// </summary>
        Never,

        /// <summary>
        /// The fragment passes if the depth value is less than the stored depth value.
        /// </summary>
        Less,

        /// <summary>
        /// The fragment passes if the depth value is less or equal to the stored depth value.
        /// </summary>
        LessOrEqual,

        /// <summary>
        /// The fragment passes if the depth value is greater than the stored depth value.
        /// </summary>
        Greater
    }
}
