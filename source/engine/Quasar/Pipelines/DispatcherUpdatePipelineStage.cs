//-----------------------------------------------------------------------
// <copyright file="DispatcherUpdatePipelineStage.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Space.Core.DependencyInjection;
using Space.Core.Threading;

namespace Quasar.Pipelines
{
    /// <summary>
    /// Update pipeline's dispatcher stage implementation.
    /// </summary>
    /// <seealso cref="UpdatePipelineStageBase" />
    [Export(typeof(UpdatePipelineStageBase), nameof(DispatcherUpdatePipelineStage))]
    [ExecuteAfter(typeof(MessageHandlerUpdatePipelineStage))]
    [Singleton]
    public sealed class DispatcherUpdatePipelineStage : UpdatePipelineStageBase
    {
        private const int DispatcherExecutionTimeoutMs = 20;


        private readonly IDispatcherService dispatcherService;


        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherUpdatePipelineStage" /> class.
        /// </summary>
        /// <param name="dispatcherService">The dispatcher service.</param>
        internal DispatcherUpdatePipelineStage(IDispatcherService dispatcherService)
        {
            this.dispatcherService = dispatcherService;
        }


        /// <inheritdoc/>
        protected override void OnExecute()
        {
            dispatcherService.ExecuteDispatchedTasks(DispatcherExecutionTimeoutMs);
        }

        /// <inheritdoc/>
        protected override void OnStart()
        {
            dispatcherService.Start();
        }

        /// <inheritdoc/>
        protected override void OnShutdown()
        {
            dispatcherService.Stop();
        }
    }
}
