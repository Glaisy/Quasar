//-----------------------------------------------------------------------
// <copyright file="VertexElement.cs" company="Space Development">
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
    /// Represents a Vertex element descriptor structure (Immutable).
    /// </summary>
    public readonly struct VertexElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VertexElement" /> struct.
        /// </summary>
        /// <param name="semanticName">The semantic name.</param>
        /// <param name="semanticIndex">The semantic index.</param>
        /// <param name="size">The size in bytes.</param>
        /// <param name="offset">The offset.</param>
        internal VertexElement(string semanticName, int semanticIndex, int size, int offset)
        {
            SemanticName = semanticName;
            SemanticIndex = semanticIndex;
            Size = size;
            Offset = offset;
        }


        /// <summary>
        /// The offset of the element's first byte in the vertex structure.
        /// </summary>
        public readonly int Offset;

        /// <summary>
        /// The element size in bytes.
        /// </summary>
        public readonly int Size;

        /// <summary>
        /// The element's semantic name.
        /// </summary>
        public readonly string SemanticName;

        /// <summary>
        /// The element's semantic index (for multiple indices for semantics).
        /// </summary>
        public readonly int SemanticIndex;
    }
}
