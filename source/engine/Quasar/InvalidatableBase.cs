//-----------------------------------------------------------------------
// <copyright file="InvalidatableBase.cs" company="Space Development">
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
    /// Abstract base class of objects with invalidation flags.
    /// </summary>
    public abstract class InvalidatableBase
    {
        private int invalidationFlags;
        private static int lastTimestamp;


        /// <summary>
        /// Gets the invalidation timestamp.
        /// </summary>
        public int Timestamp { get; private set; }


        /// <summary>
        /// Clears the invalidation flags.
        /// </summary>
        /// <param name="invalidationFlags">The invalidation flags.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void ClearInvalidationFlags(int invalidationFlags)
        {
            this.invalidationFlags &= ~invalidationFlags;
        }

        /// <summary>
        /// Determines whether the specified invalidation flag(s) is/are set.
        /// </summary>
        /// <param name="invalidationFlags">The invalidation flags.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected bool HasInvalidationFlags(int invalidationFlags)
        {
            return (this.invalidationFlags & invalidationFlags) == invalidationFlags;
        }

        /// <summary>
        /// Invalidates the object by the specified invalidation flags.
        /// </summary>
        /// <param name="invalidationFlags">The invalidation flags.</param>
        protected void Invalidate(int invalidationFlags)
        {
            this.invalidationFlags |= invalidationFlags;
            Timestamp = Interlocked.Increment(ref lastTimestamp);
        }
    }
}
