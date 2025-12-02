//-----------------------------------------------------------------------
// <copyright file="IGraphicsOutputProvider.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace Quasar.Graphics
{
    /// <summary>
    /// Graphics output provider interface definition.
    /// </summary>
    public interface IGraphicsOutputProvider
    {
        /// <summary>
        /// Gets the active graphics output.
        /// </summary>
        IGraphicsOutput ActiveGraphicsOutput { get; }

        /// <summary>
        /// Gets all available grphics outputs in the system.
        /// </summary>
        IReadOnlyList<IGraphicsOutput> GraphicsOutputs { get; }
    }
}
