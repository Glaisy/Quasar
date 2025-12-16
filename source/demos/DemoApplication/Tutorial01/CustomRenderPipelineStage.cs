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

using System;

using Quasar;
using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.Pipelines;
using Quasar.Rendering;
using Quasar.Rendering.Pipelines;
using Quasar.Rendering.Procedurals;
using Quasar.UI.Pipelines;

using Space.Core.DependencyInjection;

namespace DemoApplication.Tutorial01
{
    /// <summary>
    /// Rendering pipeline stage which allows to run custom code.
    /// </summary>
    /// <seealso cref="RenderingPipelineStageBase" />
    [Export(typeof(RenderingPipelineStageBase), nameof(CustomRenderPipelineStage))]
    [ExecuteAfter(typeof(ClearFrameRenderingPipelineStage))]
    [ExecuteBefore(typeof(UIRenderingPipelineStage))]
    internal sealed class CustomRenderPipelineStage : RenderingPipelineStageBase
    {
        private readonly IShaderRepository shaderRepository;
        private readonly IProceduralMeshGenerator proceduralMeshGenerator;
        private readonly ITimeProvider timeProvider;
        private IMesh mesh;
        private ShaderBase shader;
        private Material material;
        private float angle;
        private int lastSecond;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomRenderPipelineStage" /> class.
        /// </summary>
        /// <param name="shaderRepository">The shader repository.</param>
        /// <param name="proceduralMeshGenerator">The procedural mesh generator.</param>
        /// <param name="timeProvider">The time provider.</param>
        internal CustomRenderPipelineStage(
            IShaderRepository shaderRepository,
            IProceduralMeshGenerator proceduralMeshGenerator,
            ITimeProvider timeProvider)
        {
            this.shaderRepository = shaderRepository;
            this.proceduralMeshGenerator = proceduralMeshGenerator;
            this.timeProvider = timeProvider;
        }


        /// <inheritdoc/>
        protected override void OnExecute()
        {
            angle += 0.03f;
            var rotation = Quaternion.AngleAxis(angle, Vector3.PositiveY);
            Matrix4 rotationMatrix;
            rotationMatrix.FromQuaternion(rotation, false);
            Matrix4 scaleMatrix;
            scaleMatrix.FromScale(new Vector3(0.5f));
            Matrix4 mvp;
            Matrix4.Multiply(scaleMatrix, rotationMatrix, ref mvp);

            shader.Activate();
            shader.SetMatrix(2, mvp);
            material.TransferToShader();
            Context.CommandProcessor.DrawMesh(mesh);
            shader.Deactivate();

            var second = (int)MathF.Floor(timeProvider.Time);

            if (lastSecond != second)
            {
                var fps = timeProvider.DeltaTime > 0 ? 1.0f / timeProvider.DeltaTime : 0.0f;
                Debug.Info($"FPS: {fps:0}, Time:{timeProvider.Time:0.0}s, Delta: {timeProvider.DeltaTime * 1000:0.0}ms,  Physics delta: {timeProvider.PhysicsDeltaTime * 1000:0.0}ms");

                lastSecond = second;
            }
        }

        /// <inheritdoc/>
        protected override void OnStart()
        {
            shader = shaderRepository.GetShader("Wireframe");
            material = new Material(shader);
            material.SetColor("LineColor", Color.Blue);
            material.SetColor("FillColor", Color.White);
            material.SetFloat("Thickness", 0.25f);


            proceduralMeshGenerator.GenerateEllipsoid(ref mesh, 8, 9, Vector3.One);
        }
    }
}
