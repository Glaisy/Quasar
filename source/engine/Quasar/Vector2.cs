//-----------------------------------------------------------------------
// <copyright file="Vector2.cs" company="Space Development">
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
    /// Single precision 2D space vector structure implementation (Immutable).
    /// </summary>
    /// <seealso cref="IEquatable{Vector2}" />
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [JsonConverter(typeof(Vector2JsonConverter))]
    public readonly struct Vector2 : IEquatable<Vector2>
    {
        /// <summary>
        /// The x coordinate.
        /// </summary>
        public readonly float X;

        /// <summary>
        /// The y coordinate.
        /// </summary>
        public readonly float Y;


        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2"/> struct.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2"/> struct.
        /// </summary>
        /// <param name="source">The source Vector2 instance.</param>
        public Vector2(in Vector2 source)
        {
            X = source.X;
            Y = source.Y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2"/> struct.
        /// </summary>
        /// <param name="source">The source Vector2 instance.</param>
        public Vector2(in Vector2D source)
        {
            X = (float)source.X;
            Y = (float)source.Y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2" /> struct.
        /// </summary>
        /// <param name="value">The x and y coordinate value.</param>
        public Vector2(float value)
        {
            X = Y = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2" /> struct.
        /// </summary>
        /// <param name="value">The x and y coordinate value.</param>
        public Vector2(double value)
        {
            X = Y = (float)value;
        }


        /// <summary>
        /// The negative X vector instance.
        /// </summary>
        public static readonly Vector2 NegativeX = new Vector2(-1.0f, 0.0f);

        /// <summary>
        /// The negative Y vector instance.
        /// </summary>
        public static readonly Vector2 NegativeY = new Vector2(0.0f, -1.0f);

        /// <summary>
        /// The non-normalized unit vector instance.
        /// </summary>
        public static readonly Vector2 One = new Vector2(1.0f, 1.0f);

        /// <summary>
        /// The pozitive X vector instance.
        /// </summary>
        public static readonly Vector2 PositiveX = new Vector2(1.0f, 0.0f);

        /// <summary>
        /// The positive Y vector instance.
        /// </summary>
        public static readonly Vector2 PositiveY = new Vector2(0.0f, 1.0f);

        /// <summary>
        /// The zero vector instance.
        /// </summary>
        public static readonly Vector2 Zero = default;


        /// <summary>
        /// Gets the length of the vector.
        /// </summary>
        public float Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => MathF.Sqrt(X * X + Y * Y);
        }

        /// <summary>
        /// Gets the squared length of the vector.
        /// </summary>
        public float LengthSquared
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
        public static implicit operator Vector2(in Vector2D vector)
        {
            return new Vector2((float)vector.X, (float)vector.Y);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator -(in Vector2 value)
        {
            return new Vector2(-value.X, -value.Y);
        }

        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        /// <returns>The sum of the 2 vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator +(in Vector2 a, in Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        /// <returns>The difference of the 2 vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator -(in Vector2 a, in Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        /// <summary>
        /// Implements the * operator with Vector2.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b scalar.</param>
        /// <returns>The vector multiplied by the scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator *(in Vector2 a, in Vector2 b)
        {
            return new Vector2(a.X * b.X, a.Y * b.Y);
        }

        /// <summary>
        /// Implements the * operator with scalar.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b scalar.</param>
        /// <returns>The vector multiplied by the scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator *(in Vector2 a, float b)
        {
            return new Vector2(a.X * b, a.Y * b);
        }

        /// <summary>
        /// Implements the * operator with scalar.
        /// </summary>
        /// <param name="a">The a scalar.</param>
        /// <param name="b">The b vector.</param>
        /// <returns>The vector multiplied by the scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator *(float a, in Vector2 b)
        {
            return new Vector2(b.X * a, b.Y * a);
        }

        /// <summary>
        /// Implements the * operator with Vector2.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b scalar.</param>
        /// <returns>The vector multiplied by the scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator /(in Vector2 a, in Vector2 b)
        {
            return new Vector2(a.X / b.X, a.Y / b.Y);
        }

        /// <summary>
        /// Implements the / operator with scalar.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b scalar.</param>
        /// <returns>The vector multiplied by the scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator /(in Vector2 a, float b)
        {
            return new Vector2(a.X / b, a.Y / b);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(in Vector2 a, in Vector2 b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(in Vector2 a, in Vector2 b)
        {
            return a.X != b.X || a.Y != b.Y;
        }


        /// <summary>
        /// Calculates the scalar product of the current and v vectors.
        /// </summary>
        /// <param name="v">The v vector.</param>
        /// <returns>The result of the scalar product.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Dot(in Vector2 v)
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
        public static float Dot(in Vector2 a, in Vector2 b)
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
        public bool Equals(Vector2 other)
        {
            return X == other.X && Y == other.Y;
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            if (obj is Vector2 other)
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
        public Vector2 Normalize()
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
        public static Vector2 Slerp(in Vector2 a, in Vector2 b, float t)
        {
            t = Ranges.FloatUnit.Clamp(t);
            return SlerpUnclamped(a, b, t);
        }

        /// <summary>
        /// Calculates the spherical interpolated value between a and b. The interpolation parameter is not clamped into the [0...1] range.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <param name="t">The interpolation parameter.</param>
        public static Vector2 SlerpUnclamped(in Vector2 a, in Vector2 b, float t)
        {
            var theta = a.Dot(b);
            var sinTheta = MathF.Sin(theta);
            var t1 = MathF.Sin((1.0f - t) * theta) / sinTheta;
            var t2 = MathF.Sin(t * theta) / sinTheta;
            return new Vector2(t1 * a.X + t2 * b.X, t1 * a.Y + t2 * b.Y);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"({X.ToString(CultureInfo.InvariantCulture)}, {Y.ToString(CultureInfo.InvariantCulture)})";
        }
    }
}
