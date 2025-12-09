//-----------------------------------------------------------------------
// <copyright file="ComponentHeader.cs" company="Space Development">
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
    /// Represents a component header to provide navigation through entity components.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct ComponentHeader
    {
        /// <summary>
        /// The link to the previous component header.
        /// </summary>
        public ComponentHeader* Previous;

        /// <summary>
        /// The link to the next component header.
        /// </summary>
        public ComponentHeader* Next;

        /// <summary>
        /// The component table chunk.
        /// </summary>
        public ComponentTableChunk* Chunk;
    }
}
