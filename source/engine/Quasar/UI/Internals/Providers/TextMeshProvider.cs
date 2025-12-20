//-----------------------------------------------------------------------
// <copyright file="TextMeshProvider.cs" company="Space Development">
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

using Space.Core;
using Space.Core.Collections;
using Space.Core.DependencyInjection;

namespace Quasar.UI.Internals.Providers
{
    /// <summary>
    /// Represents an internal mesh provider for text rendering.
    /// </summary>
    [Export]
    [Singleton]
    internal sealed class TextMeshProvider
    {
        private const int CacheTimeoutSeconds = 15;
        private const int CacheUpdateIntervalSeconds = 5;


        private readonly IMeshFactory meshFactory;
        private readonly IDataCache<TextMeshKey, IMesh> cachedMeshes;
        private readonly IPool<IMesh> meshPool;


        /// <summary>
        /// Initializes a new instance of the <see cref="TextMeshProvider" /> class.
        /// </summary>
        /// <param name="meshFactory">The mesh factory.</param>
        /// <param name="dataCacheFactory">The data cache factory.</param>
        /// <param name="poolFactory">The pool factory.</param>
        public TextMeshProvider(
            IMeshFactory meshFactory,
            IDataCacheFactory dataCacheFactory,
            IPoolFactory poolFactory)
        {
            this.meshFactory = meshFactory;

            meshPool = poolFactory.Create(false, CreateMesh, null, x => x.Dispose());
            cachedMeshes = dataCacheFactory.Create<TextMeshKey, IMesh>(
                CacheTimeoutSeconds,
                CacheUpdateIntervalSeconds,
                automaticRemovalAction: meshPool.Release);
        }


        /// <summary>
        /// Gets the mesh for the range (start, length) of the text by the specified font.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <param name="text">The text.</param>
        /// <param name="start">The start.</param>
        /// <param name="length">The length.</param>
        /// <returns>
        /// The mesh.
        /// </returns>
        public IMesh Get(Font font, string text, int start, int length)
        {
            Assertion.ThrowIfNull(font, nameof(font));
            Assertion.ThrowIfNullOrEmpty(text, nameof(text));
            Assertion.ThrowIfNegative(start, nameof(start));
            Assertion.ThrowIfNegativeOrZero(length, nameof(length));
            Assertion.ThrowIfGreaterThan(start + length, text.Length, "start + length");

            var key = new TextMeshKey(font, text, start, length);
            var mesh = cachedMeshes[key];
            if (mesh != null)
            {
                return mesh;
            }

            mesh = GenerateMesh(font, text, start, length);
            cachedMeshes.Add(key, mesh);

            return mesh;
        }


        private IMesh CreateMesh()
        {
            return meshFactory.Create(PrimitiveType.Triangle, VertexUI.Layout, true);
        }

        private unsafe IMesh GenerateMesh(Font font, string text, int start, int length)
        {
            var mesh = meshPool.Allocate();

            var vertexCount = 4 * length;
            var vertexData = stackalloc VertexUI[vertexCount];
            var vertices = new Span<VertexUI>(vertexData, vertexCount);
            var indexCount = length * 6;
            var indexData = stackalloc int[indexCount];
            var indices = new Span<int>(indexData, indexCount);

            // generate and set vertices and indices
            font.GenerateMeshData(text, start, length, vertices, indices);
            mesh.VertexBuffer.SetData(vertices);
            mesh.IndexBuffer.SetData(indices);

            return mesh;
        }
    }
}
