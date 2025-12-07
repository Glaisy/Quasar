//-----------------------------------------------------------------------
// <copyright file="ImageData.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using Quasar.Graphics;

using Space.Core;

namespace Quasar.Windows.Graphics
{
    /// <summary>
    /// Windows based native image data object implementation.
    /// </summary>
    /// <seealso cref="DisposableBase" />
    /// <seealso cref="IImageData" />
    internal sealed class ImageData : DisposableBase, IImageData
    {
        private readonly Bitmap bitmap;
        private readonly BitmapData bitmapData;


        /// <summary>
        /// Initializes a new instance of the <see cref="ImageData" /> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="flipVerticalAxis">The vertical axis flipping flag.</param>
        public ImageData(Stream stream, bool flipVerticalAxis)
        {
            bitmap = new Bitmap(stream);
            Size = bitmap.Size;

            if (flipVerticalAxis)
            {
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }

            bitmapData = bitmap.LockBits(
                new Rectangle(System.Drawing.Point.Empty, bitmap.Size),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            bitmap?.Dispose();
        }


        /// <inheritdoc/>
        public IntPtr Data => bitmapData.Scan0;

        /// <inheritdoc/>
        public Quasar.Graphics.Size Size { get; }

        /// <inheritdoc/>
        public int Stride => bitmapData.Stride;
    }
}
