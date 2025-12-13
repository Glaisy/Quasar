//-----------------------------------------------------------------------
// <copyright file="ResourceProviderFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Reflection;

using Space.Core.DependencyInjection;

namespace Quasar.Utilities.Internals
{
    /// <summary>
    /// Resource provider factory implementation.
    /// </summary>
    /// <seealso cref="IResourceProviderFactory" />
    [Export(typeof(IResourceProviderFactory))]
    [Singleton]
    internal sealed class ResourceProviderFactory : IResourceProviderFactory
    {
        private readonly IPathResolver pathResolver;


        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceProviderFactory"/> class.
        /// </summary>
        /// <param name="pathResolver">The path resolver.</param>
        public ResourceProviderFactory(IPathResolver pathResolver)
        {
            this.pathResolver = pathResolver;
        }


        /// <inheritdoc/>
        public IResourceProvider Create(Assembly assembly, string basePath)
        {
            ArgumentNullException.ThrowIfNull(assembly, nameof(assembly));

            return new EmbeddedResourceProvider(pathResolver, basePath, assembly);
        }

        /// <inheritdoc/>
        public IResourceProvider Create(string basePath)
        {
            ArgumentException.ThrowIfNullOrEmpty(basePath, nameof(basePath));

            return new FileResourceProvider(pathResolver, basePath);
        }
    }
}
