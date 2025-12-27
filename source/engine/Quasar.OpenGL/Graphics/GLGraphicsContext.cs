//-----------------------------------------------------------------------
// <copyright file="GLGraphicsContext.cs" company="Space Development">
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
using Quasar.OpenGL.Graphics.Factories;
using Quasar.UI;
using Quasar.Utilities;

using Space.Core.DependencyInjection;

namespace Quasar.OpenGL.Graphics
{
    /// <summary>
    /// OpenGL graphics context implementation.
    /// </summary>
    /// <seealso cref="IGraphicsContext" />
    [Export(typeof(GraphicsContextBase), GraphicsPlatform.OpenGL)]
    [Singleton]
    internal sealed class GLGraphicsContext : GraphicsContextBase
    {
        private readonly IGraphicsOutputProvider graphicsOutputProvider;
        private readonly IInteropFunctionProvider interopFunctionProvider;
        private readonly IServiceProvider serviceProvider;
        private readonly IServiceLoader serviceLoader;
        private IntPtr deviceContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="GLGraphicsContext" /> class.
        /// </summary>
        /// <param name="graphicsOutputProvider">The graphics output provider.</param>
        /// <param name="interopFunctionProvider">The interop function provider.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="serviceLoader">The service loader.</param>
        /// <param name="commandProcessor">The command processor.</param>
        public GLGraphicsContext(
            IGraphicsOutputProvider graphicsOutputProvider,
            [FromKeyedServices(GraphicsPlatform.OpenGL)] IInteropFunctionProvider interopFunctionProvider,
            IServiceProvider serviceProvider,
            IServiceLoader serviceLoader,
            GLCommandProcessor commandProcessor)
        {
            this.graphicsOutputProvider = graphicsOutputProvider;
            this.interopFunctionProvider = interopFunctionProvider;
            this.serviceProvider = serviceProvider;
            this.serviceLoader = serviceLoader;
            this.commandProcessor = commandProcessor;
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (deviceContext != IntPtr.Zero)
            {
                GL.DestroyContext(deviceContext);
                deviceContext = IntPtr.Zero;
            }
        }


        private readonly GLCommandProcessor commandProcessor;
        /// <inheritdoc/>
        public override IGraphicsCommandProcessor CommandProcessor => commandProcessor;

        private GraphicsDevice device;
        /// <inheritdoc/>
        public override IGraphicsDevice Device => device;

        /// <inheritdoc/>
        public override GraphicsPlatform Platform => GraphicsPlatform.OpenGL;

        private FrameBufferBase primaryFrameBuffer;
        /// <inheritdoc/>
        public override IFrameBuffer PrimaryFrameBuffer => primaryFrameBuffer;

        private Version version;
        /// <inheritdoc/>
        public override Version Version => version;


        /// <inheritdoc/>
        public override void Initialize(INativeWindow nativeWindow)
        {
            // initialize OpenGL function wrapper
            var currentDisplayMode = graphicsOutputProvider.ActiveGraphicsOutput.CurrentDisplayMode;
            deviceContext = nativeWindow.GetDeviceContext(currentDisplayMode);
            GL.Initialize(deviceContext, interopFunctionProvider);

            // initialize graphics device and version
            var deviceName = GL.GetString(StringType.GL_RENDERER);
            var vendor = GL.GetString(StringType.GL_VENDOR);
            var versionString = GL.GetString(StringType.GL_VERSION);
            version = new Version(versionString.Substring(0, versionString.IndexOf(' ')));
            device = new GraphicsDevice(deviceName, vendor);

            // initialize internal components
            commandProcessor.Initialize();

            AddOpenGLServiceImplementation<IMatrixFactory, GLMatrixFactory>();

            var frameBufferFactory = AddOpenGLServiceImplementation<IFrameBufferFactory, GLFrameBufferFactory>();
            frameBufferFactory.Initialize(this);
            primaryFrameBuffer = frameBufferFactory.CreatePrimary(nativeWindow);

            var meshHactory = AddOpenGLServiceImplementation<IMeshFactory, GLMeshFactory>();
            meshHactory.Initialize(this);

            var shaderFactory = AddOpenGLServiceImplementation<IShaderFactory, GLShaderFactory>();
            shaderFactory.Initialize(this);

            var textureFactory = AddOpenGLServiceImplementation<ITextureFactory, GLTextureFactory>();
            textureFactory.Initialize(this);

            var cubeMapTextureFactory = AddOpenGLServiceImplementation<ICubeMapTextureFactory, GLCubeMapTextureFactory>();
            cubeMapTextureFactory.Initialize(this);
        }

        private TImpl AddOpenGLServiceImplementation<T, TImpl>()
            where TImpl : T
        {
            var service = serviceProvider.GetRequiredService<TImpl>();
            serviceLoader.AddSingleton<T>(service);

            return service;
        }
    }
}
