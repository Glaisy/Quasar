//-----------------------------------------------------------------------
// <copyright file="TaggedRepositoryBase.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

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
    public abstract class TaggedRepositoryBase<TId, TItem, TItemImpl>
        : RepositoryBase<TId, TItem, TItemImpl>, ITaggedRepository<TId, TItem>
        where TItemImpl : TItem
        where TItem : ITagProvider, IIdentifierProvider<TId>
    {
        /// <inheritdoc/>
        public void DeleteByTag(string tag)
        {
            ArgumentException.ThrowIfNullOrEmpty(tag, nameof(tag));

            try
            {
                RepositoryLock.EnterWriteLock();

                var itemsToDelete = FindItems(item => item.Tag == tag);
                foreach (var item in itemsToDelete)
                {
                    DeleteItem(item);
                }
            }
            finally
            {
                RepositoryLock.ExitWriteLock();
            }
        }
    }
}
