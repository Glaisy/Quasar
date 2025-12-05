//-----------------------------------------------------------------------
// <copyright file="GLCommandProcessor.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics;
using Quasar.OpenGL.Api;

using Space.Core.DependencyInjection;

namespace Quasar.OpenGL.Graphics
{
    /// <summary>
    /// OpenGL graphics command processor implementation.
    /// </summary>
    /// <seealso cref="IGraphicsCommandProcessor" />
    [Export]
    internal sealed class GLCommandProcessor : IGraphicsCommandProcessor
    {
        private int lastMeshHandle;

        /// <inheritdoc/>
        public void CheckErrors()
        {
            GL.CheckErrors();
        }

        /// <inheritdoc/>
        public void DrawMesh(IMesh mesh)
        {
            if (mesh.Handle != lastMeshHandle)
            {
                mesh.Activate();
                lastMeshHandle = mesh.Handle;
            }

            if (mesh.IsIndexed)
            {
                GL.DrawElements(mesh.InternalPrimitiveType, mesh.IndexBuffer.ElementCount, DrawElementsType.UnsignedInt, 0);
            }
            else
            {
                GL.DrawArrays(mesh.InternalPrimitiveType, 0, mesh.VertexBuffer.ElementCount);
            }
        }

        /// <inheritdoc/>
        public void ResetState()
        {
            GL.Clear(BufferClearMask.ColorBuffer | BufferClearMask.DepthBuffer);
        }

        /// <inheritdoc/>
        public void SetViewport(in Point position, in Size size)
        {
            GL.Viewport(position.X, position.Y, size.Width, size.Height);
        }

        /// <inheritdoc/>
        public void SetVSyncMode(VSyncMode mode)
        {
            switch (mode)
            {
                case VSyncMode.Off:
                    GL.SwapInterval(0);
                    break;
                case VSyncMode.On:
                    GL.SwapInterval(1);
                    break;
            }
        }


        /// <summary>
        /// Initializes the command processor.
        /// </summary>
        internal void Initialize()
        {
            // alpha blending
            GL.Enable(Capability.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            // face culling
            GL.Enable(Capability.CullFace);
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Clockwise);
        }
    }
}
