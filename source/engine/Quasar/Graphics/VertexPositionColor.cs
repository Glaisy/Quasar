//-----------------------------------------------------------------------
// <copyright file="VertexPositionColor.cs" company="Space Development">
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
    /// Vertex structure with position and color elements.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VertexPositionColor
    {
        /// <summary>
        /// Initializes static members of the <see cref="VertexPositionColor"/> struct.
        /// </summary>
        static VertexPositionColor()
        {
            Layout = new VertexLayout(nameof(VertexPositionColor))
                .AddElement<Vector3>(VertexSemanticNames.Position)
                .AddElement<Color>(VertexSemanticNames.Color)
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
        /// The color.
        /// </summary>
        public Color Color;
    }
}
