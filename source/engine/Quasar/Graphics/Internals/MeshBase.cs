//-----------------------------------------------------------------------
// <copyright file="MeshBase.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Space.Core;

namespace Quasar.Graphics.Internals
{
    /// <summary>
    /// Abstract base class for mesh implementations.
    /// </summary>
    /// <seealso cref="DisposableBase" />
    /// <seealso cref="IMesh" />
    internal abstract class MeshBase : GraphicsResourceBase, IMesh
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeshBase" /> class.
        /// </summary>
        /// <param name="primitiveType">Type of the primitive.</param>
        /// <param name="vertexLayout">The vertex layout.</param>
        /// <param name="name">The name.</param>
        /// <param name="isIndexed">The indexed mesh flag.</param>
        /// <param name="descriptor">The descriptor.</param>
        protected MeshBase(
            PrimitiveType primitiveType,
            VertexLayout vertexLayout,
            bool isIndexed,
            string name,
            in GraphicsResourceDescriptor descriptor)
            : base(descriptor)
        {
            PrimitiveType = primitiveType;
            VertexLayout = vertexLayout;

            Name = name;

            IsIndexed = isIndexed;
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            PrimitiveType = PrimitiveType.Triangle;
            VertexLayout = null;
            Name = null;
            IsIndexed = false;
        }


        /// <inheritdoc/>
        public bool IsIndexed { get; private set; }

        /// <inheritdoc/>
        public abstract IGraphicsBuffer IndexBuffer { get; }

        /// <inheritdoc/>
        public string Name { get; private set; }

        /// <inheritdoc/>
        public PrimitiveType PrimitiveType { get; private set; }

        /// <inheritdoc/>
        public abstract IGraphicsBuffer VertexBuffer { get; }

        /// <inheritdoc/>
        public VertexLayout VertexLayout { get; private set; }
    }
}
