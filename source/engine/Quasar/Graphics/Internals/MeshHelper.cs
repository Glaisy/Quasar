//-----------------------------------------------------------------------
// <copyright file="MeshHelper.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Space.Core;
using Space.Core.DependencyInjection;

namespace Quasar.Graphics.Internals
{
    /// <summary>
    /// Mesh helper class implementation.
    /// </summary>
    /// <seealso cref="IMeshHelper" />
    [Export(typeof(IMeshHelper))]
    [Singleton]
    internal unsafe sealed class MeshHelper : IMeshHelper
    {
        /// <inheritdoc/>
        public BoundingBox CalculateBoundingBox(IntPtr vertices, int vertexCount, VertexLayout vertexLayout)
        {
            Assertion.ThrowIfNotEqual(vertices != IntPtr.Zero, true, nameof(vertices));
            Assertion.ThrowIfNegative(vertexCount, nameof(vertexCount));
            Assertion.ThrowIfNull(vertexLayout, nameof(vertexLayout));

            if (vertexCount == 0)
            {
                return default;
            }

            // find vertex position and normals
            var positionElement = vertexLayout.FindElement(VertexSemanticNames.Position);
            var positions = (byte*)vertices + positionElement.Offset;

            // calculate bounding box coordinates
            var maxX = Single.MinValue;
            var maxY = Single.MinValue;
            var maxZ = Single.MinValue;
            var minX = Single.MaxValue;
            var minY = Single.MaxValue;
            var minZ = Single.MaxValue;
            for (var i = 0; i < vertexCount; i++, positions += vertexLayout.Stride)
            {
                var position = (Vector3*)positions;
                if (maxX < position->X)
                {
                    maxX = position->X;
                }

                if (maxY < position->Y)
                {
                    maxY = position->Y;
                }

                if (maxZ < position->Z)
                {
                    maxZ = position->Z;
                }

                if (minX > position->X)
                {
                    minX = position->X;
                }

                if (minY > position->Y)
                {
                    minY = position->Y;
                }

                if (minZ > position->Z)
                {
                    minZ = position->Z;
                }
            }

            return new BoundingBox(minX, minY, minZ, maxX, maxY, maxZ);
        }

        /// <inheritdoc/>
        public void CalculateNormals(
            IntPtr vertices,
            int vertexCount,
            VertexLayout vertexLayout,
            in Span<int> indices,
            bool normalize = true)
        {
            Assertion.ThrowIfNotEqual(vertices != IntPtr.Zero, true, nameof(vertices));
            Assertion.ThrowIfNegative(vertexCount, nameof(vertexCount));
            Assertion.ThrowIfNull(vertexLayout, nameof(vertexLayout));
            Assertion.ThrowIfNegative(indices.Length, nameof(indices.Length));

            if (vertexCount == 0 || indices.Length == 0)
            {
                return;
            }

            // find vertex position and normals
            var positionElement = vertexLayout.FindElement(VertexSemanticNames.Position);
            var positions = (byte*)vertices + positionElement.Offset;

            var normalElement = vertexLayout.FindElement(VertexSemanticNames.Normal);
            var normals = (byte*)vertices + normalElement.Offset;

            // calculate normals
            CummulateNormals(positions, normals, vertexLayout.Stride, vertexCount, indices);
            if (normalize)
            {
                NormalizeNormals(normals, vertexLayout.Stride, vertexCount);
            }
        }

        /// <inheritdoc/>
        public void CalculateNormalsAndTangents(
            IntPtr vertices,
            int vertexCount,
            VertexLayout vertexLayout,
            in Span<int> indices)
        {
            Assertion.ThrowIfNotEqual(vertices != IntPtr.Zero, true, nameof(vertices));
            Assertion.ThrowIfNegative(vertexCount, nameof(vertexCount));
            Assertion.ThrowIfNull(vertexLayout, nameof(vertexLayout));
            Assertion.ThrowIfNegative(indices.Length, nameof(indices.Length));

            if (vertexCount == 0 || indices.Length == 0)
            {
                return;
            }

            // find vertex position, normals, texture coordinates and tangents
            var positionElement = vertexLayout.FindElement(VertexSemanticNames.Position);
            var positions = (byte*)vertices + positionElement.Offset;

            var normalElement = vertexLayout.FindElement(VertexSemanticNames.Normal);
            var normals = (byte*)vertices + normalElement.Offset;

            var uvElement = vertexLayout.FindElement(VertexSemanticNames.UV);
            var uvs = (byte*)vertices + uvElement.Offset;

            var tangentElement = vertexLayout.FindElement(VertexSemanticNames.Tangent);
            var tangents = (byte*)vertices + tangentElement.Offset;

            // calculate normals
            CummulateNormals(positions, normals, vertexLayout.Stride, vertexCount, indices);
            NormalizeNormals(normals, vertexLayout.Stride, vertexCount);

            // calculate tangents
            CalculateTangents(positions, normals, uvs, tangents, vertexLayout.Stride, vertexCount, indices);
        }

        /// <inheritdoc/>
        public void CalculateTangents(
            IntPtr vertices,
            int vertexCount,
            VertexLayout vertexLayout,
            in Span<int> indices)
        {
            Assertion.ThrowIfNotEqual(vertices != IntPtr.Zero, true, nameof(vertices));
            Assertion.ThrowIfNegative(vertexCount, nameof(vertexCount));
            Assertion.ThrowIfNull(vertexLayout, nameof(vertexLayout));
            Assertion.ThrowIfNegative(indices.Length, nameof(indices.Length));

            if (vertexCount == 0 || indices.Length == 0)
            {
                return;
            }

            // find vertex position, normals, texture coordinates and tangents
            var positionElement = vertexLayout.FindElement(VertexSemanticNames.Position);
            var positions = (byte*)vertices + positionElement.Offset;

            var normalElement = vertexLayout.FindElement(VertexSemanticNames.Normal);
            var normals = (byte*)vertices + normalElement.Offset;

            var uvElement = vertexLayout.FindElement(VertexSemanticNames.UV);
            var uvs = (byte*)vertices + uvElement.Offset;

            var tangentElement = vertexLayout.FindElement(VertexSemanticNames.Tangent);
            var tangents = (byte*)vertices + tangentElement.Offset;

            // calculate tangents
            CalculateTangents(positions, normals, uvs, tangents, vertexLayout.Stride, vertexCount, indices);
        }

        /// <inheritdoc/>
        public void NormalizeNormals(
            IntPtr vertices,
            int vertexCount,
            VertexLayout vertexLayout)
        {
            Assertion.ThrowIfNotEqual(vertices != IntPtr.Zero, true, nameof(vertices));
            Assertion.ThrowIfNegative(vertexCount, nameof(vertexCount));
            Assertion.ThrowIfNull(vertexLayout, nameof(vertexLayout));

            if (vertexCount == 0)
            {
                return;
            }

            // find vertex normals
            var normalElement = vertexLayout.FindElement(VertexSemanticNames.Normal);
            var normals = (byte*)vertices + normalElement.Offset;

            // normalize all vectors
            NormalizeNormals(normals, vertexLayout.Stride, vertexCount);
        }


        private static void CummulateNormals(
            byte* positions,
            byte* normals,
            int stride,
            int vertexCount,
            in Span<int> indices)
        {
            // clear existing normals
            var clearedNormals = normals;
            for (var i = 0; i < vertexCount; i++, clearedNormals += stride)
            {
                *(Vector3*)clearedNormals = Vector3.Zero;
            }

            // cummulate normals from triangle vertices
            for (var index = 0; index < indices.Length;)
            {
                var i1 = indices[index++] * stride;
                var i2 = indices[index++] * stride;
                var i3 = indices[index++] * stride;
                var v1 = *(Vector3*)(positions + i1);
                var v2 = *(Vector3*)(positions + i2);
                var v3 = *(Vector3*)(positions + i3);

                // calculate left-handed surface normal
                var normal = Vector3.Cross(v2 - v1, v3 - v1);
                normal.Normalize();

                *(Vector3*)(normals + i1) += normal;
                *(Vector3*)(normals + i2) += normal;
                *(Vector3*)(normals + i3) += normal;
            }
        }

        private static void NormalizeNormals(byte* normals, int stride, int vertexCount)
        {
            for (var i = 0; i < vertexCount; i++, normals += stride)
            {
                *(Vector3*)normals = ((Vector3*)normals)->Normalize();
            }
        }

        private static void CalculateTangents(
            byte* positions,
            byte* normals,
            byte* uvs,
            byte* tangents,
            int vertexStride,
            int vertexCount,
            in Span<int> indices)
        {
            var tempTangentBuffer = stackalloc byte[vertexCount * sizeof(float) * 3];
            var tempTangents = (Vector3*)tempTangentBuffer;

            // Step 1: cummulate tangent directions per vertices
            var indexCount = indices.Length;
            for (var i = 0; i < indexCount;)
            {
                // get vertex indices in the triangle
                var index1 = indices[i++];
                var index2 = indices[i++];
                var index3 = indices[i++];

                // get vertex positions and uv coordinates
                var offset1 = index1 * vertexStride;
                var offset2 = index2 * vertexStride;
                var offset3 = index3 * vertexStride;

                var position1 = *(Vector3*)(positions + offset1);
                var position2 = *(Vector3*)(positions + offset2);
                var position3 = *(Vector3*)(positions + offset3);
                var uv1 = *(Vector2*)(uvs + offset1);
                var uv2 = *(Vector2*)(uvs + offset2);
                var uv3 = *(Vector2*)(uvs + offset3);

                // calculate tangent direction
                var edge1 = position2 - position1;
                var edge2 = position3 - position1;
                var texEdge1 = uv2 - uv1;
                var texEdge2 = uv3 - uv1;
                var magnitude = texEdge1.X * texEdge2.Y - texEdge2.X * texEdge1.Y;

                if (magnitude == 0.0f)
                {
                    continue;
                }

                var r = 1.0f / magnitude;
                var t = (texEdge2.Y * edge1 - texEdge1.Y * edge2) * r;

                // cummulate tangent directions per vertices
                tempTangents[index1] += t;
                tempTangents[index2] += t;
                tempTangents[index3] += t;
            }

            // Step 2: set vertex orthogonal tangents
            for (int i = 0, offset = 0; i < vertexCount; i++, offset += vertexStride)
            {
                var normal = *(Vector3*)(normals + offset);
                var tangent = tempTangents[i];

                // Gram-Schmidt orthogonalization
                var unNormalizedTangent = tangent - normal * Vector3.Dot(normal, tangent);
                var length = unNormalizedTangent.Length;

                if (length == 0.0f)
                {
                    // singular case
                    tangent = Vector3.Cross(normal, Vector3.PositiveY);
                    if (tangent.Length == 0.0f)
                    {
                        tangent = Vector3.Cross(normal, Vector3.PositiveX);
                    }

                    tangent = tangent.Normalize();
                }
                else
                {
                    tangent = unNormalizedTangent.Normalize();
                }

                // set vertex tangent
                *(Vector3*)(tangents + offset) = tangent;
            }
        }
    }
}
