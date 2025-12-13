//-----------------------------------------------------------------------
// <copyright file="EmbeddedResourceProvider.cs" company="Space Development">
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
using System.Linq;
using System.Reflection;

namespace Quasar.Utilities.Internals
{
    /// <summary>
    /// Embedded resouce provider implementation.
    /// </summary>
    /// <seealso cref="ResourceProviderBase" />
    internal sealed class EmbeddedResourceProvider : ResourceProviderBase
    {
        private readonly Assembly assembly;
        private readonly Dictionary<string, string> resourceMapping;


        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedResourceProvider" /> class.
        /// </summary>
        /// <param name="pathResolver">The path resolver.</param>
        /// <param name="basePath">The base path.</param>
        /// <param name="assembly">The assembly.</param>
        public EmbeddedResourceProvider(IPathResolver pathResolver, string basePath, Assembly assembly)
            : base(pathResolver, GetFullBasePath(pathResolver, assembly, basePath))
        {
            this.assembly = assembly;

            resourceMapping = assembly.GetManifestResourceNames()
                .ToDictionary(
                    ConvertResourceNameToPath,
                    originalPath => originalPath);
        }


        /// <inheritdoc/>
        public override IReadOnlyList<string> EnumerateResources(string relativeSearchPath, bool recursive, params string[] extensions)
        {
            var searchPath = GetAbsolutePath(relativeSearchPath);

            // multiple extensions?
            var resourcePaths = new List<string>();
            if (extensions.Length > 0)
            {
                // filter resouces by extensions
                if (recursive)
                {
                    foreach (var extension in extensions)
                    {
                        EnumerateResourcesRecursively(resourcePaths, searchPath, extension);
                    }
                }
                else
                {
                    foreach (var extension in extensions)
                    {
                        EnumerateTopLevelResources(resourcePaths, searchPath, extension);
                    }
                }
            }
            else
            {
                // all resources
                if (recursive)
                {
                    EnumerateAllResourcesRecursively(resourcePaths, searchPath);
                }
                else
                {
                    EnumerateAllTopLevelResources(resourcePaths, searchPath);
                }
            }

            return resourcePaths;
        }

        /// <inheritdoc/>
        public override Stream GetResourceStream(string resourcePath)
        {
            resourcePath = GetAbsolutePath(resourcePath);
            if (!resourceMapping.TryGetValue(resourcePath, out var resourceName))
            {
                return null;
            }

            return assembly.GetManifestResourceStream(resourceName);
        }


        private string ConvertResourceNameToPath(string fullResourceName)
        {
            var extensionIndex = fullResourceName.LastIndexOf('.');
            var resourceNameIndex = fullResourceName.LastIndexOf('.', extensionIndex - 1);
            if (resourceNameIndex < 0)
            {
                return fullResourceName;
            }

            var resourcePath = fullResourceName.Substring(0, resourceNameIndex + 1)
                .Replace('.', PathResolver.PathSeparator);
            var resourceName = fullResourceName.Substring(resourceNameIndex + 1);
            return PathResolver.Combine(resourcePath, resourceName);
        }

        private void EnumerateAllResourcesRecursively(List<string> resourcePaths, string searchBasePath)
        {
            foreach (var resourcePath in resourceMapping.Keys)
            {
                if (!resourcePath.StartsWith(searchBasePath))
                {
                    continue;
                }

                resourcePaths.Add(resourcePath);
            }
        }

        private void EnumerateAllTopLevelResources(List<string> resourcePaths, string searchBasePath)
        {
            foreach (var resourcePath in resourceMapping.Keys)
            {
                var extensionLength = resourcePath.Length - resourcePath.LastIndexOf(PathResolver.ExtensionSeparator);

                if (!resourcePath.StartsWith(searchBasePath) ||
                    !IsTopLevelResource(resourcePath, searchBasePath, extensionLength))
                {
                    continue;
                }

                resourcePaths.Add(resourcePath);
            }
        }

        private void EnumerateResourcesRecursively(List<string> resourcePaths, string searchBasePath, string extension)
        {
            foreach (var resourcePath in resourceMapping.Keys)
            {
                if (!resourcePath.StartsWith(searchBasePath) ||
                   !resourcePath.EndsWith(extension))
                {
                    continue;
                }

                resourcePaths.Add(resourcePath);
            }
        }

        private void EnumerateTopLevelResources(List<string> resourcePaths, string searchBasePath, string extension)
        {
            var extensionLength = extension.Length;
            foreach (var resourcePath in resourceMapping.Keys)
            {
                if (!resourcePath.StartsWith(searchBasePath) ||
                   !resourcePath.EndsWith(extension) ||
                   !IsTopLevelResource(resourcePath, searchBasePath, extensionLength))
                {
                    continue;
                }

                resourcePaths.Add(resourcePath);
            }
        }

        private static string GetFullBasePath(IPathResolver pathResolver, Assembly assembly, string basePath)
        {
            var basePathPrefix = assembly.GetName().Name.
                Replace('.', pathResolver.PathSeparator);
            return pathResolver.Resolve(basePath, basePathPrefix);
        }

        private bool IsTopLevelResource(string resourcePath, string searchBasePath, int extensionLength)
        {
            var relativeResourceNameLength = resourcePath.Length - extensionLength - searchBasePath.Length - 1;
            var relativeResourceName = resourcePath.AsSpan()
                .Slice(searchBasePath.Length + 1, relativeResourceNameLength);
            return !relativeResourceName.Contains(PathResolver.ExtensionSeparator);
        }
    }
}
