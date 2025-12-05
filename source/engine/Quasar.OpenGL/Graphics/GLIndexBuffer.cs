//-----------------------------------------------------------------------
// <copyright file="GLIndexBuffer.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics;

namespace Quasar.OpenGL.Internals.Graphics
{
    /// <summary>
    /// OpenGL index buffer implementation.
    /// </summary>
    /// <seealso cref="GLGraphicsBufferBase" />
    internal sealed class GLIndexBuffer : GLGraphicsBufferBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GLIndexBuffer" /> class.
        /// </summary>
        /// <param name="vaoId">The vertex array object identifier.</param>
        /// <param name="descriptor">The graphic resource descriptor.</param>
        public GLIndexBuffer(int vaoId, in GraphicsResourceDescriptor descriptor)
            : base(vaoId, GraphicsBufferType.IndexBuffer, descriptor)
        {
        }
    }
}
