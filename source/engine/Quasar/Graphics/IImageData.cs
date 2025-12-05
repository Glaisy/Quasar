//-----------------------------------------------------------------------
// <copyright file="IImageData.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Graphics
{
    /// <summary>
    /// Represents the native data of an image.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public interface IImageData : IDisposable
    {
        /// <summary>
        /// Gets the pointer to the data buffer.
        /// </summary>
        public IntPtr Data { get; }

        /// <summary>
        /// Gets the size of the image.
        /// </summary>
        public Size Size { get; }

        /// <summary>
        /// Gets the stride of a scanline in bytes.
        /// </summary>
        public int Stride { get; }
    }
}
