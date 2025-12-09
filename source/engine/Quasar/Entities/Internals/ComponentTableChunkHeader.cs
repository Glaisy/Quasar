//-----------------------------------------------------------------------
// <copyright file="ComponentTableChunkHeader.cs" company="Space Development">
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
    internal unsafe struct ComponentTableChunkHeader
    {
        /// <summary>
        /// The address of the component table.
        /// </summary>
        public ComponentTable* ComponentTable;

        /// <summary>
        /// The address of the previous component table chunk.
        /// </summary>
        public ComponentTableChunk* Previous;

        /// <summary>
        /// The address of the next component table chunk.
        /// </summary>
        public ComponentTableChunk* Next;
    }
}
