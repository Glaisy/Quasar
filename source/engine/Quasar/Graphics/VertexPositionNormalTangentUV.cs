//-----------------------------------------------------------------------
// <copyright file="VertexPositionNormalTangentUV.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Runtime.InteropServices;

namespace Quasar.Graphics
{
    /// <summary>
    /// Vertex structure with position, normal, tangent and texture coordinate elements.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VertexPositionNormalTangentUV
    {
        /// <summary>
        /// Initializes static members of the <see cref="VertexPositionNormalTangentUV"/> struct.
        /// </summary>
        static VertexPositionNormalTangentUV()
        {
            Layout = new VertexLayout(nameof(VertexPositionUV))
                .AddElement<Vector3>(VertexSemanticNames.Position)
                .AddElement<Vector3>(VertexSemanticNames.Normal)
                .AddElement<Vector3>(VertexSemanticNames.Tangent)
                .AddElement<Vector2>(VertexSemanticNames.UV)
                .AsReadOnly();
        }


        /// <summary>
        /// The vertex layout.
        /// </summary>
        public static readonly VertexLayout Layout;


        /// <summary>
        /// The position.
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// The normal.
        /// </summary>
        public Vector3 Normal;

        /// <summary>
        /// The tangent.
        /// </summary>
        public Vector3 Tangent;

        /// <summary>
        /// The texture coordinate.
        /// </summary>
        public Vector2 UV;
    }
}
