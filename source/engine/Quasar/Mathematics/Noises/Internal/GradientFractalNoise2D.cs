//-----------------------------------------------------------------------
// <copyright file="GradientFractalNoise2D.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Mathematics.Noises.Internals
{
    /// <summary>
    /// 2D simplex gradient fractal noise implementation.
    /// </summary>
    /// <seealso cref="IFractalNoise2D" />
    internal sealed class GradientFractalNoise2D : IFractalNoise2D
    {
        private readonly int octaves;
        private readonly double amplitude;
        private readonly double frequency;
        private readonly double lacunarity;
        private readonly double persistence;
        private readonly double power;
        private readonly double xOffset;
        private readonly double yOffset;


        /// <summary>
        /// Initializes a new instance of the <see cref="GradientFractalNoise2D" /> class.
        /// </summary>
        /// <param name="fractalParameters">The fractal parameters.</param>
        /// <param name="power">The power.</param>
        public GradientFractalNoise2D(in FractalParameters fractalParameters, double power)
        {
            octaves = fractalParameters.Octaves;
            amplitude = fractalParameters.Amplitude;
            frequency = fractalParameters.Frequency;
            lacunarity = fractalParameters.Lacunarity;
            persistence = fractalParameters.Persistance;
            this.power = power;

            var random = new Random(fractalParameters.Seed.GetHashCode());
            xOffset = random.NextDouble() - 0.5;
            yOffset = random.NextDouble() - 0.5;
        }


        /// <summary>
        /// Samples the noise at the specified x, y position.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <returns>
        /// The noise value with gradients (x - noise value, y - grad x, z - grad y).
        /// </returns>
        public double Sample(double x, double y)
        {
            var result = 0.0;
            var a = amplitude;
            var xf = frequency * (x + xOffset);
            var yf = frequency * (y + yOffset);
            var gradX = 0.0;
            var gradY = 0.0;
            for (var i = 0; i < octaves; i++)
            {
                var noise = SimplexNoise.SampleWithDerivatives(xf, yf);
                gradX += noise.Y;
                gradY += noise.Z;
                var gradient = gradX * gradX + gradY * gradY;
                result += a * noise.X / (1.0 + power * gradient);

                xf *= lacunarity;
                yf *= lacunarity;
                a *= persistence;
            }

            return result;
        }
    }
}
