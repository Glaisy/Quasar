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
using System.Runtime.InteropServices;

using Space.Core;

namespace Quasar.Entities.Internals
{
    /// <summary>
    /// Represent an allocation table structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal unsafe struct AllocationTable
    {
        /// <summary>
        /// The number of entries in the allocation table.
        /// </summary>
        public const ushort EntryCount = BitmapArrayLength * BitCount;


        private const int BitmapArrayLength = 16;
        private const int ByteCount = sizeof(ulong);
        private const int BitCount = ByteCount * 8;
        private const int BitmapArraySizeInBytes = BitmapArrayLength * ByteCount;
        private const ulong FullBitmap = 0xFFFFFFFFFFFFFFFF;


        private int freeEntryCount;
        private fixed ulong bitmaps[BitmapArrayLength];


        /// <summary>
        /// Frees the slot at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        public void Free(int index)
        {
            Assertion.ThrowIfGreaterThanOrEqual(index, EntryCount, nameof(index));
            Assertion.ThrowIfNegative(index, nameof(index));

            var bitIndex = index % BitCount;
            var bitmask = 1UL << bitIndex;
            var bitmapIndex = index / BitCount;
            var bitmap = bitmaps[bitmapIndex];
#if DEBUG
            if ((bitmap & bitmask) == 0)
            {
                throw new InvalidOperationException($"Unable to free allocation table slot at index: {index}. Not allocated yet.");
            }
#endif
            bitmaps[bitmapIndex] &= ~bitmask;
            freeEntryCount++;
#if DEBUG
            if (freeEntryCount > EntryCount)
            {
                throw new InvalidOperationException($"Allocation table free entry count is out of range: {freeEntryCount}.");
            }
#endif
        }

        /// <summary>
        /// Initializes the allocation table.
        /// </summary>
        public void Initialize()
        {
            freeEntryCount = EntryCount;
            for (var i = 0; i < BitmapArrayLength; i++)
            {
                bitmaps[i] = 0UL;
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
                var bitmap = bitmaps[i];
                if (bitmap == FullBitmap)
                {
                    continue;
                }

                // find slot index and mask for the bitmap
                var slotIndex = FindFirstFreeSlotIndex(bitmap, out var mask);

                // update bitmaps
                bitmaps[i] = bitmap | mask;
                freeEntryCount--;

                // calculate final index
                index = slotIndex + i * BitCount;
                return true;
            }

            index = 0;
            return false;
        }


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
