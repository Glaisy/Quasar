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

using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.Graphics.Internals.Factories;
using Quasar.OpenGL.Api;
using Quasar.UI;
using Quasar.Utilities;

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
        private readonly IServiceProvider serviceProvider;
        private readonly IServiceLoader serviceLoader;
        private readonly GLCommandProcessor graphicsCommandProcessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="GLGraphicsDeviceContext" /> class.
        /// </summary>
        /// <param name="graphicsOutputProvider">The graphics output provider.</param>
        /// <param name="interopFunctionProvider">The interop function provider.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="serviceLoader">The service loader.</param>
        /// <param name="graphicsCommandProcessor">The graphics command processor.</param>
        public GLGraphicsDeviceContext(
            IGraphicsOutputProvider graphicsOutputProvider,
            [FromKeyedServices(GraphicsPlatform.OpenGL)] IInteropFunctionProvider interopFunctionProvider,
            IServiceProvider serviceProvider,
            IServiceLoader serviceLoader,
            GLCommandProcessor graphicsCommandProcessor)
        {
            this.graphicsOutputProvider = graphicsOutputProvider;
            this.interopFunctionProvider = interopFunctionProvider;
            this.serviceProvider = serviceProvider;
            this.serviceLoader = serviceLoader;
            this.graphicsCommandProcessor = graphicsCommandProcessor;
        }


        /// <inheritdoc/>
        public IGraphicsCommandProcessor CommandProcessor { get; private set; }

        /// <inheritdoc/>
        public IGraphicsDevice Device { get; private set; }

        /// <inheritdoc/>
        public GraphicsPlatform Platform => GraphicsPlatform.OpenGL;

        /// <inheritdoc/>
        public IFrameBuffer PrimaryFrameBuffer { get; private set; }

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

            var frameBufferFactory = AddOpenGLServiceImplementation<IFrameBufferFactory>();
            PrimaryFrameBuffer = frameBufferFactory.CreatePrimary(nativeWindow);

            AddOpenGLServiceImplementation<IShaderFactory>();
            AddOpenGLServiceImplementation<ITextureImageDataLoader>();
            AddOpenGLServiceImplementation<ITextureFactory>();
            AddOpenGLServiceImplementation<ICubeMapTextureFactory>();
            AddOpenGLServiceImplementation<IMeshFactory>();
        }

        private T AddOpenGLServiceImplementation<T>()
        {
            var service = serviceProvider.GetRequiredKeyedService<T>(GraphicsPlatform.OpenGL);
            serviceLoader.AddSingleton(service);

            return service;
        }
    }
}
