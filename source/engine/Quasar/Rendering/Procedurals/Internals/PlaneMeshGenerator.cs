//-----------------------------------------------------------------------
// <copyright file="PlaneMeshGenerator.cs" company="Space Development">
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

namespace Quasar.Rendering.Procedurals.Internals
{
    /// <summary>
    /// Plane mesh generator implementation.
    /// </summary>
    /// <seealso cref="MeshGeneratorBase" />
    [Export]
    [Singleton]
    internal sealed unsafe class PlaneMeshGenerator : MeshGeneratorBase
    {
        /// <summary>
        /// Generates a plane mesh.
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        /// <param name="size">The size.</param>
        /// <param name="subdivisions">The number of subdivision per sides.</param>
        public void Generate(ref IMesh mesh, in Vector2 size, int subdivisions)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(size.X, nameof(size.X));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(size.Y, nameof(size.Y));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(subdivisions, nameof(subdivisions));

            CreateOrValidateMesh(ref mesh, PrimitiveType.Triangle, VertexPositionNormalTangentUV.Layout, true);

            var verticesPerSide = 2 + subdivisions;
            var vertexCount = verticesPerSide * verticesPerSide;
            var trianglesPerSide = 1 + subdivisions;
            var indexCount = 3 * 2 * trianglesPerSide * trianglesPerSide;

            // allocate data buffers on the stack
            var indices = stackalloc int[indexCount];
            var vertices = stackalloc VertexPositionNormalTangentUV[vertexCount];

            // generate vertices
            var delta = size / (1.0f + subdivisions);
            var offset = -0.5f * size;
            var z = offset.Y;
            var v = 0.0f;
            var uvDelta = 1.0f / (1.0f + subdivisions);
            var vIndex = 0;
            for (var i = 0; i < verticesPerSide; i++, z += delta.Y, v += uvDelta)
            {
                var x = offset.X;
                var u = 0.0f;
                for (var j = 0; j < verticesPerSide; j++, x += delta.X, vIndex++, u += uvDelta)
                {
                    vertices[vIndex].Position = new Vector3(x, 0.0f, z);
                    vertices[vIndex].UV = new Vector2(u, v);
                    vertices[vIndex].Normal = Vector3.PositiveY;
                }
            }

            // generate indices
            var tIndex = 0;
            var vertexRowStartIndex = 0;
            for (var i = 0; i < trianglesPerSide; i++, vertexRowStartIndex += verticesPerSide)
            {
                vIndex = vertexRowStartIndex;
                for (var j = 0; j < trianglesPerSide; j++, vIndex++)
                {
                    indices[tIndex++] = vIndex;
                    indices[tIndex++] = vIndex + verticesPerSide + 1;
                    indices[tIndex++] = vIndex + 1;

                    indices[tIndex++] = vIndex;
                    indices[tIndex++] = vIndex + verticesPerSide;
                    indices[tIndex++] = vIndex + verticesPerSide + 1;
                }
            }

            // sets the mesh data
            SetMeshData(mesh, new IntPtr(vertices), vertexCount, new Span<int>(indices, indexCount), VertexFeatureFlags.Tangent);
        }
    }
}
