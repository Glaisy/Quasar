//-----------------------------------------------------------------------
// <copyright file="InitializeRenderingPipelineStage.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;

using Quasar.Diagnostics.Pipeline.Internals;
using Quasar.Graphics;
using Quasar.Rendering.Procedurals;

using Space.Core.DependencyInjection;

namespace Quasar.Rendering.Pipelines
{
    /// <summary>
    /// Render pipeline's frame initialization stage implementation.
    /// </summary>
    /// <seealso cref="RenderingPipelineStageBase" />
    [Export(typeof(RenderingPipelineStageBase), nameof(InitializeRenderingPipelineStage))]
    public sealed class InitializeRenderingPipelineStage : RenderingPipelineStageBase
    {
        private readonly IShaderRepository shaderRepository;
        private readonly ITextureRepository textureRepository;
        private readonly ICubeMapTextureRepository cubeMapTextureRepository;
        private readonly IFontFamilyRepository fontFamilyRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="InitializeRenderingPipelineStage" /> class.
        /// </summary>
        /// <param name="shaderRepository">The shader repository.</param>
        /// <param name="textureRepository">The texture repository.</param>
        /// <param name="cubeMapTextureRepository">The cube map texture repository.</param>
        /// <param name="fontFamilyRepository">The font family repository.</param>
        internal InitializeRenderingPipelineStage(
            IShaderRepository shaderRepository,
            ITextureRepository textureRepository,
            ICubeMapTextureRepository cubeMapTextureRepository,
            IFontFamilyRepository fontFamilyRepository)
        {
            this.shaderRepository = shaderRepository;
            this.textureRepository = textureRepository;
            this.cubeMapTextureRepository = cubeMapTextureRepository;
            this.fontFamilyRepository = fontFamilyRepository;
        }


        /// <inheritdoc/>
        protected override void OnExecute()
        {
            Context.CommandProcessor.Reset();
        }

        /// <inheritdoc/>
        protected override void OnStart()
        {
            // initialize internal graphics components
            GraphicsResourceBase.InitializeServices(ServiceProvider);
            Font.InitializeServices(ServiceProvider);

            // load built-in resources
            shaderRepository.LoadBuiltInShaders();
            textureRepository.LoadBuiltInTextures();
            cubeMapTextureRepository.LoadBuiltInCubeMapTextures();
            fontFamilyRepository.LoadBuiltInFontFamilies();

            // initialize internal rendering/graphics related components
            Debug.InitializeServices(ServiceProvider);
            MeshGeneratorBase.InitializeServices(ServiceProvider);
            ////RenderObject.InitializeDependencies(resolver);
            ////Material.InitializeDependencies(resolver);

            // initialize late-started rendering services (due to dependency on GraphicsContext)
            var debugTextService = ServiceProvider.GetRequiredService<DebugTextService>();
            debugTextService.Initialize();
        }
    }
}
