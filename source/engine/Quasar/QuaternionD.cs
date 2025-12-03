//-----------------------------------------------------------------------
// <copyright file="QuaternionD.cs" company="Space Development">
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
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

using Quasar.IO.Serialization.Json;

using Space.Core.Mathematics;

namespace Quasar
{
    /// <summary>
    /// Double precision quaternion representation.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [JsonConverter(typeof(QuaternionDJsonConverter))]
    public readonly struct QuaternionD : IEquatable<QuaternionD>
    {
        private const double PIOver2 = Math.PI * 0.5;
        private const double SingularityThreshold = 0.5 - MathematicsConstants.EpsilonD;
        private const double OneMinusEpsilon = 1.0 - MathematicsConstants.EpsilonD;
        private const double MinusOnePlusEpsilon = -1.0 + MathematicsConstants.EpsilonD;


        /// <summary>
        /// Initializes a new instance of the <see cref="QuaternionD" /> struct.
        /// </summary>
        /// <param name="x">The x component.</param>
        /// <param name="y">The y component.</param>
        /// <param name="z">The z component.</param>
        /// <param name="w">The w component.</param>
        public QuaternionD(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }


        /// <summary>
        /// The x component.
        /// </summary>
        public readonly double X;

        /// <summary>
        /// The y component.
        /// </summary>
        public readonly double Y;

        /// <summary>
        /// The z component.
        /// </summary>
        public readonly double Z;

        /// <summary>
        /// The w component.
        /// </summary>
        public readonly double W;


        /// <summary>
        /// Gets the magnitude of the quaternion.
        /// </summary>
        public double Magnitude
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Math.Sqrt(X * X + Y * Y + Z * Z + W * W);
        }

        /// <summary>
        /// Gets the squared magnitude of the quaternion.
        /// </summary>
        /// <value>
        /// The squared length of the quaternion.
        /// </value>
        public double SquaredMagnitude
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => X * X + Y * Y + Z * Z + W * W;
        }


        /// <summary>
        /// The identity quaternion.
        /// </summary>
        public static readonly QuaternionD Identity = new QuaternionD(0.0, 0.0, 0.0, 1.0);

        /// <summary>
        /// The zero quaternion.
        /// </summary>
        public static readonly QuaternionD Zero = new QuaternionD(0.0, 0.0, 0.0, 0.0);


        /// <summary>
        /// Performs an implicit conversion from <see cref="Quaternion"/> to <see cref="QuaternionD"/>.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator QuaternionD(Quaternion source)
        {
            return new QuaternionD(source.X, source.Y, source.Z, source.W);
        }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(in QuaternionD a, in QuaternionD b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.W == b.W;
        }

        /// <summary>
        /// In-equality operator.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(in QuaternionD a, in QuaternionD b)
        {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z || a.W != b.W;
        }

        /// <summary>
        /// Multiplication operator. Multiplies quaternion with a scalar value.
        /// </summary>
        /// <param name="quaternion">The quaternion value.</param>
        /// <param name="scalar">The divisor scalar value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionD operator *(in QuaternionD quaternion, double scalar)
        {
            return new QuaternionD(quaternion.X * scalar, quaternion.Y * scalar, quaternion.Z * scalar, quaternion.W * scalar);
        }

        /// <summary>
        /// Multiplication operator. Multiplies quaternion with a Vector3d.
        /// </summary>
        /// <param name="quaternion">The quaternion value.</param>
        /// <param name="vector">The vector value.</param>
        public static Vector3D operator *(in QuaternionD quaternion, in Vector3D vector)
        {
            var a = 2.0 * quaternion.X;
            var b = 2.0 * quaternion.Y;
            var c = 2.0 * quaternion.Z;
            var d = quaternion.X * a;
            var e = quaternion.Y * b;
            var f = quaternion.Z * c;
            var g = quaternion.X * b;
            var h = quaternion.X * c;
            var i = quaternion.Y * c;
            var j = quaternion.W * a;
            var k = quaternion.W * b;
            var l = quaternion.W * c;
            return new Vector3D(
                (1.0 - e - f) * vector.X + (g - l) * vector.Y + (h + k) * vector.Z,
                (g + l) * vector.X + (1.0 - d - f) * vector.Y + (i - j) * vector.Z,
                (h - k) * vector.X + (i + j) * vector.Y + (1.0 - d - e) * vector.Z);
        }

        /// <summary>
        /// Multiplication operator. Multiplies quaternions.
        /// </summary>
        /// <param name="a">The quaternion a.</param>
        /// <param name="b">The quaternion b.</param>
        public static QuaternionD operator *(in QuaternionD a, in QuaternionD b)
        {
            var aybzMazby = a.Y * b.Z - a.Z * b.Y;
            var azbxMaxbz = a.Z * b.X - a.X * b.Z;
            var axbyMaybx = a.X * b.Y - a.Y * b.X;
            var axbxPaybyPazbz = a.X * b.X + a.Y * b.Y + a.Z * b.Z;
            return new QuaternionD(
                a.X * b.W + b.X * a.W + aybzMazby,
                a.Y * b.W + b.Y * a.W + azbxMaxbz,
                a.Z * b.W + b.Z * a.W + axbyMaybx,
                a.W * b.W - axbxPaybyPazbz);
        }

        /// <summary>
        /// Divison operator. Divides quaternion with a scalar value.
        /// </summary>
        /// <param name="quaternion">The quaternion value.</param>
        /// <param name="scalar">The divisor scalar value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionD operator /(in QuaternionD quaternion, double scalar)
        {
            return new QuaternionD(quaternion.X / scalar, quaternion.Y / scalar, quaternion.Z / scalar, quaternion.W / scalar);
        }


        /// <summary>
        /// Creates a rotation which rotates angle [radians] around axis.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <param name="axis">The axis.</param>
        public static QuaternionD AngleAxis(double angle, in Vector3D axis)
        {
            var magnitude = axis.Length;
            if (magnitude <= MathematicsConstants.EpsilonD)
            {
                return Identity;
            }

            var halfAngle = angle * 0.5;
            var sinPerMagnitude = Math.Sin(halfAngle) / magnitude;
            return new QuaternionD(
                axis.X * sinPerMagnitude,
                axis.Y * sinPerMagnitude,
                axis.Z * sinPerMagnitude,
                Math.Cos(halfAngle));
        }

        /// <summary>
        /// Conjugates the quaternion.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public QuaternionD Conjugate()
        {
            return new QuaternionD(-X, -Y, -Z, W);
        }

        /// <summary>
        /// Performs the quaternion scalar dot product.
        /// </summary>
        /// <param name="a">The a quaternion value.</param>
        /// <param name="b">The b quaternion value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Dot(in QuaternionD a, in QuaternionD b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
        }

        /// <summary>
        /// Performs the quaternion scalar dot product.
        /// </summary>
        /// <param name="quaternon">Second quaternion value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Dot(in QuaternionD quaternon)
        {
            return Dot(this, quaternon);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(QuaternionD other)
        {
            return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is QuaternionD quaternion)
            {
                return Equals(quaternion);
            }

            return false;
        }

        /// <summary>
        /// Creates a quaternion from Euler angles [pitch, yaw, roll] [radians].
        /// </summary>
        /// <param name="euler">The euler vector value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuaternionD FromEulerAngles(in Vector3D euler)
        {
            return FromEulerAngles(euler.X, euler.Y, euler.Z);
        }

        /// <summary>
        /// Creates a quaternion from Euler angles [radians].
        /// </summary>
        /// <param name="pitch">The roll.</param>
        /// <param name="yaw">The yaw.</param>
        /// <param name="roll">The pitch.</param>
        public static QuaternionD FromEulerAngles(double pitch, double yaw, double roll)
        {
            pitch *= 0.5;
            var sp = Math.Sin(pitch);
            var cp = Math.Cos(pitch);

            yaw *= 0.5;
            var sy = Math.Sin(yaw);
            var cy = Math.Cos(yaw);

            roll *= 0.5;
            var sr = Math.Sin(roll);
            var cr = Math.Cos(roll);

            return new QuaternionD(
                cy * sp * cr + sy * cp * sr,
                sy * cp * cr - cy * sp * sr,
                cy * cp * sr - sy * sp * cr,
                cy * cp * cr + sy * sp * sr);
        }

        /// <summary>
        /// Creates a rotation between from and to vectors.
        /// </summary>
        /// <param name="fromVector">From vector.</param>
        /// <param name="toVector">To vector.</param>
        public static QuaternionD FromToRotation(in Vector3D fromVector, in Vector3D toVector)
        {
            var fromDirection = fromVector.Normalize();
            var toDirection = toVector.Normalize();
            var dot = fromDirection.Dot(toDirection);

            Vector3 rotationAxis;
            if (dot < MinusOnePlusEpsilon)
            {
                rotationAxis = Vector3D.PositiveX.Cross(fromDirection);
                if (rotationAxis.Length < MathematicsConstants.EpsilonD)
                {
                    rotationAxis = Vector3D.PositiveY.Cross(fromDirection);
                }

                return AngleAxis(Math.PI, rotationAxis);
            }

            if (dot > OneMinusEpsilon)
            {
                return Identity;
            }

            rotationAxis = fromDirection.Cross(toDirection);
            var s = Math.Sqrt(2.0 * (1.0 + dot));
            var invS = 1.0 / s;
            return new QuaternionD(rotationAxis.X * invS, rotationAxis.Y * invS, rotationAxis.Z * invS, 0.5 * s);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z, W);
        }

        /// <summary>
        /// Calculates the inverse of the quaternion.
        /// </summary>
        public QuaternionD Invert()
        {
            var squaredMagnitude = SquaredMagnitude;
            if (squaredMagnitude < MathematicsConstants.EpsilonD)
            {
                return Zero;
            }

            return new QuaternionD(-X / squaredMagnitude, -Y / squaredMagnitude, -Z / squaredMagnitude, W / squaredMagnitude);
        }

        /// <summary>
        /// Calculates the look rotation from forward and up vectors.
        /// </summary>
        /// <param name="eyePosition">The eye position.</param>
        /// <param name="targetPosition">The target position.</param>
        /// <param name="up">The up direction.</param>
        /// <param name="leftHanded">If true left-handed rotation is generated; otherwise right handed.</param>
        /// <returns>The look rotation quaternion.</returns>
        public static QuaternionD LookRotation(in Vector3D eyePosition, in Vector3D targetPosition, in Vector3D up, bool leftHanded)
        {
            Vector3D forwardDirection;
            if (leftHanded)
            {
                forwardDirection = targetPosition - eyePosition;
            }
            else
            {
                forwardDirection = eyePosition - targetPosition;
            }

            forwardDirection = forwardDirection.Normalize();
            var forwardRotation = FromToRotation(Vector3D.PositiveZ, forwardDirection);

            var rightDirection = forwardDirection.Cross(up.Normalize());
            var upDirection = rightDirection.Cross(forwardDirection);

            var newUpDirection = forwardRotation * Vector3D.PositiveY;
            var upRotation = FromToRotation(newUpDirection, upDirection);
            return upRotation * forwardRotation;
        }

        /// <summary>
        /// Normalizes the Quaternion structure to have unit magnitude.
        /// </summary>
        public QuaternionD Normalize()
        {
            var magnitude = Magnitude;
            if (magnitude < MathematicsConstants.EpsilonD)
            {
                return Zero;
            }

            return new QuaternionD(X / magnitude, Y / magnitude, Z / magnitude, W / magnitude);
        }

        /// <summary>
        /// Spherical linear interpolation between two quaternions.
        /// </summary>
        /// <param name="a">The quaternion a.</param>
        /// <param name="b">The quaternion b.</param>
        /// <param name="t">The interpolation factor.</param>
        public static QuaternionD Slerp(in QuaternionD a, in QuaternionD b, double t)
        {
            // if either input is zero, return the other.
            if (a.SquaredMagnitude == 0.0)
            {
                if (b.SquaredMagnitude == 0.0)
                {
                    return Identity;
                }

                return b;
            }
            else if (b.SquaredMagnitude == 0.0)
            {
                return a;
            }


            var sign = 1.0;
            var cosHalfAngle = a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
            if (cosHalfAngle >= 1.0 || cosHalfAngle <= -1.0)
            {
                // angle = 0.0, so just return one input.
                return a;
            }
            else if (cosHalfAngle < 0.0)
            {
                sign = -1.0;
                cosHalfAngle = -cosHalfAngle;
            }

            double blendA;
            double blendB;
            if (cosHalfAngle < OneMinusEpsilon)
            {
                // do proper slerp for big angles
                var halfAngle = Math.Acos(cosHalfAngle);
                var sinHalfAngle = Math.Sin(halfAngle);
                var oneOverSinHalfAngle = 1.0 / sinHalfAngle;
                blendA = Math.Sin(halfAngle * (1.0 - t)) * oneOverSinHalfAngle;
                blendB = sign * Math.Sin(halfAngle * t) * oneOverSinHalfAngle;
            }
            else
            {
                // do lerp if angle is really small.
                blendA = 1.0 - t;
                blendB = sign * t;
            }

            var x = blendA * a.X + blendB * b.X;
            var y = blendA * a.Y + blendB * b.Y;
            var z = blendA * a.Z + blendB * b.Z;
            var w = blendA * a.W + blendB * b.W;
            var squaredMagnitude = x * x + y * y + z * z + w * w;
            if (squaredMagnitude > 0.0)
            {
                return new QuaternionD(x / squaredMagnitude, y / squaredMagnitude, z / squaredMagnitude, w / squaredMagnitude);
            }

            return Identity;
        }

        /// <summary>
        /// Converts the quaternion to Euler angle vector (x = pitch, y = yaw, z = roll) [radians].
        /// </summary>
        public Vector3 ToEulerAngles()
        {
            var xx = X * X;
            var yy = Y * Y;
            var zz = Z * Z;
            var ww = W * W;
            var unit = xx + yy + zz + ww;
            var singularityTest = X * W - Y * Z;

            if (singularityTest > SingularityThreshold * unit)
            {
                // singularity at north pole
                return new Vector3D(PIOver2, 2.0 * Math.Atan2(Y, X), 0);
            }

            if (singularityTest < -SingularityThreshold * unit)
            {
                // singularity at south pole
                return new Vector3D(-PIOver2, -2.0 * Math.Atan2(Y, X), 0);
            }

            var pitch = Math.Asin(2.0 * (W * X - Y * Z));
            var yaw = Math.Atan2(2.0 * W * Y + 2.0 * Z * X, 1.0 - 2.0 * (X * X + Y * Y));
            var roll = Math.Atan2(2.0 * W * Z + 2.0 * X * Y, 1.0 - 2.0 * (Z * Z + X * X));
            return new Vector3D(pitch, yaw, roll);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[{X:0.##0}, {Y:0.##0}, {Z:0.##0}, {W:0.##0}]";
        }
    }
}