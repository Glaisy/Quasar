//-----------------------------------------------------------------------
// <copyright file="IDisplayMode.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Space.Core;

namespace Quasar.Graphics
{
    /// <summary>
    /// Represents a graphics display mode.
    /// </summary>
    /// <seealso cref="IIdentifierProvider{String}" />
    public interface IDisplayMode : IIdentifierProvider<string>
    {
        /// <summary>
        /// Gets the number of bits per pixel.
        /// </summary>
        int BitsPerPixel { get; }

        /// <summary>
        /// Gets the refresh rate.
        /// </summary>
        int RefreshRate { get; }

        /// <summary>
        /// Gets the resolution.
        /// </summary>
        Size Resolution { get; }
    }
}