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
using Quasar.Diagnostics.Profiler;
using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.Pipelines;
using Quasar.Rendering;
using Quasar.Rendering.Pipelines;
using Quasar.Rendering.Procedurals;
using Quasar.Rendering.Profiler;
using Quasar.UI.Pipelines;

using Space.Core.DependencyInjection;
using Space.Core.Diagnostics;

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
        private readonly IProfilerDataProvider<IRenderingStatistics> renderingStatisticsProvider;
        private readonly ILogger logger;
        private Camera camera;
        private IMesh mesh;
        private ShaderBase shader;
        private int mvpPropertyIndex;
        private Material material;
        private float angle;
        private int lastSecond;


        /// <summary>
        /// Initializes a new instance of the <see cref="CustomRenderPipelineStage" /> class.
        /// </summary>
        /// <param name="shaderRepository">The shader repository.</param>
        /// <param name="proceduralMeshGenerator">The procedural mesh generator.</param>
        /// <param name="timeProvider">The time provider.</param>
        /// <param name="renderingStatisticsProvider">The rendering statistics provider.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        internal CustomRenderPipelineStage(
            IShaderRepository shaderRepository,
            IProceduralMeshGenerator proceduralMeshGenerator,
            ITimeProvider timeProvider,
            IProfilerDataProvider<IRenderingStatistics> renderingStatisticsProvider,
            ILoggerFactory loggerFactory)
        {
            this.shaderRepository = shaderRepository;
            this.proceduralMeshGenerator = proceduralMeshGenerator;
            this.timeProvider = timeProvider;
            this.renderingStatisticsProvider = renderingStatisticsProvider;

            logger = loggerFactory.Create<CustomRenderPipelineStage>();
        }


        /// <inheritdoc/>
        protected override void OnExecute()
        {
            angle += 0.03f;
            var rotation = Quaternion.AngleAxis(angle, Vector3.PositiveY);
            Matrix4 modelMatrix;
            modelMatrix.FromQuaternion(rotation, false);

            Matrix4 mvp;
            Matrix4.Multiply(modelMatrix, camera.ViewProjectionMatrix, ref mvp);

            shader.Activate();
            shader.SetMatrix(mvpPropertyIndex, mvp);
            material.TransferToShader();
            Context.CommandProcessor.DrawMesh(mesh);
            shader.Deactivate();

            var second = (int)MathF.Floor(timeProvider.Time);

            if (lastSecond != second)
            {
                var fps = renderingStatisticsProvider.Get().FramesPerSecond;
                var logMessage = $"FPS: {fps:0}, Time:{timeProvider.Time:0.0}s, Delta: {timeProvider.DeltaTime * 1000:0.0}ms,  Physics delta: {timeProvider.PhysicsDeltaTime * 1000:0.0}ms";
                logger.Info(logMessage);
                Debug.Info(logMessage);

                lastSecond = second;
            }
        }

        /// <inheritdoc/>
        protected override void OnStart()
        {
            camera = new Camera
            {
                Name = "Main Camera",
            };
            camera.Transform.LocalPosition = new Vector3(0, 1, -2);
            camera.Transform.LocalRotation = Quaternion.LookRotation(camera.Transform.LocalPosition, Vector3.Zero, Vector3.PositiveY, true);

            shader = shaderRepository.GetShader("Wireframe");
            mvpPropertyIndex = shader["ModelViewProjectionMatrix"].Index;

            material = new Material(shader);
            material.SetColor("LineColor", Color.Blue);
            material.SetColor("FillColor", Color.White);
            material.SetFloat("Thickness", 0.1f);


            proceduralMeshGenerator.GenerateCube(ref mesh, Vector3.One);
        }
    }
}
