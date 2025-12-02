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
using Space.Core.Utilities;

namespace Quasar.Windows.UI
{
    /// <summary>
    /// Application window implementation for Windows platform.
    /// </summary>
    /// <seealso cref="Form" />
    /// <seealso cref="IApplicationWindow" />
    internal sealed class ApplicationWindow : Form, IApplicationWindow
    {
        private System.Drawing.Point savedLocation;
        private System.Drawing.Size savedClientSize;
        private FormBorderStyle savedFormBorderStyle;


        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationWindow" /> class.
        /// </summary>
        /// <param name="windowType">The window type.</param>
        /// <param name="title">The title.</param>
        /// <param name="size">The size.</param>
        public ApplicationWindow(
            ApplicationWindowType windowType,
            string title,
            in Size size)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.StandardClick, true);
            SetStyle(ControlStyles.StandardDoubleClick, true);
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            switch (windowType)
            {
                case ApplicationWindowType.Borderless:
                    FormBorderStyle = FormBorderStyle.None;
                    break;
                case ApplicationWindowType.Resizable:
                    FormBorderStyle = FormBorderStyle.Sizable;
                    break;
                default:
                    FormBorderStyle = FormBorderStyle.Fixed3D;
                    break;
            }

            Text = title;

            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = size;
        }

        /// <inheritdoc/>
        public OperatingSystemPlatform Platform => OperatingSystemPlatform.Windows;


        private bool fullscreen;
        /// <inheritdoc/>
        public bool Fullscreen
        {
            get => fullscreen;
            set
            {
                if (fullscreen == value)
                {
                    return;
                }

                if (value)
                {
                    savedClientSize = ClientSize;
                    savedLocation = Location;
                    savedFormBorderStyle = FormBorderStyle;
                    FormBorderStyle = FormBorderStyle.None;
                    Location = Point.Empty;
                    ClientSize = Screen.PrimaryScreen.Bounds.Size;
                }
                else
                {
                    FormBorderStyle = savedFormBorderStyle;
                    Location = savedLocation;
                    ClientSize = savedClientSize;
                }

                fullscreen = value;
            }
        }



        /// <inheritdoc/>
        INativeWindow INativeWindow.Parent => null;

        /// <summary>
        /// Gets the size.
        /// </summary>
        public new Size Size { get; private set; }

        private readonly Observable<Size> sizeChanged = new Observable<Size>();
        /// <inheritdoc/>
        public new IObservable<Size> SizeChanged => sizeChanged;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title
        {
            get => Text;
            set => Text = value;
        }


        /// <inheritdoc/>
        protected override void OnSizeChanged(EventArgs e)
        {
            Size = ClientSize;
            sizeChanged.Push(Size);
        }
    }
}
