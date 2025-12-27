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

#if DEBUG
#endif

using Quasar.Assets;
using Quasar.Graphics;
using Quasar.Rendering.Procedurals;
using Quasar.Utilities;

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
        private const string BuiltInAssetPackResourcePath = nameof(Quasar) + ".assets";


        private readonly IResourceProvider builtInResourceProvider;
        private readonly IShaderRepository shaderRepository;
        private readonly ITextureRepository textureRepository;
        private readonly ICubeMapTextureRepository cubeMapTextureRepository;
        private readonly IFontFamilyRepository fontFamilyRepository;
        private readonly IAssetPackageFactory assetPackageFactory;


        /// <summary>
        /// Initializes a new instance of the <see cref="InitializeRenderingPipelineStage" /> class.
        /// </summary>
        /// <param name="quasarContext">The Quasar context.</param>
        /// <param name="shaderRepository">The shader repository.</param>
        /// <param name="textureRepository">The texture repository.</param>
        /// <param name="cubeMapTextureRepository">The cube map texture repository.</param>
        /// <param name="fontFamilyRepository">The font family repository.</param>
        /// <param name="assetPackageFactory">The asset package factory.</param>
        internal InitializeRenderingPipelineStage(
            IQuasarContext quasarContext,
            IShaderRepository shaderRepository,
            ITextureRepository textureRepository,
            ICubeMapTextureRepository cubeMapTextureRepository,
            IFontFamilyRepository fontFamilyRepository,
            IAssetPackageFactory assetPackageFactory)
        {
            this.shaderRepository = shaderRepository;
            this.textureRepository = textureRepository;
            this.cubeMapTextureRepository = cubeMapTextureRepository;
            this.fontFamilyRepository = fontFamilyRepository;
            this.assetPackageFactory = assetPackageFactory;

            builtInResourceProvider = quasarContext.ResourceProvider;
        }


        /// <inheritdoc/>
        protected override void OnExecute()
        {
            Context.CommandProcessor.Reset();
        }

        /// <inheritdoc/>
        protected override void OnStart()
        {
            // initialize internal graphics/rendering components
            GraphicsResourceBase.InitializeStaticServices(ServiceProvider);
            Font.InitializeStaticServices(ServiceProvider);
            MeshGeneratorBase.InitializeStaticServices(ServiceProvider);
            Material.InitializeStaticServices(ServiceProvider);
            Camera.InitializeStaticServices(ServiceProvider);
            RenderModel.InitializeStaticServices(ServiceProvider);

            LoadBuiltInAssets();
        }

        private void LoadBuiltInAssets()
        {
            shaderRepository.LoadBuiltInShaders();

            var assetPackage = assetPackageFactory.Create(builtInResourceProvider, BuiltInAssetPackResourcePath);
            assetPackage.ImportAssets();

            textureRepository.ValidateBuiltInAssets();
            cubeMapTextureRepository.ValidateBuiltInAssets();
            fontFamilyRepository.ValidateBuiltInAssets();
        }
    }
}
