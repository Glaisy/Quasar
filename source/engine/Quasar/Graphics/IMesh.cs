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

using System;

using Space.Core;

namespace Quasar.Graphics
{
    /// <summary>
    /// Represents a raw mesh object.
    /// </summary>
    /// <seealso cref="IGraphicsResource" />
    /// <seealso cref="IIdentifierProvider{String}" />
    /// <seealso cref="IDisposable" />
    public interface IMesh : IGraphicsResource, IIdentifierProvider<string>, IDisposable
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


        /// <summary>
        /// Gets the internal type of the primitives in the mesh.
        /// </summary>
        internal int InternalPrimitiveType { get; }


        /// <summary>
        /// Activates the mesh.
        /// </summary>
        internal void Activate();

        /// <summary>
        /// Deactivates the mesh.
        /// </summary>
        internal void Deactivate();
    }
}
