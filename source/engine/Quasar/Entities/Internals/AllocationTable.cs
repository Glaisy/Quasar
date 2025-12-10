//-----------------------------------------------------------------------
// <copyright file="AllocationTable.cs" company="Space Development">
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

namespace Quasar.Entities.Internals
{
    /// <summary>
    /// Represent an allocation table structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct AllocationTable
    {
        /// <summary>
        /// The maximum number of entries in an allocation table.
        /// </summary>
        public const int MaxEntryCount = BitmapArrayLength * BitCount;

        /// <summary>
        /// The size of the structure in bytes.
        /// </summary>
        public static readonly int Size = Marshal.SizeOf<AllocationTable>();


        private const int BitmapArrayLength = 16;
        private const int ByteCount = sizeof(ulong);
        private const int BitCount = ByteCount * 8;
        private const int BitmapArraySizeInBytes = BitmapArrayLength * ByteCount;
        private const ulong FullBitmap = 0xFFFFFFFFFFFFFFFF;


        private int entryCount;
        private int freeEntryCount;
        private fixed ulong bitmapArray[BitmapArrayLength];


        /// <summary>
        /// Frees the slot at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        public void Free(int index)
        {
            Assertion.ThrowIfGreaterThanOrEqual(index, entryCount, nameof(index));
            Assertion.ThrowIfNegative(index, nameof(index));

            var bitIndex = index % BitCount;
            var bitmask = 1UL << bitIndex;
            var bitmapIndex = index / BitCount;
            var bitmap = bitmapArray[bitmapIndex];

#if QUASAR_ECS_SANITY_CHECKS
            if ((bitmap & bitmask) == 0)
            {
                throw new InvalidOperationException($"Unable to free allocation table slot at index: {index}. Not allocated yet.");
            }
#endif

            bitmapArray[bitmapIndex] &= ~bitmask;
            freeEntryCount++;

#if QUASAR_ECS_SANITY_CHECKS
            if (freeEntryCount > entryCount)
            {
                throw new InvalidOperationException($"Allocation table free entry count is out of range: {freeEntryCount}.");
            }
#endif
        }

        /// <summary>
        /// Initializes the allocation table.
        /// The maximum number of entries should in the range [0...AllocationTable.MaxEntryCount].
        /// </summary>
        /// <param name="entryCount">The maximum number of entries.</param>
        public void Initialize(int entryCount)
        {
            Assertion.ThrowIfEqual(entryCount < 0 || entryCount > MaxEntryCount, true, nameof(entryCount));

            this.entryCount = entryCount;
            freeEntryCount = entryCount;
            for (var i = 0; i < BitmapArrayLength; i++)
            {
                bitmapArray[i] = 0UL;
            }
        }

        /// <summary>
        /// Tries the allocate a slot.
        /// </summary>
        /// <param name="index">The index of the allocated slot.</param>
        /// <returns>True if the allocation was successfull; otherwise false.</returns>
        public bool TryAllocate(out int index)
        {
            if (freeEntryCount == 0)
            {
                index = 0;
                return false;
            }

            for (var i = 0; i < BitmapArrayLength; i++)
            {
                var bitmap = bitmapArray[i];
                if (bitmap == FullBitmap)
                {
                    continue;
                }

                // find slot index and mask for the bitmap
                var slotIndex = FindFirstFreeSlotIndex(bitmap, out var mask);

                // update bitmaps
                bitmapArray[i] = bitmap | mask;
                freeEntryCount--;

                // calculate final index
                index = slotIndex + i * BitCount;
                return true;
            }

            index = 0;
            return false;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int FindFirstFreeSlotIndex(ulong bitmap, out ulong mask)
        {
            mask = 1UL;
            for (var i = 0; i < BitCount; i++, mask <<= 1)
            {
                if ((bitmap & mask) == 0UL)
                {
                    return i;
                }
            }

            throw new InvalidOperationException($"There is no free slot for bitmap: 0x{bitmap:X8}");
        }
    }
}
