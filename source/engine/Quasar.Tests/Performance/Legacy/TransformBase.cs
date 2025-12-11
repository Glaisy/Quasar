//-----------------------------------------------------------------------
// <copyright file="TransformBase.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

using Space.Core;

namespace Quasar.Tests.Performance.Legacy
{
    /// <summary>
    /// Abstract base class for 3D space transformation objects.
    /// </summary>
    /// <typeparam name="T">The transformation implementation type.</typeparam>
    /// <seealso cref="DisposableBase" />
    /// <seealso cref="IEnumerable{T}" />
    /// <seealso cref="INameProvider" />
    public abstract class TransformBase<T> : DisposableBase, IEnumerable<T>, INameProvider
        where T : TransformBase<T>
    {
        /// <summary>
        /// Transformation invalidation type flags enumeration.
        /// </summary>
        [Flags]
        protected internal enum InvalidationFlags
        {
            /// <summary>
            /// The none invalidation type.
            /// </summary>
            None = 0,

            /// <summary>
            /// The world space position invalidation type.
            /// </summary>
            Position = 1,

            /// <summary>
            /// The world space negative Z vector invalidation type.
            /// </summary>
            NegativeX = 2,

            /// <summary>
            /// The world space negative Y vector invalidation type.
            /// </summary>
            NegativeY = 4,

            /// <summary>
            /// The world space negative Z vector invalidation type.
            /// </summary>
            NegativeZ = 8,

            /// <summary>
            /// The world space positive X vector invalidation type.
            /// </summary>
            PositiveX = 16,

            /// <summary>
            /// The world space positive Y vector invalidation type.
            /// </summary>
            PositiveY = 32,

            /// <summary>
            /// The world space positive Z vector invalidation type.
            /// </summary>
            PositiveZ = 64,

            /// <summary>
            /// The world space scale invalidation type.
            /// </summary>
            Scale = 128,

            /// <summary>
            /// The world space rotation invalidation type.
            /// </summary>
            Rotation = 256,

            /// <summary>
            /// The world space inverse rotation invalidation type.
            /// </summary>
            InverseRotation = 512,

            /// <summary>
            /// All vectors invalidation type.
            /// </summary>
            AllVectors = NegativeX | NegativeY | NegativeZ | PositiveX | PositiveY | PositiveZ,

            /// <summary>
            /// The all invalidation types.
            /// </summary>
            All = Position | Rotation | InverseRotation | Scale | AllVectors
        }


        private T firstTransform;
        private T lastTransform;
        private T previousTransform;
        private T nextTransform;
        private InvalidationFlags invalidationFlags;
        private static int lastTimestamp;


        /// <summary>
        /// Initializes a new instance of the <see cref="TransformBase{T}" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected TransformBase(string name = null)
        {
            Name = name;
            Invalidate(InvalidationFlags.All);
        }


        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent transformation.
        /// </summary>
        public abstract T Parent { get; set; }

        /// <summary>
        /// Gets the timestamp.
        /// </summary>
        public int Timestamp { get; private set; }


        /// <summary>
        /// Returns an enumerator that iterates through the child transforms.
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            var transform = firstTransform;
            while (transform != null)
            {
                yield return transform;
                transform = transform.nextTransform;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        /// <summary>
        /// Adds the child tranformation.
        /// </summary>
        /// <param name="childTransform">The child transformation.</param>
        protected internal void AddChildTransform(T childTransform)
        {
            if (firstTransform == null)
            {
                // first child
                firstTransform = lastTransform = childTransform;
            }
            else
            {
                // insert as last
                lastTransform.nextTransform = childTransform;
                childTransform.previousTransform = lastTransform;
                lastTransform = childTransform;
            }
        }

        /// <summary>
        /// Clears the invalidation flags.
        /// </summary>
        /// <param name="invalidationflags">The invalidationflags.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal void ClearInvalidationFlags(InvalidationFlags invalidationflags)
        {
            invalidationFlags &= ~invalidationflags;
        }

        /// <summary>
        /// Determines whether the specified invalidation flag(s) is/are set.
        /// </summary>
        /// <param name="invalidationFlags">The invalidation flags.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal bool HasInvalidationFlags(InvalidationFlags invalidationFlags)
        {
            return (this.invalidationFlags & invalidationFlags) == invalidationFlags;
        }

        /// <summary>
        /// Invalidates the transformation by the specified invalidation flags.
        /// </summary>
        /// <param name="invalidationFlags">The invalidation types.</param>
        protected internal void Invalidate(InvalidationFlags invalidationFlags)
        {
            // invalidate this transform
            this.invalidationFlags |= invalidationFlags;
            UpdateTimestamp();

            // invalidate child transforms
            var transform = firstTransform;
            while (transform != null)
            {
                transform.Invalidate(InvalidationFlags.All);
                transform = transform.nextTransform;
            }
        }

        /// <summary>
        /// Removes the child transformation.
        /// </summary>
        /// <param name="childTransform">The child transformation.</param>
        protected internal void RemoveChildTransform(T childTransform)
        {
            // update last transform if removed
            if (lastTransform == childTransform)
            {
                lastTransform = lastTransform.previousTransform;
            }

            // update child transform chain
            if (childTransform.previousTransform == null)
            {
                // first child
                firstTransform = childTransform.nextTransform;
                if (firstTransform != null)
                {
                    firstTransform.previousTransform = null;
                }
            }
            else
            {
                // not first child
                childTransform.previousTransform.nextTransform = childTransform.nextTransform;
                if (childTransform.nextTransform != null)
                {
                    childTransform.nextTransform.previousTransform = childTransform.previousTransform;
                }
            }

            childTransform.previousTransform = childTransform.nextTransform = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateTimestamp()
        {
            Timestamp = Interlocked.Increment(ref lastTimestamp);
        }
    }
}
