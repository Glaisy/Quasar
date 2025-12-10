//-----------------------------------------------------------------------
// <copyright file="ResourceProviderBase.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;

namespace Quasar.Utilities.Internals
{
    /// <summary>
    /// Abstract base class for resource providers.
    /// </summary>
    /// <seealso cref="IResourceProvider" />
    internal abstract class ResourceProviderBase : IResourceProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceProviderBase"/> class.
        /// </summary>
        /// <param name="pathResolver">The path resolver.</param>
        /// <param name="basePath">The base path.</param>
        protected ResourceProviderBase(IPathResolver pathResolver, string basePath)
        {
            PathResolver = pathResolver;

            basePath = pathResolver.FixPathSeparators(basePath);
            BasePath = pathResolver.TrimEnd(basePath);
        }


        /// <inheritdoc/>
        public string BasePath { get; }

        /// <inheritdoc/>
        public IPathResolver PathResolver { get; }


        /// <inheritdoc/>
        public abstract IReadOnlyList<string> EnumerateResources(string relativeSearchPath, bool recursive, params string[] extensions);

        /// <inheritdoc/>
        public string GetAbsolutePath(string resourcePath)
        {
            var resolvedPath = PathResolver.Resolve(resourcePath, String.Empty);
            if (resolvedPath.StartsWith(BasePath))
            {
                return resolvedPath;
            }

            return PathResolver.Combine(BasePath, resolvedPath);
        }

        /// <inheritdoc/>
        public string GetDirectoryPath(string resourcePath)
        {
            ArgumentException.ThrowIfNullOrEmpty(resourcePath, nameof(resourcePath));

            return PathResolver.GetParentDirectoryPath(resourcePath);
        }

        /// <inheritdoc/>
        public string GetRelativePath(string resourcePath)
        {
            ArgumentException.ThrowIfNullOrEmpty(resourcePath, nameof(resourcePath));

            var resolvedPath = PathResolver.Resolve(resourcePath, String.Empty);
            if (resolvedPath.StartsWith(BasePath))
            {
                return resolvedPath.Substring(BasePath.Length + 1);
            }

            return resolvedPath;
        }

        /// <inheritdoc/>
        public string GetRelativePathWithoutExtension(string resourcePath)
        {
            var relativeResourcePath = GetRelativePath(resourcePath);
            var extensionIndex = relativeResourcePath.LastIndexOf(PathResolver.ExtensionSeparator);
            if (extensionIndex < 0)
            {
                return relativeResourcePath;
            }

            return relativeResourcePath.Substring(0, extensionIndex);
        }

        /// <inheritdoc/>
        public string GetResourceExtension(string resourcePath)
        {
            ArgumentException.ThrowIfNullOrEmpty(resourcePath, nameof(resourcePath));

            return Path.GetExtension(resourcePath);
        }

        /// <inheritdoc/>
        public string GetResourceNameWithoutExtension(string resourcePath)
        {
            ArgumentException.ThrowIfNullOrEmpty(resourcePath, nameof(resourcePath));

            return Path.GetFileNameWithoutExtension(resourcePath);
        }

        /// <inheritdoc/>
        public string GetResourceName(string resourcePath)
        {
            ArgumentException.ThrowIfNullOrEmpty(resourcePath, nameof(resourcePath));

            return Path.GetFileName(resourcePath);
        }

        /// <inheritdoc/>
        public abstract Stream GetResourceStream(string resourcePath);
    }
}
