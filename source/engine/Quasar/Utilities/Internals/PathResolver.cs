//-----------------------------------------------------------------------
// <copyright file="PathResolver.cs" company="Space Development">
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
using System.Runtime.CompilerServices;
using System.Text;

using Space.Core.DependencyInjection;
using Space.Core.Extensions;

namespace Quasar.Utilities.Internals
{
    /// <summary>
    /// Path resolver implementation.
    /// </summary>
    /// <seealso cref="IPathResolver" />
    [Export(typeof(IPathResolver))]
    [Singleton]
    internal sealed class PathResolver : IPathResolver
    {
        private const char PathSeparatorChar = '/';
        private const char AlternativePathSeparatorChar = '\\';
        private const string RelativePathTokenPrefix = ".";
        private const string ParentPathTokenPrefix = "..";


        private readonly IStringOperationContext stringOperationContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="PathResolver" /> class.
        /// </summary>
        /// <param name="stringOperationContext">The string operation context.</param>
        public PathResolver(IStringOperationContext stringOperationContext)
        {
            this.stringOperationContext = stringOperationContext;
        }


        /// <inheritdoc/>
        public char ExtensionSeparator => '.';

        /// <inheritdoc/>
        public string ParentPathToken => "../";

        /// <inheritdoc/>
        public char PathSeparator => PathSeparatorChar;

        /// <inheritdoc/>
        public string RelativePathToken => "./";


        /// <inheritdoc/>
        public string Combine(string first, string second)
        {
            if (String.IsNullOrEmpty(first))
            {
                return second;
            }

            if (String.IsNullOrEmpty(second))
            {
                return first;
            }

            return CombineInternal(first, second);
        }

        /// <inheritdoc/>
        public string Combine(string first, string second, string third)
        {
            if (String.IsNullOrEmpty(first))
            {
                return Combine(second, third);
            }

            if (String.IsNullOrEmpty(second))
            {
                return Combine(first, third);
            }

            if (String.IsNullOrEmpty(third))
            {
                return Combine(first, second);
            }

            return CombineInternal(first, second, third);
        }

        /// <inheritdoc/>
        public string FixPathSeparators(string path)
        {
            if (String.IsNullOrEmpty(path))
            {
                return path;
            }

            return FixPathSeparatorsInternal(path);
        }

        /// <inheritdoc/>
        public string GetParentDirectoryPath(string path)
        {
            if (String.IsNullOrEmpty(path))
            {
                return path ?? String.Empty;
            }

            var fixedPath = FixPathSeparatorsInternal(path);
            var separatorIndex = FindLastSeparatorIndex(fixedPath);
            if (separatorIndex > 0)
            {
                return path.Substring(0, separatorIndex);
            }

            return String.Empty;
        }

        /// <inheritdoc/>
        public string Resolve(string path, string basePath)
        {
            basePath ??= String.Empty;
            if (String.IsNullOrEmpty(path))
            {
                return basePath;
            }

            var fixedPath = FixPathSeparators(path);

            List<string> parts = null;
            StringBuilder builder = null;
            try
            {
                // tokenize path
                parts = stringOperationContext.ListPool.Allocate();
                path.Split(parts, PathSeparatorChar, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                // process path tokens
                var hadCommandTokens = false;
                var hasBasePathInserted = false;
                for (var i = 0; i < parts.Count;)
                {
                    switch (parts[i])
                    {
                        case RelativePathTokenPrefix:
                            hadCommandTokens = true;

                            // remove token and insert base path
                            parts.RemoveAt(i);
                            if (!hasBasePathInserted && i == 0)
                            {
                                var fixedBasePath = FixPathSeparators(basePath);
                                InsertBasePath(parts, fixedBasePath);
                                hasBasePathInserted = true;
                            }

                            break;

                        case ParentPathTokenPrefix:
                            hadCommandTokens = true;

                            // remove token and parent token parent if exists
                            parts.RemoveAt(i);
                            if (i > 0)
                            {
                                parts.RemoveAt(--i);
                            }

                            break;

                        default:
                            // no action, next token
                            i++;
                            break;
                    }
                }

                // return fixed path if path command tokens
                if (!hadCommandTokens)
                {
                    return fixedPath;
                }

                // rebuild path
                builder = stringOperationContext.BuilderPool.Allocate();
                for (var i = 0; i < parts.Count; i++)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(PathSeparatorChar);
                    }

                    builder.Append(parts[i]);
                }

                return builder.ToString();
            }
            finally
            {
                stringOperationContext.ListPool.Release(parts);
                stringOperationContext.BuilderPool.Release(builder);
            }
        }

        /// <inheritdoc/>
        public string TrimEnd(string path)
        {
            if (String.IsNullOrEmpty(path))
            {
                return String.Empty;
            }

            // check trailing separator characters
            var lastIndex = path.Length - 1;
            var index = lastIndex;
            while (index >= 0)
            {
                if (!IsSeparatorCharacter(path[index]))
                {
                    // no more trailing separators
                    if (index == lastIndex)
                    {
                        return path;
                    }

                    return path.Substring(0, index + 1);
                }

                index--;
            }

            return String.Empty;
        }


        private string CombineInternal(params string[] paths)
        {
            StringBuilder builder = null;
            try
            {
                builder = stringOperationContext.BuilderPool.Allocate();

                var shouldAppendSeparator = false;
                foreach (var path in paths)
                {
                    if (String.IsNullOrEmpty(path))
                    {
                        continue;
                    }

                    // trim separators
                    var startIndex = IsSeparatorCharacter(path[0]) ? 1 : 0;
                    var endIndex = path.Length - (IsSeparatorCharacter(path[^1]) ? 1 : 0);
                    var count = endIndex - startIndex;
                    if (count < 0)
                    {
                        continue;
                    }

                    // append separator
                    if (shouldAppendSeparator)
                    {
                        builder.Append(PathSeparator);
                    }

                    builder.Append(path, startIndex, count);
                    shouldAppendSeparator = true;
                }

                return builder.ToString();
            }
            finally
            {
                stringOperationContext.BuilderPool.Release(builder);
            }
        }

        private static int FindLastSeparatorIndex(string path)
        {
            var index = path.Length - 1;
            while (index >= 0)
            {
                if (IsSeparatorCharacter(path[index]))
                {
                    return index;
                }

                index--;
            }

            return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string FixPathSeparatorsInternal(string path)
        {
            return path.Replace(AlternativePathSeparatorChar, PathSeparatorChar);
        }

        private void InsertBasePath(List<string> parts, string basePath)
        {
            List<string> basePathParts = null;
            try
            {
                basePathParts = stringOperationContext.ListPool.Allocate();
                basePath.Split(basePathParts, PathSeparatorChar, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (basePathParts.Count == 0)
                {
                    return;
                }

                parts.InsertRange(0, basePathParts);
            }
            finally
            {
                stringOperationContext.ListPool.Release(basePathParts);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsSeparatorCharacter(char c)
        {
            return c == PathSeparatorChar || c == AlternativePathSeparatorChar;
        }
    }
}
