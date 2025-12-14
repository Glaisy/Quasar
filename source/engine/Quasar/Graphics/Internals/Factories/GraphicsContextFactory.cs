//-----------------------------------------------------------------------
// <copyright file="GraphicsContextFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.IO;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Quasar.UI;

using Space.Core;
using Space.Core.DependencyInjection;

namespace Quasar.Graphics.Internals.Factories
{
    /// <summary>
    /// Internal graphics context factory component.
    /// </summary>
    [Export]
    [Singleton]
    internal sealed class GraphicsContextFactory
    {
        private const string PlatformSpecificAssemblyNameFormatStringP1 = $"{nameof(Quasar)}.{{0}}.dll";


        private readonly IServiceProvider serviceProvider;
        private readonly IServiceLoader serviceLoader;


        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsContextFactory" /> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="serviceLoader">The service loader.</param>
        public GraphicsContextFactory(
            IServiceProvider serviceProvider,
            IServiceLoader serviceLoader)
        {
            this.serviceProvider = serviceProvider;
            this.serviceLoader = serviceLoader;
        }


        /// <summary>
        /// Creates the graphics context by the specified platform and native window.
        /// </summary>
        /// <param name="graphicsPlatform">The graphics platform.</param>
        /// <param name="nativeWindow">The native window.</param>
        public IGraphicsContext Create(GraphicsPlatform graphicsPlatform, INativeWindow nativeWindow)
        {
            Assertion.ThrowIfEqual(graphicsPlatform == GraphicsPlatform.Unknown, true, nameof(graphicsPlatform));
            Assertion.ThrowIfNull(nativeWindow, nameof(nativeWindow));

            var graphicsContext = CreateGraphicsDeviceContext(graphicsPlatform);
            graphicsContext.Initialize(nativeWindow);

            return graphicsContext;
        }


        private void AddPlatformSpecificServices(GraphicsPlatform graphicsPlatform)
        {
            var platformSpecificAssemblyName = String.Format(PlatformSpecificAssemblyNameFormatStringP1, graphicsPlatform);
            var platformSpecificAssemblyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, platformSpecificAssemblyName);
            var platformSpecificAssembly = Assembly.LoadFile(platformSpecificAssemblyPath);
            if (platformSpecificAssembly == null)
            {
                throw new GraphicsException($"Graphics platform specific assembly not found: {platformSpecificAssemblyPath}");
            }

            serviceLoader.AddExportedServices(platformSpecificAssembly);
        }

        private GraphicsContextBase CreateGraphicsDeviceContext(GraphicsPlatform graphicsPlatform)
        {
            AddPlatformSpecificServices(graphicsPlatform);

            var graphicsDeviceContext = serviceProvider.GetRequiredKeyedService<GraphicsContextBase>(graphicsPlatform);
            if (graphicsDeviceContext == null)
            {
                throw new GraphicsException($"Graphics device context is not registered for graphics platform: {graphicsPlatform}");
            }

            return graphicsDeviceContext;
        }
    }
}
