//-----------------------------------------------------------------------
// <copyright file="GLGraphicsDeviceContext.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Microsoft.Extensions.DependencyInjection;

using Quasar.Core.Utilities;
using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.OpenGL.Api;
using Quasar.UI;

using Space.Core.DependencyInjection;

namespace Quasar.OpenGL.Graphics
{
    /// <summary>
    /// OpenGL graphics device context implementation.
    /// </summary>
    /// <seealso cref="IGraphicsDeviceContext" />
    [Export(typeof(IGraphicsDeviceContext), GraphicsPlatform.OpenGL)]
    [Singleton]
    internal sealed class GLGraphicsDeviceContext : IGraphicsDeviceContext
    {
        private readonly IGraphicsOutputProvider graphicsOutputProvider;
        private readonly IInteropFunctionProvider interopFunctionProvider;
        private readonly GLCommandProcessor graphicsCommandProcessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="GLGraphicsDeviceContext" /> class.
        /// </summary>
        /// <param name="graphicsOutputProvider">The graphics output provider.</param>
        /// <param name="interopFunctionProvider">The interop function provider.</param>
        /// <param name="graphicsCommandProcessor">The graphics command processor.</param>
        public GLGraphicsDeviceContext(
            IGraphicsOutputProvider graphicsOutputProvider,
            [FromKeyedServices(GraphicsPlatform.OpenGL)] IInteropFunctionProvider interopFunctionProvider,
            GLCommandProcessor graphicsCommandProcessor)
        {
            this.graphicsOutputProvider = graphicsOutputProvider;
            this.interopFunctionProvider = interopFunctionProvider;
            this.graphicsCommandProcessor = graphicsCommandProcessor;
        }


        /// <summary>
        /// Gets the command processor.
        /// </summary>
        public IGraphicsCommandProcessor CommandProcessor { get; private set; }

        /// <inheritdoc/>
        public IGraphicsDevice Device { get; private set; }

        /// <inheritdoc/>
        public GraphicsPlatform Platform => GraphicsPlatform.OpenGL;

        /// <inheritdoc/>
        public Version Version { get; private set; }


        /// <inheritdoc/>
        public void Initialize(INativeWindow nativeWindow)
        {
            // initialize OpenGL function wrapper
            var currentDisplayMode = graphicsOutputProvider.ActiveGraphicsOutput.CurrentDisplayMode;
            var deviceContext = nativeWindow.GetDeviceContext(currentDisplayMode);
            GL.Initialize(deviceContext, interopFunctionProvider);

            // initialize graphics device and version
            var deviceName = GL.GetString(StringType.GL_RENDERER);
            var vendor = GL.GetString(StringType.GL_VENDOR);
            var versionString = vendor.Substring(0, vendor.IndexOf(' '));
            Version = new Version(versionString);
            Device = new GraphicsDevice(deviceName, vendor);

            // initialize internal components
            graphicsCommandProcessor.Initialize();
            CommandProcessor = graphicsCommandProcessor;
        }
    }
}
