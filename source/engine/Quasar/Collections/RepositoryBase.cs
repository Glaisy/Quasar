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
        where TItem : IIdentifierProvider<TId>
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
                    DeleteItem(item);
                }

                items.Clear();
            }
            finally
            {
                RepositoryLock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        public bool Delete(TId id)
        {
            ValidateIdentifier(id);

            try
            {
                RepositoryLock.EnterWriteLock();

                return DeleteItemById(id);
            }
            finally
            {
                RepositoryLock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        public List<TItem> Find(Func<TItem, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));

            try
            {
                RepositoryLock.EnterReadLock();

                var items = new List<TItem>();
                FindItems(predicate, items);
                return items;
            }
            finally
            {
                RepositoryLock.ExitReadLock();
            }
        }

        /// <inheritdoc/>
        public void Find(Func<TItem, bool> predicate, in ICollection<TItem> items)
        {
            ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));
            ArgumentNullException.ThrowIfNull(items, nameof(items));

            try
            {
                RepositoryLock.EnterReadLock();

                FindItems(predicate, items);
            }
            finally
            {
                RepositoryLock.ExitReadLock();
            }
        }

        /// <inheritdoc/>
        public TItem Get(TId id)
        {
            ValidateIdentifier(id);

            try
            {
                RepositoryLock.EnterReadLock();

                return GetItemById(id);
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
        /// <param name="item">The item.</param>
        protected virtual void AddItem(TItemImpl item)
        {
            Assertion.ThrowIfNull(item, nameof(item));

            items.Add(item.Id, item);
        }

        /// <summary>
        /// Adds the items to the repository by the specified tag (non-synchronized).
        /// </summary>
        /// <param name="items">The items.</param>
        protected void AddItems(IEnumerable<TItemImpl> items)
        {
            foreach (var item in items)
            {
                AddItem(item);
            }
        }

        /// <summary>
        /// Deletes the item from the repository (non-synchronized).
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>True if the item exists and deleted; otherwise false.</returns>
        protected virtual bool DeleteItem(TItemImpl item)
        {
            return items.Remove(item.Id);
        }

        /// <summary>
        /// Deletes the item from the repository by the specified identifier (non-synchronized).
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if the item exists and deleted; otherwise false.</returns>
        protected virtual bool DeleteItemById(TId id)
        {
            return items.Remove(id);
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
        /// Finds the items by the specified predicate (non-synchronized).
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        protected IEnumerable<TItemImpl> FindItems(Func<TItem, bool> predicate)
        {
            Assertion.ThrowIfNull(predicate, nameof(predicate));

            foreach (var item in items.Values)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Finds the items by the specified predicate and adds them to the selected items (non-synchronized).
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="selectedItems">The selected items.</param>
        protected void FindItems(Func<TItem, bool> predicate, in ICollection<TItem> selectedItems)
        {
            Assertion.ThrowIfNull(predicate, nameof(predicate));
            Assertion.ThrowIfNull(selectedItems, nameof(selectedItems));

            foreach (var item in items.Values)
            {
                if (predicate(item))
                {
                    selectedItems.Add(item);
                }
            }
        }

        /// <summary>
        /// Gets the item by the specified identifier (non-synchronized).
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The item object or null if not found.</returns>
        protected TItemImpl GetItemById(TId id)
        {
            items.TryGetValue(id, out var item);
            return item;
        }

        /// <summary>
        /// Validates the identifier.
        /// Should throw an argument exception if the identifier is not valid.
        /// </summary>
        /// <param name="id">The identifier.</param>
        protected abstract void ValidateIdentifier(TId id);
    }
}
