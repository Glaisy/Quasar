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
        private ComponentTableChunkHeader header;
        private AllocationTable allocationTable;


        /// <summary>
        /// Initializes the component table chunk.
        /// </summary>
        /// <param name="componentTable">The component table.</param>
        /// <param name="previous">The previous chunk.</param>
        /// <param name="next">The next chunk.</param>
        public void Initialize(ComponentTable* componentTable, ComponentTableChunk* previous, ComponentTableChunk* next)
        {
            header.ComponentTable = componentTable;
            header.Previous = previous;
            header.Next = next;
            allocationTable.Initialize();
        }

        /// <summary>
        /// Removes the chunk from the component table's chunk chain.
        /// </summary>
        public void Remove()
        {
            // maintain double linked list references
            var previousChunk = header.Previous;
            var nextChunk = header.Next;
            if (previousChunk != null)
            {
                previousChunk->header.Next = nextChunk;
                if (nextChunk != null)
                {
                    nextChunk->header.Previous = previousChunk;
                }
            }

            if (nextChunk != null)
            {
                nextChunk->header.Previous = previousChunk;
                if (previousChunk != null)
                {
                    previousChunk->header.Next = nextChunk;
                }
            }
        }
    }
}
