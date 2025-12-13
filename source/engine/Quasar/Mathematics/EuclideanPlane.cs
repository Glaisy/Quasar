//-----------------------------------------------------------------------
// <copyright file="EuclideanPlane.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Mathematics
{
    /// <summary>
    /// Single precision Euclidean plane structure (Immutable).
    /// </summary>
    public readonly struct EuclideanPlane
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EuclideanPlane" /> struct.
        /// </summary>
        /// <param name="normal">The normal vector.</param>
        /// <param name="point">The point on the plane.</param>
        public EuclideanPlane(in Vector3 normal, in Vector3 point)
        {
            ArgumentOutOfRangeException.ThrowIfZero(normal.LengthSquared, nameof(normal));

            Normal = normal.Normalize();
            D = -Normal.Dot(point);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EuclideanPlane" /> struct.
        /// </summary>
        /// <param name="normal">The normal vector.</param>
        /// <param name="distance">The distance from origin.</param>
        public EuclideanPlane(in Vector3 normal, float distance)
        {
            ArgumentOutOfRangeException.ThrowIfZero(normal.LengthSquared, nameof(normal));

            Normal = normal.Normalize();
            D = distance;
        }


        /// <summary>
        /// The distance from origin.
        /// </summary>
        public readonly float D;

        /// <summary>
        /// The unit normal vector.
        /// </summary>
        public readonly Vector3 Normal;


        /// <summary>
        /// Calculates the (signed) distance of the specified point from the plane.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        /// <returns>
        /// The calculated distance.
        /// </returns>
        public float Distance(float x, float y, float z)
        {
            return Normal.X * x + Normal.Y * y + Normal.Z * z + D;
        }

        /// <summary>
        /// Calculates the (signed) distance of the specified point from the plane.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The calculated distance.</returns>
        public float Distance(in Vector3 point)
        {
            return Normal.Dot(point) + D;
        }
    }
}
