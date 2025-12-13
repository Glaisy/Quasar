//-----------------------------------------------------------------------
// <copyright file="WindowsIcon.cs" company="Space Development">
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
    /// Native icon wrapper implementation for Windows platform.
    /// </summary>
    /// <seealso cref="Quasar.UI.Icon" />
    internal sealed class WindowsIcon : Quasar.UI.Icon
    {
        private readonly bool disposeNativeIcon;


        /// <summary>
        /// Initializes a new instance of the <see cref="UI.WindowsIcon" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="size">The size.</param>
        /// <param name="nativeIcon">The native icon.</param>
        /// <param name="disposeNativeIcon">if set to <c>true</c> the native icon is automatically disposed..</param>
        public WindowsIcon(
            string id,
            in Size size,
            System.Drawing.Icon nativeIcon,
            bool disposeNativeIcon)
            : base(id, size, nativeIcon.Handle)
        {
            this.disposeNativeIcon = disposeNativeIcon;

            NativeIcon = nativeIcon;
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (disposeNativeIcon)
            {
                NativeIcon?.Dispose();
            }

            NativeIcon = null;
        }


        /// <summary>
        /// Gets the native icon.
        /// </summary>
        public System.Drawing.Icon NativeIcon { get; private set; }
    }
}
