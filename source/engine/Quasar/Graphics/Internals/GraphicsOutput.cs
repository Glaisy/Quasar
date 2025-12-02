//-----------------------------------------------------------------------
// <copyright file="GraphicsOutput.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Quasar.Graphics.Internals
{
    /// <summary>
    /// Graphics output object implementation.
    /// </summary>
    /// <seealso cref="IGraphicsOutput" />
    internal sealed class GraphicsOutput : IGraphicsOutput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsOutput" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="currentDisplayMode">The current display mode.</param>
        /// <param name="supportedDisplayModes">The supported display modes.</param>
        public GraphicsOutput(
            string id,
            string name,
            IDisplayMode currentDisplayMode,
            IReadOnlyList<IDisplayMode> supportedDisplayModes)
        {
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentNullException.ThrowIfNull(currentDisplayMode, nameof(currentDisplayMode));
            ArgumentNullException.ThrowIfNull(supportedDisplayModes, nameof(supportedDisplayModes));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(supportedDisplayModes.Count, nameof(supportedDisplayModes.Count));

            Id = id;
            Name = name;
            CurrentDisplayMode = currentDisplayMode;
            SupportedDisplayModes = supportedDisplayModes;
        }


        /// <inheritdoc/>
        public IDisplayMode CurrentDisplayMode { get; }

        /// <inheritdoc/>
        public string Id { get; }

        /// <inheritdoc/>
        public string Name { get; }

        /// <inheritdoc/>
        public IReadOnlyList<IDisplayMode> SupportedDisplayModes { get; }
    }
}
