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
            renderModelState.UpdateFlags(RenderModelStateFlags.Enabled, isEnabled);
        }

        private static void HandleDoubleSidedChangedCommand(RenderModel renderModel, bool newValue)
        {
            renderModel.State.UpdateFlags(RenderModelStateFlags.DoubleSided, newValue);

            if (!renderModel.State.IsRendered)
            {
                return;
            }

            renderModel.State.RenderBatch.MoveModel(renderModel);
        }

        private static void HandleEnabledChangedCommand(RenderModel renderModel, bool newValue)
        {
            // enabling?
            if (newValue)
            {
                Assertion.ThrowIfEqual(renderModel.State.IsRendered, true, nameof(RenderModelState.IsRendered));
                Assertion.ThrowIfEqual(renderModel.State.RenderBatch != null, true, nameof(RenderModelState.RenderBatch));

                renderModel.State.UpdateFlags(RenderModelStateFlags.Enabled, newValue);
                if (renderModel.State.IsRenderableByProperties)
                {
                    var shader = renderModel.State.Material.GetShader();
                    renderModel.State.RenderBatch = renderModel.State.RenderingLayer.GetRenderBatch(shader);
                    renderModel.State.RenderBatch.AddModel(renderModel);

                    Assertion.ThrowIfEqual(renderModel.State.IsRendered, false, nameof(RenderModelState.IsRendered));
                    Assertion.ThrowIfEqual(renderModel.State.RenderBatch != null, false, nameof(RenderModelState.RenderBatch));
                }
#if DEBUG
                else
                {
                    Assertion.ThrowIfEqual(renderModel.State.IsRendered, true, nameof(RenderModelState.IsRendered));
                    Assertion.ThrowIfEqual(renderModel.State.RenderBatch != null, true, nameof(RenderModelState.RenderBatch));
                }
#endif
                return;
            }

            // disabling
            if (renderModel.State.IsRendered)
            {
                renderModel.State.RenderBatch.RemoveModel(renderModel);
            }

            renderModel.State.UpdateFlags(RenderModelStateFlags.Enabled, newValue);

            Assertion.ThrowIfEqual(renderModel.State.IsRendered, true, nameof(RenderModelState.IsRendered));
            Assertion.ThrowIfEqual(renderModel.State.RenderBatch != null, true, nameof(RenderModelState.RenderBatch));
        }

        private static void HandleLayerChangedCommand(RenderModel renderModel, RenderingLayer newValue)
        {
            renderModel.State.RenderingLayer = newValue;
            if (renderModel.State.RenderBatch == null)
            {
                return;
            }

            renderModel.State.RenderBatch.RemoveModel(renderModel);
            var shader = renderModel.State.Material.GetShader();
            renderModel.State.RenderBatch = newValue.GetRenderBatch(shader);
            renderModel.State.RenderBatch.AddModel(renderModel);

            Assertion.ThrowIfEqual(renderModel.State.IsRendered, false, nameof(RenderModelState.IsRendered));
            Assertion.ThrowIfEqual(renderModel.State.RenderBatch != null, false, nameof(RenderModelState.RenderBatch));
        }

        private static void HandleMaterialChangedCommand(RenderModel renderModel, Material newValue)
        {
            // remove old material
            if (renderModel.State.Material != null)
            {
                if (renderModel.State.IsRendered)
                {
                    renderModel.State.RenderBatch.RemoveModel(renderModel);
                }

                renderModel.State.Material = null;

                Assertion.ThrowIfEqual(renderModel.State.IsRendered, true, nameof(RenderModelState.IsRendered));
                Assertion.ThrowIfEqual(renderModel.State.RenderBatch != null, true, nameof(RenderModelState.RenderBatch));
            }

            // update state & material
            renderModel.State.Material = newValue;

            if (!renderModel.State.IsRenderableByProperties)
            {
                Assertion.ThrowIfEqual(renderModel.State.RenderBatch != null, true, nameof(RenderModelState.RenderBatch));
                Assertion.ThrowIfEqual(renderModel.State.IsRendered, true, nameof(RenderModelState.IsRendered));
                return;
            }

            var shader = newValue.GetShader();
            renderModel.State.RenderBatch = renderModel.State.RenderingLayer.GetRenderBatch(shader);
            renderModel.State.RenderBatch.AddModel(renderModel);

            Assertion.ThrowIfEqual(renderModel.State.RenderBatch != null, false, nameof(RenderModelState.RenderBatch));
            Assertion.ThrowIfEqual(renderModel.State.IsRendered, false, nameof(RenderModelState.IsRendered));
        }

        private static void HandleMeshChangedCommand(RenderModel renderModel, IMesh newValue, bool newSharedMeshValue)
        {
            // remove existing mesh
            if (renderModel.State.Mesh != null)
            {
                if (renderModel.State.IsRendered)
                {
                    renderModel.State.RenderBatch.RemoveModel(renderModel);
                }

                if (!renderModel.State.Flags.HasFlag(RenderModelStateFlags.SharedMesh))
                {
                    renderModel.State.Mesh.Dispose();
                }

                Assertion.ThrowIfEqual(renderModel.State.IsRendered, true, nameof(RenderModelState.IsRendered));
            }

            Assertion.ThrowIfEqual(renderModel.State.RenderBatch != null, true, nameof(RenderModelState.RenderBatch));

            // update state & mesh
            renderModel.State.Mesh = newValue;
            renderModel.State.UpdateFlags(RenderModelStateFlags.SharedMesh, newSharedMeshValue);

            if (newValue == null ||
                !renderModel.State.IsRenderableByProperties)
            {
                Assertion.ThrowIfEqual(renderModel.State.IsRendered, true, nameof(RenderModelState.IsRendered));
                return;
            }

            var shader = renderModel.State.Material.GetShader();
            renderModel.State.RenderBatch = renderModel.State.RenderingLayer.GetRenderBatch(shader);
            renderModel.State.RenderBatch.AddModel(renderModel);

            Assertion.ThrowIfEqual(renderModel.State.RenderBatch != null, false, nameof(RenderModelState.RenderBatch));
            Assertion.ThrowIfEqual(renderModel.State.IsRendered, false, nameof(RenderModelState.IsRendered));
        }
    }
}
