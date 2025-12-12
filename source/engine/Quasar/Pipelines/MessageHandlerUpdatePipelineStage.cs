//-----------------------------------------------------------------------
// <copyright file="MessageHandlerUpdatePipelineStage.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.UI.Internals;

using Space.Core.DependencyInjection;

namespace Quasar.Pipelines
{
    /// <summary>
    /// Update pipeline's message handler stage implementation.
    /// </summary>
    /// <seealso cref="UpdatePipelineStageBase" />
    [Export(typeof(UpdatePipelineStageBase), nameof(MessageHandlerUpdatePipelineStage))]
    [Singleton]
    public sealed class MessageHandlerUpdatePipelineStage : UpdatePipelineStageBase
    {
        private readonly INativeMessageHandler nativeMessageHandler;


        /// <summary>
        /// Initializes a new instance of the <see cref="MessageHandlerUpdatePipelineStage"/> class.
        /// </summary>
        /// <param name="nativeMessageHandler">The native message handler.</param>
        internal MessageHandlerUpdatePipelineStage(INativeMessageHandler nativeMessageHandler)
        {
            this.nativeMessageHandler = nativeMessageHandler;
        }


        /// <inheritdoc/>
        protected override void OnExecute()
        {
            nativeMessageHandler.ProcessMessages();
        }
    }
}
