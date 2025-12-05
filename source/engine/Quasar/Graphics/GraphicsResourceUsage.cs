//-----------------------------------------------------------------------
// <copyright file="GraphicsResourceUsage.cs" company="Space Development">
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
    /// Identifies expected resource use during rendering. The usage directly reflects whether a resource is accessible by the CPU and/or the GPU.
    /// </summary>
    public enum GraphicsResourceUsage
    {
        /// <summary>
        /// A resource that is not bound to GPU.
        /// </summary>
        None,

        /// <summary>
        /// A resource that requires read and write access by the GPUs.
        /// </summary>
        Default,

        /// <summary>
        /// A resource that can only be read by the GPU. It cannot be written by the GPU, and cannot be accessed at all by the CPU.
        /// </summary>
        Immutable,

        /// <summary>
        /// A resource that is accessible by both the GPU (read only) and the CPU (write only).
        /// </summary>
        Dynamic,

        /// <summary>
        /// A resource that supports data transfer (copy) from the GPU to the CPU.
        /// </summary>
        Staging
    }
}
