//-----------------------------------------------------------------------
// <copyright file="RepositoryBase.cs" company="Space Development">
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
using System.Threading;

using Space.Core;

namespace Quasar.Collections
{
    /// <summary>
    /// Abstract base class for item repository implementations.
    /// </summary>
    /// <typeparam name="TId">The identifier type.</typeparam>
    /// <typeparam name="TItem">The item type.</typeparam>
    /// <typeparam name="TItemImpl">The item implementation type.</typeparam>
    /// <seealso cref=" IRepository{TId, TItem}" />
    public abstract class RepositoryBase<TId, TItem, TItemImpl> : IRepository<TId, TItem>
        where TItemImpl : TItem
    {
        private readonly Dictionary<TId, TItemImpl> items = new Dictionary<TId, TItemImpl>();


        /// <inheritdoc/>
        public int Count => items.Count;


        /// <inheritdoc/>
        public void Clear()
        {
            try
            {
                RepositoryLock.EnterWriteLock();

                foreach (var item in items.Values)
                {
                    OnItemDeleted(item);
                }

                items.Clear();
            }
            finally
            {
                RepositoryLock.ExitWriteLock();
            }
        }


        /// <inheritdoc/>
        public void Delete(TId id)
        {
            ValidateIdentifier(id);

            try
            {
                RepositoryLock.EnterWriteLock();

                if (!items.TryGetValue(id, out var item))
                {
                    return;
                }

                items.Remove(id);
                OnItemDeleted(item);
            }
            finally
            {
                RepositoryLock.ExitWriteLock();
            }
        }


        /// <inheritdoc/>
        public TItem Get(TId id)
        {
            ValidateIdentifier(id);

            try
            {
                RepositoryLock.EnterReadLock();

                return GetItem(id);
            }
            finally
            {
                RepositoryLock.ExitReadLock();
            }
        }


        /// <summary>
        /// The repository's synchronization lock.
        /// </summary>
        protected readonly ReaderWriterLockSlim RepositoryLock = new ReaderWriterLockSlim();


        /// <summary>
        /// Adds the item to the repository by the specified identifier (non-synchronized).
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="item">The item.</param>
        protected virtual void AddItem(TId id, TItemImpl item)
        {
            Assertion.ThrowIfNull(item, nameof(item));

            items.Add(id, item);
        }

        /// <summary>
        /// Check the whether the identifier is available for use.
        /// Throws an error if the identifier is already used (non-synchronized).
        /// </summary>
        /// <param name="id">The identifier.</param>
        protected void EnsureIdentifierIsAvailable(TId id)
        {
            if (items.ContainsKey(id))
            {
                throw new InvalidOperationException($"The item identifier '{id}' is already used.");
            }
        }

        /// <summary>
        /// Gets the item by the specified identifier (non-synchronized).
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The item object or null if not found.</returns>
        protected TItemImpl GetItem(TId id)
        {
            items.TryGetValue(id, out var resource);
            return resource;
        }

        /// <summary>
        /// Item deleted event handler (synchronized).
        /// </summary>
        /// <param name="item">The item.</param>
        protected virtual void OnItemDeleted(TItemImpl item)
        {
        }

        /// <summary>
        /// Validates the identifier.
        /// Should throw an argument exception if the identifier is not valid.
        /// </summary>
        /// <param name="id">The identifier.</param>
        protected abstract void ValidateIdentifier(TId id);
    }
}
