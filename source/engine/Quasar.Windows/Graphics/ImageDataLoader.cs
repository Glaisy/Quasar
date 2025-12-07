//-----------------------------------------------------------------------
// <copyright file="ImageDataLoader.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.IO;

using Quasar.Graphics;
using Quasar.Graphics.Internals;

using Space.Core.DependencyInjection;

namespace Quasar.Windows.Graphics
{
    /// <summary>
    /// Image data loader implementation.
    /// </summary>
    [Export(typeof(IImageDataLoader))]
    [Singleton]
    internal sealed class ImageDataLoader : IImageDataLoader
    {
        /// <inheritdoc/>
        public IImageData Load(Stream stream, bool flipVerticalAxis)
        {
            ArgumentNullException.ThrowIfNull(stream, nameof(stream));

            return new ImageData(stream, flipVerticalAxis);
        }

        /// <inheritdoc/>
        public IImageData Load(string filePath, bool flipVerticalAxis)
        {
            ArgumentException.ThrowIfNullOrEmpty(filePath, nameof(filePath));

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return new ImageData(stream, flipVerticalAxis);
            }
        }
    }
}
