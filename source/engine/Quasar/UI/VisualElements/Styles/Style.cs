//-----------------------------------------------------------------------
// <copyright file="Style.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Quasar.Graphics;

namespace Quasar.UI.VisualElements.Styles
{
    /// <summary>
    /// UI style object implementation.
    /// </summary>
    /// <seealso cref="IStyle" />
    public sealed class Style : IStyle
    {
        private Dictionary<string, string> inheritedCustomProperties;


        /// <summary>
        /// Initializes static members of the <see cref="Style"/> class.
        /// </summary>
        static Style()
        {
            Template = CreateStyleTemplate();
        }


        /// <summary>
        /// Gets or sets the background texture.
        /// </summary>
        public StyleTexture Background { get; set; }

        /// <summary>
        /// Gets or sets the background color.
        /// </summary>
        public StyleColor BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the background slicing.
        /// </summary>
        public StyleBorder BackgroundSlicing { get; set; }

        /// <summary>
        /// Gets or sets the bottom position.
        /// </summary>
        public StyleUnit Bottom { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public StyleColor Color { get; set; }

        private StyleProperties customProperties;
        /// <summary>
        /// Gets or sets the custom properties.
        /// </summary>
        public StyleProperties CustomProperties
        {
            get
            {
                if (customProperties == inheritedCustomProperties)
                {
                    InitializeCustomProperties(inheritedCustomProperties);
                }

                return customProperties;
            }
            set
            {
                // TODO: review the custom property handling in style objects
                if (customProperties == value)
                {
                    return;
                }

                if (customProperties == null)
                {
                    // lazy copy of input custom properties
                    inheritedCustomProperties = value;
                    customProperties = value;
                }
                else
                {
                    // full copy of source's custom properties
                    if (customProperties == inheritedCustomProperties)
                    {
                        InitializeCustomProperties(value);
                    }
                    else
                    {
                        customProperties.Clear();
                        if (value != null)
                        {
                            foreach (var pair in value)
                            {
                                customProperties.Add(pair.Key, pair.Value);
                            }
                        }
                    }
                }
            }
        }

        /// <inheritdoc/>
        IReadOnlyDictionary<string, string> IStyle.CustomProperties => customProperties;

        /// <summary>
        /// Gets or sets the display mode.
        /// </summary>
        public StyleEnum<DisplayMode> Display { get; set; }

        /// <summary>
        /// Gets or sets the font family name.
        /// </summary>
        public StyleString FontFamily { get; set; }

        /// <summary>
        /// Gets or sets the font size.
        /// </summary>
        public StyleFloat FontSize { get; set; }

        /// <summary>
        /// Gets or sets the font style.
        /// </summary>
        public StyleEnum<FontStyle> FontStyle { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public StyleUnit Height { get; set; }

        /// <summary>
        /// Gets or sets the horizontal alignment of the content.
        /// </summary>
        public StyleEnum<Alignment> HorizontalAlignment { get; set; }

        /// <summary>
        /// Gets or sets the horizontal item alignment.
        /// </summary>
        public StyleEnum<ItemAlignment> HorizontalItemAlignment { get; set; }

        /// <summary>
        /// Gets or sets the horizontal item spacing.
        /// </summary>
        public StyleFloat HorizontalItemSpacing { get; set; }

        /// <summary>
        /// Gets or sets the left position.
        /// </summary>
        public StyleUnit Left { get; set; }

        /// <summary>
        /// Gets or sets the bottom margin.
        /// </summary>
        public StyleFloat MarginBottom { get; set; }

        /// <summary>
        /// Gets or sets the left margin.
        /// </summary>
        public StyleFloat MarginLeft { get; set; }

        /// <summary>
        /// Gets or sets the right margin.
        /// </summary>
        public StyleFloat MarginRight { get; set; }

        /// <summary>
        /// Gets or sets the top margin.
        /// </summary>
        public StyleFloat MarginTop { get; set; }

        /// <summary>
        /// Gets or sets the maximum height.
        /// </summary>
        public StyleFloat MaximumHeight { get; set; }

        /// <summary>
        /// Gets or sets the maximum width.
        /// </summary>
        public StyleFloat MaximumWidth { get; set; }

        /// <summary>
        /// Gets or sets the minimum height.
        /// </summary>
        public StyleFloat MinimumHeight { get; set; }

        /// <summary>
        /// Gets or sets the minimum width.
        /// </summary>
        public StyleFloat MinimumWidth { get; set; }

        /// <summary>
        /// Gets or sets the overflow.
        /// </summary>
        public StyleEnum<Overflow> Overflow { get; set; }

        /// <summary>
        /// Gets or sets the overflow clipping mode.
        /// </summary>
        public StyleEnum<OverflowClippingMode> OverflowClippingMode { get; set; }

        /// <summary>
        /// Gets or sets the bottom padding.
        /// </summary>
        public StyleFloat PaddingBottom { get; set; }

        /// <summary>
        /// Gets or sets the left padding.
        /// </summary>
        public StyleFloat PaddingLeft { get; set; }

        /// <summary>
        /// Gets or sets the right padding.
        /// </summary>
        public StyleFloat PaddingRight { get; set; }

        /// <summary>
        /// Gets or sets the top padding.
        /// </summary>
        public StyleFloat PaddingTop { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public StyleEnum<Position> Position { get; set; }

        /// <summary>
        /// Gets or sets the right position.
        /// </summary>
        public StyleUnit Right { get; set; }

        /// <summary>
        /// Gets or sets the top position.
        /// </summary>
        public StyleUnit Top { get; set; }

        /// <summary>
        /// Gets or sets the vertical alignment of the content.
        /// </summary>
        public StyleEnum<Alignment> VerticalAlignment { get; set; }

        /// <summary>
        /// Gets or sets the vertical item alignment.
        /// </summary>
        public StyleEnum<ItemAlignment> VerticalItemAlignment { get; set; }

        /// <summary>
        /// Gets or sets the vertical item spacing.
        /// </summary>
        public StyleFloat VerticalItemSpacing { get; set; }

        /// <summary>
        /// Gets or sets the visibility.
        /// </summary>
        public StyleEnum<Visibility> Visibility { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public StyleUnit Width { get; set; }


        /// <summary>
        /// Gets or sets the selector.
        /// </summary>
        internal string Selector { get; set; }

        /// <summary>
        /// Gets the template.
        /// </summary>
        internal static Style Template { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InitializeCustomProperties(IReadOnlyDictionary<string, string> source)
        {
            customProperties = source == null ?
                new StyleProperties() :
                new StyleProperties(source);
            inheritedCustomProperties = null;
        }

        private static Style CreateStyleTemplate()
        {
            var style = new Style
            {
                Background = new StyleTexture(StyleFlag.NotSet),
                BackgroundColor = new StyleColor(StyleFlag.NotSet),
                BackgroundSlicing = new StyleBorder(StyleFlag.NotSet),
                Bottom = new StyleUnit(StyleFlag.Auto),
                Color = new StyleColor(Graphics.Color.White, StyleFlag.Initial),
                Display = new StyleEnum<DisplayMode>(DisplayMode.Display, StyleFlag.Initial),
                FontFamily = new StyleString(StyleFlag.NotSet),
                FontSize = new StyleFloat(StyleFlag.NotSet),
                FontStyle = new StyleEnum<FontStyle>(StyleFlag.NotSet),
                Height = new StyleUnit(StyleFlag.Auto),
                HorizontalAlignment = new StyleEnum<Alignment>(Alignment.Start, StyleFlag.Initial),
                HorizontalItemAlignment = new StyleEnum<ItemAlignment>(ItemAlignment.Start, StyleFlag.Initial),
                HorizontalItemSpacing = new StyleFloat(StyleFlag.NotSet),
                Left = new StyleUnit(StyleFlag.Auto),
                MarginBottom = new StyleFloat(StyleFlag.NotSet),
                MarginLeft = new StyleFloat(StyleFlag.NotSet),
                MarginRight = new StyleFloat(StyleFlag.NotSet),
                MarginTop = new StyleFloat(StyleFlag.NotSet),
                MaximumHeight = new StyleFloat(StyleFlag.NotSet),
                MaximumWidth = new StyleFloat(StyleFlag.NotSet),
                MinimumHeight = new StyleFloat(StyleFlag.NotSet),
                MinimumWidth = new StyleFloat(StyleFlag.NotSet),
                Overflow = new StyleEnum<Overflow>(Styles.Overflow.Visible, StyleFlag.Initial),
                OverflowClippingMode = new StyleEnum<OverflowClippingMode>(Styles.OverflowClippingMode.PaddingBox, StyleFlag.Initial),
                PaddingBottom = new StyleFloat(StyleFlag.NotSet),
                PaddingLeft = new StyleFloat(StyleFlag.NotSet),
                PaddingRight = new StyleFloat(StyleFlag.NotSet),
                PaddingTop = new StyleFloat(StyleFlag.NotSet),
                Position = new StyleEnum<Position>(Styles.Position.Relative, StyleFlag.Initial),
                Right = new StyleUnit(StyleFlag.Auto),
                Top = new StyleUnit(StyleFlag.Auto),
                VerticalAlignment = new StyleEnum<Alignment>(Alignment.End, StyleFlag.Initial),
                VerticalItemAlignment = new StyleEnum<ItemAlignment>(ItemAlignment.End, StyleFlag.Initial),
                VerticalItemSpacing = new StyleFloat(StyleFlag.NotSet),
                Visibility = new StyleEnum<Visibility>(Styles.Visibility.Visible, StyleFlag.Initial),
                Width = new StyleUnit(StyleFlag.Auto),
            };

            return style;
        }
    }
}
