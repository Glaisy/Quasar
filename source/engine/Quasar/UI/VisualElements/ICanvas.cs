//-----------------------------------------------------------------------
// <copyright file="ICanvas.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics;

namespace Quasar.UI.VisualElements
{
    /// <summary>
    /// Represents a rendering surface for UI visual elements with offset position and size.
    /// The rendering coordinate system's origin is at the bottom-left corner.
    /// Horizontal axis - poitive X direction, Vertical axis - positive Y direction.
    /// Every position and size values have pixel units.
    /// </summary>
    public interface ICanvas
    {
        /// <summary>
        /// Gets the offset position relative to the parent (Default: [0, 0]).
        /// </summary>
        Vector2 Offset { get; }

        /// <summary>
        /// Gets the parent canvas.
        /// </summary>
        ICanvas Parent { get; }

        /// <summary>
        /// Gets the rendering area's size.
        /// </summary>
        Vector2 Size { get; }


        /// <summary>
        /// Draws the sprite by the specified size, position and tint color.
        /// </summary>
        /// <param name="sprite">The sprite.</param>
        /// <param name="position">The position.</param>
        /// <param name="size">The size.</param>
        /// <param name="tintColor">The tint color.</param>
        void DrawSprite(in Sprite sprite, in Vector2 position, in Vector2 size, in Color tintColor);

        /// <summary>
        /// Draws the text by the specified font, position and color.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        void DrawText(string text, Font font, in Vector2 position, in Color color);

        /// <summary>
        /// Draw the text range (start, length) by the specified font, position and color.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="length">The length.</param>
        void DrawText(string text, Font font, in Vector2 position, in Color color, int startIndex, int length);
    }
}
