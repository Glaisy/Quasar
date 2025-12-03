//-----------------------------------------------------------------------
// <copyright file="IRigidBody.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Physics
{
    /// <summary>
    /// Represents the basic properties if a rigid body object in the physics world.
    /// </summary>
    public interface IRigidBody
    {
        /// <summary>
        /// Gets the mass.
        /// </summary>
        double Mass { get; }

        /// <summary>
        /// Gets the position.
        /// </summary>
        Vector3D Position { get; }
    }
}
