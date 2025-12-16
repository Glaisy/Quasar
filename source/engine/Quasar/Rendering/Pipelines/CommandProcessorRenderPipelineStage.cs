//-----------------------------------------------------------------------
// <copyright file="CommandProcessorRenderPipelineStage.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using Quasar.Pipelines;
using Quasar.Rendering.Processors;

using Space.Core.DependencyInjection;

namespace Quasar.Rendering.Pipelines
{
    /// <summary>
    /// Command procoessor render pipeline stage implementation.
    /// </summary>
    /// <seealso cref="RenderingPipelineStageBase" />
    [Export(typeof(RenderingPipelineStageBase), nameof(CommandProcessorRenderPipelineStage))]
    [Singleton]
    [ExecuteAfter(typeof(InitializeRenderingPipelineStage))]
    [ExecuteBefore(typeof(ClearFrameRenderingPipelineStage))]
    public sealed class CommandProcessorRenderPipelineStage : RenderingPipelineStageBase
    {
        private readonly IEnumerable<RenderCommandProcessorBase> commandProcessors;


        /// <summary>
        /// Initializes a new instance of the <see cref="CommandProcessorRenderPipelineStage" /> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        internal CommandProcessorRenderPipelineStage(IServiceProvider serviceProvider)
        {
            commandProcessors = serviceProvider.GetServices<RenderCommandProcessorBase>();
        }


        /// <inheritdoc/>
        protected override void OnExecute()
        {
            foreach (var commandProcessor in commandProcessors)
            {
                commandProcessor.ExecuteCommands();
            }
        }

        /// <inheritdoc/>
        protected override void OnStart()
        {
            foreach (var commandProcessor in commandProcessors)
            {
                commandProcessor.Start();
            }
        }

        /// <inheritdoc/>
        protected override void OnShutdown()
        {
            foreach (var commandProcessor in commandProcessors)
            {
                commandProcessor.Shutdown();
            }
        }
    }
}
