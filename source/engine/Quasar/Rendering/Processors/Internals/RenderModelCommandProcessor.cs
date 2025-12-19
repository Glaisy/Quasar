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
using Quasar.Rendering.Internals;
using Quasar.Rendering.Internals.Services;

using Space.Core;
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
            RenderingLayer renderingLayer;
            var renderModel = command.RenderModel;
            switch (command.Type)
            {
                case RenderModelCommandType.Create:
                    renderingLayer = renderingLayerService[command.Layer];
                    HandleCreateCommand(ref renderModel.State, renderingLayer, command.Value);
                    break;

                case RenderModelCommandType.DoubleSidedChanged:
                    HandleDoubleSidedChangedCommand(renderModel, command.Value);
                    break;

                case RenderModelCommandType.EnabledChanged:
                    HandleEnabledChangedCommand(renderModel, command.Value);
                    break;

                case RenderModelCommandType.LayerChanged:
                    renderingLayer = renderingLayerService[command.Layer];
                    HandleLayerChangedCommand(renderModel, renderingLayer);
                    break;

                case RenderModelCommandType.MaterialChanged:
                    HandleMaterialChangedCommand(renderModel, command.Material);
                    break;

                case RenderModelCommandType.MeshChanged:
                    HandleMeshChangedCommand(renderModel, command.Mesh, command.Value);
                    break;

                default:
                    throw new NotSupportedException(command.Type.ToString());
            }
        }


        private static void HandleCreateCommand(ref RenderModelState renderModelState, RenderingLayer renderingLayer, bool isEnabled)
        {
            renderModelState.RenderingLayer = renderingLayer;
            renderModelState.Flags = isEnabled ? RenderModelStateFlags.Enabled : RenderModelStateFlags.None;
        }

        private static void HandleDoubleSidedChangedCommand(RenderModel renderModel, bool newValue)
        {
            if (newValue)
            {
                renderModel.State.Flags |= RenderModelStateFlags.DoubleSided;
            }
            else
            {
                renderModel.State.Flags &= ~RenderModelStateFlags.DoubleSided;
            }

            if (!renderModel.State.IsActive)
            {
                return;
            }

            var renderBatch = renderModel.State.RenderBatch;
            renderBatch.MoveModel(renderModel);
        }

        private static void HandleEnabledChangedCommand(RenderModel renderModel, bool newValue)
        {
            if (newValue)
            {
                renderModel.State.Flags |= RenderModelStateFlags.Enabled;
            }
            else
            {
                renderModel.State.Flags &= ~RenderModelStateFlags.Enabled;
            }

            if (!renderModel.State.IsRenderable)
            {
                return;
            }

            // enable
            if (renderModel.State.RenderBatch == null)
            {
                var shader = renderModel.State.Material.GetShader();
                renderModel.State.RenderBatch = renderModel.State.RenderingLayer.GetRenderBatch(shader);
            }

            if (newValue)
            {
                renderModel.State.RenderBatch.AddModel(renderModel);
                return;
            }

            // disable
            renderModel.State.RenderBatch.RemoveModel(renderModel);
        }

        private static void HandleLayerChangedCommand(RenderModel renderModel, RenderingLayer newValue)
        {
            renderModel.State.RenderingLayer = newValue;
            if (!renderModel.State.IsActive)
            {
                return;
            }

            renderModel.State.RenderBatch.RemoveModel(renderModel);
            var shader = renderModel.State.Material.GetShader();
            renderModel.State.RenderBatch = newValue.GetRenderBatch(shader);
            renderModel.State.RenderBatch.AddModel(renderModel);
        }

        private static void HandleMaterialChangedCommand(RenderModel renderModel, Material newValue)
        {
            var oldShader = renderModel.State.Material?.GetShader();
            var newShader = newValue?.GetShader();
            renderModel.State.Material = newValue;

            if (oldShader != null &&
               newShader == null)
            {
                renderModel.State.Flags &= ~RenderModelStateFlags.Renderable;
                if (renderModel.State.IsEnabled)
                {
                    renderModel.State.RenderBatch.RemoveModel(renderModel);
                }

                return;
            }

            if (!renderModel.State.IsRenderableByProperties)
            {
                return;
            }

            renderModel.State.Flags |= RenderModelStateFlags.Renderable;
            if (!renderModel.State.IsEnabled)
            {
                return;
            }

            if (oldShader != null)
            {
                if (oldShader != newShader)
                {
                    renderModel.State.RenderBatch.RemoveModel(renderModel);
                }
            }

            renderModel.State.RenderBatch = renderModel.State.RenderingLayer.GetRenderBatch(newShader);
            renderModel.State.RenderBatch.AddModel(renderModel);
        }

        private static void HandleMeshChangedCommand(RenderModel renderModel, IMesh newValue, bool newSharedMeshValue)
        {
            if (renderModel.State.Mesh != null &&
                !renderModel.State.Flags.HasFlag(RenderModelStateFlags.SharedMesh))
            {
                renderModel.State.Mesh.Dispose();
            }

            renderModel.State.Mesh = newValue;
            if (newSharedMeshValue)
            {
                renderModel.State.Flags |= RenderModelStateFlags.SharedMesh;
            }
            else
            {
                renderModel.State.Flags &= ~RenderModelStateFlags.SharedMesh;
            }

            if (newValue == null)
            {
                if (renderModel.State.IsActive)
                {
                    renderModel.State.RenderBatch.RemoveModel(renderModel);
                    renderModel.State.Flags &= ~RenderModelStateFlags.Renderable;
                }

                return;
            }

            if (renderModel.State.IsRenderable ||
                !renderModel.State.IsRenderableByProperties ||
                !renderModel.State.Flags.HasFlag(RenderModelStateFlags.Enabled))
            {
                // already renderable or not renderable or not enabled => we do nothing
                return;
            }

            Assertion.ThrowIfNotEqual(renderModel.State.RenderBatch, null, "RenderBatch should have been null at this point.");
            var shader = renderModel.State.Material.GetShader();
            renderModel.State.RenderBatch = renderModel.State.RenderingLayer.GetRenderBatch(shader);
            renderModel.State.RenderBatch.AddModel(renderModel);
            renderModel.State.Flags |= RenderModelStateFlags.Renderable;
        }
    }
}
