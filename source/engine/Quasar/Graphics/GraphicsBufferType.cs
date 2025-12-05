//-----------------------------------------------------------------------
// <copyright file="GraphicsBufferType.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Graphics
{
    /// <summary>
    /// The graphics buffer type enumeration.
    /// </summary>
    public enum GraphicsBufferType
    {
        /// <summary>
        /// The uninitialized buffer type.
        /// </summary>
        Uninitialized = 0,

        /// <summary>
        /// The vertex buffer type.
        /// </summary>
        VertexBuffer = 1,

        /// <summary>
        /// The index buffer type.
        /// </summary>
        IndexBuffer = 2,
    }
}
