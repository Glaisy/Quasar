//-----------------------------------------------------------------------
// <copyright file="ComponentTableChunkInitializationData.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Entities.Internals
{
    /// <summary>
    /// Represents the initialization data structure for component table chunks.
    /// </summary>
    internal unsafe readonly struct ComponentTableChunkInitializationData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentTableChunkInitializationData"/> struct.
        /// </summary>
        /// <param name="nativeAddress">The native address.</param>
        /// <param name="chunkSize">The chunk size in bytes.</param>
        /// <param name="componentSize">The underlying component structure size in bytes.</param>
        /// <param name="componentTable">The component table's address.</param>
        /// <param name="previousChunk">The previous chunk's address.</param>
        /// <param name="nextChunk">The next chunk's address.</param>
        public ComponentTableChunkInitializationData(
            IntPtr nativeAddress,
            int chunkSize,
            int componentSize,
            ComponentTable* componentTable,
            ComponentTableChunk* previousChunk,
            ComponentTableChunk* nextChunk)
        {
            NativeAddress = nativeAddress;
            ChunkSize = chunkSize;
            ComponentSize = componentSize;
            ComponentTable = componentTable;
            PreviousChunk = previousChunk;
            NextChunk = nextChunk;
        }


        /// <summary>
        /// The chunk size in bytes.
        /// </summary>
        public readonly int ChunkSize;

        /// <summary>
        /// The underlying component structure size in bytes.
        /// </summary>
        public readonly int ComponentSize;

        /// <summary>
        /// The component table address.
        /// </summary>
        public readonly ComponentTable* ComponentTable;

        /// <summary>
        /// The native address.
        /// </summary>
        public readonly IntPtr NativeAddress;

        /// <summary>
        /// The next chunk's address.
        /// </summary>
        public readonly ComponentTableChunk* NextChunk;

        /// <summary>
        /// The previous chunk's address.
        /// </summary>
        public readonly ComponentTableChunk* PreviousChunk;
    }
}
