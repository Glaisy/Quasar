//-----------------------------------------------------------------------
// <copyright file="Frustum.cs" company="Space Development">
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
using Quasar.Mathematics;

using Space.Core.Mathematics;

namespace Quasar.Rendering
{
    /// <summary>
    /// Perspective frustum implementation.
    /// </summary>
    /// <seealso cref="IFrustum" />
    public sealed class Frustum : IFrustum
    {
        private const int PlaneCount = 6;
        private const int PointCount = 8;


        /// <summary>
        /// Gets the frustum planes in world space.
        /// </summary>
        public EuclideanPlane[] Planes { get; } = new EuclideanPlane[PlaneCount];

        /// <summary>
        /// Gets the frustum points in world space.
        /// </summary>
        public Vector3[] Points { get; } = new Vector3[PointCount];


        /// <summary>
        /// Determines whether the specified world space point (x, y, z) is in the frustum.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        public bool IsInFrustum(float x, float y, float z)
        {
            for (var i = 0; i < PlaneCount; i++)
            {
                if (Planes[i].Distance(x, y, z) < 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether the specified world space point is in the frustum.
        /// </summary>
        /// <param name="point">The point.</param>
        public bool IsInFrustum(in Vector3 point)
        {
            for (var i = 0; i < PlaneCount; i++)
            {
                if (Planes[i].Distance(point) < 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether the specified world space bounding box is in the frustum.
        /// </summary>
        /// <param name="boundingBox">The bounding box.</param>
        public bool IsInFrustum(in BoundingBox boundingBox)
        {
            for (var i = 0; i < PlaneCount; i++)
            {
                var normal = Planes[i].Normal;
                var x = normal.X < 0 ? boundingBox.Min.X : boundingBox.Max.X;
                var y = normal.Y < 0 ? boundingBox.Min.Y : boundingBox.Max.Y;
                var z = normal.Z < 0 ? boundingBox.Min.Z : boundingBox.Max.Z;

                if (Planes[i].Distance(x, y, z) < 0.0f)
                {
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// Updates the frustum planes and points.
        /// </summary>
        /// <param name="transform">The view transformation object.</param>
        /// <param name="fieldOfView">The field of view angle [degreee].</param>
        /// <param name="aspectRatio">The aspect ratio.</param>
        /// <param name="zNear">The near Z plane distance.</param>
        /// <param name="zFar">The far Z plane distance.</param>
        public void Update(
            ITransform transform,
            float fieldOfView,
            float aspectRatio,
            float zNear,
            float zFar)
        {
            var tanFov = MathF.Tan(fieldOfView * 0.5f * MathematicsConstants.DegreeToRadian);

            // calculate frustum corners
            var cameraPosition = transform.Position;
            var nearCenter = cameraPosition + transform.PositiveZ * zNear;
            var farCenter = cameraPosition + transform.PositiveZ * zFar;

            var nearDelta = tanFov * zNear;
            var nearDeltaX = aspectRatio * nearDelta * transform.PositiveX;
            var nearDeltaY = nearDelta * transform.PositiveY;

            var farDelta = tanFov * zFar;
            var farDeltaX = aspectRatio * farDelta * transform.PositiveX;
            var farDeltaY = farDelta * transform.PositiveY;

            var nearRight = nearCenter + nearDeltaX;
            var nearLeft = nearCenter - nearDeltaX;
            var nearTop = nearCenter + nearDeltaY;
            var nearBottom = nearCenter - nearDeltaY;

            var farRight = farCenter + farDeltaX;
            var farLeft = farCenter - farDeltaX;
            var farTop = farCenter + farDeltaY;
            var farBottom = farCenter - farDeltaY;

            // near plane
            Planes[0] = new EuclideanPlane(transform.PositiveZ, nearCenter);

            // far plane
            Planes[1] = new EuclideanPlane(transform.NegativeZ, farCenter);

            // right plane
            var normal = Vector3.Cross(transform.NegativeY, farRight - nearRight).Normalize();
            Planes[2] = new EuclideanPlane(normal, farRight);

            // left plane
            normal = Vector3.Cross(transform.PositiveY, farLeft - nearLeft).Normalize();
            Planes[3] = new EuclideanPlane(normal, farLeft);

            // top plane
            normal = Vector3.Cross(transform.PositiveX, farTop - nearTop).Normalize();
            Planes[4] = new EuclideanPlane(normal, farTop);

            // bottom plane
            normal = Vector3.Cross(transform.NegativeX, farBottom - nearBottom).Normalize();
            Planes[5] = new EuclideanPlane(normal, farBottom);

            // points
            Points[0] = nearLeft + nearDeltaY;
            Points[1] = nearLeft - nearDeltaY;
            Points[2] = nearRight + nearDeltaY;
            Points[3] = nearRight - nearDeltaY;
            Points[4] = farLeft + farDeltaY;
            Points[5] = farLeft - farDeltaY;
            Points[6] = farRight + farDeltaY;
            Points[7] = farRight - farDeltaY;
        }
    }
}
