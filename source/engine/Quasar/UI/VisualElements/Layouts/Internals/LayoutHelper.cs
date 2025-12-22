//-----------------------------------------------------------------------
// <copyright file="LayoutHelper.cs" company="Space Development">
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

using Quasar.UI.VisualElements.Styles;

namespace Quasar.UI.VisualElements.Layouts.Internals
{
    /// <summary>
    /// Layout helper method collection.
    /// </summary>
    internal static class LayoutHelper
    {
        /// <summary>
        /// Aligns the visual element vertically into the avialable space.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        /// <param name="horizontalItemAlignment">The horizontal item alignment.</param>
        /// <param name="availableSize">The available size.</param>
        /// <param name="left">The left position.</param>
        /// <param name="width">The width.</param>
        public static void AlignHorizontally(
            VisualElement visualElement,
            ItemAlignment horizontalItemAlignment,
            in Vector2 availableSize,
            out float left,
            out float width)
        {
            var pixelsPerPercentage = availableSize.X / 100.0f;
            width = GetFinalWidth(visualElement, pixelsPerPercentage, availableSize.X);
            switch (horizontalItemAlignment)
            {
                case ItemAlignment.Stretched:
                    left = 0.0f;
                    width = Math.Max(visualElement.MinimumSize.X, Math.Min(visualElement.MaximumSize.X, availableSize.X));
                    break;

                case ItemAlignment.End:
                    left = availableSize.X - width;
                    break;

                case ItemAlignment.Center:
                    left = 0.5f * (availableSize.X - width);
                    break;

                case ItemAlignment.Start:
                default:
                    left = 0.0f;
                    break;
            }
        }

        /// <summary>
        /// Aligns the visual element horizontally into the avialable space.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        /// <param name="verticalItemAlignment">The vertical item alignment.</param>
        /// <param name="availableSize">The available size.</param>
        /// <param name="bottom">The bottom position.</param>
        /// <param name="height">The height.</param>
        public static void AlignVertically(
            VisualElement visualElement,
            ItemAlignment verticalItemAlignment,
            in Vector2 availableSize,
            out float bottom,
            out float height)
        {
            var pixelsPerPercentage = availableSize.X / 100.0f;
            height = GetFinalHeight(visualElement, pixelsPerPercentage, availableSize.Y);
            switch (verticalItemAlignment)
            {
                case ItemAlignment.Stretched:
                    height = Math.Max(visualElement.MinimumSize.Y, Math.Min(visualElement.MaximumSize.Y, availableSize.Y));
                    bottom = availableSize.Y - height;
                    break;

                case ItemAlignment.Start:
                    bottom = 0.0f;
                    break;

                case ItemAlignment.Center:
                    bottom = 0.5f * (availableSize.Y - height);
                    break;

                case ItemAlignment.End:
                default:
                    bottom = availableSize.Y - height;
                    break;
            }
        }

        /// <summary>
        /// Gets the final height of the visual element~.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        /// <param name="pixelsPerPercentage">The pixels per percentage.</param>
        /// <param name="availableHeight">The available height.</param>
        public static float GetFinalHeight(VisualElement visualElement, float pixelsPerPercentage, float availableHeight)
        {
            // get preferred height
            var height = Math.Min(availableHeight, visualElement.PreferredSize.Y);

            // has explicit height?
            var styleHeight = visualElement.Style.Height;
            if (styleHeight.Flag == StyleFlag.Explicit && styleHeight.Value.Type == UnitType.Percentage)
            {
                height = styleHeight.Value.Value * pixelsPerPercentage;
            }

            // apply min-max constraints
            height = Math.Max(0, Math.Max(visualElement.MinimumSize.Y, Math.Min(height, visualElement.MaximumSize.Y)));
            return MathF.Ceiling(height);
        }

        /// <summary>
        /// Gets the final width of the visual element.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        /// <param name="pixelsPerPercentage">The pixels per percentage.</param>
        /// <param name="availableWidth">The available width.</param>
        public static float GetFinalWidth(VisualElement visualElement, float pixelsPerPercentage, float availableWidth)
        {
            // get preferred width
            var width = Math.Min(availableWidth, visualElement.PreferredSize.X);

            // has explicit width?
            var styleWidth = visualElement.Style.Width;
            if (styleWidth.Flag == StyleFlag.Explicit && styleWidth.Value.Type == UnitType.Percentage)
            {
                width = styleWidth.Value.Value * pixelsPerPercentage;
            }

            // apply min-max constraints
            width = Math.Max(0, Math.Max(visualElement.MinimumSize.X, Math.Min(width, visualElement.MaximumSize.X)));
            return MathF.Ceiling(width);
        }


        /// <summary>
        /// Determines whether the specified visual element is part of the layout or not.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInLayout(VisualElement visualElement)
        {
            return visualElement.Position == Position.Relative && visualElement.Display == DisplayStyle.Display;
        }

        /// <summary>
        /// Sets the absolute position of the visual element.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        /// <param name="availableSize">Size of the available.</param>
        public static void SetAbsolutePosition(VisualElement visualElement, in Vector2 availableSize)
        {
            var boundingBox = CalculateAbsoluteBoundingBox(visualElement.Style, availableSize);
            var paddingBox = CalculatePaddingBox(visualElement.Style, boundingBox.Size);
            var contentBox = CalculateContentBox(visualElement.Style, paddingBox.Size);
            visualElement.SetBounds(boundingBox, paddingBox, contentBox);
        }

        /// <summary>
        /// Sets the relative position of the visual element.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        /// <param name="boundingBox">The bounding box.</param>
        public static void SetRelativePosition(VisualElement visualElement, in Rectangle boundingBox)
        {
            var paddingBox = CalculatePaddingBox(visualElement.Style, boundingBox.Size);
            var contentBox = CalculateContentBox(visualElement.Style, paddingBox.Size);
            visualElement.SetBounds(boundingBox, paddingBox, contentBox);
        }


        private static Rectangle CalculateAbsoluteBoundingBox(IStyle style, in Vector2 availableSize)
        {
            var left = CalculateAbsoluteValue(style.Left.Value, availableSize.X);
            var bottom = CalculateAbsoluteValue(style.Bottom.Value, availableSize.Y);

            float width;
            if (style.Width.Flag == StyleFlag.Explicit)
            {
                width = CalculateAbsoluteValue(style.Width.Value, availableSize.X);
            }
            else
            {
                var right = CalculateAbsoluteValue(style.Right.Value, availableSize.X);
                width = availableSize.X - right - left;
            }

            float height;
            if (style.Height.Flag == StyleFlag.Explicit)
            {
                height = CalculateAbsoluteValue(style.Height.Value, availableSize.Y);
            }
            else
            {
                var top = CalculateAbsoluteValue(style.Top.Value, availableSize.Y);
                height = availableSize.Y - top - bottom;
            }

            return new Rectangle(left, bottom, width, height);
        }

        private static Rectangle CalculateContentBox(IStyle style, in Vector2 avaiableSize)
        {
            var paddingLeft = style.PaddingLeft.Value;
            var paddingRight = style.PaddingRight.Value;
            var paddingTop = style.PaddingTop.Value;
            var paddingBottom = style.PaddingBottom.Value;
            return new Rectangle(
                paddingLeft,
                paddingBottom,
                avaiableSize.X - paddingLeft - paddingRight,
                avaiableSize.Y - paddingBottom - paddingTop);
        }

        private static Rectangle CalculatePaddingBox(IStyle style, in Vector2 availableSize)
        {
            var marginLeft = style.MarginLeft.Value;
            var marginRight = style.MarginRight.Value;
            var marginTop = style.MarginTop.Value;
            var marginBottom = style.MarginBottom.Value;
            return new Rectangle(
                marginLeft,
                marginBottom,
                availableSize.X - marginLeft - marginRight,
                availableSize.Y - marginBottom - marginTop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float CalculateAbsoluteValue(Unit value, float referenceValue)
        {
            if (value.Type == UnitType.Pixel)
            {
                return value.Value;
            }

            return 0.01f * value.Value * referenceValue;
        }
    }
}
