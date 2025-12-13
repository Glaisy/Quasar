//-----------------------------------------------------------------------
// <copyright file="ValueTypeCollection.cs" company="Space Development">
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
    /// Represents a generic collection optimized to hold and iterate unmanaged value type items.
    /// </summary>
    /// <typeparam name="T">The collection item data type.</typeparam>
    /// <seealso cref="ICollection{T}" />
    public unsafe class ValueTypeCollection<T> : DisposableBase
        where T : unmanaged
    {
        private const int MinimumCapacity = 8;
        private const double CapacityIncreaseRate = 1.5;


        private readonly int itemSize = Marshal.SizeOf<T>();
        private int capacity;
        private byte* items;


        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTypeCollection{T}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public ValueTypeCollection(int capacity = 0)
        {
            ReallocateItems(Math.Max(MinimumCapacity, capacity));
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (items == null)
            {
                return;
            }

            Marshal.FreeHGlobal(new IntPtr(items));
            items = null;
            capacity = 0;
        }


        /// <summary>
        /// Gets the reference of the item at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The reference of the item at index.</returns>
        public ref T this[int index]
        {
            get
            {
                Assertion.ThrowIfEqual(index < 0 || index >= Count, true, nameof(index));

                return ref Unsafe.AsRef<T>(items + itemSize * index);
            }
        }


        /// <summary>
        /// Gets the number of items in the collection.
        /// </summary>
        public int Count { get; private set; }


        /// <summary>
        /// Adds the specified item to the collection.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(in T item)
        {
            EnsureCanStoreItem();

            *(T*)(items + itemSize * Count) = item;
            Count++;
        }

        /// <summary>
        /// Clears all items from the collection.
        /// </summary>
        public void Clear()
        {
            Count = 0;
        }


        private void EnsureCanStoreItem()
        {
            if (Count < capacity)
            {
                return;
            }

            var newCapacity = Math.Max(MinimumCapacity, (int)(capacity * CapacityIncreaseRate));
            ReallocateItems(newCapacity);
        }

        private void ReallocateItems(int capacity)
        {
            var newItems = (byte*)Marshal.AllocHGlobal(capacity * itemSize);
            if (items != null)
            {
                if (Count > 0)
                {
                    Unsafe.CopyBlock(newItems, items, (uint)(Count * itemSize));
                }

                Marshal.FreeHGlobal(new IntPtr(items));
            }

            this.capacity = capacity;
            items = newItems;
        }
    }
}
