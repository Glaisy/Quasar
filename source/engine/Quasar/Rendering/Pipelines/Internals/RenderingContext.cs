//-----------------------------------------------------------------------
// <copyright file="RenderingContext.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics;

using Space.Core;
using Space.Core.DependencyInjection;

namespace Quasar.Rendering.Pipelines.Internals
{
    /// <summary>
    /// The Quasar rendering pipeline's context object implementation.
    /// </summary>
    /// <seealso cref="IRenderingContext" />
    [Export(typeof(IRenderingContext))]
    internal sealed class RenderingContext : IRenderingContext
    {
        /// <inheritdoc/>
        public IGraphicsCommandProcessor CommandProcessor { get; private set; }

        /// <inheritdoc/>
        public IFrameBuffer PrimaryFrameBuffer { get; private set; }


        /// <inheritdoc/>
        public void Initialize(IGraphicsContext graphicsDeviceContext)
        {
            Assertion.ThrowIfNull(graphicsDeviceContext, nameof(graphicsDeviceContext));

            CommandProcessor = graphicsDeviceContext.CommandProcessor;
            PrimaryFrameBuffer = graphicsDeviceContext.PrimaryFrameBuffer;
        }
    }
}
