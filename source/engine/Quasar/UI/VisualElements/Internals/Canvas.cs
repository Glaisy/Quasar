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
using System.Runtime.CompilerServices;

using Microsoft.Extensions.DependencyInjection;

using Quasar.Collections;
using Quasar.Graphics;
using Quasar.Rendering;
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


        /// <summary>
        /// Gets or sets the rendering position offset.
        /// </summary>
        public Vector2 Offset { get; set; }


        /// <inheritdoc/>
        public void DrawSprite(in Sprite sprite, in Vector2 position, in Vector2 size, in Color tintColor)
        {
            Assertion.ThrowIfNull(sprite.Texture, nameof(sprite.Texture));

            var mesh = spriteMeshProvider.Get(sprite, size);
            uiElements.Add(new UIElement(position + Offset, mesh, sprite.Texture, tintColor));
        }

        /// <inheritdoc/>
        public void DrawText(string text, Font font, in Vector2 position, in Color color)
        {
            Assertion.ThrowIfNull(text, nameof(text));

            DrawTextInternal(text, font, position, color, 0, text.Length);
        }

        /// <inheritdoc/>
        public void DrawText(string text, Font font, in Vector2 position, in Color color, int startIndex, int length)
        {
            Assertion.ThrowIfNull(text, nameof(text));
            Assertion.ThrowIfNegative(startIndex, nameof(startIndex));
            Assertion.ThrowIfGreaterThan(startIndex + length, text.Length, nameof(length));

            DrawTextInternal(text, font, position, color, startIndex, length);
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


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DrawTextInternal(string text, Font font, in Vector2 position, in Color color, int startIndex, int length)
        {
            var mesh = textMeshProvider.Get(font, text, startIndex, length);
            uiElements.Add(new UIElement(position + Offset, mesh, font.Texture, color));
        }
    }
}
