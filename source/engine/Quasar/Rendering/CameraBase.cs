//-----------------------------------------------------------------------
// <copyright file="CameraBase.cs" company="Space Development">
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

using Space.Core;

namespace Quasar.Rendering
{
    /// <summary>
    /// Abstract base class for camera implementations.
    /// </summary>
    /// <seealso cref="RenderObject" />
    /// <seealso cref="IRawCamera" />
    public abstract class CameraBase : RenderObject, IRawCamera
    {
        /// <summary>
        /// Camera invalidation flalgs.
        /// </summary>
        [Flags]
        protected enum CameraInvalidationFlags
        {
            /// <summary>
            /// The projection matrix invalidation flag.
            /// </summary>
            ProjectionMatrix = 1,

            /// <summary>
            /// The view matrix invalidation flag.
            /// </summary>
            ViewMatrix = 2,

            /// <summary>
            /// The view projection matrix invalidation flag.
            /// </summary>
            ViewProjectionMatrix = 4,

            /// <summary>
            /// The view rotation projection (skybox) matrix invalidation flag.
            /// </summary>
            ViewRotationProjectionMatrix = 8,

            /// <summary>
            /// The frustum invalidation flag.
            /// </summary>
            Frustum = 16,

            /// <summary>
            /// The aspect ratio invalidation flag.
            /// </summary>
            AspectRatio = 32,

            /// <summary>
            /// All projection flags.
            /// </summary>
            AllProjections = ProjectionMatrix | ViewProjectionMatrix | ViewRotationProjectionMatrix | Frustum | AspectRatio,

            /// <summary>
            /// All view flags.
            /// </summary>
            AllViews = ViewMatrix | ViewProjectionMatrix | ViewRotationProjectionMatrix | Frustum,

            /// <summary>
            /// All invalidation flags.
            /// </summary>
            All = AllViews | AllProjections
        }


        private Size projectionSize;


        /// <summary>
        /// Initializes a new instance of the <see cref="CameraBase" /> class.
        /// </summary>
        /// <param name="isEnabled">if set to <c>true</c> [is enabled].</param>
        protected CameraBase(bool isEnabled)
            : base(isEnabled)
        {
            Invalidate(CameraInvalidationFlags.All);
        }


        private IFrameBuffer frameBuffer;
        /// <summary>
        /// Gets or sets the frame buffer.
        /// </summary>
        public IFrameBuffer FrameBuffer
        {
            get => frameBuffer;
            protected set
            {
                Assertion.ThrowIfNull(value, nameof(FrameBuffer));
                if (frameBuffer == value)
                {
                    return;
                }

                frameBuffer = value;
                Invalidate(CameraInvalidationFlags.AllProjections);
            }
        }

        private Matrix4 projectionMatrix;
        /// <inheritdoc/>
        public ref readonly Matrix4 ProjectionMatrix
        {
            get
            {
                if (projectionSize != frameBuffer.Size)
                {
                    Invalidate(CameraInvalidationFlags.AllProjections);
                    projectionSize = frameBuffer.Size;
                }

                if (InvalidationFlags.HasFlag(CameraInvalidationFlags.ProjectionMatrix))
                {
                    UpdateProjectionMatrix(ref projectionMatrix, projectionSize);
                    ClearInvalidation(CameraInvalidationFlags.ProjectionMatrix);
                }

                return ref projectionMatrix;
            }
        }

        /// <inheritdoc/>
        public abstract ref readonly Matrix4 ViewMatrix { get; }

        /// <inheritdoc/>
        public abstract ref readonly Matrix4 ViewProjectionMatrix { get; }

        /// <inheritdoc/>
        public abstract ref readonly Matrix4 ViewRotationProjectionMatrix { get; }


        /// <summary>
        /// Gets the invalidation flags.
        /// </summary>
        protected CameraInvalidationFlags InvalidationFlags { get; private set; }


        /// <summary>
        /// Sets the specified camera invalidation flags.
        /// </summary>
        /// <param name="invalidationFlags">The invalidation flags.</param>
        protected void Invalidate(CameraInvalidationFlags invalidationFlags)
        {
            InvalidationFlags |= invalidationFlags;
        }

        /// <summary>
        /// Clears the specified camera invalidation flags.
        /// </summary>
        /// <param name="invalidationFlags">The invalidation flags.</param>
        protected void ClearInvalidation(CameraInvalidationFlags invalidationFlags)
        {
            InvalidationFlags &= ~invalidationFlags;
        }

        /// <summary>
        /// Updates the projection matrix.
        /// </summary>
        /// <param name="projectionMatrix">The projection matrix.</param>
        /// <param name="projectionSize">The projection size.</param>
        protected abstract void UpdateProjectionMatrix(ref Matrix4 projectionMatrix, in Size projectionSize);
    }
}
