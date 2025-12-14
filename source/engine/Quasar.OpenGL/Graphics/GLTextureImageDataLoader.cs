//-----------------------------------------------------------------------
// <copyright file="GLTextureImageDataLoader.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.IO;

using Quasar.Graphics;
using Quasar.Graphics.Internals;

using Space.Core.DependencyInjection;

namespace Quasar.OpenGL.Graphics
{
    /// <summary>
    /// OpenGL texture image data loader implementation.
    /// </summary>
    /// <seealso cref="ITextureImageDataLoader" />
    [Export]
    [Singleton]
    internal sealed class GLTextureImageDataLoader : ITextureImageDataLoader
    {
        private readonly IImageDataLoader imageDataLoader;


        /// <summary>
        /// Initializes a new instance of the <see cref="GLTextureImageDataLoader"/> class.
        /// </summary>
        /// <param name="imageDataLoader">The image data loader.</param>
        public GLTextureImageDataLoader(IImageDataLoader imageDataLoader)
        {
            this.imageDataLoader = imageDataLoader;
        }


        /// <inheritdoc/>
        public IImageData Load(string filePath)
        {
            return imageDataLoader.Load(filePath, true);
        }

        /// <inheritdoc/>
        public IImageData Load(Stream stream)
        {
            return imageDataLoader.Load(stream, true);
        }
    }
}
