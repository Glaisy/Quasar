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
using System.Threading;

using Microsoft.Extensions.DependencyInjection;

using Quasar.Graphics;
using Quasar.Rendering.Processors.Internals;

using Space.Core;

namespace Quasar.Rendering
{
    /// <summary>
    /// Render camera implementation.
    /// </summary>
    /// <seealso cref="InvalidatableBase" />
    /// <seealso cref="ICamera" />
    public sealed partial class Camera : InvalidatableBase, ICamera
    {
        private static readonly Range<float> fovRange = new Range<float>(0.0f, 180.0f);


        private static CameraCommandProcessor commandProcessor;
        private static IRenderingContext renderingContext;
        private static IMatrixFactory matrixFactory;
        private static int lastCameraId = 0;
        private Size projectionSize;
        private int transformationTimestamp;


        /// <summary>
        /// Initializes a new instance of the <see cref="Camera" /> class.
        /// </summary>
        /// <param name="isEnabled">The initial value of the IsEnabled property.</param>
        /// <param name="name">The name.</param>
        public Camera(bool isEnabled = true, string name = null)
        {
            this.isEnabled = isEnabled;
            Name = name;

            transformationTimestamp = Transform.Timestamp;
            frameBuffer = renderingContext.PrimaryFrameBuffer;
            Id = Interlocked.Increment(ref lastCameraId);

            SendCreateCommand();
        }


        private float aspectRatio;
        /// <summary>
        /// Gets the aspect ratio (frame buffer width/height).
        /// </summary>
        public float AspectRatio
        {
            get
            {
                if (HasInvalidationFlags(InvalidationFlags.AspectRatio))
                {
                    aspectRatio = FrameBuffer.Size.Width / (float)FrameBuffer.Size.Height;
                    ClearInvalidationFlags(InvalidationFlags.AspectRatio);
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
                Invalidate(InvalidationFlags.AllProjections);
            }
        }

        private IFrameBuffer frameBuffer;
        /// <summary>
        /// Gets or sets the frame buffer.
        /// </summary>
        public IFrameBuffer FrameBuffer
        {
            get => frameBuffer;
            set
            {
                ArgumentNullException.ThrowIfNull(value, nameof(FrameBuffer));
                if (frameBuffer == value)
                {
                    return;
                }

                frameBuffer = value;
                Invalidate(InvalidationFlags.AllProjections);
            }
        }

        private readonly Frustum frustum = new Frustum();
        /// <inheritdoc/>
        public IFrustum Frustum
        {
            get
            {
                if (HasInvalidationFlags(InvalidationFlags.Frustum))
                {
                    frustum.Update(Transform, fieldOfView, AspectRatio, zNear, zFar);

                    ClearInvalidationFlags(InvalidationFlags.Frustum);
                }

                return frustum;
            }
        }

        /// <inheritdoc/>
        public int Id { get; }

        private bool isEnabled;
        /// <summary>
        /// Gets or sets a value indicating whether the camera is enabled or not.
        /// </summary>
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                if (isEnabled == value)
                {
                    return;
                }

                isEnabled = value;
                SendEnabledChangedCommand(value);
            }
        }

        /// <summary>
        /// Gets or sets the layer mask.
        /// </summary>
        public LayerMask LayerMask { get; set; } = LayerMask.All;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get => Transform.Name;
            set => Transform.Name = value;
        }

        private Matrix4 projectionMatrix;
        /// <inheritdoc/>
        public ref readonly Matrix4 ProjectionMatrix
        {
            get
            {
                if (projectionSize != frameBuffer.Size)
                {
                    Invalidate(InvalidationFlags.AllProjections);
                    projectionSize = frameBuffer.Size;
                }

                if (HasInvalidationFlags(InvalidationFlags.ProjectionMatrix))
                {
                    UpdateProjectionMatrix(ref projectionMatrix, projectionSize);
                    ClearInvalidationFlags(InvalidationFlags.ProjectionMatrix);
                }

                return ref projectionMatrix;
            }
        }

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
                Invalidate(InvalidationFlags.AllProjections);
            }
        }

        /// <summary>
        /// The transformation.
        /// </summary>
        public readonly Transform Transform = new Transform();

        /// <inheritdoc/>
        ITransform ICamera.Transform => Transform;

        private Matrix4 viewMatrix = Matrix4.Identity;
        /// <inheritdoc/>
        public ref readonly Matrix4 ViewMatrix
        {
            get
            {
                EnsureTransformationHasNotChanged();
                if (HasInvalidationFlags(InvalidationFlags.ViewMatrix))
                {
                    matrixFactory.CreateViewMatrix(Transform, ref viewMatrix);

                    ClearInvalidationFlags(InvalidationFlags.ViewMatrix);
                }

                return ref viewMatrix;
            }
        }

        private Matrix4 viewProjectionMatrix = Matrix4.Identity;
        /// <inheritdoc/>
        public ref readonly Matrix4 ViewProjectionMatrix
        {
            get
            {
                EnsureTransformationHasNotChanged();
                if (HasInvalidationFlags(InvalidationFlags.ViewProjectionMatrix))
                {
                    Matrix4.Multiply(ViewMatrix, ProjectionMatrix, ref viewProjectionMatrix);

                    ClearInvalidationFlags(InvalidationFlags.ViewProjectionMatrix);
                }

                return ref viewProjectionMatrix;
            }
        }

        private Matrix4 viewRotationProjectionMatrix = Matrix4.Identity;
        /// <inheritdoc/>
        public ref readonly Matrix4 ViewRotationProjectionMatrix
        {
            get
            {
                EnsureTransformationHasNotChanged();
                if (HasInvalidationFlags(InvalidationFlags.ViewRotationProjectionMatrix))
                {
                    Matrix4 viewRotationMatrix;
                    matrixFactory.CreateViewRotationMatrix(ViewMatrix, ref viewRotationMatrix);
                    Matrix4.Multiply(viewRotationMatrix, ProjectionMatrix, ref viewRotationProjectionMatrix);

                    ClearInvalidationFlags(InvalidationFlags.ViewRotationProjectionMatrix);
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
                Invalidate(InvalidationFlags.AllProjections);
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
                Invalidate(InvalidationFlags.AllProjections);
            }
        }


        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is not IRawCamera other)
            {
                return false;
            }

            return Id == other.Id;
        }

        /// <inheritdoc/>
        public bool Equals(IRawCamera other)
        {
            if (other == null)
            {
                return false;
            }

            return Id == other.Id;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Id;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Name ?? $"{nameof(Camera)} {Id}";
        }


        /// <summary>
        /// Initializes the static services.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        internal static void InitializeStaticServices(IServiceProvider serviceProvider)
        {
            commandProcessor = serviceProvider.GetRequiredService<CameraCommandProcessor>();
            renderingContext = serviceProvider.GetRequiredService<IRenderingContext>();
            matrixFactory = serviceProvider.GetRequiredService<IMatrixFactory>();
        }


        private void EnsureTransformationHasNotChanged()
        {
            if (transformationTimestamp == Transform.Timestamp)
            {
                return;
            }

            Invalidate(InvalidationFlags.AllViews);
            transformationTimestamp = Transform.Timestamp;
        }

        private void SendCreateCommand()
        {
            commandProcessor.Add(new CameraCommand(this, CameraCommandType.Create)
            {
                IsEnabled = isEnabled
            });
        }

        private void SendEnabledChangedCommand(bool enabled)
        {
            commandProcessor.Add(new CameraCommand(this, CameraCommandType.EnabledChanged)
            {
                IsEnabled = enabled
            });
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

        private void UpdateProjectionMatrix(ref Matrix4 projectionMatrix, in Size projectionSize)
        {
            switch (projectionType)
            {
                case ProjectionType.Orthographic:
                    matrixFactory.CreateOrthographicProjectionMatrix(projectionSize.Width, projectionSize.Height, zNear, zFar, ref projectionMatrix);
                    break;
                case ProjectionType.Perspective:
                    matrixFactory.CreatePerspectiveProjectionMatrix(AspectRatio, fieldOfView, zNear, zFar, ref projectionMatrix);
                    break;
                default:
                    throw new NotSupportedException($"Unsupported camera projection type: {projectionType}");
            }
        }
    }
}
