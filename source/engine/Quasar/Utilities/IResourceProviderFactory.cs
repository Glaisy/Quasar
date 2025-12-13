//-----------------------------------------------------------------------
// <copyright file="IResourceProviderFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Reflection;

namespace Quasar.Utilities
{
    /// <summary>
    /// Represents a resource provider factory.
    /// </summary>
    public interface IResourceProviderFactory
    {
        /// <summary>
        /// Creates an embedded resource provider by the specified assembly and base path.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="basePath">The base path.</param>
        IResourceProvider Create(Assembly assembly, string basePath);

        /// <summary>
        /// Creates a file resource provider by the specified base path.
        /// </summary>
        /// <param name="basePath">The base path.</param>
        IResourceProvider Create(string basePath);
    }
}
