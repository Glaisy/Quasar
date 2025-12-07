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

using Quasar.Graphics;
using Quasar.Graphics.Internals;
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
        private readonly IShaderRepository shaderRepository;
        private readonly IMeshFactory meshFactory;
        private float angle;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestRenderingPipelineStage" /> class.
        /// </summary>
        /// <param name="shaderRepository">The shader repository.</param>
        /// <param name="meshFactory">The mesh factory.</param>
        internal TestRenderingPipelineStage(
            IShaderRepository shaderRepository,
            IMeshFactory meshFactory)
        {
            this.shaderRepository = shaderRepository;
            this.meshFactory = meshFactory;
        }


        /// <inheritdoc/>
        protected override void OnExecute()
        {
            var rotation = Quaternion.AngleAxis(angle, Vector3.PositiveY);
            Matrix4 rotationMatrix;
            rotationMatrix.FromQuaternion(rotation, false);

            shader.Activate();
            shader.SetMatrix(0, rotationMatrix);
            Context.CommandProcessor.DrawMesh(mesh);
            shader.Deactivate();

            angle += 0.1f;
        }

        /// <inheritdoc/>
        protected override void OnStart()
        {
            shader = shaderRepository.GetShader("Test");

            var vertices = new[]
            {
                new VertexPositionColor { Position = new Vector3(-0.5f, -0.5f, -0.5f), Color = Color.Red },
                new VertexPositionColor { Position = new Vector3(0.5f, -0.15f, -0.5f), Color = Color.Green },
                new VertexPositionColor { Position = new Vector3(0.0f, -0.5f, 0.73f), Color = Color.Blue },
                new VertexPositionColor { Position = new Vector3(0.0f, 0.65f, 0), Color = Color.Magenta }
            };
            var indices = new[] { 0, 1, 2, 0, 2, 3, 2, 1, 3, 1, 0, 3 };

            mesh = meshFactory.Create(PrimitiveType.Triangle, VertexPositionColor.Layout, true, "test");
            mesh.VertexBuffer.SetData(vertices);
            mesh.IndexBuffer?.SetData(indices);
        }


        private IMesh mesh;
        private ShaderBase shader;
    }
}
