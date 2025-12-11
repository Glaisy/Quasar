//-----------------------------------------------------------------------
// <copyright file="Transform.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Tests.Performance.Legacy
{
    /// <summary>
    /// Single precision transformation object implementation.
    /// </summary>
    /// <seealso cref="TransformBase{Transform}" />
    /// <seealso cref="ITransform" />
    public sealed class Transform : TransformBase<Transform>, ITransform
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Transform" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="name">The name.</param>
        public Transform(Transform parent = null, string name = null)
            : base(name)
        {
            this.parent = parent ?? Root;
            this.parent.AddChildTransform(this);
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Transform"/> class from being created.
        /// </summary>
        private Transform()
            : base(nameof(Root))
        {
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            parent?.RemoveChildTransform(this);
            parent = null;
        }


        private Transform parent;
        /// <summary>
        /// Gets or sets the parent transformation.
        /// </summary>
        public override Transform Parent
        {
            get => parent;
            set
            {
                if (this == Root)
                {
                    throw new InvalidOperationException("Root transformation's parent cannot be changed.");
                }

                if (parent == value)
                {
                    return;
                }

                // remove from the current parent
                parent.RemoveChildTransform(this);

                // set the new parent
                if (value == null)
                {
                    parent = Root;
                }
                else
                {
                    parent = value;
                }

                // add to the new parent
                parent.AddChildTransform(this);

                // invalidate all world space properties
                Invalidate(InvalidationFlags.All);
            }
        }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        ITransform ITransform.Parent => parent;

        private Vector3 localPosition = Vector3.Zero;
        /// <summary>
        /// Gets or sets the position relative to the parent transformation.
        /// </summary>
        public Vector3 LocalPosition
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

        /// <summary>
        /// The top level transformation object.
        /// </summary>
        public static readonly Transform Root = new Transform();

        private Quaternion localRotation = Quaternion.Identity;
        /// <summary>
        /// Gets or sets the rotation relative to the parent transformation.
        /// </summary>
        public Quaternion LocalRotation
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

        private Vector3 localScale = Vector3.One;
        /// <summary>
        /// Gets or sets the scale relative to the parent transformation.
        /// </summary>
        public Vector3 LocalScale
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

        private Quaternion inverseRotation;
        /// <summary>
        /// Gets the world space inverse rotation.
        /// </summary>
        public Quaternion InverseRotation
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

        private Vector3 negativeX;
        /// <summary>
        /// Gets the world space -X direction.
        /// </summary>
        public Vector3 NegativeX
        {
            get
            {
                if (HasInvalidationFlags(InvalidationFlags.NegativeX))
                {
                    negativeX = Rotation * Vector3.NegativeX;
                    ClearInvalidationFlags(InvalidationFlags.NegativeX);
                }

                return negativeX;
            }
        }

        private Vector3 negativeY;
        /// <summary>
        /// Gets the world space -Y direction.
        /// </summary>
        public Vector3 NegativeY
        {
            get
            {
                if (HasInvalidationFlags(InvalidationFlags.NegativeY))
                {
                    negativeY = Rotation * Vector3.NegativeY;
                    ClearInvalidationFlags(InvalidationFlags.NegativeY);
                }

                return negativeY;
            }
        }

        private Vector3 negativeZ;
        /// <summary>
        /// Gets the world space -Z direction.
        /// </summary>
        public Vector3 NegativeZ
        {
            get
            {
                if (HasInvalidationFlags(InvalidationFlags.NegativeZ))
                {
                    negativeZ = Rotation * Vector3.NegativeZ;
                    ClearInvalidationFlags(InvalidationFlags.NegativeZ);
                }

                return negativeZ;
            }
        }

        private Vector3 position;
        /// <summary>
        /// Gets the world space position.
        /// </summary>
        public Vector3 Position
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

        private Vector3 positiveX;
        /// <summary>
        /// Gets the world space +X direction.
        /// </summary>
        public Vector3 PositiveX
        {
            get
            {
                if (HasInvalidationFlags(InvalidationFlags.PositiveX))
                {
                    positiveX = Rotation * Vector3.PositiveX;
                    ClearInvalidationFlags(InvalidationFlags.PositiveX);
                }

                return positiveX;
            }
        }

        private Vector3 positiveY;
        /// <summary>
        /// Gets the world space +Y direction.
        /// </summary>
        public Vector3 PositiveY
        {
            get
            {
                if (HasInvalidationFlags(InvalidationFlags.PositiveY))
                {
                    positiveY = Rotation * Vector3.PositiveY;
                    ClearInvalidationFlags(InvalidationFlags.PositiveY);
                }

                return positiveY;
            }
        }

        private Vector3 positiveZ;
        /// <summary>
        /// Gets the world space +Z direction.
        /// </summary>
        public Vector3 PositiveZ
        {
            get
            {
                if (HasInvalidationFlags(InvalidationFlags.PositiveZ))
                {
                    positiveZ = Rotation * Vector3.PositiveZ;
                    ClearInvalidationFlags(InvalidationFlags.PositiveZ);
                }

                return positiveZ;
            }
        }

        private Quaternion rotation;
        /// <summary>
        /// Gets the world space rotation.
        /// </summary>
        public Quaternion Rotation
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

        private Vector3 scale;
        /// <summary>
        /// Gets the world space scale.
        /// </summary>
        public Vector3 Scale
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
        /// Converts the world space direction into local space.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public Vector3 ToLocalDirection(in Vector3 direction)
        {
            return InverseRotation * direction;
        }

        /// <summary>
        /// Converts the world space position into local space.
        /// </summary>
        /// <param name="position">The position.</param>
        public Vector3 ToLocalPosition(in Vector3 position)
        {
            return InverseRotation * (position - Position);
        }

        /// <summary>
        /// Converts the local space direction into world space.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public Vector3 ToWorldDirection(in Vector3 direction)
        {
            return Rotation * direction;
        }

        /// <summary>
        /// Converts the local space position into world space.
        /// </summary>
        /// <param name="position">The position.</param>
        public Vector3 ToWorldPosition(in Vector3 position)
        {
            return Position + Rotation * position;
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            LocalPosition = Vector3.Zero;
            LocalRotation = Quaternion.Identity;
            LocalScale = Vector3.One;
        }
    }
}
