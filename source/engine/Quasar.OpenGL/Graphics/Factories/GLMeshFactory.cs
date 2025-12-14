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

using Quasar.Graphics;

using Space.Core.DependencyInjection;

namespace Quasar.OpenGL.Graphics.Factories
{
    /// <summary>
    /// OpenGL mesh factory component implementation.
    /// </summary>
    /// <seealso cref="IMeshFactory" />
    [Export]
    [Singleton]
    internal sealed class GLMeshFactory : IMeshFactory
    {
        private IGraphicsContext graphicsContext;


        /// <inheritdoc/>
        public IMesh Create(
            PrimitiveType primitiveType,
            VertexLayout vertexLayout,
            bool isIndexed,
            string name = null,
            GraphicsResourceUsage usage = GraphicsResourceUsage.Immutable)
        {
            ArgumentNullException.ThrowIfNull(vertexLayout, nameof(vertexLayout));

            var resourceDescriptor = new GraphicsResourceDescriptor(graphicsContext.Device, usage);
            return new GLMesh(primitiveType, vertexLayout, isIndexed, name, resourceDescriptor);
        }

        /// <summary>
        /// Executes the mesh factory initialization.
        /// </summary>
        /// <param name="graphicsContext">The graphics context.</param>
        public void Initialize(IGraphicsContext graphicsContext)
        {
            this.graphicsContext = graphicsContext;
        }
    }
}
