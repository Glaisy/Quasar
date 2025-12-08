//-----------------------------------------------------------------------
// <copyright file="ITaggedRepository.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Space.Core;

namespace Quasar.Collections
{
    /// <summary>
    /// Represents a generic repository with tagged items.
    /// </summary>
    /// <typeparam name="TId">The identifier type.</typeparam>
    /// <typeparam name="TItem">The item type.</typeparam>
    /// <seealso cref="IRepository{TId, TItem}" />
    public interface ITaggedRepository<TId, TItem> : IRepository<TId, TItem>
        where TItem : ITagProvider, IIdentifierProvider<TId>
    {
        /// <summary>
        /// Deletes all item in the repository by the specified tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        void DeleteByTag(string tag);
    }
}
