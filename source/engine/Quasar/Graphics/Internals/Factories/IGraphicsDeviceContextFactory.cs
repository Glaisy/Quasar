//-----------------------------------------------------------------------
// <copyright file="IGraphicsDeviceContextFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Graphics.Internals.Factories
{
    /// <summary>
    /// Represents the graphics device context factory component.
    /// </summary>
    public interface IGraphicsDeviceContextFactory
    {
        /// <summary>
        /// Creates the graphics context by the specified platform.
        /// </summary>
        /// <param name="graphicsPlatform">The graphics platform.</param>
        IGraphicsDeviceContext Create(GraphicsPlatform graphicsPlatform);
    }
}
