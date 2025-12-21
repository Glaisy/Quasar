//-----------------------------------------------------------------------
// <copyright file="LayoutManagerBase.cs" company="Space Development">
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

using Quasar.Graphics;
using Quasar.Rendering;
using Quasar.UI.VisualElements.Styles;

using Space.Core;

namespace Quasar.UI.VisualElements.Layouts.Internals
{
    /// <summary>
    /// Abstract base class for UI layout managers.
    /// </summary>
    internal abstract class LayoutManagerBase : ILayoutManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutManagerBase" /> class.
        /// </summary>
        /// <param name="renderingContext">The rendering context.</param>
        protected LayoutManagerBase(IRenderingContext renderingContext)
        {
            PrimaryFrameBuffer = renderingContext.PrimaryFrameBuffer;
        }


        /// <summary>
        /// Gets the layout type.
        /// </summary>
        public abstract LayoutType LayoutType { get; }


        /// <summary>
        /// Arranges the children of the specified visual element.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        public void Arrange(VisualElement visualElement)
        {
            Assertion.ThrowIfNull(visualElement, nameof(visualElement));

            Vector2 availableSize;

            // displayed?
            if (visualElement.Display == DisplayMode.None)
            {
                // no, done.
                return;
            }

            // absolute?
            if (visualElement.Position == Position.Absolute)
            {
                // yes, positioning inside the primary canvas
                availableSize = new Vector2(PrimaryFrameBuffer.Size.Width, PrimaryFrameBuffer.Size.Height);
                LayoutHelper.SetAbsolutePosition(visualElement, availableSize);
            }

            // arrange children into the available space
            ArrangeChildren(visualElement, visualElement.ContentBox.Size);
        }

        /// <summary>
        /// Calculates the preferred size of the specified visual element.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        public Vector2 CalculatePreferredBoundingBoxSize(VisualElement visualElement)
        {
            Assertion.ThrowIfNull(visualElement, nameof(visualElement));

            // estimate the visual element's content's size and child layout's size.
            var contentSize = visualElement.CalculateContentSize();
            var estimatedLayoutSize = EstimateLayoutSize(visualElement);

            // get merged padding box size
            var x = Math.Max(0, Math.Max(estimatedLayoutSize.X, contentSize.X));
            var y = Math.Max(0, Math.Max(estimatedLayoutSize.Y, contentSize.Y));
            x += GetHorizontalPadding(visualElement);
            y += GetVerticalPadding(visualElement);

            // apply min-max constraints
            x = Math.Max(0, Math.Max(visualElement.MinimumSize.X, Math.Min(x, visualElement.MaximumSize.X)));
            y = Math.Max(0, Math.Max(visualElement.MinimumSize.Y, Math.Min(y, visualElement.MaximumSize.Y)));

            // add margins
            x += GetHorizontalMargin(visualElement);
            y += GetVerticalMargin(visualElement);

            return new Vector2(x, y);
        }


        /// <summary>
        /// The primary framebuffer.
        /// </summary>
        protected readonly IFrameBuffer PrimaryFrameBuffer;


        /// <summary>
        /// Arranges the visual element's children inside the available layout size.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        /// <param name="availableSize">Available size for layout.</param>
        protected abstract void ArrangeChildren(VisualElement visualElement, in Vector2 availableSize);

        /// <summary>
        /// Estimates the visual element's children layout size.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        protected abstract Vector2 EstimateLayoutSize(VisualElement visualElement);


        private static float GetHorizontalMargin(VisualElement visualElement)
        {
            return visualElement.Style.MarginLeft.Value + visualElement.Style.MarginRight.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float GetHorizontalPadding(VisualElement visualElement)
        {
            return visualElement.Style.PaddingLeft.Value + visualElement.Style.PaddingRight.Value;
        }

        private static float GetVerticalMargin(VisualElement visualElement)
        {
            return visualElement.Style.MarginBottom.Value + visualElement.Style.MarginTop.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float GetVerticalPadding(VisualElement visualElement)
        {
            return visualElement.Style.PaddingBottom.Value + visualElement.Style.PaddingTop.Value;
        }
    }
}
