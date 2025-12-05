//-----------------------------------------------------------------------
// <copyright file="GLMeshFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Microsoft.Extensions.DependencyInjection;

using Quasar.Graphics;

namespace Quasar.OpenGL.Graphics.Factories
{
    /// <summary>
    /// OpenGL mesh factory component implementation.
    /// </summary>
    /// <seealso cref="IMeshFactory" />
    internal sealed class GLMeshFactory : IMeshFactory
    {
        private readonly IGraphicsDevice graphicsDevice;


        /// <summary>
        /// Initializes a new instance of the <see cref="GLMeshFactory"/> class.
        /// </summary>
        /// <param name="graphicsDeviceContext">The graphics device context.</param>
        public GLMeshFactory(
            [FromKeyedServices(GraphicsPlatform.OpenGL)] IGraphicsDeviceContext graphicsDeviceContext)
        {
            graphicsDevice = graphicsDeviceContext.Device;
        }


        /// <inheritdoc/>
        public IMesh Create(PrimitiveType primitiveType, VertexLayout vertexLayout, bool isIndexed, string name = null, GraphicsResourceUsage usage = GraphicsResourceUsage.Immutable)
        {
            ArgumentNullException.ThrowIfNull(vertexLayout, nameof(vertexLayout));

            var graphicsResourceDescriptor = new GraphicsResourceDescriptor(graphicsDevice, usage);
            return new GLMesh(primitiveType, vertexLayout, isIndexed, name, graphicsResourceDescriptor);
        }
    }
}
