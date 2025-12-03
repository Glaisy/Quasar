//-----------------------------------------------------------------------
// <copyright file="Vector2D.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

using Quasar.IO.Serialization.Json;

using Space.Core;

namespace Quasar
{
    /// <summary>
    /// Double precision 2D space vector structure implementation.
    /// </summary>
    /// <seealso cref="IEquatable{Vector2D}" />
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [JsonConverter(typeof(Vector2DJsonConverter))]
    public readonly struct Vector2D : IEquatable<Vector2D>
    {
        /// <summary>
        /// The x coordinate.
        /// </summary>
        public readonly double X;

        /// <summary>
        /// The y coordinate.
        /// </summary>
        public readonly double Y;


        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D"/> struct.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D"/> struct.
        /// </summary>
        /// <param name="source">The source Vector2D instance.</param>
        public Vector2D(in Vector2D source)
        {
            X = source.X;
            Y = source.Y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D"/> struct.
        /// </summary>
        /// <param name="source">The source Vector2D instance.</param>
        public Vector2D(in Vector2 source)
        {
            X = source.X;
            Y = source.Y;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D" /> struct.
        /// </summary>
        /// <param name="value">The x and y coordinate value.</param>
        public Vector2D(float value)
        {
            X = Y = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D" /> struct.
        /// </summary>
        /// <param name="value">The x and y coordinate value.</param>
        public Vector2D(double value)
        {
            X = Y = value;
        }


        /// <summary>
        /// The negative X vector instance.
        /// </summary>
        public static readonly Vector2D NegativeX = new Vector2D(-1.0, 0.0);

        /// <summary>
        /// The negative Y vector instance.
        /// </summary>
        public static readonly Vector2D NegativeY = new Vector2D(0.0, -1.0);

        /// <summary>
        /// The non-normalized unit vector instance.
        /// </summary>
        public static readonly Vector2D One = new Vector2D(1.0, 1.0);

        /// <summary>
        /// The positive X vector instance.
        /// </summary>
        public static readonly Vector2D PositiveX = new Vector2D(1.0, 0.0);

        /// <summary>
        /// The positive Y vector instance.
        /// </summary>
        public static readonly Vector2D PositiveY = new Vector2D(0.0, 1.0);

        /// <summary>
        /// The zero vector instance.
        /// </summary>
        public static readonly Vector2D Zero = default;


        /// <summary>
        /// Gets the length of the vector.
        /// </summary>
        public double Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Math.Sqrt(X * X + Y * Y);
        }

        /// <summary>
        /// Gets the squared length of the vector.
        /// </summary>
        public double LengthSquared
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => X * X + Y * Y;
        }


        /// <summary>
        /// Performs an implicit conversion from <see cref="Vector3D"/> to <see cref="Vector3"/>.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector2D(in Vector2 vector)
        {
            return new Vector2D(vector.X, vector.Y);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator -(in Vector2D value)
        {
            return new Vector2D(-value.X, -value.Y);
        }

        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        /// <returns>The sum of the 2 vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator +(in Vector2D a, in Vector2D b)
        {
            return new Vector2D(a.X + b.X, a.Y + b.Y);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        /// <returns>The difference of the 2 vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator -(in Vector2D a, in Vector2D b)
        {
            return new Vector2D(a.X - b.X, a.Y - b.Y);
        }

        /// <summary>
        /// Implements the * operator with Vector2D.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b scalar.</param>
        /// <returns>The vector multiplied by the scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator *(in Vector2D a, in Vector2D b)
        {
            return new Vector2D(a.X * b.X, a.Y * b.Y);
        }

        /// <summary>
        /// Implements the * operator with scalar.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b scalar.</param>
        /// <returns>The vector multiplied by the scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator *(in Vector2D a, double b)
        {
            return new Vector2D(a.X * b, a.Y * b);
        }

        /// <summary>
        /// Implements the * operator with scalar.
        /// </summary>
        /// <param name="a">The a scalar.</param>
        /// <param name="b">The b vector.</param>
        /// <returns>The vector multiplied by the scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator *(double a, in Vector2D b)
        {
            return new Vector2D(b.X * a, b.Y * a);
        }

        /// <summary>
        /// Implements the / operator with Vector2D.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b scalar.</param>
        /// <returns>The vector multiplied by the scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator /(in Vector2D a, in Vector2D b)
        {
            return new Vector2D(a.X / b.X, a.Y / b.Y);
        }

        /// <summary>
        /// Implements the / operator with scalar.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b scalar.</param>
        /// <returns>The vector multiplied by the scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator /(in Vector2D a, double b)
        {
            return new Vector2D(a.X / b, a.Y / b);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(in Vector2D a, in Vector2D b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(in Vector2D a, in Vector2D b)
        {
            return a.X != b.X || a.Y != b.Y;
        }


        /// <summary>
        /// Calculates the scalar product of the current and v vectors.
        /// </summary>
        /// <param name="v">The v vector.</param>
        /// <returns>The result of the scalar product.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Dot(Vector2D v)
        {
            return Dot(this, v);
        }

        /// <summary>
        /// Calculates the scalar product of the a and b vectors.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        /// <returns>
        /// The result of the scalar product.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Dot(in Vector2D a, in Vector2D b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector2D other)
        {
            return X == other.X && Y == other.Y;
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            if (obj is Vector2D other)
            {
                return X == other.X && Y == other.Y;
            }

            return false;
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        /// <summary>
        /// Calculates a normalized vector from the current vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2D Normalize()
        {
            return this / Length;
        }

        /// <summary>
        /// Calculates the spherical interpolated value between a and b. The interpolation parameter is clamped to the [0...1] range.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <param name="t">The interpolation parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Slerp(in Vector2D a, in Vector2D b, double t)
        {
            t = Ranges.DoubleUnit.Clamp(t);
            return SlerpUnclamped(a, b, t);
        }

        /// <summary>
        /// Calculates the spherical interpolated value between a and b. The interpolation parameter is not clamped to the [0...1] range.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <param name="t">The interpolation parameter.</param>
        public static Vector2D SlerpUnclamped(in Vector2D a, in Vector2D b, double t)
        {
            var theta = a.Dot(b);
            var sinTheta = Math.Sin(theta);
            var t1 = Math.Sin((1.0 - t) * theta) / sinTheta;
            var t2 = Math.Sin(t * theta) / sinTheta;
            return new Vector2D(t1 * a.X + t2 * b.X, t1 * a.Y + t2 * b.Y);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"({X.ToString(CultureInfo.InvariantCulture)}, {Y.ToString(CultureInfo.InvariantCulture)})";
        }
    }
}
