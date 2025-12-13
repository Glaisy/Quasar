//-----------------------------------------------------------------------
// <copyright file="Cursor.cs" company="Space Development">
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

namespace Quasar.UI
{
    /// <summary>
    /// Abstract base class for native cursor wrappers.
    /// </summary>
    /// <seealso cref="Icon" />
    public abstract class Cursor : Icon
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Cursor" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="size">The size.</param>
        /// <param name="hotspot">The hotspot.</param>
        /// <param name="handle">The native handle.</param>
        protected Cursor(string id, in Size size, in Point hotspot, IntPtr handle)
            : base(id, size, handle)
        {
            Hotspot = hotspot;
        }


        /// <summary>
        /// Gets the hotspot.
        /// </summary>
        public Point Hotspot { get; }
    }
}
