//-----------------------------------------------------------------------
// <copyright file="GraphicsResourceDescriptor.cs" company="Space Development">
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
    /// Graphics resource descriptor structure which contains general resource information.
    /// </summary>
    public readonly struct GraphicsResourceDescriptor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsResourceDescriptor" /> struct.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="usage">The resource usage.</param>
        public GraphicsResourceDescriptor(IGraphicsDevice graphicsDevice, GraphicsResourceUsage usage)
        {
            GraphicsDevice = graphicsDevice;
            Usage = usage;
        }


        /// <summary>
        /// The graphics device.
        /// </summary>
        public readonly IGraphicsDevice GraphicsDevice;

        /// <summary>
        /// The graphics resource usage.
        /// </summary>
        public readonly GraphicsResourceUsage Usage;
    }
}
