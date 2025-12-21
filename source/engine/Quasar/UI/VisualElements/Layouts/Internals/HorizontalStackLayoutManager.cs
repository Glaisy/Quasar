//-----------------------------------------------------------------------
// <copyright file="HorizontalStackLayoutManager.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Quasar.Rendering;
using Quasar.UI.VisualElements.Styles;

using Space.Core.DependencyInjection;

namespace Quasar.UI.VisualElements.Layouts.Internals
{
    /// <summary>
    /// UI horizontal stack layout manager implementation.
    /// </summary>
    /// <seealso cref="LayoutManagerBase" />
    [Export(typeof(ILayoutManager), nameof(LayoutType.HorizontalStack))]
    [Singleton]
    internal sealed class HorizontalStackLayoutManager : LayoutManagerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HorizontalStackLayoutManager" /> class.
        /// </summary>
        /// <param name="renderingContext">The rendering context.</param>
        public HorizontalStackLayoutManager(IRenderingContext renderingContext)
            : base(renderingContext)
        {
        }


        /// <summary>
        /// Gets the layout type.
        /// </summary>
        public override LayoutType LayoutType => LayoutType.HorizontalStack;


        /// <summary>
        /// Arranges the visual element's children inside the available layout size.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        /// <param name="availableSize">Available size for layout.</param>
        protected override void ArrangeChildren(VisualElement visualElement, in Vector2 availableSize)
        {
            var occupiedWidth = CalculateOccupiedWidth(visualElement.Children, availableSize.X, out var gapCount);

            // calculate spacing and x position
            var spacing = 0.0f;
            var left = 0.0f;
            switch (visualElement.HorizontalItemAlignment)
            {
                case ItemAlignment.SpaceBetween:
                    spacing = (availableSize.X - occupiedWidth) / gapCount;
                    break;

                case ItemAlignment.End:
                    spacing = visualElement.ItemSpacing.X;
                    left = availableSize.X - occupiedWidth - spacing * gapCount;
                    break;

                case ItemAlignment.Center:
                    spacing = visualElement.ItemSpacing.X;
                    left = (availableSize.X - occupiedWidth - spacing * gapCount) * 0.5f;
                    break;

                default:
                    spacing = visualElement.ItemSpacing.X;
                    break;
            }

            // calculate total horizontal percentages and fixed widths
            var totalPercentages = 0.0f;
            var fixedWidths = gapCount * visualElement.ItemSpacing.X;
            foreach (var childElement in visualElement.Children)
            {
                if (!LayoutHelper.IsInLayout(childElement))
                {
                    continue;
                }

                var styleWidth = childElement.Style.Width;
                if (styleWidth.Flag != StyleFlag.Explicit)
                {
                    fixedWidths += childElement.PreferredSize.X;
                    continue;
                }

                if (styleWidth.Value.Type == UnitType.Percentage)
                {
                    totalPercentages += styleWidth.Value.Value;
                }
                else
                {
                    fixedWidths += styleWidth.Value.Value;
                }
            }

            // iterate child elements
            var pixelsPerPercentage = totalPercentages > 0.0f ? (availableSize.X - fixedWidths) / totalPercentages : 0.0f;
            foreach (var childElement in visualElement.Children)
            {
                if (!LayoutHelper.IsInLayout(childElement))
                {
                    continue;
                }

                // align element
                var width = LayoutHelper.GetFinalWidth(childElement, pixelsPerPercentage, availableSize.X);
                LayoutHelper.AlignVertically(
                    childElement,
                    visualElement.VerticalItemAlignment,
                    availableSize,
                    out var bottom,
                    out var height);

                // update bounding box and relative position of the visual element
                var boundingBox = new Rectangle(left, bottom, width, height);
                LayoutHelper.SetRelativePosition(childElement, boundingBox);

                // next element
                left += width + spacing;
            }
        }

        /// <summary>
        /// Estimates the visual element's children layout size.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        protected override Vector2 EstimateLayoutSize(VisualElement visualElement)
        {
            var x = 0.0f;
            var y = 0.0f;
            var spacing = visualElement.ItemSpacing.X;
            foreach (var childElement in visualElement.Children)
            {
                if (!LayoutHelper.IsInLayout(childElement))
                {
                    continue;
                }

                x += Math.Max(0, childElement.PreferredSize.X) + spacing;
                if (childElement.VerticalAlignment == Alignment.Stretched)
                {
                    y = Math.Max(y, childElement.MaximumSize.Y);
                }
                else
                {
                    y = Math.Max(y, childElement.PreferredSize.Y);
                }
            }

            return new Vector2(x, y);
        }


        private static float CalculateOccupiedWidth(IEnumerable<VisualElement> visualElements, float availableWidth, out int gapCount)
        {
            gapCount = 0;
            var occupiedSpace = 0.0f;
            foreach (var visualElement in visualElements)
            {
                if (!LayoutHelper.IsInLayout(visualElement))
                {
                    continue;
                }

                gapCount++;
                occupiedSpace += visualElement.PreferredSize.X;
            }

            if (gapCount > 0)
            {
                gapCount--;
            }

            return Math.Max(0, Math.Min(availableWidth, occupiedSpace));
        }
    }
}
