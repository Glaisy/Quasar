//-----------------------------------------------------------------------
// <copyright file="IRadiusProvider.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Rendering.Procedurals
{
    /// <summary>
    /// Represents a radius provider component for sphere like 3D object.
    /// </summary>
    public interface IRadiusProvider
    {
        /// <summary>
        /// Gets the radius in the specified direction.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <returns>The radius value.</returns>
        float GetRadius(in Vector3 direction);

        /// <summary>
        /// Gets the radius in the specified direction.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <returns>The radius value.</returns>
        double GetRadius(in Vector3D direction);

        /// <summary>
        /// Gets the radius vector in the specified direction.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <returns>
        /// The radius vector value.
        /// </returns>
        Vector3 GetRadiusVector(in Vector3 direction);

        /// <summary>
        /// Gets the radius vector in the specified direction.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <returns>
        /// The radius vector value.
        /// </returns>
        Vector3D GetRadiusVector(in Vector3D direction);
    }
}
