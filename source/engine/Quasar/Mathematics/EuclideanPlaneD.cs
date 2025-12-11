//-----------------------------------------------------------------------
// <copyright file="EuclideanPlaneD.cs" company="Space Development">
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
    /// Double precision Euclidean plane structure (Immutable).
    /// </summary>
    public readonly struct EuclideanPlaneD
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EuclideanPlaneD" /> struct.
        /// </summary>
        /// <param name="normal">The normal.</param>
        /// <param name="point">The point on the plane.</param>
        public EuclideanPlaneD(in Vector3D normal, in Vector3D point)
        {
            ArgumentOutOfRangeException.ThrowIfZero(normal.LengthSquared, nameof(normal));

            Normal = normal.Normalize();
            D = -Normal.Dot(point);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EuclideanPlaneD" /> struct.
        /// </summary>
        /// <param name="normal">The normal direction (Coefficients A, B, C).</param>
        /// <param name="d">The distance coefficient (D).</param>
        public EuclideanPlaneD(in Vector3D normal, double d)
        {
            ArgumentOutOfRangeException.ThrowIfZero(normal.LengthSquared, nameof(normal));

            var length = normal.Length;
            Normal = normal / length;
            D = d / length;
        }

        /// <summary>
        /// The distant coefficient (D).
        /// </summary>
        public readonly double D;

        /// <summary>
        /// The normal vector (Coefficients A, B, C).
        /// </summary>
        public readonly Vector3D Normal;


        /// <summary>
        /// Calculates the (signed) distance of the specified point from the plane.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        /// <returns>
        /// The calculated distance.
        /// </returns>
        public double Distance(double x, double y, double z)
        {
            return Normal.X * x + Normal.Y * y + Normal.Z * z - D;
        }

        /// <summary>
        /// Calculates the (signed) distance of the specified point from the plane.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The calculated distance.</returns>
        public double Distance(in Vector3D point)
        {
            return Normal.Dot(point) - D;
        }
    }
}
