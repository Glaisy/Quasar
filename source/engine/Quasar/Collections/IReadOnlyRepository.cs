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
        /// Gets the item by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The item object or default value if not found.
        /// </returns>
        TItem Get(TId id);
    }
}