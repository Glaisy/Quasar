//-----------------------------------------------------------------------
// <copyright file="IMesh.cs" company="Space Development">
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
    /// Represents a raw mesh object.
    /// </summary>
    /// <seealso cref="IGraphicsResource" />
    public interface IMesh : IGraphicsResource
    {
        /// <summary>
        /// Gets a value indicating whether the index buffer is used or not.
        /// </summary>
        bool IsIndexed { get; }

        /// <summary>
        /// Gets the index buffer.
        /// </summary>
        IGraphicsBuffer IndexBuffer { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the type of the mesh primitives.
        /// </summary>
        PrimitiveType PrimitiveType { get; }

        /// <summary>
        /// Gets the vertex buffer.
        /// </summary>
        IGraphicsBuffer VertexBuffer { get; }

        /// <summary>
        /// Gets the vertex layout.
        /// </summary>
        VertexLayout VertexLayout { get; }
    }
}
