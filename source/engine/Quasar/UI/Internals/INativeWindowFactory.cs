//-----------------------------------------------------------------------
// <copyright file="INativeWindowFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.UI.Internals
{
    /// <summary>
    /// Represents a factory object for native windows.
    /// </summary>
    internal interface INativeWindowFactory
    {
        /// <summary>
        /// Creates the application's main window.
        /// </summary>
        /// <returns>The created window instance.</returns>
        IApplicationWindow CreateApplicationWindow();

        /// <summary>
        /// Creates a new child window.
        /// </summary>
        /// <param name="parent">The parent window.</param>
        /// <returns>The created window instance.</returns>
        INativeWindow CreateChildWindow(INativeWindow parent);
    }
}
