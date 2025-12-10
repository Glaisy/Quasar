//-----------------------------------------------------------------------
// <copyright file="Icon.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Quasar.Graphics;

using Space.Core;

namespace Quasar.UI
{
    /// <summary>
    /// Abstract base class native icon wrappers.
    /// </summary>
    /// <seealso cref="DisposableBase" />
    /// <seealso cref="IIdentifierProvider{String}" />
    public abstract class Icon : DisposableBase, IIdentifierProvider<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Icon" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="size">The size.</param>
        /// <param name="handle">The native handle.</param>
        protected Icon(string id, in Size size, IntPtr handle)
        {
            Id = id;
            Size = size;
            Handle = handle;
        }


        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the native handle.
        /// </summary>
        public IntPtr Handle { get; }

        /// <summary>
        /// Gets the size.
        /// </summary>
        public Size Size { get; }
    }
}
