//-----------------------------------------------------------------------
// <copyright file="IResourceRepository.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Core.IO;

using Space.Core;

namespace Quasar.Collections
{
    /// <summary>
    /// Represents a generic resource item repository with a subset of basic operations.
    /// </summary>
    /// <typeparam name="TId">The identifier type.</typeparam>
    /// <typeparam name="TItem">The item type.</typeparam>
    public interface IResourceRepository<TId, TItem> : IRepository<TId, TItem>
        where TItem : IIdentifierProvider<TId>
    {
        /// <summary>
        /// Deletes all item in the repository by the specified tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        void DeleteByTag(string tag);

        /// <summary>
        /// Loads items from the resource directory path by the resource provider.
        /// </summary>
        /// <param name="resourceProvider">The resource provider.</param>
        /// <param name="resourceDirectoryPath">The resource directory path.</param>
        /// <param name="tag">The custom tag value.</param>
        void Load(IResourceProvider resourceProvider, string resourceDirectoryPath, string tag = null);
    }
}
