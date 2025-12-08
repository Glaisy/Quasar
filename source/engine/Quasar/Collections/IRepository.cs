//-----------------------------------------------------------------------
// <copyright file="IRepository.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Collections
{
    /// <summary>
    /// Represents a generic item repository with a subset of basic operations.
    /// </summary>
    /// <typeparam name="TId">The identifier type.</typeparam>
    /// <typeparam name="TItem">The item type.</typeparam>
    public interface IRepository<TId, TItem> : IReadOnlyRepository<TId, TItem>
    {
        /// <summary>
        /// Clears all items in the repository.
        /// </summary>
        void Clear();

        /// <summary>
        /// Deletes the item by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if the item exists and deleted; otherwise false.</returns>
        bool Delete(TId id);
    }
}