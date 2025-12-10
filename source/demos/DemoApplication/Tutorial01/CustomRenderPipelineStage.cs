//-----------------------------------------------------------------------
// <copyright file="CustomRenderPipelineStage.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar;
using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.Pipelines;
using Quasar.Rendering.Pipeline;
using Quasar.Rendering.Procedurals;

using Space.Core.DependencyInjection;

namespace DemoApplication.Tutorial01
{
    /// <summary>
    /// Rendering pipeline stage which allows to run custom code.
    /// </summary>
    /// <seealso cref="RenderingPipelineStageBase" />
    [Export(typeof(RenderingPipelineStageBase), nameof(CustomRenderPipelineStage))]
    [ExecuteAfter(typeof(ClearFrameRenderingPipelineStage))]
    [ExecuteBefore(typeof(FrameBufferSwapperRenderingPipelineStage))]
    internal sealed class CustomRenderPipelineStage : RenderingPipelineStageBase
    {
        private readonly IShaderRepository shaderRepository;
        private readonly IProceduralMeshGenerator proceduralMeshGenerator;
        private IMesh mesh;
        private ShaderBase shader;
        private float angle;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomRenderPipelineStage" /> class.
        /// </summary>
        /// <param name="shaderRepository">The shader repository.</param>
        /// <param name="proceduralMeshGenerator">The procedural mesh generator.</param>
        internal CustomRenderPipelineStage(
            IShaderRepository shaderRepository,
            IProceduralMeshGenerator proceduralMeshGenerator)
        {
            this.shaderRepository = shaderRepository;
            this.proceduralMeshGenerator = proceduralMeshGenerator;
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
            proceduralMeshGenerator.GenerateEllipsoid(ref mesh, 3, 3, Vector3.One);
        }
    }
}
