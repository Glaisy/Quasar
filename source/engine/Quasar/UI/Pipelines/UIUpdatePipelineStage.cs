//-----------------------------------------------------------------------
// <copyright file="UIUpdatePipelineStage.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Audio.Internals.Pipeline;
using Quasar.Pipelines;
using Quasar.Rendering.Pipelines;
using Quasar.UI.Internals;

using Space.Core.DependencyInjection;

namespace Quasar.UI.Pipelines
{
    /// <summary>
    /// Update pipeline's UI update stage implementation.
    /// </summary>
    /// <seealso cref="RenderingPipelineStageBase" />
    [Export(typeof(UpdatePipelineStageBase), nameof(UIUpdatePipelineStage))]
    [ExecuteAfter(typeof(DispatcherUpdatePipelineStage))]
    [ExecuteBefore(typeof(AudioUpdatePipelineStage))]
    public sealed class UIUpdatePipelineStage : UpdatePipelineStageBase
    {
        private IUIEventProcessor uiEventProcessor;


        /// <summary>
        /// Initializes a new instance of the <see cref="UIUpdatePipelineStage" /> class.
        /// </summary>
        /// <param name="uiEventProcessor">The UI event processor.</param>
        internal UIUpdatePipelineStage(IUIEventProcessor uiEventProcessor)
        {
            this.uiEventProcessor = uiEventProcessor;
        }


        /// <inheritdoc/>
        protected override void OnExecute()
        {
            uiEventProcessor.ProcessUpdateEvent();
        }
    }
}
