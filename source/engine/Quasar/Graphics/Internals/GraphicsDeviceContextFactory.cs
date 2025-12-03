//-----------------------------------------------------------------------
// <copyright file="GraphicsDeviceContextFactory.cs" company="Space Development">
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

using Space.Core.DependencyInjection;

namespace Quasar.Graphics.Internals
{
    /// <summary>
    /// Graphics device context factory implementation.
    /// </summary>
    /// <seealso cref="IGraphicsDeviceContextFactory" />
    [Export(typeof(IGraphicsDeviceContextFactory))]
    [Singleton]
    internal sealed class GraphicsDeviceContextFactory : IGraphicsDeviceContextFactory
    {
        private const string PlatformSpecificAssemblyNameFormatStringP1 = $"{nameof(Quasar)}.{{0}}.dll";


        private readonly IServiceProvider serviceProvider;
        private readonly IServiceLoader serviceLoader;
        private readonly IGraphicsDeviceContext[] graphicsDeviceContexts;


        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsDeviceContextFactory" /> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="serviceLoader">The service loader.</param>
        public GraphicsDeviceContextFactory(
            IServiceProvider serviceProvider,
            IServiceLoader serviceLoader)
        {
            this.serviceProvider = serviceProvider;
            this.serviceLoader = serviceLoader;

            var numberOfGraphicsPlatforms = Enum.GetValues<GraphicsPlatform>().Length;
            graphicsDeviceContexts = new IGraphicsDeviceContext[numberOfGraphicsPlatforms];
        }


        /// <inheritdoc/>
        public IGraphicsDeviceContext Create(GraphicsPlatform graphicsPlatform)
        {
            ArgumentOutOfRangeException.ThrowIfEqual(graphicsPlatform == GraphicsPlatform.Unknown, true, nameof(graphicsPlatform));

            var graphicsDeviceContextIndex = (int)graphicsPlatform;
            var graphicsDeviceContext = graphicsDeviceContexts[graphicsDeviceContextIndex];
            if (graphicsDeviceContext == null)
            {
                graphicsDeviceContext = CreateGraphicsDeviceContext(graphicsPlatform);
                graphicsDeviceContexts[graphicsDeviceContextIndex] = graphicsDeviceContext;
            }

            return graphicsDeviceContext;
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

        private IGraphicsDeviceContext CreateGraphicsDeviceContext(GraphicsPlatform graphicsPlatform)
        {
            AddPlatformSpecificServices(graphicsPlatform);

            var graphicsDeviceContext = serviceProvider.GetRequiredKeyedService<IGraphicsDeviceContext>(graphicsPlatform);
            if (graphicsDeviceContext == null)
            {
                throw new GraphicsException($"Graphics device context is not registered for graphics platform: {graphicsPlatform}");
            }

            return graphicsDeviceContext;
        }
    }
}
