//-----------------------------------------------------------------------
// <copyright file="EllipsoidMeshGenerator.cs" company="Space Development">
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
    /// Ellipsoid mesh generator implementation.
    /// </summary>
    /// <seealso cref="MeshGeneratorBase" />
    [Export]
    [Singleton]
    internal sealed unsafe partial class EllipsoidMeshGenerator : MeshGeneratorBase
    {
        /// <summary>
        /// Generates ellipsoid mesh with panoramic UVs into the mesh by the specified radiuses.
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        /// <param name="longitudes">The longitudes.</param>
        /// <param name="latitudes">The latitudes.</param>
        /// <param name="radiuses">The radiuses.</param>
        public void Generate(ref IMesh mesh, int longitudes, int latitudes, in Vector3 radiuses)
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(longitudes, 2, nameof(longitudes));
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(latitudes, 2, nameof(latitudes));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(radiuses.X, nameof(Vector3.X));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(radiuses.Y, nameof(Vector3.Y));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(radiuses.Z, nameof(Vector3.Z));

            CreateOrValidateMesh(ref mesh, PrimitiveType.Triangle, VertexPositionNormalTangentUV.Layout, true);
            var radiusProvider = new EllipsoidRadiusProvider(radiuses);
            GenerateInternal(mesh, longitudes, latitudes, radiusProvider);
        }

        /// <summary>
        /// Generates a mesh for a distorted UV sphere.
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        /// <param name="longitudes">The longitudes.</param>
        /// <param name="latitudes">The latitudes.</param>
        /// <param name="radiusProvider">The radius provider.</param>
        public void Generate(ref IMesh mesh, int longitudes, int latitudes, IRadiusProvider radiusProvider)
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(longitudes, 2, nameof(longitudes));
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(latitudes, 2, nameof(latitudes));
            ArgumentNullException.ThrowIfNull(radiusProvider, nameof(radiusProvider));

            CreateOrValidateMesh(ref mesh, PrimitiveType.Triangle, VertexPositionNormalTangentUV.Layout, true);
            GenerateInternal(mesh, longitudes, latitudes, radiusProvider);
        }


        private void GenerateInternal(IMesh mesh, int longitudes, int latitudes, IRadiusProvider radiusProvider)
        {
            // calculate number of vertices and indices
            var vertexCount = latitudes * (longitudes + 1);
            var triangleCount = 2 * longitudes * (latitudes - 2);
            var indexCount = 3 * triangleCount;

            // allocate vertices and indices on the stack
            var indices = stackalloc int[indexCount];
            var vertices = stackalloc VertexPositionNormalTangentUV[vertexCount];

            // I) generate all vertex positions and uvs
            var deltaLongitude = 2.0F * MathF.PI / longitudes;
            var deltaLatitude = -MathF.PI / (latitudes - 1);
            var deltaU = 1f / longitudes;
            var deltaV = -1.0f / (latitudes - 1);
            var longitude = 0.0f;
            var vIndex = 0;
            var u = 0.0f;

            for (var i = 0; i < longitudes; i++, longitude += deltaLongitude, u += deltaU)
            {
                var latitude = MathF.PI / 2.0f;
                var v = 1.0f;
                for (var j = 0; j < latitudes; j++, latitude += deltaLatitude, v += deltaV, vIndex++)
                {
                    var y = MathF.Sin(latitude);
                    var r = MathF.Sqrt(1.0f - y * y);
                    var x = r * MathF.Sin(longitude);
                    var z = -r * MathF.Cos(longitude);
                    var position = radiusProvider.GetRadiusVector(new Vector3(x, y, z));
                    vertices[vIndex].Position = position;
                    vertices[vIndex].UV = new Vector2(u, v);
                    vertices[vIndex].Normal = position.Normalize();
                }
            }

            // I.b) generate duplicated vertices
            for (var i = 0; i < latitudes; i++, vIndex++)
            {
                vertices[vIndex].Position = vertices[i].Position;
                vertices[vIndex].Normal = vertices[i].Normal;
                vertices[vIndex].UV = new Vector2(1.0f, vertices[i].UV.Y);
            }

            // II. generate all triangles
            var tIndex = 0;
            vIndex = 0;
            var segmentsPlus1 = latitudes + 1;
            var segmentsMinus3 = latitudes - 3;
            for (var i = 0; i < longitudes; i++)
            {
                // north pole
                indices[tIndex++] = vIndex;
                indices[tIndex++] = vIndex + segmentsPlus1;
                indices[tIndex++] = vIndex + 1;
                vIndex++;

                // middle segments
                for (var j = 0; j < segmentsMinus3; j++, vIndex++)
                {
                    // lower triangle
                    indices[tIndex++] = vIndex;
                    indices[tIndex++] = vIndex + segmentsPlus1;
                    indices[tIndex++] = vIndex + 1;

                    // upper triangle
                    indices[tIndex++] = vIndex;
                    indices[tIndex++] = vIndex + latitudes;
                    indices[tIndex++] = vIndex + segmentsPlus1;
                }

                // south pole
                indices[tIndex++] = vIndex;
                indices[tIndex++] = vIndex + latitudes;
                indices[tIndex++] = vIndex + 1;

                // first vertex of the next meridian
                vIndex += 2;
            }

            // calculate normals and tangents
            var indicesSpan = new Span<int>(indices, indexCount);
            var verticesPtr = new IntPtr(vertices);

            // set the mesh data
            SetMeshData(mesh, verticesPtr, vertexCount, indicesSpan, VertexFeatureFlags.Tangent);
        }
    }
}
