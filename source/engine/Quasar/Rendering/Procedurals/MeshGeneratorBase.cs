//-----------------------------------------------------------------------
// <copyright file="MeshGeneratorBase.cs" company="Space Development">
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

namespace Quasar.Rendering.Procedurals
{
    /// <summary>
    /// Abstract base class for mesh generators.
    /// </summary>
    public abstract class MeshGeneratorBase
    {
        /// <summary>
        /// Initializes the static services.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        internal static void InitializeServices(IServiceProvider serviceProvider)
        {
            MeshFactory = serviceProvider.GetRequiredService<IMeshFactory>();
            MeshHelper = serviceProvider.GetRequiredService<IMeshHelper>();
        }


        /// <summary>
        /// Gets the mesh factory.
        /// </summary>
        protected static IMeshFactory MeshFactory { get; private set; }

        /// <summary>
        /// Gets the mesh helper.
        /// </summary>
        protected static IMeshHelper MeshHelper { get; private set; }


        /// <summary>
        /// Creates a new mesh with primitive type, layout and indcies or validates whether the input mesh meet the parameters.
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        /// <param name="primitiveType">The primitive type.</param>
        /// <param name="vertexLayout">The vertex layout.</param>
        /// <param name="shouldHaveIndices">Index buffer flag.</param>
        protected static void CreateOrValidateMesh(
            ref IMesh mesh,
            PrimitiveType primitiveType,
            VertexLayout vertexLayout,
            bool shouldHaveIndices)
        {
            if (mesh == null)
            {
                mesh = MeshFactory.Create(primitiveType, vertexLayout, shouldHaveIndices);
            }
            else
            {
                ArgumentOutOfRangeException.ThrowIfNotEqual(mesh.VertexLayout == vertexLayout, true, nameof(IMesh.VertexLayout));
                ArgumentOutOfRangeException.ThrowIfNotEqual(mesh.PrimitiveType == primitiveType, true, nameof(IMesh.PrimitiveType));

                if (shouldHaveIndices)
                {
                    ArgumentNullException.ThrowIfNull(mesh.IndexBuffer, nameof(mesh.IndexBuffer));
                }
                else
                {
                    ArgumentOutOfRangeException.ThrowIfNotEqual(mesh.IndexBuffer == null, true, nameof(mesh.IndexBuffer));
                }
            }
        }

        /// <summary>
        /// Create the mesh.
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        /// <param name="vertices">The vertices.</param>
        /// <param name="vertexCount">The vertex count.</param>
        /// <param name="indices">The indices.</param>
        /// <param name="flags">The flags.</param>
        protected static void SetMeshData(
            IMesh mesh,
            IntPtr vertices,
            int vertexCount,
            in Span<int> indices,
            VertexFeatureFlags flags)
        {
            switch (flags)
            {
                case VertexFeatureFlags.Normal:
                    MeshHelper.CalculateNormals(vertices, vertexCount, mesh.VertexLayout, indices, true);
                    break;
                case VertexFeatureFlags.Tangent | VertexFeatureFlags.Normal:
                    MeshHelper.CalculateNormalsAndTangents(vertices, vertexCount, mesh.VertexLayout, indices);
                    break;
                case VertexFeatureFlags.Tangent:
                    MeshHelper.CalculateTangents(vertices, vertexCount, mesh.VertexLayout, indices);
                    break;
            }

            mesh.IndexBuffer?.SetData(indices, 0, indices.Length);
            mesh.VertexBuffer.SetData(vertices, vertexCount * mesh.VertexLayout.Stride, mesh.VertexLayout.Stride);
            mesh.BoundingBox = MeshHelper.CalculateBoundingBox(vertices, vertexCount, mesh.VertexLayout);
        }
    }
}
