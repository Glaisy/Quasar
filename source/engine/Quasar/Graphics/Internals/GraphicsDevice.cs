//-----------------------------------------------------------------------
// <copyright file="GraphicsDevice.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Space.Core;

namespace Quasar.Graphics.Internals
{
    /// <summary>
    /// Graphics device object implementation.
    /// </summary>
    /// <seealso cref="IGraphicsDevice" />
    internal sealed class GraphicsDevice : IGraphicsDevice
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsDevice" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="vendor">The vendor.</param>
        public GraphicsDevice(string name, string vendor)
        {
            Assertion.ThrowIfNullOrEmpty(name, nameof(name));
            Assertion.ThrowIfNullOrEmpty(vendor, nameof(vendor));

            Name = name;
            Vendor = vendor;
        }


        /// <inheritdoc/>
        public string Name { get; }

        /// <inheritdoc/>
        public string Vendor { get; }
    }
}
