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
        private readonly IProceduralMeshGenerator proceduralMeshGenerator;
        private readonly ITimeProvider timeProvider;
        private readonly IProfilerDataProvider<IRenderingStatistics> renderingStatisticsProvider;
        private readonly ILogger logger;
        private Camera camera;
        private RenderModel renderModel;
        private float angle;
        private int lastSecond;
        private IMesh mesh;


        /// <summary>
        /// Initializes a new instance of the <see cref="CustomRenderPipelineStage" /> class.
        /// </summary>
        /// <param name="proceduralMeshGenerator">The procedural mesh generator.</param>
        /// <param name="timeProvider">The time provider.</param>
        /// <param name="renderingStatisticsProvider">The rendering statistics provider.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        internal CustomRenderPipelineStage(
            IProceduralMeshGenerator proceduralMeshGenerator,
            ITimeProvider timeProvider,
            IProfilerDataProvider<IRenderingStatistics> renderingStatisticsProvider,
            ILoggerFactory loggerFactory)
        {
            this.proceduralMeshGenerator = proceduralMeshGenerator;
            this.timeProvider = timeProvider;
            this.renderingStatisticsProvider = renderingStatisticsProvider;

            logger = loggerFactory.Create<CustomRenderPipelineStage>();
        }


        /// <inheritdoc/>
        protected override void OnExecute()
        {
            angle += 0.03f;
            renderModel.Transform.LocalRotation = Quaternion.AngleAxis(angle, Vector3.PositiveY);

            var second = (int)MathF.Floor(timeProvider.Time);

            if (lastSecond != second)
            {
                var fps = renderingStatisticsProvider.Get().FramesPerSecond;
                var logMessage = $"FPS: {fps:0}, Time:{timeProvider.Time:0.0}s, Delta: {timeProvider.DeltaTime * 1000:0.0}ms,  Physics delta: {timeProvider.PhysicsDeltaTime * 1000:0.0}ms";
                logger.Info(logMessage);
                Debug.Info(logMessage);

                lastSecond = second;
                renderModel.SetMesh(renderModel.Mesh == null ? mesh : null, true);
                ////renderModel.IsEnabled = !renderModel.IsEnabled;
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

            var material = new Material("Wireframe");
            material.SetColor("LineColor", Color.Blue);
            material.SetColor("FillColor", Color.White);
            material.SetFloat("Thickness", 0.1f);

            proceduralMeshGenerator.GenerateCube(ref mesh, Vector3.One);

            renderModel = new RenderModel
            {
                Name = "TestCube",
                Material = material
            };

            renderModel.SetMesh(mesh, true);
        }
    }
}
