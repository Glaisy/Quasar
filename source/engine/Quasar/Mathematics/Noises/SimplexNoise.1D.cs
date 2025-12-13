//-----------------------------------------------------------------------
// <copyright file="SimplexNoise.1D.cs" company="Space Development">
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
    /// Simplex noise functions - 1D.
    /// </summary>
    public static partial class SimplexNoise
    {
        private const double Simplex1DRescale = 0.3961965135;
        private const double Simplex1DDerivativeRescale = 0.25;


        /// <summary>
        /// Samples 1D noise value.
        /// </summary>
        /// <param name="x">The sampling x value.</param>
        /// <returns>The sampled noise.</returns>
        public static double Sample(double x)
        {
            var i0 = MathematicsHelper.FastFloor(x);
            var i1 = i0 + 1;
            var x0 = x - i0;
            var x1 = x0 - 1.0;

            var t0 = 1.0 - x0 * x0;
            var t20 = t0 * t0;
            var t40 = t20 * t20;

            var t1 = 1.0 - x1 * x1;
            var t21 = t1 * t1;
            var t41 = t21 * t21;

            var n0 = t40 * Gradient(Hash(i0)) * x0;
            var n1 = t41 * Gradient(Hash(i1)) * x1;

            // The maximum value of this noise is 8*(3/4)^4 = 2.53125
            // A factor of 0.3961965135 scales to fit exactly within [-1,1]
            return Simplex1DRescale * (n0 + n1);
        }

        /// <summary>
        /// 1D noise value with analytic derivative.
        /// </summary>
        /// <param name="x">The sampling x value.</param>
        /// <returns>The sampled noise (x - noise, y - derivative).</returns>
        public static Vector2D SampleWithDerivative(double x)
        {
            var i0 = MathematicsHelper.FastFloor(x);
            var i1 = i0 + 1;
            var x0 = x - i0;
            var x1 = x0 - 1.0;

            var x20 = x0 * x0;
            var t0 = 1.0 - x20;
            var t20 = t0 * t0;
            var t40 = t20 * t20;

            var x21 = x1 * x1;
            var t1 = 1.0 - x21;
            var t21 = t1 * t1;
            var t41 = t21 * t21;

            var gx0 = Gradient(Hash(i0));
            var n0 = t40 * gx0 * x0;
            var gx1 = Gradient(Hash(i1));
            var n1 = t41 * gx1 * x1;

            /* Compute derivative according to:
                 *  derivative = -8.0 * t20 * t0 * x0 * (gx0 * x0) + t40 * gx0;
                 *  derivative += -8.0 * t21 * t1 * x1 * (gx1 * x1) + t41 * gx1;
            */
            var derivative = t20 * t0 * gx0 * x20;
            derivative += t21 * t1 * gx1 * x21;
            derivative *= -8.0;
            derivative += t40 * gx0 + t41 * gx1;
            derivative *= Simplex1DDerivativeRescale;  /* Scale derivative to match the noise scaling */

            // The maximum value of this noise is 8*(3/4)^4 = 2.53125
            // A factor of 0.3961965135 would scale to fit exactly within [-1,1], but
            // to better match classic Perlin noise, we scale it down some more.
            return new Vector2D(Simplex1DRescale * (n0 + n1), derivative);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double Gradient(int hash)
        {
            var h = hash & 0x0F;
            var gradient = 1.0 + (h & 7);
            if ((h & 8) != 0)
            {
                gradient = -gradient;
            }

            return gradient;
        }
    }
}
