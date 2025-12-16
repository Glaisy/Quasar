//-----------------------------------------------------------------------
// <copyright file="IFrustum.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics;
using Quasar.Mathematics;

namespace Quasar.Rendering
{
    /// <summary>
    /// Perspective frustum interface definition.
    /// </summary>
    public interface IFrustum
    {
        /// <summary>
        /// Gets the frustum planes in world space.
        /// </summary>
        EuclideanPlane[] Planes { get; }

        /// <summary>
        /// Gets the frustum points in world space.
        /// </summary>
        Vector3[] Points { get; }


        /// <summary>
        /// Determines whether the specified world space point (x, y, z) is in the frustum.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        bool IsInFrustum(float x, float y, float z);

        /// <summary>
        /// Determines whether the specified world space point is in the frustum.
        /// </summary>
        /// <param name="point">The point.</param>
        bool IsInFrustum(in Vector3 point);

        /// <summary>
        /// Determines whether the specified world space bounding box is in the frustum.
        /// </summary>
        /// <param name="boundingBox">The bounding box.</param>
        bool IsInFrustum(in BoundingBox boundingBox);
    }
}
