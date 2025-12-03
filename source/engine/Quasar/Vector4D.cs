//-----------------------------------------------------------------------
// <copyright file="Vector4D.cs" company="Space Development">
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
    /// Double precision 4D space vector structure implementation. (right-handed coordinate system) (Immutable).
    /// </summary>
    /// <seealso cref="IEquatable{Vector4D}" />
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [JsonConverter(typeof(Vector4DJsonConverter))]
    public readonly struct Vector4D : IEquatable<Vector4D>
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
        /// The z coordinate.
        /// </summary>
        public readonly double Z;

        /// <summary>
        /// The w coordinate.
        /// </summary>
        public readonly double W;


        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4D" /> struct.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        /// <param name="w">The w coordinate.</param>
        public Vector4D(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4D" /> struct.
        /// </summary>
        /// <param name="source">The source Vector2 instance.</param>
        /// <param name="z">The z coordinate.</param>
        /// <param name="w">The w coordinate.</param>
        public Vector4D(in Vector2 source, double z = 0.0, double w = 0.0)
        {
            X = source.X;
            Y = source.Y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4D" /> struct.
        /// </summary>
        /// <param name="source">The source Vector3 instance.</param>
        /// <param name="w">The w coordinate.</param>
        public Vector4D(in Vector3 source, double w = 0.0)
        {
            X = source.X;
            Y = source.Y;
            Z = source.Z;
            W = w;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4D" /> struct.
        /// </summary>
        /// <param name="source">The source Vector4D instance.</param>
        public Vector4D(in Vector4 source)
        {
            X = source.X;
            Y = source.Y;
            Z = source.Z;
            W = source.W;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4D" /> struct.
        /// </summary>
        /// <param name="value">The x, y, z, w coordinate value.</param>
        public Vector4D(float value)
        {
            X = Y = Z = W = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4D" /> struct.
        /// </summary>
        /// <param name="value">The x, y, z, w coordinate value.</param>
        public Vector4D(double value)
        {
            X = Y = Z = W = value;
        }


        /// <summary>
        /// The -X vector instance.
        /// </summary>
        public static readonly Vector4D NegativeX = new Vector4D(-1.0, 0.0, 0.0, 0.0);

        /// <summary>
        /// The -Y vector instance.
        /// </summary>
        public static readonly Vector4D NegativeY = new Vector4D(0.0, -1.0, 0.0, 0.0);

        /// <summary>
        /// The -Z vector instance.
        /// </summary>
        public static readonly Vector4D NegativeZ = new Vector4D(0.0, 0.0, -1.0, 0.0);

        /// <summary>
        /// The -W vector instance.
        /// </summary>
        public static readonly Vector4D NegativeW = new Vector4D(0.0, 0.0, 0.0, -1.0);

        /// <summary>
        /// The non-normalized unit vector instance.
        /// </summary>
        public static readonly Vector4D One = new Vector4D(1.0, 1.0, 1.0, 1.0);

        /// <summary>
        /// The +X vector instance.
        /// </summary>
        public static readonly Vector4D PositiveX = new Vector4D(1.0, 0.0, 0.0, 0.0);

        /// <summary>
        /// The +Y vector instance.
        /// </summary>
        public static readonly Vector4D PositiveY = new Vector4D(0.0, 1.0, 0.0, 0.0);

        /// <summary>
        /// The +Z vector instance.
        /// </summary>
        public static readonly Vector4D PositiveZ = new Vector4D(0.0, 0.0, 1.0, 0.0);

        /// <summary>
        /// The +W vector instance.
        /// </summary>
        public static readonly Vector4D PositiveW = new Vector4D(0.0, 0.0, 0.0, 1.0);

        /// <summary>
        /// The zero vector instance.
        /// </summary>
        public static readonly Vector4D Zero;


        /// <summary>
        /// Gets the length of the vector.
        /// </summary>
        public double Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Math.Sqrt(X * X + Y * Y + Z * Z + W * W);
        }

        /// <summary>
        /// Gets the squared length of the vector.
        /// </summary>
        public double LengthSquared
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => X * X + Y * Y + Z * Z + W * W;
        }


        /// <summary>
        /// Performs an implicit conversion from <see cref="Vector4"/> to <see cref="Vector4D"/>.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector4D(in Vector4 vector)
        {
            return new Vector4D(vector.X, vector.Y, vector.Z, vector.W);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator -(in Vector4D value)
        {
            return new Vector4D(-value.X, -value.Y, -value.Z, -value.W);
        }

        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        /// <returns>The sum of the 2 vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator +(in Vector4D a, in Vector4D b)
        {
            return new Vector4D(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        /// <returns>The difference of the 2 vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator -(in Vector4D a, in Vector4D b)
        {
            return new Vector4D(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        /// <returns>The difference of the 2 vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator *(in Vector4D a, in Vector4D b)
        {
            return new Vector4D(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);
        }

        /// <summary>
        /// Implements the * operator with scalar.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b scalar.</param>
        /// <returns>The vector multiplied by the scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator *(in Vector4D a, double b)
        {
            return new Vector4D(a.X * b, a.Y * b, a.Z * b, a.W * b);
        }

        /// <summary>
        /// Implements the * operator with scalar.
        /// </summary>
        /// <param name="a">The a scalar.</param>
        /// <param name="b">The b vector.</param>
        /// <returns>The vector multiplied by the scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator *(double a, in Vector4D b)
        {
            return new Vector4D(b.X * a, b.Y * a, b.Z * a, b.W * a);
        }

        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        /// <returns>The difference of the 2 vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator /(in Vector4D a, in Vector4D b)
        {
            return new Vector4D(a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.W / b.W);
        }

        /// <summary>
        /// Implements the / operator with scalar.
        /// </summary>
        /// <param name="a">The a vectpr.</param>
        /// <param name="b">The b scalar.</param>
        /// <returns>The vector multiplied by the scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator /(in Vector4D a, double b)
        {
            return new Vector4D(a.X / b, a.Y / b, a.Z / b, a.W / b);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(in Vector4D a, in Vector4D b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.W == b.W;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="a">The a vector.</param>
        /// <param name="b">The b vector.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(in Vector4D a, in Vector4D b)
        {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z || a.W != b.W;
        }


        /// <summary>
        /// Calculates the scalar product of the current and v vectors.
        /// </summary>
        /// <param name="v">The v vector.</param>
        /// <returns>The result of the scalar product.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Dot(in Vector4D v)
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
        public static double Dot(in Vector4D a, in Vector4D b)
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
        public bool Equals(Vector4D other)
        {
            return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            if (obj is Vector4D other)
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
        public static Vector4D LerpClamped(in Vector4D a, in Vector4D b, double t)
        {
            t = Ranges.DoubleUnit.Clamp(t);
            return LerpUnclamped(a, b, t);
        }

        /// <summary>
        /// Calculates the linear interpolated value between a and b. The interpolation parameter is unclamped.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <param name="t">The interpolation parameter.</param>
        public static Vector4D LerpUnclamped(in Vector4D a, in Vector4D b, double t)
        {
            return new Vector4D(
                MathematicsHelper.LerpUnclamped(a.X, b.X, t),
                MathematicsHelper.LerpUnclamped(a.Y, b.Y, t),
                MathematicsHelper.LerpUnclamped(a.Z, b.Z, t),
                MathematicsHelper.LerpUnclamped(a.W, b.W, t));
        }

        /// <summary>
        /// Calculates a normalized vector from the current vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D Normalize()
        {
            return this / Length;
        }

        /// <summary>
        /// Calculates the spherical interpolated value between a and b. The interpolation parameter is clamped to the [0...1] range.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <param name="t">The interpolation parameter.</param>
        public static Vector4D Slerp(in Vector4D a, in Vector4D b, double t)
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
        public static Vector4D SlerpUnclamped(in Vector4D a, in Vector4D b, double t)
        {
            var theta = a.Dot(b);
            var sinTheta = Math.Sin(theta);
            var t1 = Math.Sin((1.0 - t) * theta) / sinTheta;
            var t2 = Math.Sin(t * theta) / sinTheta;
            return new Vector4D(t1 * a.X + t2 * b.X, t1 * a.Y + t2 * b.Y, t1 * a.Z + t2 * b.Z, t1 * a.W + t2 * b.W);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"({X.ToString(CultureInfo.InvariantCulture)}, {Y.ToString(CultureInfo.InvariantCulture)}, {Z.ToString(CultureInfo.InvariantCulture)}, {W.ToString(CultureInfo.InvariantCulture)})";
        }
    }
}
