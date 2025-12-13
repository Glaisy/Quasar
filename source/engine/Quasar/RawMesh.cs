//-----------------------------------------------------------------------
// <copyright file="RawMesh.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics;

namespace Quasar
{
    /// <summary>
    /// Raw mesh data structure.
    /// </summary>
    public readonly struct RawMesh
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RawMesh"/> struct.
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        public RawMesh(IMesh mesh)
        {
            Handle = mesh.Handle;
            IsIndexed = mesh.IsIndexed;
            ElementCount = IsIndexed ? mesh.IndexBuffer.ElementCount : mesh.VertexBuffer.ElementCount;
            PrimitiveType = mesh.InternalPrimitiveType;
        }


        /// <summary>
        /// The element (vertex/index) count.
        /// </summary>
        public readonly int ElementCount;

        /// <summary>
        /// The handle.
        /// </summary>
        public readonly int Handle;

        /// <summary>
        /// The indexed flag.
        /// </summary>
        public readonly bool IsIndexed;

        /// <summary>
        /// The primitive type.
        /// </summary>
        public readonly int PrimitiveType;
    }
}
