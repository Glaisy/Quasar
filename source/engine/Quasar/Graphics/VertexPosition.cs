//-----------------------------------------------------------------------
// <copyright file="VertexPosition.cs" company="Space Development">
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
    /// Vertex structure with position element.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VertexPosition
    {
        /// <summary>
        /// Initializes static members of the <see cref="VertexPosition"/> struct.
        /// </summary>
        static VertexPosition()
        {
            Layout = new VertexLayout(nameof(VertexPosition))
                .AddElement<Vector3>(VertexSemanticNames.Position)
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
    }
}
