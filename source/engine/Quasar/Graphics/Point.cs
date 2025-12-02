//-----------------------------------------------------------------------
// <copyright file="Point.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

using Quasar.Graphics.Json;

namespace Quasar.Graphics
{
    /// <summary>
    /// 2D point structure for integer operations (Immutable).
    /// </summary>
    /// <seealso cref="IEquatable{Point}" />
    [JsonConverter(typeof(PointJsonConverter))]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct Point : IEquatable<Point>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> struct.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public Point(int value)
        {
            X = Y = value;
        }


        /// <summary>
        /// The x value.
        /// </summary>
        public readonly int X;

        /// <summary>
        /// The y value.
        /// </summary>
        public readonly int Y;

        /// <summary>
        /// The empty value.
        /// </summary>
        public static readonly Point Empty;


        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(in Point a, in Point b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(in Point a, in Point b)
        {
            return a.X != b.X || a.Y != b.Y;
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Point operator +(in Point a, in Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Point operator +(in Point a, in Size b)
        {
            return new Point(a.X + b.Width, a.Y + b.Height);
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Point operator +(in Point a, int b)
        {
            return new Point(a.X + b, a.Y + b);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Point operator -(in Point a, in Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Point operator -(in Point a, in Size b)
        {
            return new Point(a.X - b.Width, a.Y - b.Height);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Point operator -(in Point a, int b)
        {
            return new Point(a.X - b, a.Y - b);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Size"/> to <see cref="System.Drawing.Size"/>.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator System.Drawing.Point(in Point point)
        {
            return new System.Drawing.Point(point.X, point.Y);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Drawing.Point"/> to <see cref="Point"/>.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Point(in System.Drawing.Point point)
        {
            return new Point(point.X, point.Y);
        }


        /// <summary>
        /// Adds the specified value to this instance.
        /// </summary>
        /// <param name="point">The point value.</param>
        /// <returns>The result of operation.</returns>
        public Point Add(in Point point)
        {
            return new Point(X + point.X, Y + point.Y);
        }

        /// <summary>
        /// Adds the specified value to this instance.
        /// </summary>
        /// <param name="size">The size value.</param>
        /// <returns>The result of operation.</returns>
        public Point Add(in Size size)
        {
            return new Point(X + size.Width, Y + size.Height);
        }

        /// <summary>
        /// Adds the specified value to this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of operation.</returns>
        public Point Add(int value)
        {
            return new Point(X + value, Y + value);
        }

        /// <inheritdoc/>
        public bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is Point point && Equals(point);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        /// <summary>
        /// Sustracts the specified value from this instance.
        /// </summary>
        /// <param name="point">The point value.</param>
        /// <returns>The result of operation.</returns>
        public Point Substract(in Point point)
        {
            return new Point(X - point.X, Y - point.Y);
        }

        /// <summary>
        /// Sustracts the specified value from this instance.
        /// </summary>
        /// <param name="size">The size value.</param>
        /// <returns>The result of operation.</returns>
        public Point Substract(in Size size)
        {
            return new Point(X - size.Width, Y - size.Height);
        }

        /// <summary>
        /// Sustracts the specified value from this instance.
        /// </summary>
        /// <param name="value">The size value.</param>
        /// <returns>The result of operation.</returns>
        public Point Substract(int value)
        {
            return new Point(X - value, Y - value);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"(X:{X}, Y:{Y})";
        }
    }
}
