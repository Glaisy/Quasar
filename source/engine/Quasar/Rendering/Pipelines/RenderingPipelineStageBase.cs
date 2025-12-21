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

using Quasar.Graphics;
using Quasar.Pipelines;

namespace Quasar.Rendering.Pipelines
{
    /// <summary>
    /// Represents an abstract base class for Quasar rendering pipeline's processing stages.
    /// </summary>
    /// <seealso cref="PipelineStageBase{IRenderingContext}" />
    public abstract class RenderingPipelineStageBase : PipelineStageBase<IRenderingContext>
    {
        /// <summary>
        /// Invokes the apply settings event handler for the current stage.
        /// </summary>
        /// <param name="renderingSettings">The rendering settings.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void InvokeApplySettings(IRenderingSettings renderingSettings)
        {
            OnApplySettings(renderingSettings);
        }

        /// <summary>
        /// Invokes the render surface size changed event handler for the current stage.
        /// </summary>
        /// <param name="size">The size.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void InvokeSizeChanged(in Size size)
        {
            OnSizeChanged(size);
        }


        /// <summary>
        /// Apply rendering settings event handler.
        /// </summary>
        /// <param name="renderingSettings">The rendering settings.</param>
        protected virtual void OnApplySettings(IRenderingSettings renderingSettings)
        {
        }

        /// <summary>
        /// Rendering surface size changed event handler.
        /// </summary>
        /// <param name="size">The size.</param>
        protected virtual void OnSizeChanged(in Size size)
        {
        }
    }
}
