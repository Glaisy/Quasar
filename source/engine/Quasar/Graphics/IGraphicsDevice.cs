//-----------------------------------------------------------------------
// <copyright file="IGraphicsDevice.cs" company="Space Development">
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
    /// Represents the basic properties of a graphics device.
    /// </summary>
    public interface IGraphicsDevice
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the vendor.
        /// </summary>
        string Vendor { get; }
    }
}
