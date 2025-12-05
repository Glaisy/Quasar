//-----------------------------------------------------------------------
// <copyright file="IPathResolver.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Core.IO
{
    /// <summary>
    /// Path resolver interface definition.
    /// </summary>
    public interface IPathResolver
    {
        /// <summary>
        /// Gets the extension separator.
        /// </summary>
        char ExtensionSeparator { get; }

        /// <summary>
        /// Gets the parent path token.
        /// </summary>
        string ParentPathToken { get; }

        /// <summary>
        /// Gets the path separator.
        /// </summary>
        char PathSeparator { get; }

        /// <summary>
        /// Gets the relative path token.
        /// </summary>
        string RelativePathToken { get; }


        /// <summary>
        /// Concatenates the specified paths.
        /// </summary>
        /// <param name="first">The first path.</param>
        /// <param name="second">The second path.</param>
        /// <returns>
        /// The combined path.
        /// </returns>
        string Combine(string first, string second);

        /// <summary>
        /// Concatenates the specified paths.
        /// </summary>
        /// <param name="first">The first path.</param>
        /// <param name="second">The second path.</param>
        /// <param name="third">The third path.</param>
        /// <returns>
        /// The combined path.
        /// </returns>
        string Combine(string first, string second, string third);

        /// <summary>
        /// Fixes the path separator characters (Replaces \ characters).
        /// </summary>
        /// <param name="path">The path.</param>
        string FixPathSeparators(string path);

        /// <summary>
        /// Gets the parent directory's path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The directory path.</returns>
        string GetParentDirectoryPath(string path);

        /// <summary>
        /// Resolves the relative path prefixes in the path by the specified base path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="basePath">The base path.</param>
        /// <returns>
        /// The resolved path.
        /// </returns>
        string Resolve(string path, string basePath);

        /// <summary>
        /// Removes the trailing path separator from the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The trimmed path.</returns>
        string TrimEnd(string path);
    }
}
