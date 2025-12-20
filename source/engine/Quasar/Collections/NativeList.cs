//-----------------------------------------------------------------------
// <copyright file="NativeList.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Space.Core;

namespace Quasar.Collections
{
    /// <summary>
    /// Native data list structure to store and fast access an dynamic list of unmanaged data items.
    /// </summary>
    /// <typeparam name="T">The value data type.</typeparam>
    public unsafe struct NativeList<T> : IDisposable
        where T : unmanaged
    {
        private T* buffer;


        /// <summary>
        /// Initializes a new instance of the <see cref="NativeList{T}"/> struct.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public NativeList(int capacity)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(capacity, nameof(capacity));

            buffer = capacity > 0 ? AllocateBuffer(capacity) : null;
            Capacity = capacity;
            Count = 0;
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
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count, nameof(Count));
#endif
                return buffer[index];
            }
            set
            {
#if QUASAR_BOUNDS_CHECKS
                ArgumentOutOfRangeException.ThrowIfNegative(index, nameof(index));
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count, nameof(Count));
#endif
                buffer[index] = value;
            }
        }


        /// <summary>
        /// Gets the total item capacity of the list.
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        /// Gets the actual number of items in the list.
        /// </summary>
        public int Count { get; private set; }


        /// <summary>
        /// Clears the items in the array.
        /// </summary>
        public void Clear()
        {
            Count = 0;
        }

        /// <summary>
        /// Adds the specified value to the end of the list.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Add(in T value)
        {
            EnsureBufferIsBigEnough(1);

            buffer[Count] = value;
            Count++;
        }

        /// <summary>
        /// Adds the specified range of values to the end of the list.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="count">The count.</param>
        public void AddRange(T* values, int count)
        {
            Assertion.ThrowIfEqual(values == null, true, nameof(values));
            Assertion.ThrowIfNegative(count, nameof(count));
            if (count == 0)
            {
                return;
            }

            EnsureBufferIsBigEnough(count);

            CopyBuffer(buffer + Count, values, count);

            Count += count;
        }

        /// <summary>
        /// Adds the specified range of values to the end of the list.
        /// </summary>
        /// <param name="values">The values.</param>
        public void AddRange(T[] values)
        {
            Assertion.ThrowIfNull(values, nameof(values));

            if (values.Length == 0)
            {
                return;
            }

            EnsureBufferIsBigEnough(values.Length);

            fixed (T* valuesPtr = values)
            {
                CopyBuffer(buffer + Count, valuesPtr, values.Length);
            }

            Count += values.Length;
        }

        /// <summary>
        /// Adds the specified range of values to the end of the list.
        /// </summary>
        /// <param name="values">The values.</param>
        public void AddRange(IReadOnlyCollection<T> values)
        {
            Assertion.ThrowIfNull(values, nameof(values));

            EnsureBufferIsBigEnough(values.Count);

            var itemPtr = buffer + Count;
            var count = values.Count;
            foreach (var value in values)
            {
                *itemPtr++ = value;
            }

            Count += count;
        }

        /// <summary>
        /// Gets the native data pointer of item(s) at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        public T* GetData(int index = 0)
        {
#if QUASAR_BOUNDS_CHECKS
            ArgumentOutOfRangeException.ThrowIfNegative(index, nameof(index));
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count, nameof(Count));
#endif

            return buffer + index;
        }


        private static T* AllocateBuffer(int capacity)
        {
            return (T*)Marshal.AllocHGlobal(capacity * sizeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CopyBuffer(T* target, T* source, int count)
        {
            var byteCount = count * sizeof(T);
            Unsafe.CopyBlock(target, source, (uint)byteCount);
        }

        private void EnsureBufferIsBigEnough(int capacityNeeded)
        {
            var requiredCapacity = Count + capacityNeeded;
            if (Capacity >= capacityNeeded)
            {
                return;
            }

            var newCapacity = Math.Max(2, Capacity);
            while (newCapacity < requiredCapacity)
            {
                newCapacity = (newCapacity * 3) >> 1;
            }

            var newBuffer = AllocateBuffer(newCapacity);
            if (Count > 0)
            {
                CopyBuffer(newBuffer, buffer, Count);
            }

            ReleaseBuffer();
            buffer = newBuffer;
            Capacity = newCapacity;
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
