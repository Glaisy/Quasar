//-----------------------------------------------------------------------
// <copyright file="IStyle.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;

using Quasar.Graphics;

namespace Quasar.UI.VisualElements.Styles
{
    /// <summary>
    /// Represents a Quasar UI style object.
    /// This interface provides access to the inline style data of a VisualElement.
    /// </summary>
    public interface IStyle
    {
        /// <summary>
        /// Gets the background texture.
        /// </summary>
        StyleTexture Background { get; }

        /// <summary>
        /// Gets the background color.
        /// </summary>
        StyleColor BackgroundColor { get; }

        /// <summary>
        /// Gets the background slicing.
        /// </summary>
        StyleBorder BackgroundSlicing { get; }

        /// <summary>
        /// Gets the bottom position.
        /// </summary>
        StyleUnit Bottom { get; }

        /// <summary>
        /// Gets the color.
        /// </summary>
        StyleColor Color { get; }

        /// <summary>
        /// Gets the custom properties.
        /// </summary>
        IReadOnlyDictionary<string, string> CustomProperties { get; }

        /// <summary>
        /// Gets the display style.
        /// </summary>
        StyleEnum<DisplayStyle> Display { get; }

        /// <summary>
        /// Gets the font family name.
        /// </summary>
        StyleString FontFamily { get; }

        /// <summary>
        /// Gets the font size.
        /// </summary>
        StyleFloat FontSize { get; }

        /// <summary>
        /// Gets the font style.
        /// </summary>
        StyleEnum<FontStyle> FontStyle { get; }

        /// <summary>
        /// Gets the height.
        /// </summary>
        StyleUnit Height { get; }

        /// <summary>
        /// Gets the horizontal alignment of the content.
        /// </summary>
        StyleEnum<Alignment> HorizontalAlignment { get; }

        /// <summary>
        /// Gets the horizontal item alignment.
        /// </summary>
        StyleEnum<ItemAlignment> HorizontalItemAlignment { get; }

        /// <summary>
        /// Gets the horizontal item spacing.
        /// </summary>
        StyleFloat HorizontalItemSpacing { get; }

        /// <summary>
        /// Gets the left position.
        /// </summary>
        StyleUnit Left { get; }

        /// <summary>
        /// Gets the bottom margin.
        /// </summary>
        StyleFloat MarginBottom { get; }

        /// <summary>
        /// Gets the left margin.
        /// </summary>
        StyleFloat MarginLeft { get; }

        /// <summary>
        /// Gets the right margin.
        /// </summary>
        StyleFloat MarginRight { get; }

        /// <summary>
        /// Gets the top margin.
        /// </summary>
        StyleFloat MarginTop { get; }

        /// <summary>
        /// Gets the maximum height.
        /// </summary>
        StyleFloat MaximumHeight { get; }

        /// <summary>
        /// Gets the maximum width.
        /// </summary>
        StyleFloat MaximumWidth { get; }

        /// <summary>
        /// Gets the minimum height.
        /// </summary>
        StyleFloat MinimumHeight { get; }

        /// <summary>
        /// Gets the minimum width.
        /// </summary>
        StyleFloat MinimumWidth { get; }

        /// <summary>
        /// Gets the overflow.
        /// </summary>
        StyleEnum<Overflow> Overflow { get; }

        /// <summary>
        /// Gets the overflow clipping mode.
        /// </summary>
        StyleEnum<OverflowClippingMode> OverflowClippingMode { get; }

        /// <summary>
        /// Gets the bottom padding.
        /// </summary>
        StyleFloat PaddingBottom { get; }

        /// <summary>
        /// Gets the left padding.
        /// </summary>
        StyleFloat PaddingLeft { get; }

        /// <summary>
        /// Gets the right padding.
        /// </summary>
        StyleFloat PaddingRight { get; }

        /// <summary>
        /// Gets the top padding.
        /// </summary>
        StyleFloat PaddingTop { get; }

        /// <summary>
        /// Gets the position.
        /// </summary>
        StyleEnum<Position> Position { get; }

        /// <summary>
        /// Gets the right position.
        /// </summary>
        StyleUnit Right { get; }

        /// <summary>
        /// Gets the top position.
        /// </summary>
        /// <value>
        /// The top.
        /// </value>
        StyleUnit Top { get; }

        /// <summary>
        /// Gets the vertical alignment of the content.
        /// </summary>
        StyleEnum<Alignment> VerticalAlignment { get; }

        /// <summary>
        /// Gets the vertical item alignment.
        /// </summary>
        StyleEnum<ItemAlignment> VerticalItemAlignment { get; }

        /// <summary>
        /// Gets the vertical item spacing.
        /// </summary>
        StyleFloat VerticalItemSpacing { get; }

        /// <summary>
        /// Gets the visibility.
        /// </summary>
        StyleEnum<Visibility> Visibility { get; }

        /// <summary>
        /// Gets the width.
        /// </summary>
        StyleUnit Width { get; }
    }
}
