//-----------------------------------------------------------------------
// <copyright file="VerticalStackLayoutManager.cs" company="Space Development">
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
    /// UI grid layout manager implementation.
    /// </summary>
    /// <seealso cref="LayoutManagerBase" />
    [Export(typeof(ILayoutManager), nameof(LayoutType.VerticalStack))]
    [Singleton]
    internal sealed class VerticalStackLayoutManager : LayoutManagerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VerticalStackLayoutManager" /> class.
        /// </summary>
        /// <param name="renderingContext">The rendering context.</param>
        public VerticalStackLayoutManager(IRenderingContext renderingContext)
            : base(renderingContext)
        {
        }


        /// <summary>
        /// Gets the layout type.
        /// </summary>
        public override LayoutType LayoutType => LayoutType.VerticalStack;


        /// <summary>
        /// Arranges the visual element's children inside the available layout size.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        /// <param name="availableSize">Available size for layout.</param>
        protected override void ArrangeChildren(VisualElement visualElement, in Vector2 availableSize)
        {
            var occupiedHeight = CalculateOccupiedHeight(visualElement.Children, availableSize.Y, out var gapCount);

            // calculate spacing and y position
            var spacing = 0.0f;
            var top = 0.0f;
            switch (visualElement.VerticalItemAlignment)
            {
                case ItemAlignment.SpaceBetween:
                    spacing = (availableSize.Y - occupiedHeight) / gapCount;
                    top = availableSize.Y;
                    break;

                case ItemAlignment.Start:
                    spacing = visualElement.ItemSpacing.Y;
                    top = occupiedHeight + spacing * gapCount;
                    break;

                case ItemAlignment.Center:
                    spacing = visualElement.ItemSpacing.Y;
                    var offsetY = (availableSize.Y - occupiedHeight - spacing * gapCount) * 0.5f;
                    top = occupiedHeight + offsetY;
                    break;

                default:
                    spacing = visualElement.ItemSpacing.Y;
                    top = availableSize.Y;
                    break;
            }

            // calculate total vertical percentages and fixed heights
            var totalPercentages = 0.0f;
            var fixedHeights = gapCount * visualElement.ItemSpacing.Y;
            foreach (var childElement in visualElement.Children)
            {
                if (!LayoutHelper.IsInLayout(childElement))
                {
                    continue;
                }

                var styleHeight = childElement.Style.Height;
                if (styleHeight.Flag != StyleFlag.Explicit)
                {
                    fixedHeights += childElement.PreferredSize.Y;
                    continue;
                }

                if (styleHeight.Value.Type == UnitType.Percentage)
                {
                    totalPercentages += styleHeight.Value.Value;
                }
                else
                {
                    fixedHeights += styleHeight.Value.Value;
                }
            }

            // iterate child elements
            var pixelsPerPercentage = totalPercentages > 0.0f ? (availableSize.Y - fixedHeights) / totalPercentages : 0.0f;
            foreach (var childElement in visualElement.Children)
            {
                if (!LayoutHelper.IsInLayout(childElement))
                {
                    continue;
                }

                // align element
                var height = LayoutHelper.GetFinalHeight(childElement, pixelsPerPercentage, availableSize.Y);
                var bottom = top - height;
                LayoutHelper.AlignHorizontally(
                    childElement,
                    visualElement.HorizontalItemAlignment,
                    availableSize,
                    out var left,
                    out var width);

                // update the size and the relative position of the visual element
                LayoutHelper.SetRelativePositionAndSize(
                    childElement,
                    new Vector2(left, bottom),
                    new Vector2(width, height));

                // next element
                top = bottom - spacing;
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
            var spacing = visualElement.ItemSpacing.Y;
            foreach (var childElement in visualElement.Children)
            {
                if (!LayoutHelper.IsInLayout(childElement))
                {
                    continue;
                }

                if (childElement.HorizontalAlignment == Alignment.Stretched)
                {
                    x = Math.Max(y, childElement.MaximumSize.X);
                }
                else
                {
                    x = Math.Max(x, childElement.PreferredSize.X);
                }

                y += Math.Max(0, childElement.PreferredSize.Y) + spacing;
            }

            return new Vector2(x, y);
        }


        private static float CalculateOccupiedHeight(IEnumerable<VisualElement> visualElements, float availableHeight, out int gapCount)
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
                occupiedSpace += visualElement.PreferredSize.Y;
            }

            if (gapCount > 0)
            {
                gapCount--;
            }

            return Math.Max(0, Math.Min(availableHeight, occupiedSpace));
        }
    }
}
