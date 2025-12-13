//-----------------------------------------------------------------------
// <copyright file="ALEnumExtensions.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Runtime.CompilerServices;

using Quasar.Audio;
using Quasar.OpenAL.Api;

namespace Quasar.OpenAL.Extensions
{
    /// <summary>
    /// OpenAL enumeration type extensions.
    /// </summary>
    internal static class ALEnumExtensions
    {
        private static readonly DistanceModel[] distanceModels = new DistanceModel[]
        {
            DistanceModel.None,
            DistanceModel.Exponential,
            DistanceModel.ExponentialClamped,
            DistanceModel.Linear,
            DistanceModel.LinearClamped
        };


        /// <summary>
        /// Converts the attenuation type to distance model.
        /// </summary>
        /// <param name="attenuationType">Type of the attenuation.</param>
        /// <returns>The distance model.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DistanceModel ToDistanceModel(this AttenuationType attenuationType)
        {
            return distanceModels[(int)attenuationType];
        }
    }
}
