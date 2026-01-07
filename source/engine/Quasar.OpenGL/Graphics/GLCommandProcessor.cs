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
        /// <inheritdoc/>
        public bool IsBackfaceCullingEnabled
        {
            get => isBackfaceCullingEnabled;
            set
            {
                if (isBackfaceCullingEnabled == value)
                {
                    return;
                }

                SetBackfaceCullingEnabled(value);
            }
        }

        private DepthTestMode depthTestMode = DepthTestMode.Less;
        /// <inheritdoc/>
        public DepthTestMode DepthTestMode
        {
            get => depthTestMode;
            set
            {
                if (depthTestMode == value)
                {
                    return;
                }

                SetDepthTestMode(value);
            }
        }

        private bool isDepthTestingEnabled;
        /// <inheritdoc/>
        public bool IsDepthTestingEnabled
        {
            get => isDepthTestingEnabled;
            set
            {
                if (isDepthTestingEnabled == value)
                {
                    return;
                }

                SetDepthTestingEnabled(value);
            }
        }


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
            GL.UseProgram(0);
            GL.BindVertexArray(0);
            lastMeshHandle = 0;

            SetBackfaceCullingEnabled(true);
            SetDepthTestingEnabled(true);
            SetDepthTestMode(DepthTestMode.Less);
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


        private void SetBackfaceCullingEnabled(bool isBackfaceCullingEnabled)
        {
            this.isBackfaceCullingEnabled = isBackfaceCullingEnabled;

            if (isBackfaceCullingEnabled)
            {
                GL.Enable(Capability.CullFace);
            }
            else
            {
                GL.Disable(Capability.CullFace);
            }
        }

        private void SetDepthTestingEnabled(bool isDepthTestingEnabled)
        {
            this.isDepthTestingEnabled = isDepthTestingEnabled;

            if (isDepthTestingEnabled)
            {
                GL.Enable(Capability.DepthTest);
            }
            else
            {
                GL.Disable(Capability.DepthTest);
            }
        }

        private void SetDepthTestMode(DepthTestMode depthTestMode)
        {
            this.depthTestMode = depthTestMode;

            var depthFunction = depthTestMode.ToDepthFunction();
            GL.DepthFunc(depthFunction);
        }
    }
}
