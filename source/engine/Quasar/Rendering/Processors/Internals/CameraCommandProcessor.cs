//-----------------------------------------------------------------------
// <copyright file="CameraCommandProcessor.cs" company="Space Development">
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
    /// Camera command processor implementation.
    /// </summary>
    /// <seealso cref="RenderCommandProcessorBase{CameraCommand}" />
    [Export(typeof(RenderCommandProcessorBase), nameof(CameraCommand))]
    [Singleton]
    internal sealed class CameraCommandProcessor : RenderCommandProcessorBase<CameraCommand>
    {
        private CameraService cameraService;


        /// <summary>
        /// Initializes a new instance of the <see cref="CameraCommandProcessor"/> class.
        /// </summary>
        /// <param name="cameraService">The camera service.</param>
        public CameraCommandProcessor(CameraService cameraService)
        {
            this.cameraService = cameraService;
        }


        /// <inheritdoc/>
        protected override CameraCommand CreateResetCommand()
        {
            return new CameraCommand(null, CameraCommandType.Reset);
        }

        /// <summary>
        /// Command execution event handler.
        /// </summary>
        /// <param name="command">The command.</param>
        protected override void OnExecuteCommand(in CameraCommand command)
        {
            switch (command.Type)
            {
                case CameraCommandType.Reset:
                    cameraService.Clear();
                    break;

                case CameraCommandType.EnabledChanged:
                    if (command.Enabled)
                    {
                        cameraService.Activate(command.Camera);
                    }
                    else
                    {
                        cameraService.Deactive(command.Camera);
                    }

                    break;

                default:
                    throw new NotSupportedException(command.Type.ToString());
            }
        }

        /// <summary>
        /// Start event handler.
        /// </summary>
        protected override void OnStart()
        {
            Camera.CommandProcessor = this;
        }

        /// <summary>
        /// Shutdown event handler.
        /// </summary>
        protected override void OnShutdown()
        {
            Camera.CommandProcessor = null;
        }
    }
}
