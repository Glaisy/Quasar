//-----------------------------------------------------------------------
// <copyright file="TransformD.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar
{
    /// <summary>
    /// Double precision transformation object implementation.
    /// </summary>
    /// <seealso cref="TransformBase" />
    /// <seealso cref="ITransformD" />
    public sealed class TransformD : TransformBase, ITransformD
    {
        private int parentTimestamp;


        /// <summary>
        /// Initializes a new instance of the <see cref="TransformD" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="name">The name.</param>
        public TransformD(TransformD parent = null, string name = null)
            : base(name)
        {
            SetParent(parent);
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="TransformD"/> class from being created.
        /// </summary>
        private TransformD()
            : base(nameof(Root))
        {
            Invalidate(InvalidationFlags.All);
        }


        private QuaternionD inverseRotation;
        /// <inheritdoc/>
        public QuaternionD InverseRotation
        {
            get
            {
                if (HasInvalidationFlags(InvalidationFlags.InverseRotation))
                {
                    if (parent == null)
                    {
                        inverseRotation = localRotation.Invert();
                    }
                    else
                    {
                        inverseRotation = Rotation.Invert();
                    }

                    ClearInvalidationFlags(InvalidationFlags.InverseRotation);
                }

                return inverseRotation;
            }
        }

        private Vector3D localPosition = Vector3D.Zero;
        /// <summary>
        /// Gets or sets the position relative to the parent transformation.
        /// </summary>
        public Vector3D LocalPosition
        {
            get => localPosition;
            set
            {
                if (localPosition == value)
                {
                    return;
                }

                localPosition = value;
                Invalidate(InvalidationFlags.Position);
            }
        }

        private QuaternionD localRotation = QuaternionD.Identity;
        /// <summary>
        /// Gets or sets the rotation relative to the parent transformation.
        /// </summary>
        public QuaternionD LocalRotation
        {
            get => localRotation;
            set
            {
                if (localRotation == value)
                {
                    return;
                }

                localRotation = value;
                Invalidate(InvalidationFlags.Rotation | InvalidationFlags.InverseRotation | InvalidationFlags.AllVectors);
            }
        }

        private Vector3D localScale = Vector3D.One;
        /// <summary>
        /// Gets or sets the scale relative to the parent transformation.
        /// </summary>
        public Vector3D LocalScale
        {
            get => localScale;
            set
            {
                if (localScale == value)
                {
                    return;
                }

                localScale = value;
                Invalidate(InvalidationFlags.Scale);
            }
        }

        private Vector3D negativeX;
        /// <inheritdoc/>
        public Vector3D NegativeX
        {
            get
            {
                if (HasInvalidationFlags(InvalidationFlags.NegativeX))
                {
                    negativeX = Rotation * Vector3D.NegativeX;
                    ClearInvalidationFlags(InvalidationFlags.NegativeX);
                }

                return negativeX;
            }
        }

        private Vector3D negativeY;
        /// <inheritdoc/>
        public Vector3D NegativeY
        {
            get
            {
                if (HasInvalidationFlags(InvalidationFlags.NegativeY))
                {
                    negativeY = Rotation * Vector3D.NegativeY;
                    ClearInvalidationFlags(InvalidationFlags.NegativeY);
                }

                return negativeY;
            }
        }

        private Vector3D negativeZ;
        /// <inheritdoc/>
        public Vector3D NegativeZ
        {
            get
            {
                if (HasInvalidationFlags(InvalidationFlags.NegativeZ))
                {
                    negativeZ = Rotation * Vector3D.NegativeZ;
                    ClearInvalidationFlags(InvalidationFlags.NegativeZ);
                }

                return negativeZ;
            }
        }

        private TransformD parent;
        /// <summary>
        /// Gets or sets the parent transformation.
        /// </summary>
        public TransformD Parent
        {
            get => parent;
            set
            {
                if (parent == value)
                {
                    return;
                }

                if (this == Root)
                {
                    throw new InvalidOperationException("Root transformation's parent cannot be changed.");
                }

                SetParent(value);
            }
        }

        /// <inheritdoc/>
        ITransformD ITransformD.Parent => parent;

        private Vector3D position;
        /// <inheritdoc/>
        public Vector3D Position
        {
            get
            {
                if (HasInvalidationFlags(InvalidationFlags.Position))
                {
                    if (parent == null)
                    {
                        position = localPosition;
                    }
                    else
                    {
                        position = parent.Position + parent.Rotation * localPosition;
                    }

                    ClearInvalidationFlags(InvalidationFlags.Position);
                }

                return position;
            }
        }

        private Vector3D positiveX;
        /// <inheritdoc/>
        public Vector3D PositiveX
        {
            get
            {
                if (HasInvalidationFlags(InvalidationFlags.PositiveX))
                {
                    positiveX = Rotation * Vector3D.PositiveX;
                    ClearInvalidationFlags(InvalidationFlags.PositiveX);
                }

                return positiveX;
            }
        }

        private Vector3D positiveY;
        /// <inheritdoc/>
        public Vector3D PositiveY
        {
            get
            {
                if (HasInvalidationFlags(InvalidationFlags.PositiveY))
                {
                    positiveY = Rotation * Vector3D.PositiveY;
                    ClearInvalidationFlags(InvalidationFlags.PositiveY);
                }

                return positiveY;
            }
        }

        private Vector3D positiveZ;
        /// <inheritdoc/>
        public Vector3D PositiveZ
        {
            get
            {
                if (HasInvalidationFlags(InvalidationFlags.PositiveZ))
                {
                    positiveZ = Rotation * Vector3D.PositiveZ;
                    ClearInvalidationFlags(InvalidationFlags.PositiveZ);
                }

                return positiveZ;
            }
        }

        private static readonly TransformD root = new TransformD();
        /// <summary>
        /// Gets the top level transformation object.
        /// </summary>
        public static ITransformD Root => root;

        private QuaternionD rotation;
        /// <inheritdoc/>
        public QuaternionD Rotation
        {
            get
            {
                if (HasInvalidationFlags(InvalidationFlags.Rotation))
                {
                    if (parent == null)
                    {
                        rotation = localRotation;
                    }
                    else
                    {
                        rotation = parent.Rotation * localRotation;
                    }

                    ClearInvalidationFlags(InvalidationFlags.Rotation);
                }

                return rotation;
            }
        }

        private Vector3D scale;
        /// <inheritdoc/>
        public Vector3D Scale
        {
            get
            {
                if (HasInvalidationFlags(InvalidationFlags.Scale))
                {
                    if (parent == null)
                    {
                        scale = localScale;
                    }
                    else
                    {
                        scale = parent.Scale * localScale;
                    }

                    ClearInvalidationFlags(InvalidationFlags.Scale);
                }

                return scale;
            }
        }


        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            LocalPosition = Vector3D.Zero;
            LocalRotation = QuaternionD.Identity;
            LocalScale = Vector3D.One;
        }

        /// <summary>
        /// Converts the world space direction into local space.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public Vector3D ToLocalDirection(in Vector3D direction)
        {
            return InverseRotation * direction;
        }

        /// <summary>
        /// Converts the world space position into local space.
        /// </summary>
        /// <param name="position">The position.</param>
        public Vector3D ToLocalPosition(in Vector3D position)
        {
            return InverseRotation * (position - Position);
        }

        /// <summary>
        /// Converts the local space direction into world space.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public Vector3D ToWorldDirection(in Vector3D direction)
        {
            return Rotation * direction;
        }

        /// <summary>
        /// Converts the local space position into world space.
        /// </summary>
        /// <param name="position">The position.</param>
        public Vector3D ToWorldPosition(in Vector3D position)
        {
            return Position + Rotation * position;
        }


        private new bool HasInvalidationFlags(int invalidationFlags)
        {
            if (parent != null && parentTimestamp != parent.Timestamp)
            {
                Invalidate(InvalidationFlags.All);
                parentTimestamp = parent.Timestamp;
                return true;
            }

            return base.HasInvalidationFlags(invalidationFlags);
        }

        private void SetParent(TransformD value)
        {
            parent = value ?? root;
            parentTimestamp = parent.Timestamp;
            Invalidate(InvalidationFlags.All);
        }
    }
}
