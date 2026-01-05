//-----------------------------------------------------------------------
// <copyright file="IdentifierExtractor.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Space.Core.DependencyInjection;

namespace Quasar.Utilities.Internals
{
    /// <summary>
    /// Identifier extractor component implementation.
    /// </summary>
    /// <seealso cref="IIdentifierExtractor" />
    [Export(typeof(IIdentifierExtractor))]
    [Singleton]
    internal sealed class IdentifierExtractor : IIdentifierExtractor
    {
        private readonly IPathResolver pathResolver;


        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifierExtractor"/> class.
        /// </summary>
        /// <param name="pathResolver">The path resolver.</param>
        public IdentifierExtractor(IPathResolver pathResolver)
        {
            this.pathResolver = pathResolver;
        }


        /// <inheritdoc/>
        public string GetIdentifier(string composedIdentifier, int startIndex = 0)
        {
            ArgumentException.ThrowIfNullOrEmpty(composedIdentifier, nameof(composedIdentifier));

            var extensionIndex = composedIdentifier.LastIndexOf(pathResolver.ExtensionSeparator);
            if (extensionIndex <= 0)
            {
                return startIndex == 0 ? composedIdentifier : composedIdentifier.Substring(startIndex);
            }

            return composedIdentifier.Substring(startIndex, extensionIndex - startIndex);
        }
    }
}
