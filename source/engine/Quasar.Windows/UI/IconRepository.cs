//-----------------------------------------------------------------------
// <copyright file="IconRepository.cs" company="Space Development">
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
    /// Icon repository implementation.
    /// </summary>
    /// <seealso cref="IIconRepository" />
    [Export(typeof(IIconRepository))]
    [Singleton]
    internal sealed partial class IconRepository : RepositoryBase<string, Quasar.UI.Icon, WindowsIcon>, IIconRepository
    {
        /// <inheritdoc/>
        public List<Quasar.UI.Icon> List()
        {
            try
            {
                RepositoryLock.EnterReadLock();

                return FindItems(icon => true)
                    .OfType<Quasar.UI.Icon>()
                    .ToList();
            }
            finally
            {
                RepositoryLock.ExitReadLock();
            }
        }

        /// <inheritdoc/>
        public Quasar.UI.Icon Load(string id, string filePath)
        {
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));
            ArgumentException.ThrowIfNullOrEmpty(filePath, nameof(filePath));


            WindowsIcon icon = null;
            Stream stream = null;
            try
            {
                RepositoryLock.EnterWriteLock();

                stream = File.OpenRead(filePath);
                icon = CreateIcon(id, stream);
                AddItem(icon);

                return icon;
            }
            catch
            {
                icon?.Dispose();

                throw;
            }
            finally
            {
                stream?.Dispose();
                RepositoryLock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        public Quasar.UI.Icon Load(string id, Stream stream)
        {
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));
            ArgumentNullException.ThrowIfNull(stream, nameof(stream));

            WindowsIcon icon = null;
            try
            {
                RepositoryLock.EnterWriteLock();

                icon = CreateIcon(id, stream);
                AddItem(icon);

                return icon;
            }
            catch
            {
                icon?.Dispose();

                throw;
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


        private static WindowsIcon CreateIcon(string id, Stream stream)
        {
            Bitmap bitmap = null;
            try
            {
                bitmap = new Bitmap(stream);
                var handle = bitmap.GetHicon();

                // Initialize the cursor information.
                User32.GetIconInfo(handle, out var iconInfo);
                iconInfo.IsIcon = true;
                var iconHandle = User32.CreateIconIndirect(ref iconInfo);
                var nativeIcon = System.Drawing.Icon.FromHandle(iconHandle);
                return new WindowsIcon(id, bitmap.Size, nativeIcon, true);
            }
            finally
            {
                bitmap?.Dispose();
            }
        }
    }
}
