//-----------------------------------------------------------------------
// <copyright file="ShaderPropertyType.cs" company="Space Development">
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
    /// Shader property type enumeration.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ShaderPropertyType
    {
        /// <summary>
        /// The unknown property type.
        /// </summary>
        Unknown,

        /// <summary>
        /// The color property type.
        /// </summary>
        Color,

        /// <summary>
        /// The cube map texture type.
        /// </summary>
        CubeMapTexture,

        /// <summary>
        /// The float type.
        /// </summary>
        Float,

        /// <summary>
        /// The integer type.
        /// </summary>
        Integer,

        /// <summary>
        /// The 4x4 matrix type.
        /// </summary>
        Matrix4,

        /// <summary>
        /// The normal map texture type.
        /// </summary>
        NormalMapTexture,

        /// <summary>
        /// The texture type.
        /// </summary>
        Texture,

        /// <summary>
        /// The Vector2 type.
        /// </summary>
        Vector2,

        /// <summary>
        /// The Vector3 type.
        /// </summary>
        Vector3,

        /// <summary>
        /// The Vector4 type.
        /// </summary>
        Vector4
    }
}
