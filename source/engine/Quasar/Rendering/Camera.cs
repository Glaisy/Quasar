//-----------------------------------------------------------------------
// <copyright file="Camera.cs" company="Space Development">
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
using Quasar.Rendering.Processors.Internals;

using Space.Core;

namespace Quasar.Rendering
{
    /// <summary>
    /// Render camera implementation.
    /// </summary>
    /// <seealso cref="CameraBase" />
    /// <seealso cref="ICamera" />
    public class Camera : CameraBase, ICamera
    {
        private static readonly Range<float> fovRange = new Range<float>(0.0f, 180.0f);
        private int transformationTimestamp;


        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class.
        /// </summary>
        public Camera()
            : base(true)
        {
            transformationTimestamp = Transform.Timestamp;
        }


        private float aspectRatio;
        /// <summary>
        /// Gets the aspect ratio (frame buffer width/height).
        /// </summary>
        public float AspectRatio
        {
            get
            {
                if (InvalidationFlags.HasFlag(CameraInvalidationFlags.AspectRatio))
                {
                    aspectRatio = FrameBuffer.Size.Width / (float)FrameBuffer.Size.Height;
                    ClearInvalidation(CameraInvalidationFlags.AspectRatio);
                }

                return aspectRatio;
            }
        }

        /// <summary>
        /// Gets or sets the clear color.
        /// </summary>
        public Color ClearColor { get; set; } = Color.Black;

        /// <summary>
        /// Gets or sets the clear type.
        /// </summary>
        public CameraClearType ClearType { get; set; } = CameraClearType.SolidColor;

        private float fieldOfView = 90.0f;
        /// <summary>
        /// Gets or sets the field of view (0...180][degree].
        /// </summary>
        public float FieldOfView
        {
            get => fieldOfView;
            set
            {
                value = fovRange.Clamp(value);

                if (fieldOfView == value)
                {
                    return;
                }

                fieldOfView = value;
                Invalidate(CameraInvalidationFlags.AllProjections);
            }
        }

        private readonly Frustum frustum = new Frustum();
        /// <inheritdoc/>
        public IFrustum Frustum
        {
            get
            {
                if (InvalidationFlags.HasFlag(CameraInvalidationFlags.Frustum))
                {
                    frustum.Update(Transform, fieldOfView, AspectRatio, zNear, zFar);

                    ClearInvalidation(CameraInvalidationFlags.Frustum);
                }

                return frustum;
            }
        }

        /// <summary>
        /// Gets or sets the layer mask.
        /// </summary>
        public LayerMask LayerMask { get; set; } = LayerMask.Opaque | LayerMask.Transparent;

        private ProjectionType projectionType = ProjectionType.Perspective;
        /// <summary>
        /// Gets or sets the projection type.
        /// </summary>
        public ProjectionType ProjectionType
        {
            get => projectionType;
            set
            {
                if (projectionType == value)
                {
                    return;
                }

                projectionType = value;
                Invalidate(CameraInvalidationFlags.AllProjections);
            }
        }

        /// <inheritdoc/>
        ITransform ICamera.Transform => Transform;

        private Matrix4 viewMatrix = Matrix4.Identity;
        /// <inheritdoc/>
        public override ref readonly Matrix4 ViewMatrix
        {
            get
            {
                EnsureTransformationHasNotChanged();
                if (InvalidationFlags.HasFlag(CameraInvalidationFlags.ViewMatrix))
                {
                    MatrixFactory.CreateViewMatrix(Transform, ref viewMatrix);

                    ClearInvalidation(CameraInvalidationFlags.ViewMatrix);
                }

                return ref viewMatrix;
            }
        }

        private Matrix4 viewProjectionMatrix = Matrix4.Identity;
        /// <inheritdoc/>
        public override ref readonly Matrix4 ViewProjectionMatrix
        {
            get
            {
                EnsureTransformationHasNotChanged();
                if (InvalidationFlags.HasFlag(CameraInvalidationFlags.ViewProjectionMatrix))
                {
                    Matrix4.Multiply(ViewMatrix, ProjectionMatrix, ref viewProjectionMatrix);

                    ClearInvalidation(CameraInvalidationFlags.ViewProjectionMatrix);
                }

                return ref viewProjectionMatrix;
            }
        }

        private Matrix4 viewRotationProjectionMatrix = Matrix4.Identity;
        /// <inheritdoc/>
        public override ref readonly Matrix4 ViewRotationProjectionMatrix
        {
            get
            {
                EnsureTransformationHasNotChanged();
                if (InvalidationFlags.HasFlag(CameraInvalidationFlags.ViewRotationProjectionMatrix))
                {
                    Matrix4 viewRotationMatrix;
                    MatrixFactory.CreateViewRotationMatrix(ViewMatrix, ref viewRotationMatrix);
                    Matrix4.Multiply(viewRotationMatrix, ProjectionMatrix, ref viewRotationProjectionMatrix);

                    ClearInvalidation(CameraInvalidationFlags.ViewRotationProjectionMatrix);
                }

                return ref viewRotationProjectionMatrix;
            }
        }

        private float zFar = 1000.0f;
        /// <summary>
        /// Gets or sets the far z-plane (0...+Inf).
        /// </summary>
        public float ZFar
        {
            get => zFar;
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value, nameof(ZFar));
                if (zFar == value)
                {
                    return;
                }

                zFar = value;
                SortZPlanes();
                Invalidate(CameraInvalidationFlags.AllProjections);
            }
        }

        private float zNear = 0.1f;
        /// <summary>
        /// Gets or sets the near z-plane (0...+Inf).
        /// </summary>
        public float ZNear
        {
            get => zNear;
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value, nameof(ZNear));
                if (zNear == value)
                {
                    return;
                }

                zNear = value;
                SortZPlanes();
                Invalidate(CameraInvalidationFlags.AllProjections);
            }
        }


        /// <summary>
        /// Gets or sets the command processor.
        /// </summary>
        internal static CameraCommandProcessor CommandProcessor { get; set; }


        /// <summary>
        /// Sends the enabled changed command to the processor.
        /// </summary>
        /// <param name="enabled">The new value of the enabled flag.</param>
        protected override void SendEnabledChangedCommand(bool enabled)
        {
            var command = new CameraCommand(this, CameraCommandType.EnabledChanged)
            {
                Enabled = enabled
            };
            CommandProcessor.Add(command);
        }


        /// <summary>
        /// Updates the projection matrix.
        /// </summary>
        /// <param name="projectionMatrix">The projection matrix.</param>
        /// <param name="projectionSize">The projection size.</param>
        protected override void UpdateProjectionMatrix(ref Matrix4 projectionMatrix, in Size projectionSize)
        {
            switch (projectionType)
            {
                case ProjectionType.Orthographic:
                    MatrixFactory.CreateOrthographicProjectionMatrix(projectionSize.Width, projectionSize.Height, zNear, zFar, ref projectionMatrix);
                    break;
                case ProjectionType.Perspective:
                    MatrixFactory.CreatePerspectiveProjectionMatrix(AspectRatio, fieldOfView, zNear, zFar, ref projectionMatrix);
                    break;
                default:
                    throw new NotSupportedException($"Unsupported camera projection type: {projectionType}");
            }
        }


        private void EnsureTransformationHasNotChanged()
        {
            if (transformationTimestamp == Transform.Timestamp)
            {
                return;
            }

            Invalidate(CameraInvalidationFlags.AllViews);
            transformationTimestamp = Transform.Timestamp;
        }

        private void SortZPlanes()
        {
            if (zFar >= zNear)
            {
                return;
            }

            var temp = zFar;
            zFar = zNear;
            zNear = temp;
        }
    }
}
