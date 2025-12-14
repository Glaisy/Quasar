//-----------------------------------------------------------------------
// <copyright file="IGraphicsContext.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Graphics
{
    /// <summary>
    /// Represents the context information for a graphics environment.
    /// </summary>
    public interface IGraphicsContext
    {
        /// <summary>
        /// Gets the command processor.
        /// </summary>
        IGraphicsCommandProcessor CommandProcessor { get; }

        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        IGraphicsDevice Device { get; }

        /// <summary>
        /// Gets the graphics platform.
        /// </summary>
        GraphicsPlatform Platform { get; }

        /// <summary>
        /// Gets the primary frame buffer.
        /// </summary>
        IFrameBuffer PrimaryFrameBuffer { get; }

        /// <summary>
        /// Gets the graphics platform version.
        /// </summary>
        Version Version { get; }
    }
}
