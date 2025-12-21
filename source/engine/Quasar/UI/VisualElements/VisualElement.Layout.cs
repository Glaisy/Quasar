//-----------------------------------------------------------------------
// <copyright file="VisualElement.Layout.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Quasar.UI.VisualElements.Styles;

namespace Quasar.UI.VisualElements
{
    /// <summary>
    /// Represents a basic UI visual element - Layout.
    /// </summary>
    public partial class VisualElement
    {
        /// <summary>
        /// Internal content size calculation wrapper.
        /// </summary>
        internal Vector2 CalculateContentSize()
        {
            // get raw content size
            var rawSize = CalculateRawContentSize();
            var width = rawSize.X;
            var height = rawSize.Y;

            // apply explicit width and height pixel values
            if (Style.Width.Flag == StyleFlag.Explicit && Style.Width.Value.Type == UnitType.Pixel)
            {
                width = Style.Width.Value.Value;
            }

            if (Style.Height.Flag == StyleFlag.Explicit && Style.Height.Value.Type == UnitType.Pixel)
            {
                height = Style.Height.Value.Value;
            }

            return new Vector2(width, height);
        }

        /// <summary>
        /// Sets the visual element bounds.
        /// </summary>
        /// <param name="boundingBox">The bounding box.</param>
        /// <param name="paddingBox">The padding box.</param>
        /// <param name="contentBox">The content box.</param>
        internal void SetBounds(in Rectangle boundingBox, in Rectangle paddingBox, in Rectangle contentBox)
        {
            // change tracking
            var hasChanges = ContentBox != contentBox;

            // update bounding, padding and content box
            BoundingBox = boundingBox;
            PaddingBox = paddingBox;
            ContentBox = contentBox;
            var relativePosition = boundingBox.Position + paddingBox.Position;

            // update absolute position
            var oldAbsolutePosition = AbsolutePosition;
            AbsolutePosition = relativePosition;
            if (Parent != null)
            {
                AbsolutePosition += Parent.AbsolutePosition + Parent.ContentBox.Position;
            }

            hasChanges = hasChanges || AbsolutePosition != oldAbsolutePosition;

            // update canvas position
            if (createCanvas)
            {
                CanvasPosition = Vector2.Zero;
                return;
            }

            CanvasPosition = relativePosition;
            if (Parent != null)
            {
                CanvasPosition += Parent.CanvasPosition + Parent.ContentBox.Position;
            }

            // invalidate canvas and content alignment
            Invalidate(InvalidationFlags.Canvas | InvalidationFlags.ContentAlignment);

            // invalidate child layouts if there were significant changes
            if (hasChanges)
            {
                foreach (var child in children)
                {
                    child.Invalidate(InvalidationFlags.Layout);
                }
            }
        }


        /// <summary>
        /// Gets the aligned text position of the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        protected Vector2 GetAlignedTextPosition(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return default;
            }

            var textSize = Font.MeasureString(text);

            // horizontal position
            var x = ContentBox.Position.X;
            switch (HorizontalAlignment)
            {
                case Alignment.Center:
                    x += 0.5f * (ContentBox.Size.X - textSize.X);
                    break;

                case Alignment.End:
                    x += ContentBox.Size.X - textSize.X;
                    break;
            }

            // vertical position
            var y = ContentBox.Position.Y;
            switch (VerticalAlignment)
            {
                case Alignment.Center:
                    y += 0.5f * (ContentBox.Size.Y - textSize.Y);
                    break;

                case Alignment.End:
                    y += ContentBox.Size.Y - textSize.Y;
                    break;
            }

            return new Vector2(x, y);
        }

        /// <summary>
        /// Calculates the raw content size of the visual element without margin, padding, size constraints and child elements.
        /// </summary>
        protected virtual Vector2 CalculateRawContentSize()
        {
            if (String.IsNullOrEmpty(text))
            {
                return Vector2.Zero;
            }

            return font.MeasureString(text);
        }

        /// <summary>
        /// Updates the content's alignment.
        /// </summary>
        protected virtual void UpdateContentAlignment()
        {
            textPosition = GetAlignedTextPosition(text);
        }

        /// <summary>
        /// Updates the pseudo class of the visual element by the internal states.
        /// </summary>
        protected virtual PseudoClass GetPseudoClass()
        {
            if (!IsEnabled)
            {
                return PseudoClass.Disabled;
            }

            return PointerOver ? PseudoClass.Hover : PseudoClass.Default;
        }
    }
}
