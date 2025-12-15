//-----------------------------------------------------------------------
// <copyright file="PhysicsPipelineStageBase.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Runtime.CompilerServices;

using Quasar.Pipelines;

namespace Quasar.Physics.Pipelines
{
    /// <summary>
    /// Represents an abstract base class for Quasar physics pipeline's processing stages.
    /// </summary>
    /// <seealso cref="PipelineStageBase{IPhysicsContext}" />
    public abstract class PhysicsPipelineStageBase : PipelineStageBase<IPhysicsContext>
    {
        /// <summary>
        /// Applies the physics settings for the current stage.
        /// </summary>
        /// <param name="physicsSettings">The physics settings.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ApplySettings(IPhysicsSettings physicsSettings)
        {
            OnApplySettings(physicsSettings);
        }


        /// <summary>
        /// Apply physics settings event handler.
        /// </summary>
        /// <param name="physicsSettings">The physics settings.</param>
        protected virtual void OnApplySettings(IPhysicsSettings physicsSettings)
        {
        }
    }
}
