//-----------------------------------------------------------------------
// <copyright file="CubeMeshGenerator.cs" company="Space Development">
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

namespace Quasar.Rendering.Procedural.Internals
{
    /// <summary>
    /// Cube mesh generator implementation.
    /// </summary>
    /// <seealso cref="MeshGeneratorBase" />
    [Export]
    [Singleton]
    internal sealed unsafe class CubeMeshGenerator : MeshGeneratorBase
    {
        private const float OneThird = 1.0f / 3.0f;
        private const float TwoThird = 2.0f / 3.0f;


        private static readonly Vector3[] cornerVertices =
        {
            new Vector3(0.5f, -0.5f, 0.5f),
            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, 0.5f),
            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, 0.5f, 0.5f)
        };

        private static readonly Vector3[] cubeVertices =
        {
            cornerVertices[0], cornerVertices[4], cornerVertices[7], cornerVertices[3], // FRONT
            cornerVertices[4], cornerVertices[5], cornerVertices[6], cornerVertices[7], // TOP
            cornerVertices[3], cornerVertices[7], cornerVertices[6], cornerVertices[2], // RIGHT

            cornerVertices[2], cornerVertices[6], cornerVertices[5], cornerVertices[1], // BACK
            cornerVertices[1], cornerVertices[0], cornerVertices[3], cornerVertices[2], // BOTTOM
            cornerVertices[1], cornerVertices[5], cornerVertices[4], cornerVertices[0], // LEFT
        };

        private static readonly int[] indices =
        {
            0, 1, 2, 0, 2, 3,        4, 5, 6, 4, 6, 7,        8, 9, 10, 8, 10, 11,      // Front, Top, Right
            12, 13, 14, 12, 14, 15,  16, 17, 18, 16, 18, 19,  20, 21, 22, 20, 22, 23,   // Back, Bottom, Left
        };

        private static readonly int[] skyboxIndices =
        {
            3, 7, 4, 3, 4, 0,   2, 6, 7, 2, 7, 3,   7, 6, 5, 7, 5, 4,   // Front, Top, Right
            1, 5, 6, 1, 6, 2,   0, 4, 5, 0, 5, 1,   2, 3, 0, 2, 0, 1,   // Back, Bottom, Left
        };

        private static readonly Vector2[] uvs =
        {
            new Vector2(0.0f, 0.5f), new Vector2(0.0f, 1.0f), new Vector2(OneThird, 1.0f), new Vector2(OneThird, 0.5f),           // FRONT
            new Vector2(OneThird, 0.5f), new Vector2(OneThird, 1.0f), new Vector2(TwoThird, 1.0f), new Vector2(TwoThird, 0.5f),   // RIGHT
            new Vector2(TwoThird, 0.5f), new Vector2(TwoThird, 1.0f), new Vector2(1.0f, 1.0f), new Vector2(1.0f, 0.5f),           // TOP

            new Vector2(0.0f, 0.0f), new Vector2(0.0f, 0.5f), new Vector2(OneThird, 0.5f), new Vector2(OneThird, 0.0f),           // BACK
            new Vector2(OneThird, 0.0f), new Vector2(OneThird, 0.5f), new Vector2(TwoThird, 0.5f), new Vector2(TwoThird, 0.0f),   // LEFT
            new Vector2(TwoThird, 0.0f), new Vector2(TwoThird, 0.5f), new Vector2(1.0f, 0.5f), new Vector2(1.0f, 0.0f),           // BOTTOM
        };

        private static readonly Vector3[] normals =
        {
            Vector3.PositiveZ, Vector3.PositiveZ, Vector3.PositiveZ, Vector3.PositiveZ, // FRONT
            Vector3.PositiveY, Vector3.PositiveY, Vector3.PositiveY, Vector3.PositiveY, // TOP
            Vector3.NegativeX, Vector3.NegativeX, Vector3.NegativeX, Vector3.NegativeX, // RIGHT

            Vector3.NegativeZ, Vector3.NegativeZ, Vector3.NegativeZ, Vector3.NegativeZ, // BACK
            Vector3.NegativeY, Vector3.NegativeY, Vector3.NegativeY, Vector3.NegativeY, // BOTTOM
            Vector3.PositiveX, Vector3.PositiveX, Vector3.PositiveX, Vector3.PositiveX, // LEFT
        };


        /// <summary>
        /// Generates a cube mesh.
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        /// <param name="size">The size.</param>
        public void Generate(ref IMesh mesh, in Vector3 size)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(size.X, nameof(size.X));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(size.Y, nameof(size.Y));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(size.Z, nameof(size.Z));

            CreateOrValidateMesh(ref mesh, PrimitiveType.Triangle, VertexPositionNormalTangentUV.Layout, true);

            var vertices = stackalloc VertexPositionNormalTangentUV[cubeVertices.Length];

            // generate vertices (position, texture coordinate, normal)
            for (var i = 0; i < cubeVertices.Length; i++)
            {
                vertices[i].Position = size * cubeVertices[i];
                vertices[i].UV = uvs[i];
                vertices[i].Normal = normals[i];
            }

            // set the mesh data with tangents
            SetMeshData(mesh, new IntPtr(vertices), cubeVertices.Length, indices, VertexFeatureFlags.Tangent);
        }

        /// <summary>
        /// Generates a skybox cube mesh.
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        public void GenerateSkybox(ref IMesh mesh)
        {
            CreateOrValidateMesh(ref mesh, PrimitiveType.Triangle, VertexPosition.Layout, true);

            var vertices = stackalloc VertexPosition[cornerVertices.Length];

            // generate vertices (position)
            for (var i = 0; i < cornerVertices.Length; i++)
            {
                vertices[i].Position = cornerVertices[i] * 2;
            }

            // create the mesh with tangents
            SetMeshData(mesh, new IntPtr(vertices), cubeVertices.Length, skyboxIndices, VertexFeatureFlags.None);
        }
    }
}
