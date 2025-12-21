//-----------------------------------------------------------------------
// <copyright file="Rectangle.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.UI.VisualElements
{
    /// <summary>
    /// Rectangular area structure.
    /// </summary>
    /// <seealso cref="IEquatable{Rectangle}" />
    public readonly struct Rectangle : IEquatable<Rectangle>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle" /> struct.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="bottom">The bottom.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Rectangle(float left, float bottom, float width, float height)
        {
            Position = new Vector2(left, bottom);
            Size = new Vector2(width, height);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle" /> struct.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="size">The size.</param>
        public Rectangle(in Vector2 position, in Vector2 size)
        {
            Position = position;
            Size = size;
        }


        /// <summary>
        /// The position (bottom-left corner).
        /// </summary>
        public readonly Vector2 Position;

        /// <summary>
        /// The size.
        /// </summary>
        public readonly Vector2 Size;


        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        public static bool operator ==(in Rectangle a, in Rectangle b)
        {
            return a.Position == b.Position && a.Size == b.Size;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        public static bool operator !=(in Rectangle a, in Rectangle b)
        {
            return a.Position != b.Position || a.Size != b.Size;
        }


        /// <summary>
        /// Determines whether the specified position is in the rectagle (including the borders).
        /// </summary>
        /// <param name="position">The position.</param>
        public bool Contains(in Vector2 position)
        {
            return position.X >= Position.X &&
                position.X - Position.X < Size.X &&
                position.Y >= Position.Y &&
                position.Y - Position.Y < Size.Y;
        }

        /// <summary>
        /// Determines whether the specified position (x, y) is in the rectagle (including the borders).
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public bool Contains(float x, float y)
        {
            return x >= Position.X &&
                x - Position.X < Size.X &&
                y >= Position.Y &&
                y - Position.Y < Size.Y;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is not Rectangle other)
            {
                return false;
            }

            return this == other;
        }

        /// <inheritdoc/>
        public bool Equals(Rectangle other)
        {
            return this == other;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Position.GetHashCode(), Size.GetHashCode());
        }

        /// <summary>
        /// Extends the rectangle with the specified size.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>
        /// The extended rectangle.
        /// </returns>
        public Rectangle Extend(in Vector2 size)
        {
            return new Rectangle(
                Position.X - size.X,
                Position.Y - size.Y,
                Size.X + 2 * size.X,
                Size.Y + 2 * size.Y);
        }

        /// <summary>
        /// Extends the rectangle with the specified border.
        /// </summary>
        /// <param name="border">The border.</param>
        /// <returns>
        /// The extended rectangle.
        /// </returns>
        public Rectangle Extend(in Border border)
        {
            return new Rectangle(
                Position.X - border.Left,
                Position.Y - border.Bottom,
                Size.X + border.Width,
                Size.Y + border.Height);
        }

        /// <summary>
        /// Shrinks the rectangle with the specified size.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>
        /// The shrinked rectangle.
        /// </returns>
        public Rectangle Shrink(in Vector2 size)
        {
            return new Rectangle(
                Position.X + size.X,
                Position.Y + size.Y,
                Size.X - 2 * size.X,
                Size.Y - 2 * size.Y);
        }

        /// <summary>
        /// Shrinks the rectangle with the specified border.
        /// </summary>
        /// <param name="border">The border.</param>
        /// <returns>
        /// The shrinked rectangle.
        /// </returns>
        public Rectangle Shrink(in Border border)
        {
            return new Rectangle(
                Position.X + border.Left,
                Position.Y + border.Bottom,
                Size.X - border.Width,
                Size.Y - border.Height);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"(Position:{Position}, Size:{Size})";
        }
    }
}
