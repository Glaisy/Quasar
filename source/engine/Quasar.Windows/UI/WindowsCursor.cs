//-----------------------------------------------------------------------
// <copyright file="WindowsCursor.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics;

namespace Quasar.Windows.UI
{
    /// <summary>
    /// Native cursor wrapper implementation for Windows platform.
    /// </summary>
    /// <seealso cref="Quasar.UI.Cursor" />
    internal sealed class WindowsCursor : Quasar.UI.Cursor
    {
        private readonly bool disposeNativeCursor;


        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsCursor" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="size">The size.</param>
        /// <param name="hotspot">The hotspot.</param>
        /// <param name="nativeCursor">The native cursor.</param>
        /// <param name="disposeNativeCursor">if set to <c>true</c> the native cursor is automatically disposed..</param>
        public WindowsCursor(
            string id,
            in Size size,
            in Point hotspot,
            System.Windows.Forms.Cursor nativeCursor,
            bool disposeNativeCursor)
            : base(id, size, hotspot, nativeCursor.Handle)
        {
            this.disposeNativeCursor = disposeNativeCursor;

            NativeCursor = nativeCursor;
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (disposeNativeCursor)
            {
                NativeCursor?.Dispose();
            }

            NativeCursor = null;
        }


        /// <summary>
        /// Gets the native cursor.
        /// </summary>
        internal System.Windows.Forms.Cursor NativeCursor { get; private set; }
    }
}
