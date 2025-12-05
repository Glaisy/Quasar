//-----------------------------------------------------------------------
// <copyright file="ClearFrameRenderingPipelineStage.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.Graphics.Internals.Factories;
using Quasar.Pipelines;

using Space.Core.DependencyInjection;

namespace Quasar.Rendering.Pipeline
{
    /// <summary>
    /// Render pipeline's clear stage implementation.
    /// </summary>
    /// <seealso cref="RenderingPipelineStageBase" />
    [Export(typeof(RenderingPipelineStageBase), nameof(ClearFrameRenderingPipelineStage))]
    [ExecuteAfter(typeof(InitializeRenderingPipelineStage))]
    public sealed class ClearFrameRenderingPipelineStage : RenderingPipelineStageBase
    {
        /// <inheritdoc/>
        protected override void OnExecute(IRenderingContext renderingContext)
        {
            // TODO: implement rendering framebuffer size by the application window size
            shader.Activate();
            renderingContext.CommandProcessor.DrawMesh(mesh);
            shader.Deactivate();
        }

        /// <summary>
        /// Start event handler.
        /// </summary>
        protected override void OnStart()
        {
            base.OnStart();

            shader = ServiceProvider.GetRequiredService<IShaderFactory>().LoadBuiltInShaders().FirstOrDefault();

            var meshFactory = ServiceProvider.GetRequiredService<IMeshFactory>();
            var vertices = new[]
            {
                new VertexPosition { Position = new Vector3(-0.5f, -0.5f, 0) },
                new VertexPosition { Position = new Vector3(0.5f, -0.5f, 0) },
                new VertexPosition { Position = new Vector3(0.0f, 0.5f, 0) },
            };

            mesh = meshFactory.Create(PrimitiveType.Triangle, VertexPosition.Layout, false, "test");
            mesh.VertexBuffer.SetData(vertices);
        }

        private IMesh mesh;
        private ShaderBase shader;
    }
}
