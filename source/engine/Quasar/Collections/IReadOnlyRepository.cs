//-----------------------------------------------------------------------
// <copyright file="IReadOnlyRepository.cs" company="Space Development">
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

namespace Quasar.Collections
{
    /// <summary>
    /// Represents a generic readonly item repository.
    /// </summary>
    /// <typeparam name="TId">The key type.</typeparam>
    /// <typeparam name="TItem">The item type.</typeparam>
    public interface IReadOnlyRepository<TId, TItem>
    {
        /// <summary>
        /// Gets the number of stored items in the repository.
        /// </summary>
        int Count { get; }


        /// <summary>
        /// Finds the matching items by the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The list of matching items in the repository.</returns>
        List<TItem> Find(Func<TItem, bool> predicate);

        /// <summary>
        /// Finds the matching items by the specified predicate and adds them to the items collection.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="items">The items.</param>
        void Find(Func<TItem, bool> predicate, in ICollection<TItem> items);

        /// <summary>
        /// Gets the item by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The item object or default value if not found.
        /// </returns>
        TItem Get(TId id);
    }
}