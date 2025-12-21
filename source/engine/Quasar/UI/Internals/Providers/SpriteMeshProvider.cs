//-----------------------------------------------------------------------
// <copyright file="SpriteMeshProvider.cs" company="Space Development">
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
using Quasar.UI.VisualElements;

using Space.Core;
using Space.Core.Collections;
using Space.Core.DependencyInjection;

namespace Quasar.UI.Internals.Providers.Internals
{
    /// <summary>
    /// Sprite mesh provider component implementation.
    /// </summary>
    [Export]
    [Singleton]
    internal sealed unsafe class SpriteMeshProvider
    {
        private const int CacheTimeoutSeconds = 15;
        private const int CacheUpdateIntervalSeconds = 5;
        private const int VertexCount = 4;
        private const int BorderedVertexCount = 16;


        private static readonly Vector2[] uvs =
        {
            new Vector2(0.0f, 0.0f),
            new Vector2(0.0f, 1.0f),
            new Vector2(1.0f, 1.0f),
            new Vector2(1.0f, 0.0f)
        };

        private static readonly int[] indices =
        {
            0, 1, 2, 0, 2, 3
        };

        private static readonly int[] borderedIndices =
        {
            0, 1, 12, 0, 12, 11,  1, 2, 13, 1, 13, 12,  2, 3, 4, 2, 4, 13,
            13, 4, 5, 13, 5, 14,  14, 5, 6, 14, 6, 7,   15, 14, 7, 15, 7, 8,
            10, 15, 8, 10, 8, 9,  11, 12, 15, 11, 15, 10,  12, 13, 14, 12, 14, 15
        };


        private readonly IMeshFactory meshFactory;
        private readonly IDataCache<SpriteMeshKey, IMesh> cachedMeshes;
        private readonly IPool<IMesh> meshPool;


        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteMeshProvider" /> class.
        /// </summary>
        /// <param name="meshFactory">The mesh factory.</param>
        /// <param name="dataCacheFactory">The data cache factory.</param>
        /// <param name="poolFactory">The pool factory.</param>
        public SpriteMeshProvider(
            IMeshFactory meshFactory,
            IDataCacheFactory dataCacheFactory,
            IPoolFactory poolFactory)
        {
            this.meshFactory = meshFactory;

            meshPool = poolFactory.Create(false, CreateMesh, null, x => x.Dispose());
            cachedMeshes = dataCacheFactory.Create<SpriteMeshKey, IMesh>(
                CacheTimeoutSeconds,
                CacheUpdateIntervalSeconds,
                automaticRemovalAction: meshPool.Release);
        }


        /// <summary>
        /// Gets the mesh for the sprite by the specified size.
        /// </summary>
        /// <param name="sprite">The sprite.</param>
        /// <param name="size">The size.</param>
        public IMesh Get(in Sprite sprite, in Vector2 size)
        {
            Assertion.ThrowIfNull(sprite.Texture, nameof(sprite.Texture));

            var key = new SpriteMeshKey(sprite, size);
            var mesh = cachedMeshes[key];
            if (mesh != null)
            {
                return mesh;
            }

            mesh = sprite.IsBorderless ?
                GenerateBorderlessMesh(size) :
                GenerateBorderedMesh(size, sprite);
            cachedMeshes.Add(key, mesh);

            return mesh;
        }


        private IMesh CreateMesh()
        {
            return meshFactory.Create(PrimitiveType.Triangle, VertexUI.Layout, true);
        }

        private IMesh GenerateBorderlessMesh(in Vector2 size)
        {
            // generate vertices
            var vertices = stackalloc VertexUI[VertexCount];
            vertices[0].Position = Vector2.Zero;
            vertices[0].UV = uvs[0];
            vertices[1].Position = new Vector2(0.0f, size.Y);
            vertices[1].UV = uvs[1];
            vertices[2].Position = new Vector2(size.X, size.Y);
            vertices[2].UV = uvs[2];
            vertices[3].Position = new Vector2(size.X, 0.0f);
            vertices[3].UV = uvs[3];

            // create mesh
            var mesh = meshPool.Allocate();
            mesh.VertexBuffer.SetData(new Span<VertexUI>(vertices, VertexCount));
            mesh.IndexBuffer.SetData(indices);

            return mesh;
        }

        private IMesh GenerateBorderedMesh(in Vector2 size, in Sprite sprite)
        {
            // calculate inner vertex positions
            var x1 = sprite.Border.Left;
            var x2 = size.X - sprite.Border.Right;
            var x3 = size.X;
            var y1 = sprite.Border.Bottom;
            var y2 = size.Y - sprite.Border.Top;
            var y3 = size.Y;

            // calculate inner texture coordinates
            var scale = new Vector2(1.0f / sprite.Size.X, 1.0f / sprite.Size.Y);
            var u1 = sprite.Border.Left * scale.X;
            var u2 = (sprite.Size.X - sprite.Border.Right) * scale.X;
            var u3 = sprite.Size.X * scale.X;
            var v1 = sprite.Border.Bottom * scale.Y;
            var v2 = (sprite.Size.Y - sprite.Border.Top) * scale.Y;
            var v3 = sprite.Size.Y * scale.Y;

            // generate vertices
            var index = 0;
            var vertices = stackalloc VertexUI[BorderedVertexCount];
            vertices[index].Position = new Vector2(0.0f, 0.0f);
            vertices[index++].UV = new Vector2(0.0f, 0.0f);
            vertices[index].Position = new Vector2(0.0f, y1);
            vertices[index++].UV = new Vector2(0.0f, v1);
            vertices[index].Position = new Vector2(0.0f, y2);
            vertices[index++].UV = new Vector2(0.0f, v2);
            vertices[index].Position = new Vector2(0.0f, y3);
            vertices[index++].UV = new Vector2(0.0f, v3);

            vertices[index].Position = new Vector2(x1, y3);
            vertices[index++].UV = new Vector2(u1, v3);
            vertices[index].Position = new Vector2(x2, y3);
            vertices[index++].UV = new Vector2(u2, v3);
            vertices[index].Position = new Vector2(x3, y3);
            vertices[index++].UV = new Vector2(u3, v3);

            vertices[index].Position = new Vector2(x3, y2);
            vertices[index++].UV = new Vector2(u3, v2);
            vertices[index].Position = new Vector2(x3, y1);
            vertices[index++].UV = new Vector2(u3, v1);
            vertices[index].Position = new Vector2(x3, 0.0f);
            vertices[index++].UV = new Vector2(u3, 0.0f);

            vertices[index].Position = new Vector2(x2, 0.0f);
            vertices[index++].UV = new Vector2(u2, 0.0f);
            vertices[index].Position = new Vector2(x1, 0.0f);
            vertices[index++].UV = new Vector2(u1, 0.0f);

            vertices[index].Position = new Vector2(x1, y1);
            vertices[index++].UV = new Vector2(u1, v1);
            vertices[index].Position = new Vector2(x1, y2);
            vertices[index++].UV = new Vector2(u1, v2);
            vertices[index].Position = new Vector2(x2, y2);
            vertices[index++].UV = new Vector2(u2, v2);
            vertices[index].Position = new Vector2(x2, y1);
            vertices[index].UV = new Vector2(u2, v1);

            // create mesh
            var mesh = meshPool.Allocate();
            mesh.VertexBuffer.SetData(new Span<VertexUI>(vertices, BorderedVertexCount));
            mesh.IndexBuffer.SetData(borderedIndices);

            return mesh;
        }
    }
}
