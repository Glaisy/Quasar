//-----------------------------------------------------------------------
// <copyright file="ComponentTable.cs" company="Space Development">
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
    /// Represents a component table.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct ComponentTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentTable" /> struct.
        /// </summary>
        /// <param name="typeId">The type identifier.</param>
        /// <param name="componentSize">The component size in bytes.</param>
        /// <param name="componentsPerChunk">The components per chunk.</param>
        public ComponentTable(int typeId, int componentSize, int componentsPerChunk)
        {
            TypeId = typeId;
            ComponentSize = componentSize;
            ComponentsPerChunk = componentsPerChunk;
        }


        /// <summary>
        /// The component type identifier.
        /// </summary>
        public readonly int TypeId;

        /// <summary>
        /// The component size in bytes.
        /// </summary>
        public readonly int ComponentSize;

        /// <summary>
        /// The number of components per chunk.
        /// </summary>
        public readonly int ComponentsPerChunk;

        /// <summary>
        /// The address of the first component table chunk.
        /// </summary>
        public ComponentTableChunk* Head;

        /// <summary>
        /// The address of the last component table chunk.
        /// </summary>
        public ComponentTableChunk* Tail;
    }
}
