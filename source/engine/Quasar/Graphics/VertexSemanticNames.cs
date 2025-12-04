//-----------------------------------------------------------------------
// <copyright file="VertexSemanticNames.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Graphics
{
    /// <summary>
    /// Vertex element semantic name constants.
    /// </summary>
    public static class VertexSemanticNames
    {
        /// <summary>
        /// Vertex bitangent data.
        /// </summary>
        public static readonly string BiTangent = "BITANGENT";

        /// <summary>
        /// Vertex data contains diffuse or specular color.
        /// </summary>
        public static readonly string Color = "COLOR";

        /// <summary>
        /// Vertex normal data.
        /// </summary>
        public static readonly string Normal = "NORMAL";

        /// <summary>
        /// Position data.
        /// </summary>
        public static readonly string Position = "POSITION";

        /// <summary>
        /// Position transformed data.
        /// </summary>
        public static readonly string PositionTransformed = "SV_POSITION";

        /// <summary>
        /// Vertex tangent data.
        /// </summary>
        public static readonly string Tangent = "TANGENT";

        /// <summary>
        /// Texture coordinate data.
        /// </summary>
        public static readonly string UV = "TEXCOORD";
    }
}
