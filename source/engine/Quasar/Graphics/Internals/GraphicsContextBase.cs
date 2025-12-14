//-----------------------------------------------------------------------
// <copyright file="GraphicsContextBase.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Quasar.UI;

using Space.Core;

namespace Quasar.Graphics.Internals
{
    /// <summary>
    /// Abstract base class for graphics context implementations.
    /// </summary>
    /// <seealso cref="DisposableBase" />
    /// <seealso cref="IGraphicsContext" />
    internal abstract class GraphicsContextBase : DisposableBase, IGraphicsContext
    {
        /// <inheritdoc/>
        public abstract IGraphicsCommandProcessor CommandProcessor { get; }

        /// <inheritdoc/>
        public abstract IGraphicsDevice Device { get; }

        /// <inheritdoc/>
        public abstract GraphicsPlatform Platform { get; }

        /// <inheritdoc/>
        public abstract IFrameBuffer PrimaryFrameBuffer { get; }

        /// <inheritdoc/>
        public abstract Version Version { get; }


        /// <summary>
        /// Executes the graphics context initialization by the specified native window.
        /// </summary>
        /// <param name="nativeWindow">The native window.</param>
        public abstract void Initialize(INativeWindow nativeWindow);
    }
}
