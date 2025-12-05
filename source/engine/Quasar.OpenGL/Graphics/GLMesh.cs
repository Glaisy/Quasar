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

using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.OpenGL.Api;
using Quasar.OpenGL.Extensions;

namespace Quasar.OpenGL.Graphics
{
    /// <summary>
    /// OpenGL mesh object implementation.
    /// </summary>
    /// <seealso cref="MeshBase" />
    internal sealed class GLMesh : MeshBase
    {
        private int handle;
        private int internalPrimitiveType;


        /// <summary>
        /// Initializes a new instance of the <see cref="GLMesh" /> class.
        /// </summary>
        /// <param name="primitiveType">Type of the primitive.</param>
        /// <param name="vertexLayout">The vertex layout.</param>
        /// <param name="isIndexed">The indexed mesh flag.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="descriptor">The descriptor.</param>
        public GLMesh(
            Quasar.Graphics.PrimitiveType primitiveType,
            VertexLayout vertexLayout,
            bool isIndexed,
            string id,
            in GraphicsResourceDescriptor descriptor)
            : base(primitiveType, vertexLayout, isIndexed, id, descriptor)
        {
            // create VAO
            handle = GL.GenVertexArray();
            GL.BindVertexArray(handle);

            // create vertex buffer
            vertexBuffer = new GLVertexBuffer(handle, descriptor);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer.Handle);

            var index = 0;
            foreach (var element in vertexLayout.Elements)
            {
                GL.EnableVertexAttribArray(index);
                GL.VertexAttribPointer(
                    index,
                    element.Size >> 2,
                    VertexAttributePointerType.Float,
                    false,
                    vertexLayout.Stride,
                    element.Offset);
                index++;
            }

            // create index buffer
            if (isIndexed)
            {
                internalPrimitiveType = (int)primitiveType.ToBeginMode();
                indexBuffer = new GLIndexBuffer(handle, descriptor);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBuffer.Handle);
            }
            else
            {
                internalPrimitiveType = (int)primitiveType.ToPrimitiveType();
            }

            // unbind vao
            GL.BindVertexArray(0);
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

            if (handle > 0)
            {
                GL.DeleteVertexArray(handle);
                handle = 0;
            }

            internalPrimitiveType = 0;

            base.Dispose(disposing);
        }


        /// <inheritdoc/>
        public override int Handle => handle;

        private GLIndexBuffer indexBuffer;
        /// <inheritdoc/>
        public override IGraphicsBuffer IndexBuffer => indexBuffer;

        /// <inheritdoc/>
        public override int InternalPrimitiveType => internalPrimitiveType;

        private GLVertexBuffer vertexBuffer;
        /// <inheritdoc/>
        public override IGraphicsBuffer VertexBuffer => vertexBuffer;



        /// <inheritdoc/>
        internal override void Activate()
        {
            GL.BindVertexArray(handle);
        }

        /// <inheritdoc/>
        internal override void Deactivate()
        {
            GL.BindVertexArray(0);
        }
    }
}
