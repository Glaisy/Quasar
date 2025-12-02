//-----------------------------------------------------------------------
// <copyright file="IGraphicsOutput.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;

using Space.Core;

namespace Quasar.Graphics
{
    /// <summary>
    /// Graphics output interface definition.
    /// </summary>
    /// <seealso cref="IIdentifierProvider{String}" />
    public interface IGraphicsOutput : IIdentifierProvider<string>
    {
        /// <summary>
        /// Gets the current display mode.
        /// </summary>
        IDisplayMode CurrentDisplayMode { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the supported display modes.
        /// </summary>
        IReadOnlyList<IDisplayMode> SupportedDisplayModes { get; }
    }
}
