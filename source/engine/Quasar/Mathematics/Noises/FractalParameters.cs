//-----------------------------------------------------------------------
// <copyright file="FractalParameters.cs" company="Space Development">
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
    /// Generic noise fractal parameters.
    /// </summary>
    public struct FractalParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FractalParameters"/> struct.
        /// </summary>
        /// <param name="octaves">The number of octaves.</param>
        /// <param name="seed">The seed value.</param>
        /// <param name="frequency">The frequency value.</param>
        /// <param name="amplitude">The amplitude value.</param>
        /// <param name="lacunarity">The lacunarity value.</param>
        /// <param name="persistance">The persistance value.</param>
        public FractalParameters(
            int octaves,
            long seed,
            double frequency,
            double amplitude,
            double lacunarity = 2.0,
            double persistance = 0.5)
        {
            Octaves = octaves;
            Seed = seed;
            Frequency = frequency;
            Amplitude = amplitude;
            Lacunarity = lacunarity;
            Persistance = persistance;
        }


        /// <summary>
        /// The amplitude.
        /// </summary>
        public readonly double Amplitude;

        /// <summary>
        /// The frequency value.
        /// </summary>
        public readonly double Frequency;

        /// <summary>
        /// The lacunarity value.
        /// </summary>
        public readonly double Lacunarity;

        /// <summary>
        /// The number of octaves.
        /// </summary>
        public readonly int Octaves;

        /// <summary>
        /// The persistance value.
        /// </summary>
        public readonly double Persistance;

        /// <summary>
        /// The seed value.
        /// </summary>
        public long Seed;
    }
}
