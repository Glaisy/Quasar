//-----------------------------------------------------------------------
// <copyright file="ResourceRepositoryBase.cs" company="Space Development">
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

using Quasar.Core.IO;

using Space.Core;

namespace Quasar.Collections
{
    /// <summary>
    /// Abstract base class for resource repository implementations.
    /// </summary>
    /// <typeparam name="TId">The identifier type.</typeparam>
    /// <typeparam name="TItem">The item type.</typeparam>
    /// <typeparam name="TItemImpl">The item implementation type.</typeparam>
    /// <seealso cref=" IRepository{TId, TItem}" />
    public abstract class ResourceRepositoryBase<TId, TItem, TItemImpl>
        : RepositoryBase<TId, TItem, TItemImpl>, IResourceRepository<TId, TItem>
        where TItem : ITagProvider, IIdentifierProvider<TId>
        where TItemImpl : TItem
    {
        /// <inheritdoc/>
        public void DeleteByTag(string tag)
        {
            ArgumentException.ThrowIfNullOrEmpty(tag, nameof(tag));

            try
            {
                RepositoryLock.EnterWriteLock();

                FindItems(item => item.Tag == tag, TempItems);
                if (TempItems.Count == 0)
                {
                    return;
                }

                foreach (var item in TempItems)
                {
                    DeleteItem(item);
                }
            }
            finally
            {
                TempItems.Clear();
                RepositoryLock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        public void Load(IResourceProvider resourceProvider, string resourceDirectoryPath, string tag = null)
        {
            ArgumentNullException.ThrowIfNull(resourceProvider, nameof(resourceProvider));

            try
            {
                RepositoryLock.EnterWriteLock();

                LoadItems(resourceProvider, resourceDirectoryPath, TempItems, tag);
            }
            finally
            {
                RepositoryLock.ExitWriteLock();
            }
        }


        /// <summary>
        /// The temporary list of items in the repository.
        /// </summary>
        protected readonly List<TItemImpl> TempItems = new List<TItemImpl>();


        /// <summary>
        /// Adds the items to the repository by the specified tag (non-synchronized).
        /// </summary>
        /// <param name="items">The items.</param>
        protected void AddItems(List<TItemImpl> items)
        {
            foreach (var item in items)
            {
                AddItem(item.Id, item);
            }
        }

        /// <inheritdoc/>
        protected override void DeleteItem(TItemImpl item)
        {
            DeleteItemById(item.Id);
        }

        /// <summary>
        /// Loads the items via the resource provider and path and adds them to the loaded items collection.
        /// </summary>
        /// <param name="resourceProvider">The resource provider.</param>
        /// <param name="resourceDirectoryPath">The resource directory path.</param>
        /// <param name="loadedItems">The loaded items.</param>
        /// <param name="tag">The tag value.</param>
        protected abstract void LoadItems(
            IResourceProvider resourceProvider,
            string resourceDirectoryPath,
            in ICollection<TItemImpl> loadedItems,
            string tag = null);
    }
}
