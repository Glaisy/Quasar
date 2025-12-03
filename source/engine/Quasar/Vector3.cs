//-----------------------------------------------------------------------
// <copyright file="Vector3.cs" company="Space Development">
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
    /// Single precision 3D space vector structure implementation. (right-handed coordinate system) (Immutable).
    /// </summary>
    /// <seealso cref="IEquatable{Vector3}" />
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [JsonConverter(typeof(Vector3JsonConverter))]
    public readonly struct Vector3 : IEquatable<Vector3>
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
        /// Initializes a new instance of the <see cref="Vector3"/> struct.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3" /> struct.
        /// </summary>
        /// <param name="source">The source Vector2 instance.</param>
        /// <param name="z">The z coordinate.</param>
        public Vector3(in Vector2 source, float z = 0.0f)
        {
            X = source.X;
            Y = source.Y;
            Z = z;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3" /> struct.
        /// </summary>
        /// <param name="source">The source Vector3 instance.</param>
        public Vector3(in Vector3 source)
        {
            X = source.X;
            Y = source.Y;
            Z = source.Z;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3" /> struct.
        /// </summary>
        /// <param name="source">The source Vector3 instance.</param>
        public Vector3(in Vector3D source)
        {
            X = (float)source.X;
            Y = (float)source.Y;
            Z = (float)source.Z;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3" /> struct.
        /// </summary>
        /// <param name="value">The x, y, z coordinate value.</param>
        public Vector3(float value)
        {
            X = Y = Z = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3" /> struct.
        /// </summary>
        /// <param name="value">The x, y, z coordinate value.</param>
        public Vector3(double value)
        {
            X = Y = Z = (float)value;
        }


        /// <summary>
        /// The -X vector instance.
        /// </summary>
        public static readonly Vector3 NegativeX = new Vector3(-1.0f, 0.0f, 0.0f);

        /// <summary>
        /// The -Y vector instance.
        /// </summary>
        public static readonly Vector3 NegativeY = new Vector3(0.0f, -1.0f, 0.0f);

        /// <summary>
        /// The -Z vector instance.
        /// </summary>
        public static readonly Vector3 NegativeZ = new Vector3(0.0f, 0.0f, -1.0f);

        /// <summary>
        /// The non-normalized unit vector instance.
        /// </summary>
        public static readonly Vector3 One = new Vector3(1.0f, 1.0f, 1.0f);

        /// <summary>
        /// The +X vector instance.
        /// </summary>
        public static readonly Vector3 PositiveX = new Vector3(1.0f, 0.0f, 0.0f);

        /// <summary>
        /// The Y+ vector instance.
        /// </summary>
        public static readonly Vector3 PositiveY = new Vector3(0.0f, 1.0f, 0.0f);

        /// <summary>
        /// The +Z vector instance.
        /// </summary>
        public static readonly Vector3 PositiveZ = new Vector3(0.0f, 0.0f, 1.0f);

        /// <summary>
        /// The zero vector instance.
        /// </summary>
        public static readonly Vector3 Zero = default;


        /// <summary>
        /// Gets the length of the vector.
        /// </summary>
        public float Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => MathF.Sqrt(X * X + Y * Y + Z * Z);
        }

        /// <summary>
        /// Gets the squared length of the vector.
        /// </summary>
        public float LengthSquared
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => X * X + Y * Y + Z * Z;
        }


        /// <summary>
        /// Performs an implicit conversion from <see cref="Vector3D"/> to <see cref="Vector3"/>.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector3(in Vector3D vector)
        {
            return new Vector3((float)vector.X, (float)vector.Y, (float)vector.Z);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator -(in Vector3 value)
        {
            return new Vector3(-value.X, -value.Y, -value.Z);
        }

        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        /// <returns>The sum of the 2 vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator +(in Vector3 a, in Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        /// <returns>The difference of the 2 vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator -(in Vector3 a, in Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        /// <returns>The sum of the 2 vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator *(in Vector3 a, in Vector3 b)
        {
            return new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        /// <summary>
        /// Implements the * operator with scalar.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b scalar.</param>
        /// <returns>The vector multiplied by the scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator *(in Vector3 a, float b)
        {
            return new Vector3(a.X * b, a.Y * b, a.Z * b);
        }

        /// <summary>
        /// Implements the * operator with scalar.
        /// </summary>
        /// <param name="a">The a scalar.</param>
        /// <param name="b">The b vector.</param>
        /// <returns>The vector multiplied by the scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator *(float a, in Vector3 b)
        {
            return new Vector3(b.X * a, b.Y * a, b.Z * a);
        }

        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        /// <returns>The sum of the 2 vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator /(in Vector3 a, in Vector3 b)
        {
            return new Vector3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }

        /// <summary>
        /// Implements the / operator with scalar.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b scalar.</param>
        /// <returns>The vector multiplied by the scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator /(in Vector3 a, float b)
        {
            return new Vector3(a.X / b, a.Y / b, a.Z / b);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(in Vector3 a, in Vector3 b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(in Vector3 a, in Vector3 b)
        {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z;
        }


        /// <summary>
        /// Calculates the cross product of a and b vector.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Cross(in Vector3 a, in Vector3 b)
        {
            return new Vector3(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
        }

        /// <summary>
        /// Calculates the cross product of the current and v vector.
        /// </summary>
        /// <param name="v">The v vector.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 Cross(in Vector3 v)
        {
            return Cross(this, v);
        }

        /// <summary>
        /// Calculates the scalar product of the current and v vectors.
        /// </summary>
        /// <param name="v">The v vector.</param>
        /// <returns>The result of the scalar product.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Dot(in Vector3 v)
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
        public static float Dot(in Vector3 a, in Vector3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector3 other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            if (obj is Vector3 other)
            {
                return X == other.X && Y == other.Y && Z == other.Z;
            }

            return false;
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        /// <summary>
        /// Calculates the linear interpolated value between a and b. The interpolation parameter is clamped to the [0...1] range.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <param name="t">The interpolation parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 LerpClamped(in Vector3 a, in Vector3 b, float t)
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
        public static Vector3 LerpUnclamped(in Vector3 a, in Vector3 b, float t)
        {
            return new Vector3(
                MathematicsHelper.LerpUnclamped(a.X, b.X, t),
                MathematicsHelper.LerpUnclamped(a.Y, b.Y, t),
                MathematicsHelper.LerpUnclamped(a.Z, b.Z, t));
        }

        /// <summary>
        /// Calculates a normalized vector from the current vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 Normalize()
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
        public static Vector3 Slerp(in Vector3 a, in Vector3 b, float t)
        {
            t = Ranges.FloatUnit.Clamp(t);
            return SlerpUnclamped(a, b, t);
        }

        /// <summary>
        /// Calculates the spherical interpolated value between a and b. The interpolation parameter is not clamped to the [0...1] range.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <param name="t">The interpolation parameter.</param>
        public static Vector3 SlerpUnclamped(in Vector3 a, in Vector3 b, float t)
        {
            var theta = a.Dot(b);
            var sinTheta = MathF.Sin(theta);
            var t1 = MathF.Sin((1.0f - t) * theta) / sinTheta;
            var t2 = MathF.Sin(t * theta) / sinTheta;
            return new Vector3(t1 * a.X + t2 * b.X, t1 * a.Y + t2 * b.Y, t1 * a.Z + t2 * b.Z);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"({X.ToString(CultureInfo.InvariantCulture)}, {Y.ToString(CultureInfo.InvariantCulture)}, {Z.ToString(CultureInfo.InvariantCulture)})";
        }
    }
}
