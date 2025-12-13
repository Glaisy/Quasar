//-----------------------------------------------------------------------
// <copyright file="INoiseFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Mathematics.Noises
{
    /// <summary>
    /// Noise factory interface definition.
    /// </summary>
    public interface INoiseFactory
    {
        /// <summary>
        /// Creates a 2D simplex noise based fractal noise by the specified parameters.
        /// </summary>
        /// <param name="fractalParameters">The fractal parameters.</param>
        IFractalNoise2D CreateFractalNoise2D(in FractalParameters fractalParameters);

        /// <summary>
        /// Creates a 3D simplex noise based fractal noise by the specified parameters.
        /// </summary>
        /// <param name="fractalParameters">The fractal parameters.</param>
        IFractalNoise3D CreateFractalNoise3D(in FractalParameters fractalParameters);

        /// <summary>
        /// Creates a 2D simplex noise based gradient fractal noise by the specified parameters.
        /// </summary>
        /// <param name="fractalParameters">The fractal parameters.</param>
        /// <param name="power">The gradient power (0...+Inf).</param>
        IFractalNoise2D CreateGradientFractalNoise2D(in FractalParameters fractalParameters, double power);

        /// <summary>
        /// Creates a 3D simplex noise based gradient fractal noise by the specified parameters.
        /// </summary>
        /// <param name="fractalParameters">The fractal parameters.</param>
        /// <param name="power">The gradient power (0...+Inf).</param>
        IFractalNoise3D CreateGradientFractalNoise3D(in FractalParameters fractalParameters, double power);

        /// <summary>
        /// Creates a 2D simplex noise based ridged fractal noise.
        /// </summary>
        /// <param name="fractalParameters">The fractal parameters.</param>
        /// <param name="power">The power (0...+Inf).</param>
        IFractalNoise2D CreateRidgedFractalNoise2D(in FractalParameters fractalParameters, double power);

        /// <summary>
        /// Creates a 3D simplex noise based ridged fractal noise.
        /// </summary>
        /// <param name="fractalParameters">The fractal parameters.</param>
        /// <param name="power">The power (0...+Inf).</param>
        IFractalNoise3D CreateRidgedFractalNoise3D(in FractalParameters fractalParameters, double power);
    }
}
