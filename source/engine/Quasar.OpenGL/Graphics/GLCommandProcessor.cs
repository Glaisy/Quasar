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
using Quasar.OpenGL.Extensions;

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
        private bool isBackfaceCullingEnabled;
        private bool isDepthTestingEnabled;


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
        public void DrawMesh(in RawMesh rawMesh)
        {
            if (rawMesh.Handle != lastMeshHandle)
            {
                GL.BindVertexArray(rawMesh.Handle);
                lastMeshHandle = rawMesh.Handle;
            }

            if (rawMesh.IsIndexed)
            {
                GL.DrawElements(rawMesh.PrimitiveType, rawMesh.ElementCount, DrawElementsType.UnsignedInt, 0);
            }
            else
            {
                GL.DrawArrays(rawMesh.PrimitiveType, 0, rawMesh.ElementCount);
            }
        }

        /// <inheritdoc/>
        public void Reset()
        {
            SetBackfaceCulling(true);
            SetDepthTesting(true);
            SetDepthTestMode(DepthTestMode.Less);
        }

        /// <inheritdoc/>
        public bool SetBackfaceCulling(bool enabled)
        {
            var previousValue = isBackfaceCullingEnabled;
            if (isBackfaceCullingEnabled != enabled)
            {
                isBackfaceCullingEnabled = enabled;
                if (isBackfaceCullingEnabled)
                {
                    GL.Enable(Capability.CullFace);
                }
                else
                {
                    GL.Disable(Capability.CullFace);
                }
            }

            return previousValue;
        }

        /// <inheritdoc/>
        public bool SetDepthTesting(bool enabled)
        {
            var previousValue = isDepthTestingEnabled;
            if (isDepthTestingEnabled != enabled)
            {
                isDepthTestingEnabled = enabled;
                if (isDepthTestingEnabled)
                {
                    GL.Enable(Capability.DepthTest);
                }
                else
                {
                    GL.Disable(Capability.DepthTest);
                }
            }

            return previousValue;
        }

        /// <inheritdoc/>
        public void SetDepthTestMode(DepthTestMode depthTestMode)
        {
            var depthFunction = depthTestMode.ToDepthFunction();
            GL.DepthFunc(depthFunction);
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
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Clockwise);
        }
    }
}
