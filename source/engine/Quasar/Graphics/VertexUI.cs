//-----------------------------------------------------------------------
// <copyright file="VertexUI.cs" company="Space Development">
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
    /// Vertex structure with UI position and texture coordinate elements.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VertexUI
    {
        /// <summary>
        /// Initializes static members of the <see cref="VertexUI"/> struct.
        /// </summary>
        static VertexUI()
        {
            Layout = new VertexLayout(nameof(VertexUI))
                .AddElement<Vector2>(VertexSemanticNames.Position)
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
        public Vector2 Position;

        /// <summary>
        /// The texture coordinate.
        /// </summary>
        public Vector2 UV;
    }
}
