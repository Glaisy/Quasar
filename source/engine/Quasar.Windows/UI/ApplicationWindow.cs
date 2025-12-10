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
using Quasar.Windows.Interop.Gdi32;
using Quasar.Windows.Interop.User32;

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
        private const string DefaultIconAndCursorId = "Windows.Default";


        private System.Drawing.Point savedLocation;
        private System.Drawing.Size savedClientSize;
        private FormBorderStyle savedFormBorderStyle;
        private IDisplayMode currentDisplayMode;
        private IntPtr deviceContext;
        private WindowsCursor defaultCursor;
        private WindowsIcon defaultIcon;


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

            defaultIcon = new WindowsIcon(DefaultIconAndCursorId, Icon.Size, Icon, false);
            defaultCursor = new WindowsCursor(DefaultIconAndCursorId, Cursor.Size, Cursor.HotSpot, Cursor, false);
            cursor = defaultCursor;
        }


        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (deviceContext != IntPtr.Zero)
            {
                User32.ReleaseDC(Handle, deviceContext);
                deviceContext = IntPtr.Zero;
            }

            Icon = defaultIcon.NativeIcon;
            Cursor = defaultCursor.NativeCursor;
            if (cursor != defaultCursor && cursor != null)
            {
                cursor.Dispose();
            }

            if (icon != defaultIcon && icon != null)
            {
                cursor.Dispose();
            }


            defaultCursor.Dispose();
            defaultIcon.Dispose();

            base.Dispose(disposing);
        }


        private WindowsCursor cursor;
        /// <inheritdoc/>
        Quasar.UI.Cursor INativeWindow.Cursor
        {
            get => cursor;
            set
            {
                if (value is not WindowsCursor windowsCursor)
                {
                    windowsCursor = defaultCursor;
                }

                cursor?.Dispose();
                cursor = windowsCursor;
                Cursor = windowsCursor.NativeCursor;
            }
        }

        private CursorMode cursorMode = CursorMode.Visible;
        /// <inheritdoc/>
        public CursorMode CursorMode
        {
            get => cursorMode;
            set
            {
                if (cursorMode == value)
                {
                    return;
                }

                cursorMode = value;
                ApplyCursorMode();
            }
        }

        private bool fullscreen;
        /// <inheritdoc/>
        public bool FullscreenMode
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

        private WindowsIcon icon;
        /// <inheritdoc/>
        Icon IApplicationWindow.Icon
        {
            get => icon;
            set
            {
                if (value is not WindowsIcon windowsIcon)
                {
                    windowsIcon = defaultIcon;
                }

                icon?.Dispose();
                icon = windowsIcon;
                Icon = windowsIcon.NativeIcon;
            }
        }

        /// <inheritdoc/>
        INativeWindow INativeWindow.Parent => null;

        /// <inheritdoc/>
        public OperatingSystemPlatform Platform => OperatingSystemPlatform.Windows;

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
        public IntPtr GetDeviceContext(IDisplayMode displayMode)
        {
            ArgumentNullException.ThrowIfNull(displayMode, nameof(displayMode));

            if (deviceContext == IntPtr.Zero ||
                currentDisplayMode.BitsPerPixel != displayMode.BitsPerPixel)
            {
                if (deviceContext != IntPtr.Zero)
                {
                    User32.ReleaseDC(Handle, deviceContext);
                }

                currentDisplayMode = displayMode;
                deviceContext = CreateDeviceContext(displayMode);
            }

            return deviceContext;
        }

        /// <inheritdoc/>
        public void SwapBuffers()
        {
            Gdi32.SwapBuffers(deviceContext);
        }


        /// <inheritdoc/>
        protected override void OnSizeChanged(EventArgs e)
        {
            Size = ClientSize;
            sizeChanged.Push(Size);
        }


        private void ApplyCursorMode()
        {
            if (cursorMode == CursorMode.Visible)
            {
                System.Windows.Forms.Cursor.Show();
            }
            else
            {
                System.Windows.Forms.Cursor.Hide();
            }
        }

        private IntPtr CreateDeviceContext(IDisplayMode displayMode)
        {
            // get window's device context
            var deviceContext = User32.GetDC(Handle);

            // setup a pixel format
            var pixelFormatDescriptor = new PixelFormatDescriptor
            {
                Version = 1,
                Flags = PixelFormatFlags.DrawToWindow | PixelFormatFlags.SupportsOpenGL | PixelFormatFlags.DoubleBuffer,
                PixelType = PixelType.RGBA,
                ColorBits = (byte)displayMode.BitsPerPixel,
                DepthBits = 16,
                StencilBits = 8,
                LayerType = LayerType.Main
            };

            pixelFormatDescriptor.Init();

            // match an appropriate pixel format
            var pixelFormat = Gdi32.ChoosePixelFormat(deviceContext, ref pixelFormatDescriptor);
            if (pixelFormat == 0)
            {
                throw new ApplicationException("Unable to create device context. Pixel format is not available.");
            }

            // set the pixel format
            if (Gdi32.SetPixelFormat(deviceContext, pixelFormat, ref pixelFormatDescriptor) == 0)
            {
                throw new ApplicationException("Unable to create device context. Pixel format cannot be set.");
            }

            return deviceContext;
        }
    }
}
