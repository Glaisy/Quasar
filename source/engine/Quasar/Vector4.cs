//-----------------------------------------------------------------------
// <copyright file="Vector4.cs" company="Space Development">
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
using Space.Core.Mathematics;

namespace Quasar
{
    /// <summary>
    /// Single precision 4D space vector structure implementation. (right-handed coordinate system) (Immutable).
    /// </summary>
    /// <seealso cref="IEquatable{Vector4}" />
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [JsonConverter(typeof(Vector4JsonConverter))]
    public readonly struct Vector4 : IEquatable<Vector4>
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
        /// The z coordinate.
        /// </summary>
        public readonly float Z;

        /// <summary>
        /// The w coordinate.
        /// </summary>
        public readonly float W;


        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4" /> struct.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        /// <param name="w">The w coordinate.</param>
        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4" /> struct.
        /// </summary>
        /// <param name="source">The source Vector2 instance.</param>
        /// <param name="z">The z coordinate.</param>
        /// <param name="w">The w coordinate.</param>
        public Vector4(in Vector2 source, float z = 0.0f, float w = 0.0f)
        {
            X = source.X;
            Y = source.Y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4" /> struct.
        /// </summary>
        /// <param name="source">The source Vector3 instance.</param>
        /// <param name="w">The w coordinate.</param>
        public Vector4(in Vector3 source, float w = 0.0f)
        {
            X = source.X;
            Y = source.Y;
            Z = source.Z;
            W = w;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4" /> struct.
        /// </summary>
        /// <param name="source">The source Vector4 instance.</param>
        public Vector4(in Vector4D source)
        {
            X = (float)source.X;
            Y = (float)source.Y;
            Z = (float)source.Z;
            W = (float)source.W;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4" /> struct.
        /// </summary>
        /// <param name="value">The x, y, z, w coordinate value.</param>
        public Vector4(float value)
        {
            X = Y = Z = W = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4" /> struct.
        /// </summary>
        /// <param name="value">The x, y, z, w coordinate value.</param>
        public Vector4(double value)
        {
            X = Y = Z = W = (float)value;
        }


        /// <summary>
        /// The -X vector instance.
        /// </summary>
        public static readonly Vector4 NegativeX = new Vector4(-1.0f, 0.0f, 0.0f, 0.0f);

        /// <summary>
        /// The -Y vector instance.
        /// </summary>
        public static readonly Vector4 NegativeY = new Vector4(0.0f, -1.0f, 0.0f, 0.0f);

        /// <summary>
        /// The -Z vector instance.
        /// </summary>
        public static readonly Vector4 NegativeZ = new Vector4(0.0f, 0.0f, -1.0f, 0.0f);

        /// <summary>
        /// The -W vector instance.
        /// </summary>
        public static readonly Vector4 NegativeW = new Vector4(0.0f, 0.0f, 0.0f, -1.0f);

        /// <summary>
        /// The non-normalized unit vector instance.
        /// </summary>
        public static readonly Vector4 One = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);

        /// <summary>
        /// The +X vector instance.
        /// </summary>
        public static readonly Vector4 PositiveX = new Vector4(1.0f, 0.0f, 0.0f, 0.0f);

        /// <summary>
        /// The +Y vector instance.
        /// </summary>
        public static readonly Vector4 PositiveY = new Vector4(0.0f, 1.0f, 0.0f, 0.0f);

        /// <summary>
        /// The +Z vector instance.
        /// </summary>
        public static readonly Vector4 PositiveZ = new Vector4(0.0f, 0.0f, 1.0f, 0.0f);

        /// <summary>
        /// The +W vector instance.
        /// </summary>
        public static readonly Vector4 PositiveW = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);

        /// <summary>
        /// The zero vector instance.
        /// </summary>
        public static readonly Vector4 Zero = default;


        /// <summary>
        /// Gets the length of the vector.
        /// </summary>
        public float Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => MathF.Sqrt(X * X + Y * Y + Z * Z + W * W);
        }

        /// <summary>
        /// Gets the squared length of the vector.
        /// </summary>
        public float LengthSquared
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => X * X + Y * Y + Z * Z + W * W;
        }


        /// <summary>
        /// Performs an implicit conversion from <see cref="Vector4D"/> to <see cref="Vector4"/>.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector4(in Vector4D vector)
        {
            return new Vector4((float)vector.X, (float)vector.Y, (float)vector.Z, (float)vector.W);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 operator -(in Vector4 value)
        {
            return new Vector4(-value.X, -value.Y, -value.Z, -value.W);
        }

        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        /// <returns>The sum of the 2 vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 operator +(in Vector4 a, in Vector4 b)
        {
            return new Vector4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        /// <returns>The difference of the 2 vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 operator -(in Vector4 a, in Vector4 b)
        {
            return new Vector4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b scalar.</param>
        /// <returns>The vector multiplied by the scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 operator *(in Vector4 a, in Vector4 b)
        {
            return new Vector4(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);
        }

        /// <summary>
        /// Implements the * operator with scalar.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b scalar.</param>
        /// <returns>The vector multiplied by the scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 operator *(in Vector4 a, float b)
        {
            return new Vector4(a.X * b, a.Y * b, a.Z * b, a.W * b);
        }

        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b scalar.</param>
        /// <returns>The vector multiplied by the scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 operator /(in Vector4 a, in Vector4 b)
        {
            return new Vector4(a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.W / b.W);
        }

        /// <summary>
        /// Implements the * operator with scalar.
        /// </summary>
        /// <param name="a">The a scalar.</param>
        /// <param name="b">The b vector.</param>
        /// <returns>The vector multiplied by the scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 operator *(float a, in Vector4 b)
        {
            return new Vector4(b.X * a, b.Y * a, b.Z * a, b.W * a);
        }

        /// <summary>
        /// Implements the / operator with scalar.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b scalar.</param>
        /// <returns>The vector multiplied by the scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 operator /(in Vector4 a, float b)
        {
            return new Vector4(a.X / b, a.Y / b, a.Z / b, a.W / b);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(in Vector4 a, in Vector4 b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.W == b.W;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(in Vector4 a, in Vector4 b)
        {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z || a.W != b.W;
        }


        /// <summary>
        /// Calculates the scalar product of the current and v vectors.
        /// </summary>
        /// <param name="v">The v vector.</param>
        /// <returns>The result of the scalar product.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Dot(in Vector4 v)
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
        public static float Dot(in Vector4 a, in Vector4 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector4 other)
        {
            return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            if (obj is Vector4 other)
            {
                return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
            }

            return false;
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z, W);
        }

        /// <summary>
        /// Calculates the linear interpolated value between a and b. The interpolation parameter is clamped to the [0...1] range.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <param name="t">The interpolation parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 LerpClamped(in Vector4 a, in Vector4 b, float t)
        {
            t = Ranges.FloatUnit.Clamp(t);
            return LerpUnclamped(a, b, t);
        }

        /// <summary>
        /// Calculates the linear interpolated value between a and b. The interpolation parameter is unclamped.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <param name="t">The interpolation parameter.</param>
        public static Vector4 LerpUnclamped(in Vector4 a, in Vector4 b, float t)
        {
            return new Vector4(
                MathematicsHelper.LerpUnclamped(a.X, b.X, t),
                MathematicsHelper.LerpUnclamped(a.Y, b.Y, t),
                MathematicsHelper.LerpUnclamped(a.Z, b.Z, t),
                MathematicsHelper.LerpUnclamped(a.W, b.W, t));
        }

        /// <summary>
        /// Calculates a normalized vector from the current vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4 Normalize()
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
        public static Vector4 Slerp(in Vector4 a, in Vector4 b, float t)
        {
            t = Ranges.FloatUnit.Clamp(t);
            return SlerpUnclamped(a, b, t);
        }

        /// <summary>
        /// Calculates the spherical interpolated value between a and b. The interpolation parameter is clamped not to the [0...1] range.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <param name="t">The interpolation parameter.</param>
        public static Vector4 SlerpUnclamped(in Vector4 a, in Vector4 b, float t)
        {
            var theta = a.Dot(b);
            var sinTheta = MathF.Sin(theta);
            var t1 = MathF.Sin((1.0f - t) * theta) / sinTheta;
            var t2 = MathF.Sin(t * theta) / sinTheta;
            return new Vector4(t1 * a.X + t2 * b.X, t1 * a.Y + t2 * b.Y, t1 * a.Z + t2 * b.Z, t1 * a.W + t2 * b.W);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"({X.ToString(CultureInfo.InvariantCulture)}, {Y.ToString(CultureInfo.InvariantCulture)}, {Z.ToString(CultureInfo.InvariantCulture)}, {W.ToString(CultureInfo.InvariantCulture)})";
        }
    }
}
