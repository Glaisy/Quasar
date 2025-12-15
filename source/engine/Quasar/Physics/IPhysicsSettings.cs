//-----------------------------------------------------------------------
// <copyright file="IPhysicsSettings.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Space.Core.Settings;

namespace Quasar.Physics
{
    /// <summary>
    /// Represents the Quasar engine's physics settings.
    /// </summary>
    /// <seealso cref="ISettings" />
    public interface IPhysicsSettings : ISettings
    {
        /// <summary>
        /// Gets the physics simulation's time step [0.01...+Inf] [ms].
        /// </summary>
        float TimeStepMs { get; }
    }
}
