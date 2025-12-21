//-----------------------------------------------------------------------
// <copyright file="IResolvedStyle.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics;

namespace Quasar.UI.VisualElements.Styles
{
    /// <summary>
    /// Resolved style interface definition.
    /// The is the final computed style values on a VisualElement.
    /// </summary>
    public interface IResolvedStyle
    {
        /// <summary>
        /// Gets the background.
        /// </summary>
        Sprite Background { get; }

        /// <summary>
        /// Gets the background color.
        /// </summary>
        Color BackgroundColor { get; }

        /// <summary>
        /// Gets the visual element's bounding box (including content, padding and margin areas).
        /// Relative to the parent visual element's bounding box.
        /// </summary>
        Rectangle BoundingBox { get; }

        /// <summary>
        /// Gets the color.
        /// </summary>
        Color Color { get; }

        /// <summary>
        /// Gets the visual element's context box.
        /// Relative to the visual element's bounding box.
        /// </summary>
        Rectangle ContentBox { get; }

        /// <summary>
        /// Gets the display mode.
        /// </summary>
        DisplayMode Display { get; }

        /// <summary>
        /// Gets the font.
        /// </summary>
        Font Font { get; }

        /// <summary>
        /// Gets the horizontal alignment of the content.
        /// </summary>
        Alignment HorizontalAlignment { get; }

        /// <summary>
        /// Gets the horizontal item alignment.
        /// </summary>
        ItemAlignment HorizontalItemAlignment { get; }

        /// <summary>
        /// Gets the item spacing.
        /// </summary>
        Vector2 ItemSpacing { get; }

        /// <summary>
        /// Gets the maximum size.
        /// </summary>
        Vector2 MaximumSize { get; }

        /// <summary>
        /// Gets the minimum size.
        /// </summary>
        Vector2 MinimumSize { get; }

        /// <summary>
        /// Gets the overflow.
        /// </summary>
        Overflow Overflow { get; }

        /// <summary>
        /// Gets the overflow clipping mode.
        /// </summary>
        OverflowClippingMode OverflowClippingMode { get; }

        /// <summary>
        /// Gets the position.
        /// </summary>
        Position Position { get; }

        /// <summary>
        /// Gets the padding box (content + padding area).
        /// Relative to the visual element's bounding box.
        /// </summary>
        Rectangle PaddingBox { get; }

        /// <summary>
        /// Gets the vertical alignment of the content.
        /// </summary>
        Alignment VerticalAlignment { get; }

        /// <summary>
        /// Gets the vertical item alignment.
        /// </summary>
        ItemAlignment VerticalItemAlignment { get; }

        /// <summary>
        /// Gets the visibility.
        /// </summary>
        Visibility Visibility { get; }
    }
}
