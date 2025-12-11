//-----------------------------------------------------------------------
// <copyright file="SimplexNoise.2D.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Runtime.CompilerServices;

using Space.Core.Mathematics;

namespace Quasar.Mathematics.Noises
{
    /// <summary>
    /// Simplex noise functions - 2D.
    /// </summary>
    public static partial class SimplexNoise
    {
        private const double Simplex2DRescale = 45.23065;
        private const double Simplex2DDerivativeRescale = 25.7816316351977;

        /* Skewing factors for 2D simplex grid:
            * F2 = 0.5*(sqrt(3.0)-1.0)
            * G2 = (3.0-Math.sqrt(3.0))/6.0
        */
        private const double F2 = 0.366025403;
        private const double G2 = 0.211324865;


        private static readonly Vector2D[] gradients2D =
        [
            new Vector2D(-1.0, -1.0),
            new Vector2D(1.0, 0.0),
            new Vector2D(-1.0, 0.0),
            new Vector2D(1.0, 1.0),
            new Vector2D(-1.0, 1.0),
            new Vector2D(0.0, -1.0),
            new Vector2D(0.0, 1.0),
            new Vector2D(1.0, -1.0)
        ];


        /// <summary>
        /// Samples 2D noise value.
        /// </summary>
        /// <param name="x">The sampling x value.</param>
        /// <param name="y">The sampling y value.</param>
        /// <returns>The sampled noise.</returns>
        public static double Sample(double x, double y)
        {
            double n0, n1, n2; // Noise contributions from the three corners

            // Skew the input space to determine which simplex cell we're in
            var s = (x + y) * F2; // Hairy factor for 2D
            var xs = x + s;
            var ys = y + s;
            var i = MathematicsHelper.FastFloor(xs);
            var j = MathematicsHelper.FastFloor(ys);

            // The x,y distances from the cell origin
            var t = (i + j) * G2;
            var x0 = x - i + t;
            var y0 = y - j + t;

            // For the 2D case, the simplex shape is an equilateral triangle.
            // Determine which simplex we are in.
            int i1, j1; // Offsets for second (middle) corner of simplex in (i,j) coords
            if (x0 > y0)
            {
                i1 = 1;
                j1 = 0;
            } // lower triangle, XY order: (0,0)->(1,0)->(1,1)
            else
            {
                i1 = 0;
                j1 = 1;
            } // upper triangle, YX order: (0,0)->(0,1)->(1,1)

            // A step of (1,0) in (i,j) means a step of (1-c,-c) in (x,y), and
            // a step of (0,1) in (i,j) means a step of (-c,1-c) in (x,y), where
            // c = (3-sqrt(3))/6
            var x1 = x0 - i1 + G2; // Offsets for middle corner in (x,y) unskewed coords
            var y1 = y0 - j1 + G2;
            var x2 = x0 - 1.0 + 2.0 * G2; // Offsets for last corner in (x,y) unskewed coords
            var y2 = y0 - 1.0 + 2.0 * G2;

            // Wrap the integer indices at 256, to avoid indexing details::perm[] out of bounds
            var ii = i & 0xFF;
            var jj = j & 0xFF;

            /* Calculate the contribution from the three corners */
            var t0 = 0.5 - x0 * x0 - y0 * y0;
            if (t0 < 0.0)
            {
                n0 = 0.0; /* No influence */
            }
            else
            {
                var gi0 = Hash(ii + Hash(jj));
                var gradient0 = gradients2D[Gradient2DIndex(gi0)];
                var t20 = t0 * t0;
                var t40 = t20 * t20;
                n0 = t40 * (gradient0.X * x0 + gradient0.Y * y0);
            }

            var t1 = 0.5 - x1 * x1 - y1 * y1;
            if (t1 < 0.0)
            {
                n1 = 0.0; /* No influence */
            }
            else
            {
                var gi1 = Hash(ii + i1 + Hash(jj + j1));
                var gradient1 = gradients2D[Gradient2DIndex(gi1)];
                var t21 = t1 * t1;
                var t41 = t21 * t21;
                n1 = t41 * (gradient1.X * x1 + gradient1.Y * y1);
            }

            var t2 = 0.5 - x2 * x2 - y2 * y2;
            if (t2 < 0.0)
            {
                n2 = 0.0; /* No influence */
            }
            else
            {
                var gi2 = Hash(ii + 1 + Hash(jj + 1));
                var gradient2 = gradients2D[Gradient2DIndex(gi2)];
                var t22 = t2 * t2;
                var t42 = t22 * t22;
                n2 = t42 * (gradient2.X * x2 + gradient2.Y * y2);
            }

            // Add contributions from each corner to get the final noise value.
            // The result is scaled to return values in the interval [-1,1].
            return Simplex2DRescale * (n0 + n1 + n2);
        }

        /// <summary>
        /// 2D noise value with analytic derivatives.
        /// </summary>
        /// <param name="x">The sampling x value.</param>
        /// <param name="y">The sampling y value.</param>
        /// <returns>The sampled noise (x - noise, y - derivative X, z - derivative y).</returns>
        public static Vector3D SampleWithDerivatives(double x, double y)
        {
            double n0, n1, n2; // Noise contributions from the three corners
            Vector2D gradient0, gradient1, gradient2;

            // Skew the input space to determine which simplex cell we're in
            var s = (x + y) * F2; // Hairy factor for 2D
            var xs = x + s;
            var ys = y + s;
            var i = MathematicsHelper.FastFloor(xs);
            var j = MathematicsHelper.FastFloor(ys);

            var t = (i + j) * G2;
            var x0 = x - i + t; // The x,y distances from the cell origin
            var y0 = y - j + t;

            // For the 2D case, the simplex shape is an equilateral triangle.
            // Determine which simplex we are in.
            int i1, j1; // Offsets for second (middle) corner of simplex in (i,j) coords
            if (x0 > y0)
            {
                i1 = 1;
                j1 = 0;
            } // lower triangle, XY order: (0,0)->(1,0)->(1,1)
            else
            {
                i1 = 0;
                j1 = 1;
            } // upper triangle, YX order: (0,0)->(0,1)->(1,1)

            // A step of (1,0) in (i,j) means a step of (1-c,-c) in (x,y), and
            // a step of (0,1) in (i,j) means a step of (-c,1-c) in (x,y), where
            // c = (3-sqrt(3))/6
            var x1 = x0 - i1 + G2; // Offsets for middle corner in (x,y) unskewed coords
            var y1 = y0 - j1 + G2;
            var x2 = x0 - 1.0 + 2.0 * G2; // Offsets for last corner in (x,y) unskewed coords
            var y2 = y0 - 1.0 + 2.0 * G2;

            // Wrap the integer indices at 256, to avoid indexing details::perm[] out of bounds
            var ii = i & 0xFF;
            var jj = j & 0xFF;

            /* Calculate the contribution from the three corners */
            var t0 = 0.5 - x0 * x0 - y0 * y0;
            double t20, t40;
            if (t0 < 0.0)
            {
                gradient0 = default;
                t40 = t20 = t0 = n0 = 0.0; /* No influence */
            }
            else
            {
                var gi0 = Hash(ii + Hash(jj));
                gradient0 = gradients2D[Gradient2DIndex(gi0)];
                t20 = t0 * t0;
                t40 = t20 * t20;
                n0 = t40 * (gradient0.X * x0 + gradient0.Y * y0);
            }

            var t1 = 0.5 - x1 * x1 - y1 * y1;
            double t21, t41;
            if (t1 < 0.0)
            {
                gradient1 = default;
                t21 = t41 = t1 = n1 = 0.0; /* No influence */
            }
            else
            {
                var gi1 = Hash(ii + i1 + Hash(jj + j1));
                gradient1 = gradients2D[Gradient2DIndex(gi1)];
                t21 = t1 * t1;
                t41 = t21 * t21;
                n1 = t41 * (gradient1.X * x1 + gradient1.Y * y1);
            }

            var t2 = 0.5 - x2 * x2 - y2 * y2;
            double t22, t42;
            if (t2 < 0.0)
            {
                gradient2 = default;
                t42 = t22 = t2 = n2 = 0.0; /* No influence */
            }
            else
            {
                var gi2 = Hash(ii + 1 + Hash(jj + 1));
                gradient2 = gradients2D[Gradient2DIndex(gi2)];
                t22 = t2 * t2;
                t42 = t22 * t22;
                n2 = t42 * (gradient2.X * x2 + gradient2.Y * y2);
            }

            /* Compute derivative, if requested by supplying non-null pointers
             * for the last two arguments */
            /*  A straight, unoptimised calculation would be like:
             *    derivativeX = -8.0f * t20 * t0 * x0 * ( gradient0.X * x0 + gradient0.Y * y0 ) + t40 * gradient0.X;
             *    derivativeY = -8.0f * t20 * t0 * y0 * ( gradient0.X * x0 + gradient0.Y * y0 ) + t40 * gradient0.Y;
             *    derivativeX += -8.0f * t21 * t1 * x1 * ( gradient1.X * x1 + gradient1.Y * y1 ) + t41 * gradient1.X;
             *    derivativeY += -8.0f * t21 * t1 * y1 * ( gradient1.X * x1 + gradient1.Y * y1 ) + t41 * gradient1.Y;
             *    derivativeX += -8.0f * t22 * t2 * x2 * ( gradient2.X * x2 + gradient2.Y * y2 ) + t42 * gradient2.X;
             *    derivativeY += -8.0f * t22 * t2 * y2 * ( gradient2.X * x2 + gradient2.Y * y2 ) + t42 * gradient2.Y;
             */
            var temp0 = t20 * t0 * (gradient0.X * x0 + gradient0.Y * y0);
            var derivativeX = temp0 * x0;
            var derivativeY = temp0 * y0;
            var temp1 = t21 * t1 * (gradient1.X * x1 + gradient1.Y * y1);
            derivativeX += temp1 * x1;
            derivativeY += temp1 * y1;
            var temp2 = t22 * t2 * (gradient2.X * x2 + gradient2.Y * y2);
            derivativeX += temp2 * x2;
            derivativeY += temp2 * y2;
            derivativeX *= -8.0;
            derivativeY *= -8.0;
            derivativeX += t40 * gradient0.X + t41 * gradient1.X + t42 * gradient2.X;
            derivativeY += t40 * gradient0.Y + t41 * gradient1.Y + t42 * gradient2.Y;
            derivativeX *= Simplex2DDerivativeRescale; /* Scale derivative to match the noise scaling */
            derivativeY *= Simplex2DDerivativeRescale;

            // Add contributions from each corner to get the final noise value.
            // The result is scaled to return values in the interval [-1,1].
            return new Vector3D(
                Simplex2DRescale * (n0 + n1 + n2),
                derivativeX,
                derivativeY);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Gradient2DIndex(int hash)
        {
            return hash & 7;
        }
    }
}
