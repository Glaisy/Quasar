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

using System.Runtime.CompilerServices;
using System.Threading;

namespace Quasar
{
    /// <summary>
    /// Abstract base class for 3D space transformation objects.
    /// </summary>
    /// <seealso cref="INameProvider" />
    public abstract partial class TransformBase : INameProvider
    {
        private InvalidationType invalidationType;
        private static int lastTimestamp;


        /// <summary>
        /// Initializes a new instance of the <see cref="TransformBase" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected TransformBase(string name = null)
        {
            Name = name;
        }


        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the timestamp.
        /// </summary>
        public int Timestamp { get; private set; }



        /// <summary>
        /// Clears the invalidation type flags.
        /// </summary>
        /// <param name="invalidationType">The invalidation type flags.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void ClearInvalidationType(InvalidationType invalidationType)
        {
            this.invalidationType &= ~invalidationType;
        }

        /// <summary>
        /// Invalidates the transformation by the specified invalidation type.
        /// </summary>
        /// <param name="invalidationType">Type of the invalidation.</param>
        protected void Invalidate(InvalidationType invalidationType)
        {
            this.invalidationType |= invalidationType;
            Timestamp = Interlocked.Increment(ref lastTimestamp);
        }

        /// <summary>
        /// Determines whether the specified invalidation type flag(s) is/are set.
        /// </summary>
        /// <param name="invalidationType">The invalidation flags.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected bool IsInvalidationTypeSet(InvalidationType invalidationType)
        {
            return (this.invalidationType & invalidationType) == invalidationType;
        }
    }
}
