//-----------------------------------------------------------------------
// <copyright file="SkyboxRenderer.cs" company="Space Development">
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
using Quasar.Rendering.Procedurals;
using Space.Core.DependencyInjection;

namespace Quasar.Rendering.Internals.Renderers
{
    /// <summary>
    /// Renderer implementation to render skyboxes.
    /// </summary>
    [Export]
    internal sealed class SkyboxRenderer
    {
        private const string ShaderId = "Environment/Skybox";


        private readonly IShaderRepository shaderRepository;
        private readonly IProceduralMeshGenerator proceduralMeshGenerator;
        private ShaderBase shader;
        private int exposureIndex;
        private int viewRotationProjectionMatrixIndex;
        private int tintColorIndex;
        private int cubeMapTextureIndex;
        private IMesh mesh;


        /// <summary>
        /// Initializes a new instance of the <see cref="SkyboxRenderer" /> class.
        /// </summary>
        /// <param name="shaderRepository">The shader repository.</param>
        /// <param name="proceduralMeshGenerator">The procedural mesh generator.</param>
        public SkyboxRenderer(
            IShaderRepository shaderRepository,
            IProceduralMeshGenerator proceduralMeshGenerator)
        {
            this.shaderRepository = shaderRepository;
            this.proceduralMeshGenerator = proceduralMeshGenerator;
        }


        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            proceduralMeshGenerator.GenerateSkybox(ref mesh);
            shader = shaderRepository.GetShader(ShaderId);
            exposureIndex = shader["Exposure"].Index;
            tintColorIndex = shader["TintColor"].Index;
            cubeMapTextureIndex = shader["CubeMapTexture"].Index;
            viewRotationProjectionMatrixIndex = shader[ShaderConstants.ViewRotationProjectionMatrix].Index;
        }

        /// <summary>
        /// Renders a skybox by the specified camera.
        /// </summary>
        /// <param name="renderingContext">The rendering context.</param>
        /// <param name="camera">The camera.</param>
        public void Render(IRenderingContext renderingContext, ICamera camera)
        {
            // save and update depth test mode to render skybox fragments
            var previousDepthTestMode = renderingContext.CommandProcessor.DepthTestMode;
            renderingContext.CommandProcessor.DepthTestMode = DepthTestMode.LessOrEqual;

            // initialize the shader
            var skybox = camera.SkyBox;
            shader.Activate();
            shader.SetMatrix(viewRotationProjectionMatrixIndex, camera.ViewRotationProjectionMatrix);
            shader.SetCubeMapTexture(cubeMapTextureIndex, skybox.CubeMapTexture);
            shader.SetColor(tintColorIndex, skybox.TintColor);
            shader.SetFloat(exposureIndex, skybox.Exposure);

            // draw the mesh
            renderingContext.CommandProcessor.DrawMesh(mesh);

            // restore depth test mode
            renderingContext.CommandProcessor.DepthTestMode = previousDepthTestMode;
        }
    }
}
