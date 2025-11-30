//-----------------------------------------------------------------------
// <copyright file="NativeWindowFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Quasar.UI;
using Quasar.UI.Internals;

namespace Quasar.Windows.UI
{
    /// <summary>
    /// Represents a native window factory component for UIs.
    /// </summary>
    /// <seealso cref="INativeWindowFactory" />
    internal sealed class NativeWindowFactory : INativeWindowFactory
    {
        /// <inheritdoc/>
        public IApplicationWindow CreateApplicationWindow()
        {
            var applicationWindow = new ApplicationWindow();
            applicationWindow.Show();
            return applicationWindow;
        }

        /// <inheritdoc/>
        public INativeWindow CreateChildWindow(INativeWindow parent)
        {
            throw new NotImplementedException();
        }
    }
}
