//-----------------------------------------------------------------------
// <copyright file="GLMesh.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Quasar.Graphics;
using Quasar.Graphics.Internals;

namespace Quasar.OpenGL.Graphics
{
    /// <summary>
    /// OpenGL mesh object implementation.
    /// </summary>
    /// <seealso cref="MeshBase" />
    internal sealed class GLMesh : MeshBase
    {
        private int vaoId;


        /// <summary>
        /// Initializes a new instance of the <see cref="GLMesh" /> class.
        /// </summary>
        /// <param name="primitiveType">Type of the primitive.</param>
        /// <param name="vertexLayout">The vertex layout.</param>
        /// <param name="isIndexed">The indexed mesh flag.</param>
        /// <param name="name">The name.</param>
        /// <param name="descriptor">The descriptor.</param>
        public GLMesh(
            PrimitiveType primitiveType,
            VertexLayout vertexLayout,
            bool isIndexed,
            string name,
            in GraphicsResourceDescriptor descriptor)
            : base(primitiveType, vertexLayout, isIndexed, name, descriptor)
        {
            vaoId = Api.GL.GenBuffer();
            vertexBuffer = new GLVertexBuffer(vaoId, descriptor);
            if (isIndexed)
            {
                indexBuffer = new GLIndexBuffer(vaoId, descriptor);
            }
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (indexBuffer != null)
            {
                indexBuffer.Dispose();
                indexBuffer = null;
            }

            if (vertexBuffer != null)
            {
                vertexBuffer.Dispose();
                vertexBuffer = null;
            }

            if (vaoId > 0)
            {
                Api.GL.DeleteBuffer(vaoId);
                vaoId = 0;
            }

            base.Dispose(disposing);
        }


        /// <inheritdoc/>
        public override int Handle => vaoId;

        private GLIndexBuffer indexBuffer;
        /// <inheritdoc/>
        public override IGraphicsBuffer IndexBuffer => indexBuffer;

        private GLVertexBuffer vertexBuffer;
        /// <inheritdoc/>
        public override IGraphicsBuffer VertexBuffer => vertexBuffer;


        /// <inheritdoc/>
        internal override void Activate()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        internal override void Deactivate()
        {
            throw new NotImplementedException();
        }
    }
}
