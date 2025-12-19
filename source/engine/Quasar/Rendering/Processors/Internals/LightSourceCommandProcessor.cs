//-----------------------------------------------------------------------
// <copyright file="LightSourceCommandProcessor.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Space.Core.DependencyInjection;

namespace Quasar.Rendering.Processors.Internals
{
    /// <summary>
    /// Light source command processor implementation.
    /// </summary>
    [Export(typeof(RenderCommandProcessorBase), nameof(LightSourceCommand))]
    [Export]
    [Singleton]
    internal class LightSourceCommandProcessor : RenderCommandProcessorBase<LightSourceCommand>
    {
        ////private readonly LightSourceService lightSourceService;


        /////// <summary>
        /////// Initializes a new instance of the <see cref="LightSourceCommandProcessor" /> class.
        /////// </summary>
        /////// <param name="lightSourceService">The light source service.</param>
        ////public LightSourceCommandProcessor(LightSourceService lightSourceService)
        ////{
        ////    this.lightSourceService = lightSourceService;
        ////}

        /// <inheritdoc/>
        protected override void OnExecuteCommand(in LightSourceCommand command)
        {
            switch (command.Type)
            {
                case LightSourceCommandType.Create:
                case LightSourceCommandType.EnabledChanged:
                    ////if (command.Enabled)
                    ////{
                    ////    lightSourceService.Add(command.LightSource);
                    ////    shadowMapService.Attach(command.LightSource);
                    ////}
                    ////else
                    ////{
                    ////    lightSourceService.Remove(command.LightSource);
                    ////    shadowMapService.Detach(command.LightSource);
                    ////}

                    break;

                default:
                    throw new NotSupportedException(command.Type.ToString());
            }
        }
    }
}
