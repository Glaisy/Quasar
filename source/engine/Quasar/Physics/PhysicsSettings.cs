//-----------------------------------------------------------------------
// <copyright file="PhysicsSettings.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Quasar.Settings;

using Space.Core;
using Space.Core.Settings;

namespace Quasar.Physics
{
    /// <summary>
    /// Quasar engine's physics settings implementation.
    /// </summary>
    /// <seealso cref="SettingsBase{IPhysicsSettings}" />
    /// <seealso cref="IPhysicsSettings" />
    [Settings(typeof(IPhysicsSettings))]
    public sealed class PhysicsSettings : SettingsBase<IPhysicsSettings>, IPhysicsSettings
    {
        private const float DefaultTimeStepMs = 0.04f;
        private static readonly Range<float> physicsTimeStepRange = new Range<float>(0.01f, Single.PositiveInfinity);


        /// <summary>
        /// The defaults.
        /// </summary>
        public static readonly IPhysicsSettings Defaults = new PhysicsSettings
        {
            TimeStepMs = DefaultTimeStepMs
        };

        private float timeStepMs = DefaultTimeStepMs;
        /// <summary>
        /// Gets or sets the physics simulation's time step [0.01...+Inf] [ms].
        /// </summary>
        public float TimeStepMs
        {
            get => timeStepMs;
            set => timeStepMs = physicsTimeStepRange.Clamp(value);
        }


        /// <inheritdoc/>
        public override void SetDefaults()
        {
            Copy(Defaults);
        }


        /// <inheritdoc/>
        protected override void CopyProperties(IPhysicsSettings source)
        {
            TimeStepMs = source.TimeStepMs;
        }
    }
}
