//-----------------------------------------------------------------------
// <copyright file="NoiseFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Space.Core.DependencyInjection;

namespace Quasar.Mathematics.Noises.Internals
{
    /// <summary>
    /// Noise factory implementation.
    /// </summary>
    /// <seealso cref="INoiseFactory" />
    [Export(typeof(INoiseFactory))]
    [Singleton]
    internal sealed class NoiseFactory : INoiseFactory
    {
        /// <summary>
        /// Creates a 2D simplex noise based fractal noise by the specified parameters.
        /// </summary>
        /// <param name="fractalParameters">The fractal parameters.</param>
        public IFractalNoise2D CreateFractalNoise2D(in FractalParameters fractalParameters)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(fractalParameters.Octaves, nameof(fractalParameters.Octaves));
            ArgumentOutOfRangeException.ThrowIfNegative(fractalParameters.Frequency, nameof(fractalParameters.Frequency));

            return new FractalNoise2D(fractalParameters);
        }

        /// <summary>
        /// Creates a 3D simplex noise based fractal noise by the specified parameters.
        /// </summary>
        /// <param name="fractalParameters">The fractal parameters.</param>
        public IFractalNoise3D CreateFractalNoise3D(in FractalParameters fractalParameters)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(fractalParameters.Octaves, nameof(fractalParameters.Octaves));
            ArgumentOutOfRangeException.ThrowIfNegative(fractalParameters.Frequency, nameof(fractalParameters.Frequency));

            return new FractalNoise3D(fractalParameters);
        }

        /// <summary>
        /// Creates a 2D simplex noise based gradient fractal noise by the specified parameters.
        /// </summary>
        /// <param name="fractalParameters">The fractal parameters.</param>
        /// <param name="power">The gradient power (0...+Inf).</param>
        public IFractalNoise2D CreateGradientFractalNoise2D(in FractalParameters fractalParameters, double power)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(fractalParameters.Octaves, nameof(fractalParameters.Octaves));
            ArgumentOutOfRangeException.ThrowIfNegative(fractalParameters.Frequency, nameof(fractalParameters.Frequency));
            ArgumentOutOfRangeException.ThrowIfNegative(power, nameof(power));

            return new GradientFractalNoise2D(fractalParameters, power);
        }

        /// <summary>
        /// Creates a 3D simplex noise based gradient fractal noise by the specified parameters.
        /// </summary>
        /// <param name="fractalParameters">The fractal parameters.</param>
        /// <param name="power">The gradient power (0...+Inf).</param>
        public IFractalNoise3D CreateGradientFractalNoise3D(in FractalParameters fractalParameters, double power)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(fractalParameters.Octaves, nameof(fractalParameters.Octaves));
            ArgumentOutOfRangeException.ThrowIfNegative(fractalParameters.Frequency, nameof(fractalParameters.Frequency));
            ArgumentOutOfRangeException.ThrowIfNegative(power, nameof(power));

            return new GradientFractalNoise3D(fractalParameters, power);
        }

        /// <summary>
        /// Creates a 2D simplex noise based ridged fractal noise.
        /// </summary>
        /// <param name="fractalParameters">The fractal parameters.</param>
        /// <param name="power">The power (0...+Inf).</param>
        public IFractalNoise2D CreateRidgedFractalNoise2D(in FractalParameters fractalParameters, double power)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(fractalParameters.Octaves, nameof(fractalParameters.Octaves));
            ArgumentOutOfRangeException.ThrowIfNegative(fractalParameters.Frequency, nameof(fractalParameters.Frequency));
            ArgumentOutOfRangeException.ThrowIfNegative(power, nameof(power));

            return new RidgedFractalNoise2D(fractalParameters, power);
        }

        /// <summary>
        /// Creates a 3D simplex noise based ridged fractal noise.
        /// </summary>
        /// <param name="fractalParameters">The fractal parameters.</param>
        /// <param name="power">The power (0...+Inf).</param>
        public IFractalNoise3D CreateRidgedFractalNoise3D(in FractalParameters fractalParameters, double power)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(fractalParameters.Octaves, nameof(fractalParameters.Octaves));
            ArgumentOutOfRangeException.ThrowIfNegative(fractalParameters.Frequency, nameof(fractalParameters.Frequency));
            ArgumentOutOfRangeException.ThrowIfNegative(power, nameof(power));

            return new RidgedFractalNoise3D(fractalParameters, power);
        }
    }
}
