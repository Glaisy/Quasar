//-----------------------------------------------------------------------
// <copyright file="IRenderingContext.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics;

namespace Quasar.Rendering
{
    /// <summary>
    /// Represents a context object for the rendering pipeline.
    /// </summary>
    public interface IRenderingContext
    {
        /// <summary>
        /// Gets the graphics command processor.
        /// </summary>
        IGraphicsCommandProcessor CommandProcessor { get; }


        /// <summary>
        /// Initializes the context by the specified graphics device context.
        /// </summary>
        /// <param name="graphicsDeviceContext">The graphics device context.</param>
        internal void Initialize(IGraphicsDeviceContext graphicsDeviceContext);
    }
}
