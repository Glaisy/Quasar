//-----------------------------------------------------------------------
// <copyright file="PipelineStageBase.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Runtime.CompilerServices;

namespace Quasar.Pipelines
{
    /// <summary>
    /// Represents an abstract base class for Quasar pipeline's processing stages.
    /// </summary>
    public abstract class PipelineStageBase
    {
        /// <summary>
        /// Starts the pipeline stage.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Start()
        {
            OnStart();
        }

        /// <summary>
        /// Shuts down the pipeline stage.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Shutdown()
        {
            OnShutdown();
        }


        /// <summary>
        /// Start event handler.
        /// </summary>
        protected virtual void OnStart()
        {
        }

        /// <summary>
        /// Shutdown event handler.
        /// </summary>
        protected virtual void OnShutdown()
        {
        }
    }
}
