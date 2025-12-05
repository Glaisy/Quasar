//-----------------------------------------------------------------------
// <copyright file="IGraphicsResource.cs" company="Space Development">
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
    /// Represents the general properties of graphics resources.
    /// </summary>
    public interface IGraphicsResource
    {
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        IGraphicsDevice GraphicsDevice { get; }

        /// <summary>
        /// Gets the internal handle.
        /// </summary>
        int Handle { get; }

        /// <summary>
        /// Gets the usage.
        /// </summary>
        GraphicsResourceUsage Usage { get; }
    }
}
