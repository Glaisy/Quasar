//-----------------------------------------------------------------------
// <copyright file="QuasarContext.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Reflection;

using Quasar.Utilities;

using Space.Core;
using Space.Core.DependencyInjection;

namespace Quasar
{
    /// <summary>
    /// The Quasar context information object implementation.
    /// </summary>
    /// <seealso cref="IQuasarContext" />
    [Export(typeof(IQuasarContext))]
    [Singleton]
    internal sealed class QuasarContext : IQuasarContext
    {
        private const string BaseResourcePath = $"./Resources";

        /// <summary>
        /// Initializes a new instance of the <see cref="QuasarContext"/> class.
        /// </summary>
        /// <param name="environmentInformation">The environment information.</param>
        /// <param name="resourceProviderFactory">The resource provider factory.</param>
        public QuasarContext(
            IEnvironmentInformation environmentInformation,
            IResourceProviderFactory resourceProviderFactory)
        {
            EnvironmentInformation = environmentInformation;
            ResourceProvider = resourceProviderFactory.Create(Assembly.GetExecutingAssembly(), BaseResourcePath);
        }


        /// <inheritdoc/>
        public IEnvironmentInformation EnvironmentInformation { get; }

        /// <inheritdoc/>
        public IResourceProvider ResourceProvider { get; }
    }
}
