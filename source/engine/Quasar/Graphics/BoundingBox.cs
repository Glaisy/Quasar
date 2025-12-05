//-----------------------------------------------------------------------
// <copyright file="BoundingBox.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Graphics
{
    /// <summary>
    /// Represents an axis-aligned bounding box (Immutable).
    /// </summary>
    public readonly struct BoundingBox
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BoundingBox" /> struct.
        /// </summary>
        /// <param name="minX">The minimum x coordinate.</param>
        /// <param name="minY">The minimum y coordinate.</param>
        /// <param name="minZ">The minimum z coordinate.</param>
        /// <param name="maxX">The maximum x coordinate.</param>
        /// <param name="maxY">The maximum y coordinate.</param>
        /// <param name="maxZ">The maximum z coordinate.</param>
        public BoundingBox(float minX, float minY, float minZ, float maxX, float maxY, float maxZ)
        {
            Min = new Vector3(minX, minY, minZ);
            Max = new Vector3(maxX, maxY, maxZ);
        }


        /// <summary>
        /// The empty bounding box.
        /// </summary>
        public static readonly BoundingBox Empty;

        /// <summary>
        /// The maximum position of the bounding box.
        /// </summary>
        public readonly Vector3 Max;

        /// <summary>
        /// The minimum position of the bounding box.
        /// </summary>
        public readonly Vector3 Min;


        /// <summary>
        /// Creates a bound box from a and b points.
        /// </summary>
        /// <param name="a">The point a.</param>
        /// <param name="b">The point b.</param>
        public static BoundingBox Create(in Vector3 a, in Vector3 b)
        {
            float minX, minY, minZ;
            float maxX, maxY, maxZ;

            // adjust min-max coordinates
            if (b.X >= a.X)
            {
                minX = a.X;
                maxX = b.X;
            }
            else
            {
                minX = b.X;
                maxX = a.X;
            }

            if (b.Y >= a.Y)
            {
                minY = a.Y;
                maxY = b.Y;
            }
            else
            {
                minY = b.Y;
                maxY = a.Y;
            }

            if (b.Z >= a.Z)
            {
                minZ = a.Z;
                maxZ = b.Z;
            }
            else
            {
                minZ = b.Z;
                maxZ = a.Z;
            }

            return new BoundingBox(minX, minY, minZ, maxX, maxY, maxZ);
        }
    }
}
