//-----------------------------------------------------------------------
// <copyright file="IApplicationWindow.cs" company="Space Development">
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
    /// Represents the application's main window.
    /// </summary>
    /// <seealso cref="INativeWindow" />
    public interface IApplicationWindow : INativeWindow
    {
        /// <summary>
        /// Gets or sets a value indicating whether the fullscreen mode is active.
        /// </summary>
        bool FullscreenMode { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        string Title { get; set; }


        /// <summary>
        /// Gets the size changed observable event.
        /// </summary>
        IObservable<Size> SizeChanged { get; }


        /// <summary>
        /// Close the window.
        /// </summary>
        void Close();


        /// <summary>
        /// Shows the window.
        /// </summary>
        internal void Show();
    }
}
