//-----------------------------------------------------------------------
// <copyright file="Canvas.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Microsoft.Extensions.DependencyInjection;

using Quasar.Collections;
using Quasar.Graphics;
using Quasar.Rendering;
using Quasar.UI.Internals;
using Quasar.UI.Internals.Providers;
using Quasar.UI.Internals.Providers.Internals;
using Quasar.UI.Internals.Renderers;

using Space.Core;

namespace Quasar.UI.VisualElements.Internals
{
    /// <summary>
    /// UI canvas object implementation.
    /// </summary>
    /// <seealso cref="DisposableBase" />
    /// <seealso cref="ICanvas" />
    internal sealed class Canvas : ICanvas
    {
        private static SpriteMeshProvider spriteMeshProvider;
        private static TextMeshProvider textMeshProvider;
        private static UIElementRenderer uiElementRenderer;
        private readonly ValueTypeCollection<UIElement> uiElements = new ValueTypeCollection<UIElement>();


        /// <inheritdoc/>
        public Vector2 Offset { get; }

        /// <inheritdoc/>
        public ICanvas Parent { get; }

        /// <inheritdoc/>
        public Vector2 Size { get; }


        /// <inheritdoc/>
        public void DrawSprite(in Sprite sprite, in Vector2 position, in Vector2 size, in Color tintColor)
        {
            var mesh = spriteMeshProvider.Get(sprite, size);
            uiElements.Add(new UIElement(position, Vector2.One, mesh, sprite.Texture, tintColor));
        }

        /// <inheritdoc/>
        public void DrawText(string text, Font font, in Vector2 position, in Color color)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public void DrawText(string text, Font font, in Vector2 position, in Color color, int startIndex, int length)
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// Initializes the static services.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        internal static void InitializeStaticServices(IServiceProvider serviceProvider)
        {
            spriteMeshProvider = serviceProvider.GetRequiredService<SpriteMeshProvider>();
            textMeshProvider = serviceProvider.GetRequiredService<TextMeshProvider>();
            uiElementRenderer = serviceProvider.GetRequiredService<UIElementRenderer>();
        }

        /// <summary>
        /// Renders the canvas by the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        internal void Render(IRenderingContext context)
        {
            uiElementRenderer.Render(context.CommandProcessor, uiElements);
            uiElements.Clear();
        }
    }
}
