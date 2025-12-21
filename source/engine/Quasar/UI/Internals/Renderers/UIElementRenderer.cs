//-----------------------------------------------------------------------
// <copyright file="UIElementRenderer.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Collections;
using Quasar.Graphics;
using Quasar.Graphics.Internals;

using Space.Core.DependencyInjection;

namespace Quasar.UI.Internals.Renderers
{
    /// <summary>
    /// Internal component to render UI elements.
    /// </summary>
    [Export]
    [Singleton]
    internal sealed class UIElementRenderer
    {
        private const string ShaderName = "UI/UIElement";

        private readonly IShaderRepository shaderRepository;
        private readonly IMatrixFactory matrixFactory;
        private Matrix4 projectionMatrix;
        private ShaderBase shader;
        private int positionIndex;
        private int scaleIndex;
        private int colorIndex;
        private int textureIndex;
        private int projectionMatrixIndex;


        /// <summary>
        /// Initializes a new instance of the <see cref="UIElementRenderer" /> class.
        /// </summary>
        /// <param name="shaderRepository">The shader repository.</param>
        /// <param name="matrixFactory">The matrix factory.</param>
        public UIElementRenderer(
            IShaderRepository shaderRepository,
            IMatrixFactory matrixFactory)
        {
            this.shaderRepository = shaderRepository;
            this.matrixFactory = matrixFactory;
        }


        /// <summary>
        /// Initalizes the renderer instance.
        /// </summary>
        public void Initalize()
        {
            shader = shaderRepository.GetShader(ShaderName);
            positionIndex = shader["Position"].Index;
            scaleIndex = shader["Scale"].Index;
            colorIndex = shader["Color"].Index;
            textureIndex = shader["Texture"].Index;
            projectionMatrixIndex = shader["ProjectionMatrix"].Index;
        }

        /// <summary>
        /// Renders UI elements via the specified command processor.
        /// </summary>
        /// <param name="commandProcessor">The command processor.</param>
        /// <param name="uiElements">The UI elements.</param>
        public void Render(IGraphicsCommandProcessor commandProcessor, ValueTypeCollection<UIElement> uiElements)
        {
            var wasDepthTestingEnabled = commandProcessor.SetDepthTesting(false);

            shader.Activate();
            shader.SetMatrix(projectionMatrixIndex, projectionMatrix);
            var lastTextureHandle = -1;
            var count = uiElements.Count;
            for (var i = 0; i < count; i++)
            {
                ref var uiElement = ref uiElements[i];

                shader.SetVector2(positionIndex, uiElement.Position);
                shader.SetVector2(scaleIndex, uiElement.Scale);
                shader.SetColor(colorIndex, uiElement.Color);

                if (lastTextureHandle != uiElement.TextureHandle)
                {
                    shader.SetTexture(textureIndex, uiElement.TextureHandle);
                    lastTextureHandle = uiElement.TextureHandle;
                }

                commandProcessor.DrawMesh(uiElement.RawMesh);
            }

            shader.Deactivate();

            commandProcessor.SetDepthTesting(wasDepthTestingEnabled);
        }

        /// <summary>
        /// Updates the renderer by specified viewport size.
        /// </summary>
        /// <param name="viewportSize">The viewport size.</param>
        public void Update(in Size viewportSize)
        {
            matrixFactory.CreateUIProjectionMatrix(viewportSize, ref projectionMatrix);
        }
    }
}
