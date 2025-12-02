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
using System.Windows.Forms;

using Quasar.Graphics;
using Quasar.UI;
using Quasar.UI.Internals;

using Space.Core.DependencyInjection;

namespace Quasar.Windows.UI
{
    /// <summary>
    /// Represents a native window factory component for UIs.
    /// </summary>
    /// <seealso cref="INativeWindowFactory" />
    [Export(typeof(INativeWindowFactory))]
    [Singleton]
    internal sealed class NativeWindowFactory : INativeWindowFactory
    {
        /// <inheritdoc/>
        public IApplicationWindow CreateApplicationWindow(
            ApplicationWindowType applicationWindowType,
            string title,
            float screenRatio)
        {
            var screenSize = Screen.PrimaryScreen.Bounds.Size;

            var size = new Size((int)(screenSize.Width * screenRatio), (int)(screenSize.Height * screenRatio));
            var applicationWindow = new ApplicationWindow(applicationWindowType, title, size);
            return applicationWindow;
        }

        /// <inheritdoc/>
        public INativeWindow CreateChildWindow(INativeWindow parent)
        {
            throw new NotImplementedException();
        }
    }
}
