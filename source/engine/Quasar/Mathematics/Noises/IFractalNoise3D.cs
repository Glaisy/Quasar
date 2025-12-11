//-----------------------------------------------------------------------
// <copyright file="IFractalNoise3D.cs" company="Space Development">
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
    /// 3D fractal noise interface definition.
    /// </summary>
    public interface IFractalNoise3D
    {
        /// <summary>
        /// Samples the noise at the specified x, y, z position.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        /// <returns>The noise value.</returns>
        double Sample(double x, double y, double z);
    }
}
