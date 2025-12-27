//-----------------------------------------------------------------------
// <copyright file="CursorRepository.cs" company="Space Development">
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
using System.Drawing;
using System.IO;
using System.Linq;

using Quasar.Collections;
using Quasar.UI;
using Quasar.Windows.Interop.User32;

using Space.Core.DependencyInjection;

namespace Quasar.Windows.UI
{
    /// <summary>
    /// Cursor repository implementation.
    /// </summary>
    /// <seealso cref="ICursorRepository" />
    [Export(typeof(ICursorRepository))]
    [Singleton]
    internal sealed partial class CursorRepository : RepositoryBase<string, Cursor, WindowsCursor>, ICursorRepository
    {
        private const string DefaultCursorId = nameof(Quasar);


        /// <inheritdoc/>
        public Cursor DefaultCursor { get; private set; }


        /// <inheritdoc/>
        public Cursor Create(string id, string filePath, in Quasar.Graphics.Point hotspot)
        {
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));
            ArgumentException.ThrowIfNullOrEmpty(filePath, nameof(filePath));

            WindowsCursor cursor = null;
            Stream stream = null;
            try
            {
                RepositoryLock.EnterWriteLock();

                stream = File.OpenRead(filePath);
                cursor = CreateCursor(id, stream, hotspot);
                AddItem(cursor);

                return cursor;
            }
            catch
            {
                cursor?.Dispose();

                throw;
            }
            finally
            {
                stream?.Dispose();
                RepositoryLock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        public Cursor Create(string id, Stream stream, in Quasar.Graphics.Point hotspot)
        {
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));
            ArgumentNullException.ThrowIfNull(stream, nameof(stream));

            WindowsCursor cursor = null;
            try
            {
                RepositoryLock.EnterWriteLock();

                cursor = CreateCursor(id, stream, hotspot);
                AddItem(cursor);

                return cursor;
            }
            catch
            {
                cursor?.Dispose();

                throw;
            }
            finally
            {
                stream?.Dispose();
                RepositoryLock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        public List<Cursor> List()
        {
            try
            {
                RepositoryLock.EnterReadLock();

                return FindItems(cursor => true)
                    .OfType<Cursor>()
                    .ToList();
            }
            finally
            {
                RepositoryLock.ExitReadLock();
            }
        }

        /// <inheritdoc/>
        public void ValidateBuiltInAssets()
        {
            try
            {
                RepositoryLock.EnterWriteLock();

                DefaultCursor = GetItemById(DefaultCursorId);
                if (DefaultCursor == null)
                {
                    throw new InvalidOperationException($"Unable to resolve default cursor '{DefaultCursorId}'.");
                }
            }
            finally
            {
                RepositoryLock.ExitWriteLock();
            }
        }


        /// <inheritdoc/>
        protected override void ValidateIdentifier(string id)
        {
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));
        }


        private static WindowsCursor CreateCursor(string id, Stream stream, in Quasar.Graphics.Point hotspot)
        {
            Bitmap bitmap = null;
            try
            {
                bitmap = new Bitmap(stream);
                var handle = bitmap.GetHicon();

                // Initialize the cursor information.
                User32.GetIconInfo(handle, out var iconInfo);
                iconInfo.IsIcon = false;
                iconInfo.Hotspot = hotspot;
                var iconHandle = User32.CreateIconIndirect(ref iconInfo);
                var nativeCursor = new System.Windows.Forms.Cursor(iconHandle);
                return new WindowsCursor(id, bitmap.Size, hotspot, nativeCursor, true);
            }
            finally
            {
                bitmap?.Dispose();
            }
        }
    }
}
