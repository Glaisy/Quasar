//-----------------------------------------------------------------------
// <copyright file="ALTests.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

using NUnit.Framework;

using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.OpenGL.Api;
using Quasar.UI;
using Quasar.Windows.Interop.User32;
using Quasar.Windows.OpenGL;
using Quasar.Windows.UI;

namespace Quasar.OpenGL.Tests.Api
{
    [TestFixture]
    internal class GLTests
    {
        [Test]
        [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Reviewed.")]
        public void Initialize()
        {
            // arrange
            var deviceContext = IntPtr.Zero;
            IApplicationWindow applicationWindow = null;

            try
            {
                var displayMode = new DisplayMode(
                    Screen.PrimaryScreen.WorkingArea.Size,
                    Screen.PrimaryScreen.BitsPerPixel,
                    60);

                applicationWindow = new ApplicationWindow(ApplicationWindowType.Borderless, String.Empty, Size.Empty);
                deviceContext = applicationWindow.GetDeviceContext(displayMode);
                var functionProvider = new OpenGLInteropFunctionProvider();

                // act
                GL.Initialize(deviceContext, functionProvider);

                // assert
                Assert.That(GL.Viewport, Is.Not.Null);
            }
            finally
            {
                if (deviceContext != IntPtr.Zero)
                {
                    User32.ReleaseDC(applicationWindow.Handle, deviceContext);
                }

                applicationWindow?.Dispose();
            }
        }
    }
}
