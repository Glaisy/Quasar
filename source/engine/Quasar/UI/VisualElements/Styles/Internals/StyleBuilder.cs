//-----------------------------------------------------------------------
// <copyright file="StyleBuilder.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Quasar.Graphics;
using Quasar.UI.VisualElements.Themes;
using Quasar.Utilities;

using Space.Core.DependencyInjection;
using Space.Core.Diagnostics;

namespace Quasar.UI.VisualElements.Styles.Internals
{
    /// <summary>
    /// Style builder implementation.
    /// </summary>
    [Export(typeof(IStyleBuilder))]
    [Singleton]
    internal sealed class StyleBuilder : IStyleBuilder
    {
        private readonly IStyleSheetValueParser valueParser;
        private readonly ITextureRepository textureRepository;
        private readonly IPathResolver pathResolver;
        private readonly ILogger logger;


        /// <summary>
        /// Initializes a new instance of the <see cref="StyleBuilder" /> class.
        /// </summary>
        /// <param name="valueParser">The value parser.</param>
        /// <param name="textureRepository">The texture repository.</param>
        /// <param name="pathResolver">The path resolver.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public StyleBuilder(
            IStyleSheetValueParser valueParser,
            ITextureRepository textureRepository,
            IPathResolver pathResolver,
            ILoggerFactory loggerFactory)
        {
            this.valueParser = valueParser;
            this.textureRepository = textureRepository;
            this.pathResolver = pathResolver;

            logger = loggerFactory.Create<StyleBorder>();
        }


        /// <inheritdoc/>
        public void Copy(Style target, IStyle source)
        {
            // style properties
            target.Background = source.Background;
            target.BackgroundColor = source.BackgroundColor;
            target.BackgroundSlicing = source.BackgroundSlicing;
            target.Bottom = source.Bottom;
            target.Color = source.Color;
            target.Display = source.Display;
            target.FontFamily = source.FontFamily;
            target.FontSize = source.FontSize;
            target.FontStyle = source.FontStyle;
            target.Height = source.Height;
            target.HorizontalAlignment = source.HorizontalAlignment;
            target.HorizontalItemAlignment = source.HorizontalItemAlignment;
            target.HorizontalItemSpacing = source.HorizontalItemSpacing;
            target.Left = source.Left;
            target.MarginBottom = source.MarginBottom;
            target.MarginLeft = source.MarginLeft;
            target.MarginRight = source.MarginRight;
            target.MarginTop = source.MarginTop;
            target.MaximumHeight = source.MaximumHeight;
            target.MaximumWidth = source.MaximumWidth;
            target.MinimumHeight = source.MinimumHeight;
            target.MinimumWidth = source.MinimumWidth;
            target.Overflow = source.Overflow;
            target.OverflowClippingMode = source.OverflowClippingMode;
            target.PaddingBottom = source.PaddingBottom;
            target.PaddingLeft = source.PaddingLeft;
            target.PaddingRight = source.PaddingRight;
            target.PaddingTop = source.PaddingTop;
            target.Position = source.Position;
            target.Right = source.Right;
            target.Top = source.Top;
            target.VerticalAlignment = source.VerticalAlignment;
            target.VerticalItemAlignment = source.VerticalItemAlignment;
            target.VerticalItemSpacing = source.VerticalItemSpacing;
            target.Visibility = source.Visibility;
            target.Width = source.Width;

            // custom properties
            CopyCustomProperties(target, source);
        }

        /// <inheritdoc/>
        public void Merge(Style target, IStyle source)
        {
            // style properties
            if (source.Background.Flag == StyleFlag.Explicit)
            {
                target.Background = source.Background;
            }

            if (source.BackgroundColor.Flag == StyleFlag.Explicit)
            {
                target.BackgroundColor = source.BackgroundColor;
            }

            if (source.BackgroundSlicing.Flag == StyleFlag.Explicit)
            {
                target.BackgroundSlicing = source.BackgroundSlicing;
            }

            if (source.Bottom.Flag == StyleFlag.Explicit)
            {
                target.Bottom = source.Bottom;
            }

            if (source.Color.Flag == StyleFlag.Explicit)
            {
                target.Color = source.Color;
            }

            if (source.Display.Flag == StyleFlag.Explicit)
            {
                target.Display = source.Display;
            }

            if (source.FontFamily.Flag == StyleFlag.Explicit)
            {
                target.FontFamily = source.FontFamily;
            }

            if (source.FontSize.Flag == StyleFlag.Explicit)
            {
                target.FontSize = source.FontSize;
            }

            if (source.FontStyle.Flag == StyleFlag.Explicit)
            {
                target.FontStyle = source.FontStyle;
            }

            if (source.Height.Flag == StyleFlag.Explicit)
            {
                target.Height = source.Height;
            }

            if (source.HorizontalAlignment.Flag == StyleFlag.Explicit)
            {
                target.HorizontalAlignment = source.HorizontalAlignment;
            }

            if (source.HorizontalItemSpacing.Flag == StyleFlag.Explicit)
            {
                target.HorizontalItemSpacing = source.HorizontalItemSpacing;
            }

            if (source.HorizontalItemAlignment.Flag == StyleFlag.Explicit)
            {
                target.HorizontalItemAlignment = source.HorizontalItemAlignment;
            }

            if (source.Left.Flag == StyleFlag.Explicit)
            {
                target.Left = source.Left;
            }

            if (source.MarginBottom.Flag == StyleFlag.Explicit)
            {
                target.MarginBottom = source.MarginBottom;
            }

            if (source.MarginLeft.Flag == StyleFlag.Explicit)
            {
                target.MarginLeft = source.MarginLeft;
            }

            if (source.MarginRight.Flag == StyleFlag.Explicit)
            {
                target.MarginRight = source.MarginRight;
            }

            if (source.MarginTop.Flag == StyleFlag.Explicit)
            {
                target.MarginTop = source.MarginTop;
            }

            if (source.MaximumHeight.Flag == StyleFlag.Explicit)
            {
                target.MaximumHeight = source.MaximumHeight;
            }

            if (source.MaximumWidth.Flag == StyleFlag.Explicit)
            {
                target.MaximumWidth = source.MaximumWidth;
            }

            if (source.MinimumHeight.Flag == StyleFlag.Explicit)
            {
                target.MinimumHeight = source.MinimumHeight;
            }

            if (source.MinimumWidth.Flag == StyleFlag.Explicit)
            {
                target.MinimumWidth = source.MinimumWidth;
            }

            if (source.Overflow.Flag == StyleFlag.Explicit)
            {
                target.Overflow = source.Overflow;
            }

            if (source.OverflowClippingMode.Flag == StyleFlag.Explicit)
            {
                target.OverflowClippingMode = source.OverflowClippingMode;
            }

            if (source.PaddingBottom.Flag == StyleFlag.Explicit)
            {
                target.PaddingBottom = source.PaddingBottom;
            }

            if (source.PaddingLeft.Flag == StyleFlag.Explicit)
            {
                target.PaddingLeft = source.PaddingLeft;
            }

            if (source.PaddingRight.Flag == StyleFlag.Explicit)
            {
                target.PaddingRight = source.PaddingRight;
            }

            if (source.PaddingTop.Flag == StyleFlag.Explicit)
            {
                target.PaddingTop = source.PaddingTop;
            }

            if (source.Position.Flag == StyleFlag.Explicit)
            {
                target.Position = source.Position;
            }

            if (source.Right.Flag == StyleFlag.Explicit)
            {
                target.Right = source.Right;
            }

            if (source.Top.Flag == StyleFlag.Explicit)
            {
                target.Top = source.Top;
            }

            if (source.VerticalAlignment.Flag == StyleFlag.Explicit)
            {
                target.VerticalAlignment = source.VerticalAlignment;
            }

            if (source.VerticalItemAlignment.Flag == StyleFlag.Explicit)
            {
                target.VerticalItemAlignment = source.VerticalItemAlignment;
            }

            if (source.VerticalItemSpacing.Flag == StyleFlag.Explicit)
            {
                target.VerticalItemSpacing = source.VerticalItemSpacing;
            }

            if (source.Visibility.Flag == StyleFlag.Explicit)
            {
                target.Visibility = source.Visibility;
            }

            if (source.Width.Flag == StyleFlag.Explicit)
            {
                target.Width = source.Width;
            }

            // custom properties
            CopyCustomProperties(target, source);
        }

        /// <inheritdoc/>
        public void Update(Style style, StyleProperties properties, ITheme theme)
        {
            foreach (var property in properties)
            {
                try
                {
                    // custom property?
                    if (property.Key.StartsWith(StyleConstants.CustomPropertyPrefix))
                    {
                        style.CustomProperties[property.Key] = property.Value;
                        continue;
                    }

                    // parse style property
                    switch (property.Key)
                    {
                        case StyleConstants.PropertyNames.Background:
                            var textureUrl = valueParser.ParseUrl(property.Value, style.CustomProperties);
                            var textureName = ResolveTextureName(textureUrl, theme);
                            var texture = textureRepository.Get(textureName);
                            style.Background = new StyleTexture(texture);
                            break;

                        case StyleConstants.PropertyNames.BackgroundColor:
                            var backgroundColor = valueParser.ParseColor(property.Value, style.CustomProperties);
                            style.BackgroundColor = new StyleColor(backgroundColor);
                            break;

                        case StyleConstants.PropertyNames.BackgroundSlicing:
                            var backgroundSlicing = valueParser.ParseBorder(property.Value, style.CustomProperties);
                            style.BackgroundSlicing = new StyleBorder(backgroundSlicing);
                            break;

                        case StyleConstants.PropertyNames.Bottom:
                            var bottom = valueParser.ParseUnit(property.Value, style.CustomProperties, true);
                            style.Bottom = new StyleUnit(bottom);
                            break;

                        case StyleConstants.PropertyNames.Color:
                            var color = valueParser.ParseColor(property.Value, style.CustomProperties);
                            style.Color = new StyleColor(color);
                            break;

                        case StyleConstants.PropertyNames.Display:
                            var display = valueParser.ParseEnum<DisplayStyle>(property.Value, style.CustomProperties);
                            style.Display = new StyleEnum<DisplayStyle>(display);
                            break;

                        case StyleConstants.PropertyNames.FontFamily:
                            var fontFamily = valueParser.ParseLiteral(property.Value, style.CustomProperties);
                            style.FontFamily = new StyleString(fontFamily);
                            break;

                        case StyleConstants.PropertyNames.FontSize:
                            var fontSize = valueParser.ParseUnit(property.Value, style.CustomProperties, false);
                            style.FontSize = new StyleFloat(fontSize.Value);
                            break;

                        case StyleConstants.PropertyNames.FontStyle:
                            var fontStyle = valueParser.ParseEnum<FontStyle>(property.Value, style.CustomProperties);
                            style.FontStyle = new StyleEnum<FontStyle>(fontStyle);
                            break;

                        case StyleConstants.PropertyNames.Height:
                            var height = valueParser.ParseUnit(property.Value, style.CustomProperties, true);
                            style.Height = new StyleUnit(height);
                            break;

                        case StyleConstants.PropertyNames.HorizontalAlignment:
                            var horizontalAlignment = valueParser.ParseEnum<Alignment>(property.Value, style.CustomProperties);
                            style.HorizontalAlignment = new StyleEnum<Alignment>(horizontalAlignment);
                            break;

                        case StyleConstants.PropertyNames.HorizontalItemAlignment:
                            var horizontalItemAlignment = valueParser.ParseEnum<ItemAlignment>(property.Value, style.CustomProperties);
                            style.HorizontalItemAlignment = new StyleEnum<ItemAlignment>(horizontalItemAlignment);
                            break;

                        case StyleConstants.PropertyNames.HorizontalItemSpacing:
                            var horizontalItemSpacing = valueParser.ParseUnit(property.Value, style.CustomProperties, false);
                            style.HorizontalItemSpacing = new StyleFloat(horizontalItemSpacing.Value);
                            break;

                        case StyleConstants.PropertyNames.ItemSpacing:
                            var itemSpacing = valueParser.ParseUnit(property.Value, style.CustomProperties, false);
                            style.HorizontalItemSpacing = new StyleFloat(itemSpacing.Value);
                            style.VerticalItemSpacing = new StyleFloat(itemSpacing.Value);
                            break;

                        case StyleConstants.PropertyNames.Left:
                            var left = valueParser.ParseUnit(property.Value, style.CustomProperties, true);
                            style.Left = new StyleUnit(left);
                            break;

                        case StyleConstants.PropertyNames.Margin:
                            var margin = valueParser.ParseBorder(property.Value, style.CustomProperties);
                            style.MarginBottom = new StyleFloat(margin.Bottom);
                            style.MarginLeft = new StyleFloat(margin.Left);
                            style.MarginRight = new StyleFloat(margin.Right);
                            style.MarginTop = new StyleFloat(margin.Top);
                            break;

                        case StyleConstants.PropertyNames.MarginBottom:
                            var marginBottom = valueParser.ParseUnit(property.Value, style.CustomProperties, false);
                            style.MarginBottom = new StyleFloat(marginBottom.Value);
                            break;

                        case StyleConstants.PropertyNames.MarginLeft:
                            var marginLeft = valueParser.ParseUnit(property.Value, style.CustomProperties, false);
                            style.MarginLeft = new StyleFloat(marginLeft.Value);
                            break;

                        case StyleConstants.PropertyNames.MarginRight:
                            var marginRight = valueParser.ParseUnit(property.Value, style.CustomProperties, false);
                            style.MarginRight = new StyleFloat(marginRight.Value);
                            break;

                        case StyleConstants.PropertyNames.MarginTop:
                            var marginTop = valueParser.ParseUnit(property.Value, style.CustomProperties, false);
                            style.MarginTop = new StyleFloat(marginTop.Value);
                            break;

                        case StyleConstants.PropertyNames.MaximumHeight:
                            var maximumHeight = valueParser.ParseUnit(property.Value, style.CustomProperties, false);
                            style.MaximumHeight = new StyleFloat(maximumHeight.Value);
                            break;

                        case StyleConstants.PropertyNames.MaximumWidth:
                            var maximumWidth = valueParser.ParseUnit(property.Value, style.CustomProperties, false);
                            style.MaximumWidth = new StyleFloat(maximumWidth.Value);
                            break;

                        case StyleConstants.PropertyNames.MinimumHeight:
                            var minimumHeight = valueParser.ParseUnit(property.Value, style.CustomProperties, false);
                            style.MinimumHeight = new StyleFloat(minimumHeight.Value);
                            break;

                        case StyleConstants.PropertyNames.MinimumWidth:
                            var minimumWidth = valueParser.ParseUnit(property.Value, style.CustomProperties, false);
                            style.MinimumWidth = new StyleFloat(minimumWidth.Value);
                            break;

                        case StyleConstants.PropertyNames.Overflow:
                            var overFlow = valueParser.ParseEnum<Overflow>(property.Value, style.CustomProperties);
                            style.Overflow = new StyleEnum<Overflow>(overFlow);
                            break;

                        case StyleConstants.PropertyNames.OverflowClippingMode:
                            var overflowClippingMode = valueParser.ParseEnum<OverflowClippingMode>(property.Value, style.CustomProperties);
                            style.OverflowClippingMode = new StyleEnum<OverflowClippingMode>(overflowClippingMode);
                            break;

                        case StyleConstants.PropertyNames.Padding:
                            var padding = valueParser.ParseBorder(property.Value, style.CustomProperties);
                            style.PaddingBottom = new StyleFloat(padding.Bottom);
                            style.PaddingLeft = new StyleFloat(padding.Left);
                            style.PaddingRight = new StyleFloat(padding.Right);
                            style.PaddingTop = new StyleFloat(padding.Top);
                            break;

                        case StyleConstants.PropertyNames.PaddingBottom:
                            var paddingBottom = valueParser.ParseUnit(property.Value, style.CustomProperties, false);
                            style.PaddingBottom = new StyleFloat(paddingBottom.Value);
                            break;

                        case StyleConstants.PropertyNames.PaddingLeft:
                            var paddingLeft = valueParser.ParseUnit(property.Value, style.CustomProperties, false);
                            style.PaddingLeft = new StyleFloat(paddingLeft.Value);
                            break;

                        case StyleConstants.PropertyNames.PaddingRight:
                            var paddingRight = valueParser.ParseUnit(property.Value, style.CustomProperties, false);
                            style.PaddingRight = new StyleFloat(paddingRight.Value);
                            break;

                        case StyleConstants.PropertyNames.PaddingTop:
                            var paddingTop = valueParser.ParseUnit(property.Value, style.CustomProperties, false);
                            style.PaddingTop = new StyleFloat(paddingTop.Value);
                            break;

                        case StyleConstants.PropertyNames.Position:
                            var position = valueParser.ParseEnum<Position>(property.Value, style.CustomProperties);
                            style.Position = new StyleEnum<Position>(position);
                            break;

                        case StyleConstants.PropertyNames.Right:
                            var right = valueParser.ParseUnit(property.Value, style.CustomProperties, true);
                            style.Right = new StyleUnit(right);
                            break;

                        case StyleConstants.PropertyNames.Top:
                            var top = valueParser.ParseUnit(property.Value, style.CustomProperties, true);
                            style.Top = new StyleUnit(top);
                            break;

                        case StyleConstants.PropertyNames.VerticalAlignment:
                            var verticalAlignment = valueParser.ParseEnum<Alignment>(property.Value, style.CustomProperties);
                            style.VerticalAlignment = new StyleEnum<Alignment>(verticalAlignment);
                            break;

                        case StyleConstants.PropertyNames.VerticalItemAlignment:
                            var verticalItemAlignment = valueParser.ParseEnum<ItemAlignment>(property.Value, style.CustomProperties);
                            style.VerticalItemAlignment = new StyleEnum<ItemAlignment>(verticalItemAlignment);
                            break;

                        case StyleConstants.PropertyNames.VerticalItemSpacing:
                            var verticalItemSpacing = valueParser.ParseUnit(property.Value, style.CustomProperties, false);
                            style.VerticalItemSpacing = new StyleFloat(verticalItemSpacing.Value);
                            break;


                        case StyleConstants.PropertyNames.Visibility:
                            var visibility = valueParser.ParseEnum<Visibility>(property.Value, style.CustomProperties);
                            style.Visibility = new StyleEnum<Visibility>(visibility);
                            break;

                        case StyleConstants.PropertyNames.Width:
                            var width = valueParser.ParseUnit(property.Value, style.CustomProperties, true);
                            style.Width = new StyleUnit(width);
                            break;

                        default:
                            logger.Warning($"Unknown '{property.Key}' property in '{theme.Id}' " +
                                $"theme at '{style.Selector}' style. Skipped");
                            break;
                    }
                }
                catch (Exception exception)
                {
                    logger.Error(exception, $"Unable to parse '{property.Key}' property in '{theme.Id}' theme at '{style.Selector}' style. Skipped");
                }
            }
        }


        private static void CopyCustomProperties(Style target, IStyle source)
        {
            if (source.CustomProperties == null)
            {
                return;
            }

            var customProperties = target.CustomProperties;
            foreach (var pair in source.CustomProperties)
            {
                customProperties[pair.Key] = pair.Value;
            }
        }

        private string ResolveTextureName(string textureUrl, ITheme theme)
        {
            if (textureUrl.StartsWith(pathResolver.PathSeparator))
            {
                return textureUrl.Substring(1);
            }

            return String.Format(ThemeConstants.TextureNameFormatString, theme.Id, textureUrl);
        }
    }
}
