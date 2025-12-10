//-----------------------------------------------------------------------
// <copyright file="FileResourceProvider.cs" company="Space Development">
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
    /// File system based resource path provider implementation.
    /// </summary>
    /// <seealso cref="ResourceProviderBase" />
    internal sealed class FileResourceProvider : ResourceProviderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileResourceProvider" /> class.
        /// </summary>
        /// <param name="pathResolver">The path resolver.</param>
        /// <param name="basePath">The base path.</param>
        public FileResourceProvider(IPathResolver pathResolver, string basePath)
            : base(pathResolver, basePath)
        {
        }


        /// <inheritdoc/>
        public override IReadOnlyList<string> EnumerateResources(string relativeSearchPath, bool recursive, params string[] extensions)
        {
            var searchPath = String.IsNullOrEmpty(relativeSearchPath) ?
                BasePath :
                PathResolver.Combine(BasePath, relativeSearchPath);

            // enumerate files
            var resourcePaths = new List<string>();
            var searchOption = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            if (extensions.Length > 0)
            {
                // filter by extensions
                foreach (var extension in extensions)
                {
                    var matchingFilePaths = Directory.EnumerateFiles(searchPath, "*" + extension, searchOption);
                    AddMatchingFiles(resourcePaths, matchingFilePaths);
                }
            }
            else
            {
                // all files
                var matchingFilePaths = Directory.EnumerateFiles(searchPath, "*.*", searchOption);
                AddMatchingFiles(resourcePaths, matchingFilePaths);
            }

            return resourcePaths;
        }

        /// <inheritdoc/>
        public override Stream GetResourceStream(string resourcePath)
        {
            resourcePath = GetAbsolutePath(resourcePath);
            return new FileStream(resourcePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        }


        private void AddMatchingFiles(List<string> resourcePaths, IEnumerable<string> matchingFilePaths)
        {
            foreach (var matchingFilePath in matchingFilePaths)
            {
                var fixedFilePath = PathResolver.FixPathSeparators(matchingFilePath);
                resourcePaths.Add(fixedFilePath);
            }
        }
    }
}
