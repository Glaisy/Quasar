//-----------------------------------------------------------------------
// <copyright file="GLGraphicsBufferBase.cs" company="Space Development">
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
using Quasar.OpenGL.Api;
using Quasar.OpenGL.Extensions;

namespace Quasar.OpenGL.Internals.Graphics
{
    /// <summary>
    /// Abstract base class for OpenGL graphics buffer implementations.
    /// </summary>
    /// <seealso cref="GraphicsBufferBase" />
    internal abstract class GLGraphicsBufferBase : GraphicsBufferBase
    {
        private readonly BufferTarget target;
        private readonly BufferUsageHint usageHint;
        private readonly int vaoId;


        /// <summary>
        /// Initializes a new instance of the <see cref="GLGraphicsBufferBase" /> class.
        /// </summary>
        /// <param name="vaoId">The vertex array object identifier.</param>
        /// <param name="type">The type.</param>
        /// <param name="descriptor">The graphics resource descriptor.</param>
        protected GLGraphicsBufferBase(int vaoId, GraphicsBufferType type, in GraphicsResourceDescriptor descriptor)
            : base(type, descriptor)
        {
            this.vaoId = vaoId;

            handle = GL.GenBuffer();

            target = type.ToBufferTarget();
            usageHint = descriptor.Usage.ToBufferUsageHint();
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (handle != 0)
            {
                GL.DeleteBuffer(handle);
                handle = 0;
            }

            base.Dispose(disposing);
        }


        private int handle;
        /// <inheritdoc/>
        public override int Handle => handle;


        /// <inheritdoc/>
        internal override void Activate()
        {
            throw new InvalidOperationException();
        }

        /// <inheritdoc/>
        internal override void Deactivate()
        {
            throw new InvalidOperationException();
        }


        /// <inheritdoc/>
        protected override void ReadData(IntPtr buffer, int size)
        {
            GL.BindVertexArray(vaoId);
            GL.BindBuffer(target, handle);
            GL.GetBufferSubData(target, 0, size, buffer);
            GL.BindVertexArray(0);
        }

        /// <inheritdoc/>
        protected override void WriteData(IntPtr buffer, int size)
        {
            GL.BindVertexArray(vaoId);
            GL.BindBuffer(target, handle);
            GL.BufferData(target, size, buffer, usageHint);
            GL.BindVertexArray(0);
        }
    }
}
