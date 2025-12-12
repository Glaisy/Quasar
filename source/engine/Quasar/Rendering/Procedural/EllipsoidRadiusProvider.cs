//-----------------------------------------------------------------------
// <copyright file="EllipsoidRadiusProvider.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Rendering.Procedural
{
    /// <summary>
    /// Ellipsoid radius provider implementation.
    /// </summary>
    /// <seealso cref="IRadiusProvider" />
    public sealed class EllipsoidRadiusProvider : IRadiusProvider
    {
        private readonly Vector3 radiuses;
        private readonly Vector3D radiusesD;


        /// <summary>
        /// Initializes a new instance of the <see cref="EllipsoidRadiusProvider"/> class.
        /// </summary>
        /// <param name="radiuses">The radiuses.</param>
        public EllipsoidRadiusProvider(in Vector3D radiuses)
        {
            this.radiuses = radiuses;
            radiusesD = radiuses;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipsoidRadiusProvider"/> class.
        /// </summary>
        /// <param name="radiuses">The radiuses.</param>
        public EllipsoidRadiusProvider(in Vector3 radiuses)
        {
            this.radiuses = radiuses;
            radiusesD = radiuses;
        }


        /// <inheritdoc/>
        public float GetRadius(in Vector3 direction)
        {
            return (direction * radiuses).Length;
        }

        /// <inheritdoc/>
        public double GetRadius(in Vector3D direction)
        {
            return (direction * radiusesD).Length;
        }

        /// <inheritdoc/>
        public Vector3 GetRadiusVector(in Vector3 direction)
        {
            return direction * radiuses;
        }

        /// <inheritdoc/>
        public Vector3D GetRadiusVector(in Vector3D direction)
        {
            return direction * radiusesD;
        }
    }
}
