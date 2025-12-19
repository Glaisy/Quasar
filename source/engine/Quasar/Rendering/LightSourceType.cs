//-----------------------------------------------------------------------
// <copyright file="LightSourceType.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Rendering
{
    /// <summary>
    /// Light source type enumeration.
    /// </summary>
    public enum LightSourceType
    {
        /// <summary>
        /// Directional light source type.
        /// </summary>
        Directional,

        /// <summary>
        /// Point light source type.
        /// </summary>
        Point,

        /// <summary>
        /// Spot light source type.
        /// </summary>
        Spot
    }
}
