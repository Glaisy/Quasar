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
        /// <summary>
        /// Initializes a new instance of the <see cref="GLCommandProcessor"/> class.
        /// </summary>
        public GLCommandProcessor()
        {
        }


        /// <summary>
        /// Checks if there is any graphics errors.
        /// </summary>
        public void CheckErrors()
        {
            GL.CheckErrors();
        }

        /// <summary>
        /// Resets the state of the processsor.
        /// </summary>
        public void ResetState()
        {
            GL.ClearColor(1, 0, 0, 1);
            GL.Clear(BufferClearMask.ColorBuffer | BufferClearMask.DepthBuffer);
        }

        /// <summary>
        /// Sets the viewport.
        /// </summary>
        /// <param name="position">The position of the lower left corner.</param>
        /// <param name="size">The size.</param>
        public void SetViewport(in Point position, in Size size)
        {
            GL.Viewport(position.X, position.Y, size.Width, size.Height);
        }

        /// <summary>
        /// Sets the V-Sync mode.
        /// </summary>
        /// <param name="mode">The V-Sync mode.</param>
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
