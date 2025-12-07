//-----------------------------------------------------------------------
// <copyright file="GLComputeShader.cs" company="Space Development">
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

namespace Quasar.OpenGL.Graphics
{
    /// <summary>
    ///  OpenGL compute shader program implementation.
    /// </summary>
    /// <seealso cref="ComputeShaderBase" />
    internal sealed class GLComputeShader : ComputeShaderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GLComputeShader" /> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="tag">The tag value.</param>
        /// <param name="descriptor">The descriptor.</param>
        public GLComputeShader(
            int handle,
            string id,
            string tag,
            in GraphicsResourceDescriptor descriptor)
            : base(id, tag, descriptor)
        {
            this.handle = handle;
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (handle != 0)
            {
                GL.DeleteProgram(handle);
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
            GL.UseProgram(handle);
        }

        /// <inheritdoc/>
        internal override void Deactivate()
        {
            GL.UseProgram(0);
        }
    }
}
