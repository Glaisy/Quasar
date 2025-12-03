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

namespace Quasar.Physics.Pipeline
{
    /// <summary>
    /// Represents an abstract base class for physics pipeline's processing stages.
    /// </summary>
    public abstract class PhysicsPipelineStageBase
    {
        /// <summary>
        /// Execute event handler.
        /// </summary>
        protected abstract void OnExecute();
    }
}
