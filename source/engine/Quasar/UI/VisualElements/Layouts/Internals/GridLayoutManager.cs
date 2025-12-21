//-----------------------------------------------------------------------
// <copyright file="GridLayoutManager.cs" company="Space Development">
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
using System.Linq;
using System.Runtime.CompilerServices;

using Quasar.Rendering;

using Space.Core.DependencyInjection;

namespace Quasar.UI.VisualElements.Layouts.Internals
{
    /// <summary>
    /// UI grid layout manager implementation.
    /// </summary>
    /// <seealso cref="LayoutManagerBase" />
    [Export(typeof(ILayoutManager), nameof(LayoutType.Grid))]
    [Singleton]
    internal sealed class GridLayoutManager : LayoutManagerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GridLayoutManager" /> class.
        /// </summary>
        /// <param name="renderingContext">The rendering context.</param>
        public GridLayoutManager(IRenderingContext renderingContext)
            : base(renderingContext)
        {
        }


        /// <summary>
        /// Gets the layout type.
        /// </summary>
        public override LayoutType LayoutType => LayoutType.Grid;


        /// <summary>
        /// Arranges the children inside the available space.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        /// <param name="availableSize">Size of the available.</param>
        protected override void ArrangeChildren(VisualElement visualElement, in Vector2 availableSize)
        {
            // update columns and rows
            UpdateColumns(visualElement.Columns, availableSize.X, visualElement.ItemSpacing.X);
            UpdateRows(visualElement.Rows, availableSize.Y, visualElement.ItemSpacing.Y);

            // arrange child elements
            var columns = visualElement.Columns;
            var rows = visualElement.Rows;
            foreach (var childElement in visualElement.Children)
            {
                if (!LayoutHelper.IsInLayout(childElement))
                {
                    continue;
                }

                if (childElement.Name == "XXXX")
                {
                }

                // get cell bounds (columns and rows included by the span values)
                var cellLeft = 0.0f;
                var cellRight = availableSize.X;
                var cellBottom = 0.0f;
                var cellTop = availableSize.Y;

                if (columns.Count > 0)
                {
                    var columnStartIndex = ClampIndex(childElement.Column, columns.Count);
                    var columnEndIndex = ClampIndex(columnStartIndex + childElement.ColumnSpan - 1, columns.Count);
                    cellLeft = columns[columnStartIndex].Left;
                    cellRight = columns[columnEndIndex].Right;
                }

                if (rows.Count > 0)
                {
                    var rowStartIndex = ClampIndex(childElement.Row, rows.Count);
                    var rowEndIndex = ClampIndex(rowStartIndex + childElement.RowSpan - 1, rows.Count);
                    cellBottom = rows[rowEndIndex].Bottom;
                    cellTop = rows[rowStartIndex].Top;
                }

                var cellSize = new Vector2(cellRight - cellLeft, cellTop - cellBottom);

                // align visual element inside the cell bounds
                LayoutHelper.AlignHorizontally(
                    childElement,
                    visualElement.HorizontalItemAlignment,
                    cellSize,
                    out var left,
                    out var width);

                LayoutHelper.AlignVertically(
                    childElement,
                    visualElement.VerticalItemAlignment,
                    cellSize,
                    out var bottom,
                    out var height);

                var boundingBox = new Rectangle(cellLeft + left, cellBottom + bottom, width, height);

                // apply bounds
                LayoutHelper.SetRelativePosition(childElement, boundingBox);
            }
        }

        /// <summary>
        /// Estimates the visual element's children layout size.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        protected override Vector2 EstimateLayoutSize(VisualElement visualElement)
        {
            var x = EstimateLayoutWidth(visualElement.Columns, visualElement.Children, visualElement.ItemSpacing.X);
            var y = EstimateLayoutHeight(visualElement.Rows, visualElement.Children, visualElement.ItemSpacing.Y);

            return new Vector2(x, y);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int ClampIndex(int index, int count)
        {
            return Math.Max(0, Math.Min(index, count - 1));
        }

        private static float EstimateLayoutWidth(IReadOnlyList<GridColumn> columns, IEnumerable<VisualElement> visualElements, float spacing)
        {
            // easy case
            if (columns.Count == 0)
            {
                return 0.0f;
            }

            // estimate column widths
            foreach (var column in columns)
            {
                column.EstimatedWidth = column.DefinedWidth.Type == GridLengthType.Star ? 0.0f : column.DefinedWidth.Value;
            }

            foreach (var visualElement in visualElements)
            {
                // sum the stars for the involved columns
                var totalStars = 0.0f;
                var startIndex = ClampIndex(visualElement.Column, columns.Count);
                var endIndex = ClampIndex(visualElement.Column + visualElement.ColumnSpan - 1, columns.Count);
                for (var i = startIndex; i <= endIndex; i++)
                {
                    var column = columns[i];
                    if (column.DefinedWidth.Type == GridLengthType.Pixel)
                    {
                        continue;
                    }

                    totalStars += column.DefinedWidth.Value;
                }

                // estimate starred columns' width by sharing the visual element's preferred width proportionally
                if (totalStars == 0.0f)
                {
                    continue;
                }

                var preferredWidth = visualElement.PreferredSize.X / totalStars;
                for (var i = startIndex; i <= endIndex; i++)
                {
                    var column = columns[i];
                    if (column.DefinedWidth.Type == GridLengthType.Pixel)
                    {
                        continue;
                    }

                    column.EstimatedWidth = Math.Max(preferredWidth * column.DefinedWidth.Value, column.EstimatedWidth);
                }
            }

            // cumultate column widths
            var totalSpacing = columns.Count > 0 ? (columns.Count - 1) * spacing : 0.0f;
            return totalSpacing + columns.Sum(column => column.EstimatedWidth);
        }

        private static float EstimateLayoutHeight(IReadOnlyList<GridRow> rows, IEnumerable<VisualElement> visualElements, float spacing)
        {
            // easy case
            if (rows.Count == 0)
            {
                return 0.0f;
            }

            // estimate row heights
            foreach (var row in rows)
            {
                row.EstimatedHeight = row.DefinedHeight.Type == GridLengthType.Star ? 0.0f : row.DefinedHeight.Value;
            }

            foreach (var visualElement in visualElements)
            {
                // sum the stars for the involved rows
                var totalStars = 0.0f;
                var startIndex = ClampIndex(visualElement.Row, rows.Count);
                var endIndex = ClampIndex(visualElement.Row + visualElement.RowSpan - 1, rows.Count);
                for (var i = startIndex; i <= endIndex; i++)
                {
                    var row = rows[i];
                    if (row.DefinedHeight.Type == GridLengthType.Pixel)
                    {
                        continue;
                    }

                    totalStars += row.DefinedHeight.Value;
                }

                // estimate starred rows' height by sharing the visual element's preferred height proportionally
                if (totalStars == 0.0f)
                {
                    continue;
                }

                var preferredHeight = visualElement.PreferredSize.Y / totalStars;
                for (var i = startIndex; i <= endIndex; i++)
                {
                    var row = rows[i];
                    if (row.DefinedHeight.Type == GridLengthType.Pixel)
                    {
                        continue;
                    }

                    row.EstimatedHeight = Math.Max(preferredHeight * row.DefinedHeight.Value, row.EstimatedHeight);
                }
            }

            // cummulate row heights
            var totalSpacing = rows.Count > 0 ? (rows.Count - 1) * spacing : 0.0f;
            return totalSpacing + rows.Sum(row => row.EstimatedHeight);
        }

        private static void UpdateColumns(IReadOnlyList<GridColumn> columns, float availableWidth, float spacing)
        {
            // determine pixel width and total star values.
            var definedPixelWidths = columns.Count > 0 ? (columns.Count - 1) * spacing : 0.0f;
            var totalDefinedStars = 0.0f;
            foreach (var column in columns)
            {
                if (column.DefinedWidth.Type == GridLengthType.Pixel)
                {
                    definedPixelWidths += column.DefinedWidth.Value;
                }
                else
                {
                    totalDefinedStars += column.DefinedWidth.Value;
                }
            }

            // update columns
            var pixelScale = availableWidth > definedPixelWidths ? 1.0f : availableWidth / definedPixelWidths;
            var starScale = totalDefinedStars == 0.0 ? 0.0f : Math.Max(0.0f, (availableWidth - definedPixelWidths) / totalDefinedStars);
            var left = 0.0f;
            spacing *= pixelScale;
            foreach (var column in columns)
            {
                if (column.DefinedWidth.Type == GridLengthType.Pixel)
                {
                    column.Width = column.DefinedWidth.Value * pixelScale;
                }
                else
                {
                    column.Width = column.DefinedWidth.Value * starScale;
                }

                column.Left = left;
                column.Right = left + column.Width;

                left = column.Right + spacing;
            }
        }

        private static void UpdateRows(IReadOnlyList<GridRow> rows, float availableHeight, float spacing)
        {
            // determine pixel height and total star values.
            var definedPixelHeights = rows.Count > 0 ? (rows.Count - 1) * spacing : 0.0f;
            var totalDefinedStars = 0.0f;
            foreach (var row in rows)
            {
                if (row.DefinedHeight.Type == GridLengthType.Pixel)
                {
                    definedPixelHeights += row.DefinedHeight.Value;
                }
                else
                {
                    totalDefinedStars += row.DefinedHeight.Value;
                }
            }

            // update rows
            var pixelScale = availableHeight > definedPixelHeights ? 1.0f : availableHeight / definedPixelHeights;
            var starScale = totalDefinedStars == 0.0 ? 0.0f : Math.Max(0.0f, (availableHeight - definedPixelHeights) / totalDefinedStars);
            var top = availableHeight;
            spacing *= pixelScale;
            foreach (var row in rows)
            {
                if (row.DefinedHeight.Type == GridLengthType.Pixel)
                {
                    row.Height = row.DefinedHeight.Value * pixelScale;
                }
                else
                {
                    row.Height = row.DefinedHeight.Value * starScale;
                }

                row.Top = top;
                row.Bottom = top - row.Height;

                top = row.Bottom - spacing;
            }
        }
    }
}
