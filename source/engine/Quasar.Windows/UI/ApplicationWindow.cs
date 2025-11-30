//-----------------------------------------------------------------------
// <copyright file="ApplicationWindow.cs" company="Space Development">
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

using Space.Core;

namespace Quasar.Windows.UI
{
    /// <summary>
    /// Application window implementation for Windows platform.
    /// </summary>
    /// <seealso cref="Form" />
    /// <seealso cref="IApplicationWindow" />
    internal sealed class ApplicationWindow : Form, IApplicationWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationWindow"/> class.
        /// </summary>
        public ApplicationWindow()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.StandardClick, true);
            SetStyle(ControlStyles.StandardDoubleClick, true);
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            StartPosition = FormStartPosition.CenterScreen;
        }

        /// <inheritdoc/>
        public OperatingSystemPlatform Platform => OperatingSystemPlatform.Windows;

        /// <inheritdoc/>
        INativeWindow INativeWindow.Parent => null;

        /// <inheritdoc/>
        Size INativeWindow.Size => throw new NotImplementedException();
    }
}
