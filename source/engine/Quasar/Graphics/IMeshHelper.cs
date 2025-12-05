//-----------------------------------------------------------------------
// <copyright file="IMeshHelper.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Graphics
{
    /// <summary>
    /// Represents a helper class fpr mesh calculations.
    /// </summary>
    public interface IMeshHelper
    {
        /// <summary>
        /// Calculates the bounding box for the vertices.
        /// </summary>
        /// <param name="vertices">The vertices.</param>
        /// <param name="vertexCount">The vertex count.</param>
        /// <param name="vertexLayout">The vertex layout.</param>
        /// <returns>The bounding box.</returns>
        BoundingBox CalculateBoundingBox(IntPtr vertices, int vertexCount, VertexLayout vertexLayout);

        /// <summary>
        /// Calculate the normal vectors for the vertices.
        /// </summary>
        /// <param name="vertices">The vertices.</param>
        /// <param name="vertexCount">The vertex count.</param>
        /// <param name="vertexLayout">The vertex layout.</param>
        /// <param name="indices">The indices.</param>
        /// <param name="normalize">if set to <c>true</c> the normal lengths will be normalized.</param>
        void CalculateNormals(IntPtr vertices, int vertexCount, VertexLayout vertexLayout, in Span<int> indices, bool normalize);

        /// <summary>
        /// Calculates the normals and tangents for the vertices.
        /// </summary>
        /// <param name="vertices">The vertices.</param>
        /// <param name="vertexCount">The vertex count.</param>
        /// <param name="vertexLayout">The vertex layout.</param>
        /// <param name="indices">The indices.</param>
        void CalculateNormalsAndTangents(IntPtr vertices, int vertexCount, VertexLayout vertexLayout, in Span<int> indices);

        /// <summary>
        /// Calculates the tangents for the vertices.
        /// </summary>
        /// <param name="vertices">The vertices.</param>
        /// <param name="vertexCount">The vertex count.</param>
        /// <param name="vertexLayout">The vertex layout.</param>
        /// <param name="indices">The indices.</param>
        void CalculateTangents(IntPtr vertices, int vertexCount, VertexLayout vertexLayout, in Span<int> indices);

        /// <summary>
        /// Normalize the normal vectors for the vertices.
        /// </summary>
        /// <param name="vertices">The vertices.</param>
        /// <param name="vertexCount">The vertex count.</param>
        /// <param name="vertexLayout">The vertex layout.</param>
        void NormalizeNormals(IntPtr vertices, int vertexCount, VertexLayout vertexLayout);
    }
}
