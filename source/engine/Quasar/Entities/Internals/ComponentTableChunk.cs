//-----------------------------------------------------------------------
// <copyright file="ComponentTableChunk.cs" company="Space Development">
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

namespace Quasar.Entities.Internals
{
    /// <summary>
    /// Represents a chunk header of a component table.
    /// Hold component header and data array + an allocation bitmap.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal unsafe struct ComponentTableChunk
    {
        /// <summary>
        /// The offset of chunk data in bytes from the beginning of the chunk.
        /// </summary>
        public static readonly int DataOffset = RoundUpToInt64Boundary(Size);

        /// <summary>
        /// The size of the structure in bytes.
        /// </summary>
        public static readonly int Size = Marshal.SizeOf<ComponentTableChunk>();


        private ComponentTable* componentTable;
        private ComponentTableChunk* previousChunk;
        private ComponentTableChunk* nextChunk;
        private AllocationTable allocationTable;
        private int stride;
        private byte* components;


        /// <summary>
        /// Calculates the stride of component slots in bytes by the specified component size.
        /// </summary>
        /// <param name="componentSize">the component size in bytes.</param>
        public static int CalculateStride(int componentSize)
        {
            return RoundUpToInt64Boundary(ComponentHeader.Size + componentSize);
        }

        /// <summary>
        /// Frees a component slot the specified index.
        /// </summary>
        /// <param name="slotIndex">The component slot index.</param>
        public void Free(int slotIndex)
        {
            allocationTable.Free(slotIndex);
        }

        /// <summary>
        /// Initializes the component table chunk by the specified initialization data.
        /// </summary>
        /// <param name="initializationData">The initialization data.</param>
        public void Initialize(in ComponentTableChunkInitializationData initializationData)
        {
            componentTable = initializationData.ComponentTable;
            previousChunk = initializationData.PreviousChunk;
            nextChunk = initializationData.NextChunk;

            stride = CalculateStride(initializationData.ComponentSize);
            components = (byte*)(initializationData.NativeAddress + DataOffset);

            var entryCount = (initializationData.ChunkSize - DataOffset) / stride;
            entryCount = Math.Min(AllocationTable.MaxEntryCount, entryCount);
            allocationTable.Initialize(entryCount);
        }

        /// <summary>
        /// Removes the chunk from the component table's chunk chain.
        /// </summary>
        public void Remove()
        {
            // maintain double linked list references
            if (previousChunk != null)
            {
                previousChunk->nextChunk = nextChunk;
                if (nextChunk != null)
                {
                    nextChunk->previousChunk = previousChunk;
                }
            }

            if (nextChunk != null)
            {
                nextChunk->previousChunk = previousChunk;
                if (previousChunk != null)
                {
                    previousChunk->nextChunk = nextChunk;
                }
            }
        }

        /// <summary>
        /// Tries to allocate a slot for a component in the chunks.
        /// </summary>
        /// <param name="slotIndex">The component slot index.</param>
        /// <param name="componentHeader">The component header.</param>
        /// <returns>True if the allocation was successfull; otherwise false.</returns>
        public bool TryAllocate(out int slotIndex, out ComponentHeader* componentHeader)
        {
            if (!allocationTable.TryAllocate(out slotIndex))
            {
                componentHeader = null;
                return false;
            }

            var slotAddress = components + stride * slotIndex;
            Unsafe.InitBlock(slotAddress, 0, (uint)stride);

            componentHeader = (ComponentHeader*)slotAddress;
            return true;
        }


        private static int RoundUpToInt64Boundary(int value)
        {
            var modulus = value % 8;
            if (modulus != 0)
            {
                value += 8 - modulus;
            }

            return value;
        }
    }
}
