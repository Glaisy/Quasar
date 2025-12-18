//-----------------------------------------------------------------------
// <copyright file="RenderModelCommandProcessor.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Quasar.Rendering.Internals.Services;

using Space.Core.DependencyInjection;

namespace Quasar.Rendering.Processors.Internals
{
    /// <summary>
    /// Render model command processor implementation.
    /// </summary>
    /// <seealso cref="RenderCommandProcessorBase{RenderModelCommand}" />
    [Export(typeof(RenderCommandProcessorBase), nameof(RenderModelCommand))]
    [Export]
    [Singleton]
    internal sealed class RenderModelCommandProcessor : RenderCommandProcessorBase<RenderModelCommand>
    {
        private readonly RenderingLayerService renderingLayerService;


        /// <summary>
        /// Initializes a new instance of the <see cref="RenderModelCommandProcessor"/> class.
        /// </summary>
        /// <param name="renderingLayerService">The rendering layer service.</param>
        public RenderModelCommandProcessor(RenderingLayerService renderingLayerService)
        {
            this.renderingLayerService = renderingLayerService;
        }


        /// <inheritdoc/>
        protected override void OnExecuteCommand(in RenderModelCommand command)
        {
            var renderModel = command.RenderModel;
            switch (command.Type)
            {
                case RenderModelCommandType.Create:
                    break;

                case RenderModelCommandType.DoubleSidedChanged:
                    break;

                case RenderModelCommandType.EnabledChanged:
                    break;

                case RenderModelCommandType.LayerChanged:
                    break;

                case RenderModelCommandType.MeshChanged:
                    break;

                case RenderModelCommandType.ShaderChanged:
                    break;

                default:
                    throw new NotSupportedException(command.Type.ToString());
            }
        }
    }
}
