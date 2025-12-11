//-----------------------------------------------------------------------
// <copyright file="SimplexNoise.3D.cs" company="Space Development">
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
    /// Simplex noise functions - 3D.
    /// </summary>
    public static partial class SimplexNoise
    {
        private const double Simple3DRescale = 34.525277436;
        private const double Simplex3DDerivativeRescale = 28.0;

        /* Skewing factors for 3D simplex grid:
         * F3 = 1/3
         * G3 = 1/6 */
        private const double F3 = 0.333333333;
        private const double G3 = 0.166666667;

        private static readonly Vector3D[] gradients3D =
        [

            // 12 cube edges
            new Vector3D(1.0, 0.0, 1.0),
            new Vector3D(0.0, 1.0, 1.0),
            new Vector3D(-1.0, 0.0, 1.0),
            new Vector3D(0.0, -1.0, 1.0),

            new Vector3D(1.0, 0.0, -1.0),
            new Vector3D(0.0, 1.0, -1.0),
            new Vector3D(-1.0, 0.0, -1.0),
            new Vector3D(0.0, -1.0, -1.0),

            new Vector3D(1.0, -1.0, 0.0),
            new Vector3D(1.0, 1.0, 0.0),
            new Vector3D(-1.0, 1.0, 0.0),
            new Vector3D(-1.0, -1.0, 0.0),

            // 4 repeats to make 16
            new Vector3D(1.0, 0.0, 1.0),
            new Vector3D(0.0, 1.0, 1.0),
            new Vector3D(-1.0, 0.0, 1.0),
            new Vector3D(0.0, -1.0, 1.0)
        ];

        /// <summary>
        /// Samples 3D noise value.
        /// </summary>
        /// <param name="x">The sampling x value.</param>
        /// <param name="y">The sampling y value.</param>
        /// <param name="z">The sampling z value.</param>
        /// <returns>The sampled noise.</returns>
        public static double Sample(double x, double y, double z)
        {
            double n0, n1, n2, n3; /* Noise contributions from the four simplex corners */

            /* Skew the input space to determine which simplex cell we're in */
            var s = (x + y + z) * F3; /* Very nice and simple skew factor for 3D */
            var xs = x + s;
            var ys = y + s;
            var zs = z + s;
            var i = MathematicsHelper.FastFloor(xs);
            var j = MathematicsHelper.FastFloor(ys);
            var k = MathematicsHelper.FastFloor(zs);

            /* The x,y,z distances from the cell origin */
            var t = (i + j + k) * G3;
            var x0 = x - i + t;
            var y0 = y - j + t;
            var z0 = z - k + t;

            /* For the 3D case, the simplex shape is a slightly irregular tetrahedron.
             * Determine which simplex we are in. */
            int i1, j1, k1; /* Offsets for second corner of simplex in (i,j,k) coords */
            int i2, j2, k2; /* Offsets for third corner of simplex in (i,j,k) coords */

            if (x0 >= y0)
            {
                if (y0 >= z0)
                {
                    /* X Y Z order */
                    j1 = k1 = k2 = 0;
                    i1 = i2 = j2 = 1;
                }
                else if (x0 >= z0)
                {
                    /* X Z Y order */
                    j1 = j2 = k1 = 0;
                    i1 = i2 = k2 = 1;
                }
                else
                {
                    /* Z X Y order */
                    i1 = j1 = j2 = 0;
                    i2 = k1 = k2 = 1;
                }
            }
            else
            {
                // x0<y0
                if (y0 < z0)
                {
                    /* Z Y X order */
                    i1 = j1 = i2 = 0;
                    j2 = k1 = k2 = 1;
                }
                else if (x0 < z0)
                {
                    /* Y Z X order */
                    i1 = i2 = k1 = 0;
                    j1 = j2 = k2 = 1;
                }
                else
                {
                    /* Y X Z order */
                    i1 = k1 = k2 = 0;
                    i2 = j1 = j2 = 1;
                }
            }

            /* A step of (1,0,0) in (i,j,k) means a step of (1-c,-c,-c) in (x,y,z),
             * a step of (0,1,0) in (i,j,k) means a step of (-c,1-c,-c) in (x,y,z), and
             * a step of (0,0,1) in (i,j,k) means a step of (-c,-c,1-c) in (x,y,z), where
             * c = 1/6.   */

            var x1 = x0 - i1 + G3; /* Offsets for second corner in (x,y,z) coords */
            var y1 = y0 - j1 + G3;
            var z1 = z0 - k1 + G3;
            var x2 = x0 - i2 + 2.0 * G3; /* Offsets for third corner in (x,y,z) coords */
            var y2 = y0 - j2 + 2.0 * G3;
            var z2 = z0 - k2 + 2.0 * G3;
            var x3 = x0 - 1.0 + 3.0 * G3; /* Offsets for last corner in (x,y,z) coords */
            var y3 = y0 - 1.0 + 3.0 * G3;
            var z3 = z0 - 1.0 + 3.0 * G3;

            /* Wrap the integer indices at 256, to avoid indexing details::perm[] out of bounds */
            var ii = i & 0xff;
            var jj = j & 0xff;
            var kk = k & 0xff;

            /* Calculate the contribution from the four corners */
            var t0 = 0.6 - x0 * x0 - y0 * y0 - z0 * z0;
            double t20, t40;
            if (t0 < 0.0)
            {
                n0 = 0.0;
            }
            else
            {
                var gradient0 = gradients3D[Gradient3DIndex(Hash(ii + Hash(jj + Hash(kk))))];
                t20 = t0 * t0;
                t40 = t20 * t20;
                n0 = t40 * (gradient0.X * x0 + gradient0.Y * y0 + gradient0.Z * z0);
            }

            var t1 = 0.6 - x1 * x1 - y1 * y1 - z1 * z1;
            double t21, t41;
            if (t1 < 0.0)
            {
                n1 = 0.0;
            }
            else
            {
                var gradient1 = gradients3D[Gradient3DIndex(Hash(ii + i1 + Hash(jj + j1 + Hash(kk + k1))))];
                t21 = t1 * t1;
                t41 = t21 * t21;
                n1 = t41 * (gradient1.X * x1 + gradient1.Y * y1 + gradient1.Z * z1);
            }

            var t2 = 0.6 - x2 * x2 - y2 * y2 - z2 * z2;
            double t22, t42;
            if (t2 < 0.0)
            {
                n2 = 0.0;
            }
            else
            {
                var gradient2 = gradients3D[Gradient3DIndex(Hash(ii + i2 + Hash(jj + j2 + Hash(kk + k2))))];
                t22 = t2 * t2;
                t42 = t22 * t22;
                n2 = t42 * (gradient2.X * x2 + gradient2.Y * y2 + gradient2.Z * z2);
            }

            var t3 = 0.6 - x3 * x3 - y3 * y3 - z3 * z3;
            double t23, t43;
            if (t3 < 0.0)
            {
                n3 = 0.0;
            }
            else
            {
                var gradient3 = gradients3D[Gradient3DIndex(Hash(ii + 1 + Hash(jj + 1 + Hash(kk + 1))))];
                t23 = t3 * t3;
                t43 = t23 * t23;
                n3 = t43 * (gradient3.X * x3 + gradient3.Y * y3 + gradient3.Z * z3);
            }

            /* Add contributions from each corner to get the final noise value.
             * The result is scaled to return values in the range [-1,1] */
            return Simple3DRescale * (n0 + n1 + n2 + n3);
        }

        /// <summary>
        /// 3D noise value with analytic derivatives.
        /// </summary>
        /// <param name="x">The sampling x value.</param>
        /// <param name="y">The sampling y value.</param>
        /// <param name="z">The sampling z value.</param>
        /// <returns>The sampled noise (x - noise, y - derivative X, z - derivative y, w - derivative z).</returns>
        public static Vector4D SampleWithDerivatives(double x, double y, double z)
        {
            double n0, n1, n2, n3; /* Noise contributions from the four simplex corners */
            Vector3D gradient0, gradient1, gradient2, gradient3; /* Gradients at simplex corners */

            /* Skew the input space to determine which simplex cell we're in */
            var s = (x + y + z) * F3; /* Very nice and simple skew factor for 3D */
            var xs = x + s;
            var ys = y + s;
            var zs = z + s;
            var i = MathematicsHelper.FastFloor(xs);
            var j = MathematicsHelper.FastFloor(ys);
            var k = MathematicsHelper.FastFloor(zs);

            /* The x,y,z distances from the cell origin */
            var t = (i + j + k) * G3;
            var x0 = x - i + t;
            var y0 = y - j + t;
            var z0 = z - k + t;

            /* For the 3D case, the simplex shape is a slightly irregular tetrahedron.
             * Determine which simplex we are in. */
            int i1, j1, k1; /* Offsets for second corner of simplex in (i,j,k) coords */
            int i2, j2, k2; /* Offsets for third corner of simplex in (i,j,k) coords */

            if (x0 >= y0)
            {
                if (y0 >= z0)
                {
                    /* X Y Z order */
                    j1 = k1 = k2 = 0;
                    i1 = i2 = j2 = 1;
                }
                else if (x0 >= z0)
                {
                    /* X Z Y order */
                    j1 = j2 = k1 = 0;
                    i1 = i2 = k2 = 1;
                }
                else
                {
                    /* Z X Y order */
                    i1 = j1 = j2 = 0;
                    i2 = k1 = k2 = 1;
                }
            }
            else
            {
                // x0<y0
                if (y0 < z0)
                {
                    /* Z Y X order */
                    i1 = j1 = i2 = 0;
                    j2 = k1 = k2 = 1;
                }
                else if (x0 < z0)
                {
                    /* Y Z X order */
                    i1 = i2 = k1 = 0;
                    j1 = j2 = k2 = 1;
                }
                else
                {
                    /* Y X Z order */
                    i1 = k1 = k2 = 0;
                    i2 = j1 = j2 = 1;
                }
            }

            /* A step of (1,0,0) in (i,j,k) means a step of (1-c,-c,-c) in (x,y,z),
             * a step of (0,1,0) in (i,j,k) means a step of (-c,1-c,-c) in (x,y,z), and
             * a step of (0,0,1) in (i,j,k) means a step of (-c,-c,1-c) in (x,y,z), where
             * c = 1/6.   */

            var x1 = x0 - i1 + G3; /* Offsets for second corner in (x,y,z) coords */
            var y1 = y0 - j1 + G3;
            var z1 = z0 - k1 + G3;
            var x2 = x0 - i2 + 2.0 * G3; /* Offsets for third corner in (x,y,z) coords */
            var y2 = y0 - j2 + 2.0 * G3;
            var z2 = z0 - k2 + 2.0 * G3;
            var x3 = x0 - 1.0 + 3.0 * G3; /* Offsets for last corner in (x,y,z) coords */
            var y3 = y0 - 1.0 + 3.0 * G3;
            var z3 = z0 - 1.0 + 3.0 * G3;

            /* Wrap the integer indices at 256, to avoid indexing details::perm[] out of bounds */
            var ii = i & 0xff;
            var jj = j & 0xff;
            var kk = k & 0xff;

            /* Calculate the contribution from the four corners */
            var t0 = 0.6 - x0 * x0 - y0 * y0 - z0 * z0;
            double t20, t40;
            if (t0 < 0.0)
            {
                n0 = t0 = t20 = t40 = 0.0;
                gradient0 = default;
            }
            else
            {
                gradient0 = gradients3D[Gradient3DIndex(Hash(ii + Hash(jj + Hash(kk))))];
                t20 = t0 * t0;
                t40 = t20 * t20;
                n0 = t40 * (gradient0.X * x0 + gradient0.Y * y0 + gradient0.Z * z0);
            }

            var t1 = 0.6 - x1 * x1 - y1 * y1 - z1 * z1;
            double t21, t41;
            if (t1 < 0.0)
            {
                n1 = t1 = t21 = t41 = 0.0;
                gradient1 = default;
            }
            else
            {
                gradient1 = gradients3D[Gradient3DIndex(Hash(ii + i1 + Hash(jj + j1 + Hash(kk + k1))))];
                t21 = t1 * t1;
                t41 = t21 * t21;
                n1 = t41 * (gradient1.X * x1 + gradient1.Y * y1 + gradient1.Z * z1);
            }

            var t2 = 0.6 - x2 * x2 - y2 * y2 - z2 * z2;
            double t22, t42;
            if (t2 < 0.0)
            {
                n2 = t2 = t22 = t42 = 0.0;
                gradient2 = default;
            }
            else
            {
                gradient2 = gradients3D[Gradient3DIndex(Hash(ii + i2 + Hash(jj + j2 + Hash(kk + k2))))];
                t22 = t2 * t2;
                t42 = t22 * t22;
                n2 = t42 * (gradient2.X * x2 + gradient2.Y * y2 + gradient2.Z * z2);
            }

            var t3 = 0.6 - x3 * x3 - y3 * y3 - z3 * z3;
            double t23, t43;
            if (t3 < 0.0)
            {
                n3 = t3 = t23 = t43 = 0.0;
                gradient3 = default;
            }
            else
            {
                gradient3 = gradients3D[Gradient3DIndex(Hash(ii + 1 + Hash(jj + 1 + Hash(kk + 1))))];
                t23 = t3 * t3;
                t43 = t23 * t23;
                n3 = t43 * (gradient3.X * x3 + gradient3.Y * y3 + gradient3.Z * z3);
            }

            /*  A straight, unoptimised calculation would be like:
             *     *derivativeX = -8.0 * t20 * t0 * x0 * dot(gradient0.X, gradient0.Y, gradient0.Z, x0, y0, z0) + t40 * gradient0.X;
             *    *derivativeY = -8.0 * t20 * t0 * y0 * dot(gradient0.X, gradient0.Y, gradient0.Z, x0, y0, z0) + t40 * gradient0.Y;
             *    *derivativeZ = -8.0 * t20 * t0 * z0 * dot(gradient0.X, gradient0.Y, gradient0.Z, x0, y0, z0) + t40 * gradient0.Z;
             *    *derivativeX += -8.0 * t21 * t1 * x1 * dot(gradient1.X, gradient1.Y, gradient1.Z, x1, y1, z1) + t41 * gradient1.X;
             *    *derivativeY += -8.0 * t21 * t1 * y1 * dot(gradient1.X, gradient1.Y, gradient1.Z, x1, y1, z1) + t41 * gradient1.Y;
             *    *derivativeZ += -8.0 * t21 * t1 * z1 * dot(gradient1.X, gradient1.Y, gradient1.Z, x1, y1, z1) + t41 * gradient1.Z;
             *    *derivativeX += -8.0 * t22 * t2 * x2 * dot(gradient2.X, gradient2.Y, gradient2.Z, x2, y2, z2) + t42 * gradient2.X;
             *    *derivativeY += -8.0 * t22 * t2 * y2 * dot(gradient2.X, gradient2.Y, gradient2.Z, x2, y2, z2) + t42 * gradient2.Y;
             *    *derivativeZ += -8.0 * t22 * t2 * z2 * dot(gradient2.X, gradient2.Y, gradient2.Z, x2, y2, z2) + t42 * gradient2.Z;
             *    *derivativeX += -8.0 * t23 * t3 * x3 * dot(gradient3.X, gradient3.Y, gradient3.Z, x3, y3, z3) + t43 * gradient3.X;
             *    *derivativeY += -8.0 * t23 * t3 * y3 * dot(gradient3.X, gradient3.Y, gradient3.Z, x3, y3, z3) + t43 * gradient3.Y;
             *    *derivativeZ += -8.0 * t23 * t3 * z3 * dot(gradient3.X, gradient3.Y, gradient3.Z, x3, y3, z3) + t43 * gradient3.Z;
             */
            var temp0 = t20 * t0 * (gradient0.X * x0 + gradient0.Y * y0 + gradient0.Z * z0);
            var derivativeX = temp0 * x0;
            var derivativeY = temp0 * y0;
            var derivativeZ = temp0 * z0;
            var temp1 = t21 * t1 * (gradient1.X * x1 + gradient1.Y * y1 + gradient1.Z * z1);
            derivativeX += temp1 * x1;
            derivativeY += temp1 * y1;
            derivativeZ += temp1 * z1;
            var temp2 = t22 * t2 * (gradient2.X * x2 + gradient2.Y * y2 + gradient2.Z * z2);
            derivativeX += temp2 * x2;
            derivativeY += temp2 * y2;
            derivativeZ += temp2 * z2;
            var temp3 = t23 * t3 * (gradient3.X * x3 + gradient3.Y * y3 + gradient3.Z * z3);
            derivativeX += temp3 * x3;
            derivativeY += temp3 * y3;
            derivativeZ += temp3 * z3;
            derivativeX *= -8.0;
            derivativeY *= -8.0;
            derivativeZ *= -8.0;
            derivativeX += t40 * gradient0.X + t41 * gradient1.X + t42 * gradient2.X + t43 * gradient3.X;
            derivativeY += t40 * gradient0.Y + t41 * gradient1.Y + t42 * gradient2.Y + t43 * gradient3.Y;
            derivativeZ += t40 * gradient0.Z + t41 * gradient1.Z + t42 * gradient2.Z + t43 * gradient3.Z;

            derivativeX *= Simplex3DDerivativeRescale; /* Scale derivative to match the noise scaling */
            derivativeY *= Simplex3DDerivativeRescale;
            derivativeZ *= Simplex3DDerivativeRescale;

            /* Add contributions from each corner to get the final noise value.
             * The result is scaled to return values in the range [-1,1] */
            return new Vector4D(
                Simple3DRescale * (n0 + n1 + n2 + n3),
                derivativeX,
                derivativeY,
                derivativeZ);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Gradient3DIndex(int hash)
        {
            return hash & 15;
        }
    }
}
