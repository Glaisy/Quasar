//-----------------------------------------------------------------------
// <copyright file="ObjectsModelsAndSkyboxTutorialView.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Quasar;
using Quasar.Graphics;
using Quasar.Pipelines;
using Quasar.Rendering;
using Quasar.Rendering.Procedurals;

using Space.Core.DependencyInjection;

namespace DemoApplication.Tutorials.Objects
{
    /// <summary>
    /// Tutorial which demonstrates loading models, creating rendered object and skybox.
    /// </summary>
    /// <seealso cref="TutorialViewBase" />
    [Export]
    internal sealed class ObjectsModelsAndSkyboxTutorialView : TutorialViewBase
    {
        private const int CubeCount = 1000;
        private const int MaterialCount = 1;
        private const float BoxSize = 32;


        private readonly ITimeProvider timeProvider;
        private readonly IProceduralMeshGenerator proceduralMeshGenerator;

        private readonly LightSource light;
        private readonly Camera camera;
        private readonly List<Cube> cubes = new List<Cube>(CubeCount);
        private readonly Material[] cubeMaterials = new Material[MaterialCount];


        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectsModelsAndSkyboxTutorialView" /> class.
        /// </summary>
        /// <param name="timeProvider">The time provider.</param>
        /// <param name="proceduralMeshGenerator">The procedural mesh generator.</param>
        /// <param name="textureRepository">The texture repository.</param>
        /// <param name="cubeMapTextureRepository">The cube map texture repository.</param>
        public ObjectsModelsAndSkyboxTutorialView(
            ITimeProvider timeProvider,
            IProceduralMeshGenerator proceduralMeshGenerator,
            ITextureRepository textureRepository,
            ICubeMapTextureRepository cubeMapTextureRepository)
        {
            this.timeProvider = timeProvider;
            this.proceduralMeshGenerator = proceduralMeshGenerator;

            camera = new Camera(name: "Main Camera")
            {
                ClearType = CameraClearType.Skybox
            };
            camera.SkyBox.CubeMapTexture = cubeMapTextureRepository.Get("Skybox");
            camera.Transform.LocalPosition = new Vector3(0, BoxSize * 0.5f, -BoxSize);
            camera.Transform.LocalRotation = Quaternion.LookRotation(camera.Transform.LocalPosition, Vector3.Zero, Vector3.PositiveY, true);
            ////freeCameraController.Initialize(camera);

            // initialize light
            light = new LightSource(LightSourceType.Directional);
            light.Transform.LocalRotation =
                Quaternion.AngleAxis(MathF.PI * -0.25f, Vector3.NegativeZ) *
                Quaternion.AngleAxis(-MathF.PI * 0.5f, Vector3.PositiveY);

            // generate plane
            _ = Plane.Create(
                proceduralMeshGenerator,
                new Vector3(0, -8.4f, 0),
                Vector3.PositiveY,
                new Vector2(BoxSize * 10.0f),
                new Color(1, 1, 0.8f, 1),
                16);

            // generate random cubes and materials
            var random = new Random(1);
            var cubeDiffuseMap = textureRepository.Get("cube");
            var cubeNormalMap = textureRepository.Get("cube_normal");
            for (var i = 0; i < MaterialCount; i++)
            {
                var material = new Material("Diffuse");
                material.SetColor("DiffuseColor", Color.White);
                material.SetTexture("DiffuseMap", cubeDiffuseMap);
                material.SetNormalMapTexture("NormalMap", cubeNormalMap);
                material.SetFloat("NormalStrength", 1f);
                material.SetFloat("AmbientLightIntensity", 0.05f);

                cubeMaterials[i] = material;
            }

            var bigCube = Cube.Create(proceduralMeshGenerator, Vector3.Zero, new Vector3(5.5f, 17f, 6.875f), Vector3.Zero, cubeMaterials[0]);
            cubes.Add(bigCube);
            for (var i = 1; i < CubeCount; i++)
            {
                cubes.Add(CreateCube(random, i));
            }
        }


        /// <inheritdoc/>
        public override string Title => "Objects and models";


        /// <inheritdoc/>
        protected override void OnUpdate()
        {
            ////var renderStatistics = profilingDataProvider.ProfilingData.RenderStatistics;
            ////var updateStatistics = profilingDataProvider.ProfilingData.UpdateStatistics;

            ////renderLabel.Text = $"FPS: {renderStatistics.FramesPerSecond:0.0}, " +
            ////    $"Time: {1000 * renderStatistics.FrameStatistics.FrameTime: 0.0}ms, " +
            ////    $"Calls: {renderStatistics.FrameStatistics.Calls}";

            ////updateLabel.Text = $"Update time: {1000 * updateStatistics.FrameStatistics.FrameTime: 0.0}ms";

            // update cubes
            var deltaTime = timeProvider.DeltaTime;
            var frustum = camera.Frustum;
            foreach (var cube in cubes)
            {
                cube.Update(deltaTime);
                ////var isInFrustum = frustum.IsInFrustum(cube);
                ////if (isInFrustum != cube.Enabled)
                ////{
                ////    cube.Enabled = isInFrustum;
                ////}
            }
        }


        private Cube CreateCube(Random random, int index)
        {
            var position = GeneratePosition(random, BoxSize);
            var size = GenerateSize(random);
            var rotationSpeed = GenerateRotationSpeed(random);

            var material = cubeMaterials[index % cubeMaterials.Length];
            return Cube.Create(proceduralMeshGenerator, position, size, rotationSpeed, material);
        }

        private static Vector3 GeneratePosition(Random random, float range)
        {
            return new Vector3(GenerateValue(random, -range, range), GenerateValue(random, -range, range), GenerateValue(random, -range, range));
        }

        private static Vector3 GenerateRotationSpeed(Random random)
        {
            return new Vector3(GenerateValue(random, -1.0f, 1.0f), GenerateValue(random, -1.0f, 1.0f), GenerateValue(random, -1.0f, 1.0f));
        }

        private static Vector3 GenerateSize(Random random)
        {
            return new Vector3(GenerateValue(random, 0.33f, 1.0f), GenerateValue(random, 0.33f, 1.0f), GenerateValue(random, 0.33f, 1.0f));
        }

        private static float GenerateValue(Random random, float min, float max)
        {
            return min + (max - min) * random.NextSingle();
        }
    }
}
