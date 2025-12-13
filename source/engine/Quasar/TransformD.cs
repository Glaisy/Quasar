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
            Invalidate(InvalidationType.All);
        }


        private QuaternionD inverseRotation;
        /// <inheritdoc/>
        public QuaternionD InverseRotation
        {
            get
            {
                if (HasInvalidationType(InvalidationType.InverseRotation))
                {
                    if (parent == null)
                    {
                        inverseRotation = localRotation.Invert();
                    }
                    else
                    {
                        inverseRotation = Rotation.Invert();
                    }

                    ClearInvalidationType(InvalidationType.InverseRotation);
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
                Invalidate(InvalidationType.Position);
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
                Invalidate(InvalidationType.Rotation | InvalidationType.InverseRotation | InvalidationType.AllVectors);
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
                Invalidate(InvalidationType.Scale);
            }
        }

        private Vector3D negativeX;
        /// <inheritdoc/>
        public Vector3D NegativeX
        {
            get
            {
                if (HasInvalidationType(InvalidationType.NegativeX))
                {
                    negativeX = Rotation * Vector3D.NegativeX;
                    ClearInvalidationType(InvalidationType.NegativeX);
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
                if (HasInvalidationType(InvalidationType.NegativeY))
                {
                    negativeY = Rotation * Vector3D.NegativeY;
                    ClearInvalidationType(InvalidationType.NegativeY);
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
                if (HasInvalidationType(InvalidationType.NegativeZ))
                {
                    negativeZ = Rotation * Vector3D.NegativeZ;
                    ClearInvalidationType(InvalidationType.NegativeZ);
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
                if (HasInvalidationType(InvalidationType.Position))
                {
                    if (parent == null)
                    {
                        position = localPosition;
                    }
                    else
                    {
                        position = parent.Position + parent.Rotation * localPosition;
                    }

                    ClearInvalidationType(InvalidationType.Position);
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
                if (HasInvalidationType(InvalidationType.PositiveX))
                {
                    positiveX = Rotation * Vector3D.PositiveX;
                    ClearInvalidationType(InvalidationType.PositiveX);
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
                if (HasInvalidationType(InvalidationType.PositiveY))
                {
                    positiveY = Rotation * Vector3D.PositiveY;
                    ClearInvalidationType(InvalidationType.PositiveY);
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
                if (HasInvalidationType(InvalidationType.PositiveZ))
                {
                    positiveZ = Rotation * Vector3D.PositiveZ;
                    ClearInvalidationType(InvalidationType.PositiveZ);
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
                if (HasInvalidationType(InvalidationType.Rotation))
                {
                    if (parent == null)
                    {
                        rotation = localRotation;
                    }
                    else
                    {
                        rotation = parent.Rotation * localRotation;
                    }

                    ClearInvalidationType(InvalidationType.Rotation);
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
                if (HasInvalidationType(InvalidationType.Scale))
                {
                    if (parent == null)
                    {
                        scale = localScale;
                    }
                    else
                    {
                        scale = parent.Scale * localScale;
                    }

                    ClearInvalidationType(InvalidationType.Scale);
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


        private bool HasInvalidationType(InvalidationType invalidationType)
        {
            if (parent != null && parentTimestamp != parent.Timestamp)
            {
                Invalidate(InvalidationType.All);
                parentTimestamp = parent.Timestamp;
                return true;
            }

            return IsInvalidationTypeSet(invalidationType);
        }

        private void SetParent(TransformD value)
        {
            parent = value ?? root;
            parentTimestamp = parent.Timestamp;
            Invalidate(InvalidationType.All);
        }
    }
}
