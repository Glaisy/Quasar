//-----------------------------------------------------------------------
// <copyright file="NativeArray.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Space.Core;

namespace Quasar.Collections
{
    /// <summary>
    /// Native data array structure to store and fast access an array of unmanaged data items.
    /// </summary>
    /// <typeparam name="T">The value data type.</typeparam>
    public unsafe struct NativeArray<T> : IDisposable
        where T : unmanaged
    {
        private T* buffer;


        /// <summary>
        /// Initializes a new instance of the <see cref="NativeArray{T}"/> struct.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public NativeArray(int capacity)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(capacity, nameof(capacity));

            buffer = capacity > 0 ? AllocateBuffer(capacity) : null;
            Capacity = capacity;
            Length = 0;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            ReleaseBuffer();
        }


        /// <summary>
        /// Gets or sets the item at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        public T this[int index]
        {
            get
            {
#if QUASAR_BOUNDS_CHECKS
                ArgumentOutOfRangeException.ThrowIfNegative(index, nameof(index));
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Length, nameof(Length));
#endif
                return buffer[index];
            }
            set
            {
#if QUASAR_BOUNDS_CHECKS
                ArgumentOutOfRangeException.ThrowIfNegative(index, nameof(index));
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Length, nameof(Length));
#endif
                buffer[index] = value;
            }
        }


        /// <summary>
        /// Gets the total item capacity of the array.
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        /// Gets the actual number of items in the array.
        /// </summary>
        public int Length { get; private set; }


        /// <summary>
        /// Clears the items in the array.
        /// </summary>
        public void Clear()
        {
            Length = 0;
        }

        /// <summary>
        /// Expands the number of items in the array by the specified count.
        /// </summary>
        /// <param name="count">The count.</param>
        public void Expand(int count)
        {
            Assertion.ThrowIfNegative(count, nameof(count));

            if (count <= 0)
            {
                return;
            }

            // fits into the current capacity?
            var requiredCapacity = Length + count;
            if (Capacity >= requiredCapacity)
            {
                Length += count;
                return;
            }

            // determine new capacity
            var newCapacity = Capacity;
            while (newCapacity < requiredCapacity)
            {
                newCapacity = (newCapacity * 3) >> 1;
            }

            // copy items to a new increased buffer
            var newBuffer = AllocateBuffer(newCapacity);
            if (Length > 0)
            {
                CopyBuffer(newBuffer, buffer, Length);
            }

            // update properties
            ReleaseBuffer();
            buffer = newBuffer;
            Capacity = newCapacity;
            Length += count;
        }

        /// <summary>
        /// Gets the native data pointer of item(s) at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        public T* GetData(int index = 0)
        {
#if QUASAR_BOUNDS_CHECKS
            ArgumentOutOfRangeException.ThrowIfNegative(index, nameof(index));
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Length, nameof(Length));
#endif

            return buffer + index;
        }

        /// <summary>
        /// Shrinks the number of the items in the array by the specified count.
        /// </summary>
        /// <param name="count">The count.</param>
        public void Shrink(int count)
        {
            Assertion.ThrowIfNegative(count, nameof(count));
            Assertion.ThrowIfGreaterThan(count, Length, nameof(count));

            Length -= count;
        }


        private static T* AllocateBuffer(int capacity)
        {
            return (T*)Marshal.AllocHGlobal(capacity * sizeof(T));
        }

        private static void CopyBuffer(T* target, T* source, int count)
        {
            var byteCount = count * sizeof(T);
            Unsafe.CopyBlock(target, source, (uint)byteCount);
        }

        private void ReleaseBuffer()
        {
            if (buffer == null)
            {
                return;
            }

            Marshal.FreeHGlobal(new IntPtr(buffer));
            buffer = null;
        }
    }
}
