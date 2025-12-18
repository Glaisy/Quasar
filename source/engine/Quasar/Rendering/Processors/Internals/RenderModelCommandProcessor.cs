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

using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.Rendering.Internals;
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
                    renderModel.State.RenderingLayer = renderingLayerService[command.Layer];
                    renderModel.State.IsRenderable = false;
                    renderModel.State.IsDoubleSided = false;
                    renderModel.State.IsEnabled = command.Value;
                    break;

                case RenderModelCommandType.DoubleSidedChanged:
                    HandleDoubleSidedChangedCommand(renderModel, command.Value);
                    break;

                case RenderModelCommandType.EnabledChanged:
                    HandleEnabledChangedCommand(renderModel, command.Value);
                    break;

                case RenderModelCommandType.LayerChanged:
                    var renderingLayer = renderingLayerService[command.Layer];
                    HandleLayerChangedCommand(renderModel, renderingLayer);
                    break;

                case RenderModelCommandType.MeshChanged:
                    HandleMeshChangedCommand(renderModel, command.Mesh, command.Value);
                    break;

                case RenderModelCommandType.ShaderChanged:
                    HandleShaderChangedCommand(renderModel, command.Shader);
                    break;

                default:
                    throw new NotSupportedException(command.Type.ToString());
            }
        }


        private static void HandleDoubleSidedChangedCommand(RenderModel renderModel, bool newValue)
        {
            renderModel.State.IsDoubleSided = newValue;
            if (!renderModel.State.IsRenderable)
            {
                return;
            }

            var renderBatch = renderModel.State.RenderBatch;
            renderBatch.MoveModel(renderModel);
        }

        private static void HandleEnabledChangedCommand(RenderModel renderModel, bool newValue)
        {
            renderModel.State.IsEnabled = newValue;
            if (!renderModel.State.IsRenderable)
            {
                return;
            }

            // enable
            var renderBatch = renderModel.State.RenderBatch;
            if (newValue)
            {
                renderBatch.AddModel(renderModel);
                return;
            }

            // disable
            renderBatch.RemoveModel(renderModel);
        }

        private static void HandleLayerChangedCommand(RenderModel renderModel, RenderingLayer newValue)
        {
            var oldValue = renderModel.State.RenderingLayer;
            renderModel.State.RenderingLayer = newValue;
            if (!renderModel.State.IsRenderable ||
                !renderModel.State.IsEnabled)
            {
                return;
            }

            renderModel.State.RenderBatch.RemoveModel(renderModel);
            renderModel.State.RenderBatch = newValue.GetRenderBatch(renderModel.State.Shader);
            renderModel.State.RenderBatch.AddModel(renderModel);
        }

        private static void HandleMeshChangedCommand(RenderModel renderModel, IMesh newValue, bool newSharedMeshValue)
        {
            if (renderModel.State.Mesh != null &&
                !renderModel.State.SharedMesh)
            {
                renderModel.State.Mesh.Dispose();
            }

            renderModel.State.Mesh = newValue;
            renderModel.State.SharedMesh = newSharedMeshValue;

            if (newValue == null)
            {
                if (renderModel.State.IsRenderable &&
                    renderModel.State.IsEnabled)
                {
                    renderModel.State.RenderBatch.RemoveModel(renderModel);
                    renderModel.State.IsRenderable = false;
                }

                return;
            }

            if (renderModel.State.IsRenderable ||
                !renderModel.State.IsEnabled ||
                !IsRenderable(renderModel.State))
            {
                return;
            }

            var shader = renderModel.State.Shader;
            renderModel.State.RenderBatch = renderModel.State.RenderingLayer.GetRenderBatch(shader);
            renderModel.State.RenderBatch.AddModel(renderModel);
            renderModel.State.IsRenderable = true;
        }

        private static void HandleShaderChangedCommand(RenderModel renderModel, ShaderBase newValue)
        {
            renderModel.State.Shader = newValue;

            var isActivated = renderModel.State.IsEnabled && renderModel.State.IsRenderable;
            if (renderModel.State.RenderBatch != null && isActivated)
            {
                renderModel.State.RenderBatch.RemoveModel(renderModel);
            }

            renderModel.State.RenderBatch = renderModel.State.RenderingLayer.GetRenderBatch(newValue);
            if (!isActivated)
            {
                return;
            }

            renderModel.State.RenderBatch.AddModel(renderModel);
        }

        private static bool IsRenderable(in RenderModelState renderModelState)
        {
            return renderModelState.RenderBatch != null && renderModelState.Mesh != null;
        }
    }
}
