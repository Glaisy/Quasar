//-----------------------------------------------------------------------
// <copyright file="RenderingPipelineStageBase.cs" company="Space Development">
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

namespace Quasar.Rendering.Pipeline
{
    /// <summary>
    /// Represents an abstract base class for Quasar rendering pipeline's processing stages.
    /// </summary>
    /// <seealso cref="PipelineStageBase" />
    public abstract class RenderingPipelineStageBase : PipelineStageBase
    {
        /// <summary>
        /// Applies the rendering settings for the current stage.
        /// </summary>
        /// <param name="settings">The settings.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ApplySettings(IRenderingSettings settings)
        {
            OnApplySettings(settings);
        }

        /// <summary>
        /// Executes the rendering stage for the current frame.
        /// </summary>
        /// <param name="renderingContext">The rendering context.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Execute(IRenderingContext renderingContext)
        {
            OnExecute(renderingContext);
        }


        /// <summary>
        /// Apply rendering settings event handler.
        /// </summary>
        /// <param name="settings">The settings.</param>
        protected virtual void OnApplySettings(IRenderingSettings settings)
        {
        }

        /// <summary>
        /// Execute event handler.
        /// </summary>
        /// <param name="renderingContext">The rendering context.</param>
        protected abstract void OnExecute(IRenderingContext renderingContext);
    }
}
