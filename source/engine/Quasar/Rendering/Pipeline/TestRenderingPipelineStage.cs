//-----------------------------------------------------------------------
// <copyright file="TestRenderingPipelineStage.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Linq;

using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.Graphics.Internals.Factories;
using Quasar.Pipelines;

using Space.Core.DependencyInjection;

namespace Quasar.Rendering.Pipeline
{
    /// <summary>
    /// Rendering pipeline stage which allows to run test code.
    /// </summary>
    /// <seealso cref="RenderingPipelineStageBase" />
    [Export(typeof(RenderingPipelineStageBase), nameof(TestRenderingPipelineStage))]
    [ExecuteAfter(typeof(ClearFrameRenderingPipelineStage))]
    [ExecuteBefore(typeof(FrameBufferSwapperRenderingPipelineStage))]
    public sealed class TestRenderingPipelineStage : RenderingPipelineStageBase
    {
        private readonly IShaderFactory shaderFactory;
        private readonly IMeshFactory meshFactory;


        /// <summary>
        /// Initializes a new instance of the <see cref="TestRenderingPipelineStage" /> class.
        /// </summary>
        /// <param name="shaderFactory">The shader factory.</param>
        /// <param name="meshFactory">The mesh factory.</param>
        internal TestRenderingPipelineStage(
            IShaderFactory shaderFactory,
            IMeshFactory meshFactory)
        {
            this.shaderFactory = shaderFactory;
            this.meshFactory = meshFactory;
        }


        /// <inheritdoc/>
        protected override void OnExecute()
        {
            shader.Activate();
            Context.CommandProcessor.DrawMesh(mesh);
            shader.Deactivate();
        }

        /// <inheritdoc/>
        protected override void OnStart()
        {
            shader = shaderFactory.LoadBuiltInShaders().FirstOrDefault();

            var vertices = new[]
            {
                new VertexPositionColor { Position = new Vector3(-0.5f, -0.5f, 1), Color = Color.Red },
                new VertexPositionColor { Position = new Vector3(0.0f, 0.5f, 1), Color = Color.Green },
                new VertexPositionColor { Position = new Vector3(0.5f, -0.5f, 1), Color = Color.Blue }
            };

            mesh = meshFactory.Create(PrimitiveType.Triangle, VertexPositionColor.Layout, false, "test");
            mesh.VertexBuffer.SetData(vertices);
        }


        private IMesh mesh;
        private ShaderBase shader;
    }
}
