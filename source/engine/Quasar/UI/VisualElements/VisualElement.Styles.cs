//-----------------------------------------------------------------------
// <copyright file="VisualElement.Styles.cs" company="Space Development">
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
using Quasar.UI.VisualElements.Styles;
using Quasar.UI.VisualElements.Themes;

namespace Quasar.UI.VisualElements
{
    /// <summary>
    /// Represents a basic UI visual element - Styles.
    /// </summary>
    public partial class VisualElement
    {
        private static Font CreateFont(IStyle style)
        {
            // get font attributes fron the style
            var hasFontAttribute = false;
            var fontFamily = defaultFont.Family.Id;
            if (style.FontFamily.Flag == StyleFlag.Explicit && !String.IsNullOrEmpty(style.FontFamily.Value))
            {
                fontFamily = style.FontFamily.Value;
                hasFontAttribute = fontFamily != defaultFont.Family.Id;
            }

            var fontSize = defaultFont.Size;
            if (style.FontSize.Flag == StyleFlag.Explicit && style.FontSize.Value > 0.0f)
            {
                fontSize = (int)style.FontSize.Value;
                hasFontAttribute = hasFontAttribute || fontSize != defaultFont.Size;
            }

            var fontStyle = defaultFont.Style;
            if (style.FontStyle.Flag == StyleFlag.Explicit)
            {
                fontStyle = style.FontStyle.Value;
                hasFontAttribute = hasFontAttribute || fontStyle != defaultFont.Style;
            }

            if (!hasFontAttribute)
            {
                return defaultFont;
            }

            return new Font(fontFamily, fontSize, fontStyle);
        }

        private void MergeStyles(ITheme theme)
        {
            // initialize merged style
            mergedStyle ??= context.StylePool.Allocate();
            styleBuilder.Copy(mergedStyle, theme.RootStyle);

            // element name selector
            var themeStyle = theme.GetStyleByTag(TagSelector, PseudoClass.Default);
            if (themeStyle != null)
            {
                styleBuilder.Merge(mergedStyle, themeStyle);
            }

            // process class style list
            foreach (var classSelector in classes)
            {
                themeStyle = theme.GetStyleByClass(classSelector, PseudoClass.Default);
                if (themeStyle == null)
                {
                    continue;
                }

                styleBuilder.Merge(mergedStyle, themeStyle);
            }

            // merge name style
            if (!String.IsNullOrEmpty(Name))
            {
                themeStyle = theme.GetStyleByName(Name, PseudoClass.Default);
                if (themeStyle != null)
                {
                    styleBuilder.Merge(mergedStyle, themeStyle);
                }
            }

            // merge inline style
            if (InlineStyle != null)
            {
                styleBuilder.Merge(mergedStyle, InlineStyle);
            }
        }

        private void MergePseudoClassStyle(ITheme theme)
        {
            // initialize final style with the merged style
            style ??= context.StylePool.Allocate();
            styleBuilder.Copy(style, mergedStyle);


            // merge pseudo class styles
            if (pseudoClass == PseudoClass.Default)
            {
                return;
            }

            // element name selector
            var themeStyle = theme.GetStyleByTag(TagSelector, pseudoClass);
            if (themeStyle != null)
            {
                styleBuilder.Merge(style, themeStyle);
            }

            // process class style list
            foreach (var classSelector in classes)
            {
                themeStyle = theme.GetStyleByClass(classSelector, pseudoClass);
                if (themeStyle == null)
                {
                    continue;
                }

                styleBuilder.Merge(style, themeStyle);
            }

            // merge name style
            if (!String.IsNullOrEmpty(Name))
            {
                themeStyle = theme.GetStyleByName(Name, pseudoClass);
                if (themeStyle != null)
                {
                    styleBuilder.Merge(style, themeStyle);
                }
            }
        }

        private void ResolveStyleProperties()
        {
            // background
            if (style.Background.Value != null)
            {
                Background = new Sprite(style.Background.Value, style.BackgroundSlicing.Value);
            }
            else
            {
                Background = default;
            }

            // background color
            BackgroundColor = style.BackgroundColor.Flag != StyleFlag.NotSet ?
                style.BackgroundColor.Value :
                Color.White;

            // color
            Color = style.Color.Flag != StyleFlag.NotSet ?
                style.Color.Value :
                Color.White;

            // font
            Font = CreateFont(style);

            // overflow
            Overflow = style.Overflow.Flag != StyleFlag.NotSet ?
                style.Overflow.Value :
                Overflow.Visible;

            OverflowClippingMode = style.OverflowClippingMode.Flag != StyleFlag.NotSet ?
                style.OverflowClippingMode.Value :
                OverflowClippingMode.PaddingBox;

            // position
            Position = style.Position.Flag != StyleFlag.NotSet ?
                style.Position.Value :
                Position.Relative;

            // alignments
            HorizontalAlignment = style.HorizontalAlignment.Flag != StyleFlag.NotSet ?
                style.HorizontalAlignment.Value :
                Alignment.Start;

            HorizontalItemAlignment = style.HorizontalItemAlignment.Flag != StyleFlag.NotSet ?
                style.HorizontalItemAlignment.Value :
                ItemAlignment.Start;

            VerticalAlignment = style.VerticalAlignment.Flag != StyleFlag.NotSet ?
                style.VerticalAlignment.Value :
                Alignment.End;

            VerticalItemAlignment = style.VerticalItemAlignment.Flag != StyleFlag.NotSet ?
                style.VerticalItemAlignment.Value :
                ItemAlignment.End;

            // item spacing
            ItemSpacing = new Vector2(style.HorizontalItemSpacing.Value, style.VerticalItemSpacing.Value);

            // min/max size
            var maximumWidth = style.MaximumWidth.Flag == StyleFlag.Explicit ?
                style.MaximumWidth.Value :
                Single.MaxValue;
            var minimumWidth = style.MinimumWidth.Flag == StyleFlag.Explicit ?
                style.MinimumWidth.Value :
                0.0f;
            if (minimumWidth > maximumWidth)
            {
                var tempWidth = maximumWidth;
                maximumWidth = minimumWidth;
                minimumWidth = tempWidth;
            }

            var maximumHeight = style.MaximumHeight.Flag == StyleFlag.Explicit ?
                style.MaximumHeight.Value :
                Single.MaxValue;
            var minimumHeight = style.MinimumHeight.Flag == StyleFlag.Explicit ?
                style.MinimumHeight.Value :
                0.0f;
            if (minimumHeight > maximumHeight)
            {
                var tempHeight = maximumHeight;
                maximumHeight = minimumHeight;
                minimumHeight = tempHeight;
            }

            MaximumSize = new Vector2(maximumWidth, maximumHeight);
            MinimumSize = new Vector2(minimumWidth, minimumHeight);

            // diplay & visibility
            Display = style.Display.Flag != StyleFlag.NotSet ?
                style.Display.Value :
                DisplayStyle.Display;

            Visibility = style.Visibility.Flag != StyleFlag.NotSet ?
                style.Visibility.Value :
                Visibility.Visible;
        }
    }
}
