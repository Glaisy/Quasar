//-----------------------------------------------------------------------
// <copyright file="IResourceProvider.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;

namespace Quasar.Core.IO
{
    /// <summary>
    /// Represents a general resource provider component.
    /// </summary>
    public interface IResourceProvider
    {
        /// <summary>
        /// Gets the base path.
        /// </summary>
        string BasePath { get; }

        /// <summary>
        /// Gets the path resolver.
        /// </summary>
        IPathResolver PathResolver { get; }


        /// <summary>
        /// Enumerates the resources.
        /// </summary>
        /// <param name="relativeSearchPath">The relative search base path.</param>
        /// <param name="recursive">if set to <c>true</c> [recursive].</param>
        /// <param name="extensions">The extensions.</param>
        /// <returns>The list of matching resource paths.</returns>
        IReadOnlyList<string> EnumerateResources(string relativeSearchPath, bool recursive, params string[] extensions);

        /// <summary>
        /// Gets the absolute resource path for the specified path.
        /// </summary>
        /// <param name="resourcePath">The resource path.</param>
        string GetAbsolutePath(string resourcePath);

        /// <summary>
        /// Gets the directory path for the specified path.
        /// </summary>
        /// <param name="resourcePath">The resource path.</param>
        string GetDirectoryPath(string resourcePath);

        /// <summary>
        /// Gets the resource extension (including extension separator).
        /// </summary>
        /// <param name="resourcePath">The resource path.</param>
        string GetResourceExtension(string resourcePath);

        /// <summary>
        /// Gets the name of the resource.
        /// </summary>
        /// <param name="resourcePath">The resource path.</param>
        string GetResourceName(string resourcePath);

        /// <summary>
        /// Gets the name of the resource without extension.
        /// </summary>
        /// <param name="resourcePath">The resource path.</param>
        string GetResourceNameWithoutExtension(string resourcePath);

        /// <summary>
        /// Gets the resource stream by the specified path.
        /// </summary>
        /// <param name="resourcePath">The resource path.</param>
        Stream GetResourceStream(string resourcePath);

        /// <summary>
        /// Gets the relative resource path (relative to the base path).
        /// </summary>
        /// <param name="resourcePath">The resource path.</param>
        string GetRelativePath(string resourcePath);

        /// <summary>
        /// Gets the relative resource path without resource name extension. (relative to the base path).
        /// </summary>
        /// <param name="resourcePath">The resource path.</param>
        string GetRelativePathWithoutExtension(string resourcePath);
    }
}
