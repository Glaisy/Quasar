//-----------------------------------------------------------------------
// <copyright file="Quaternion.cs" company="Space Development">
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
    /// Single precision quaternion representation.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [JsonConverter(typeof(QuaternionJsonConverter))]
    public readonly struct Quaternion : IEquatable<Quaternion>
    {
        private const float PIOver2 = 0.5f * MathF.PI;
        private const float SingularityThreshold = 0.5f - MathematicsConstants.Epsilon;
        private const float OneMinusEpsilon = 1.0f - MathematicsConstants.Epsilon;
        private const float MinusOnePlusEpsilon = -1.0f + MathematicsConstants.Epsilon;


        /// <summary>
        /// Initializes a new instance of the <see cref="Quaternion" /> struct.
        /// </summary>
        /// <param name="x">The x component.</param>
        /// <param name="y">The y component.</param>
        /// <param name="z">The z component.</param>
        /// <param name="w">The w component.</param>
        public Quaternion(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }


        /// <summary>
        /// The x component.
        /// </summary>
        public readonly float X;

        /// <summary>
        /// The y component.
        /// </summary>
        public readonly float Y;

        /// <summary>
        /// The z component.
        /// </summary>
        public readonly float Z;

        /// <summary>
        /// The w component.
        /// </summary>
        public readonly float W;


        /// <summary>
        /// Gets the magnitude of the quaternion.
        /// </summary>
        public float Magnitude
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => MathF.Sqrt(X * X + Y * Y + Z * Z + W * W);
        }

        /// <summary>
        /// Gets the squared magnitude of the quaternion.
        /// </summary>
        /// <value>
        /// The squared length of the quaternion.
        /// </value>
        public float SquaredMagnitude
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => X * X + Y * Y + Z * Z + W * W;
        }


        /// <summary>
        /// The identity quaternion.
        /// </summary>
        public static readonly Quaternion Identity = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);

        /// <summary>
        /// The zero quaternion.
        /// </summary>
        public static readonly Quaternion Zero = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);


        /// <summary>
        /// Performs an implicit conversion from <see cref="QuaternionD"/> to <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Quaternion(QuaternionD source)
        {
            return new Quaternion((float)source.X, (float)source.Y, (float)source.Z, (float)source.W);
        }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(in Quaternion a, in Quaternion b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.W == b.W;
        }

        /// <summary>
        /// In-equality operator.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(in Quaternion a, in Quaternion b)
        {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z || a.W != b.W;
        }

        /// <summary>
        /// Multiplication operator. Multiplies quaternion with a scalar value.
        /// </summary>
        /// <param name="quaternion">The quaternion value.</param>
        /// <param name="scalar">The ivisor scalar value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion operator *(in Quaternion quaternion, float scalar)
        {
            return new Quaternion(quaternion.X * scalar, quaternion.Y * scalar, quaternion.Z * scalar, quaternion.W * scalar);
        }

        /// <summary>
        /// Multiplication operator. Multiplies quaternion with a Vector3.
        /// </summary>
        /// <param name="q">The quaternion value.</param>
        /// <param name="vector">The vector value.</param>
        public static Vector3 operator *(in Quaternion q, in Vector3 vector)
        {
            var a = 2.0f * q.X;
            var b = 2.0f * q.Y;
            var c = 2.0f * q.Z;
            var d = q.X * a;
            var e = q.Y * b;
            var f = q.Z * c;
            var g = q.X * b;
            var h = q.X * c;
            var i = q.Y * c;
            var j = q.W * a;
            var k = q.W * b;
            var l = q.W * c;
            return new Vector3(
                (1.0f - e - f) * vector.X + (g - l) * vector.Y + (h + k) * vector.Z,
                (g + l) * vector.X + (1.0f - d - f) * vector.Y + (i - j) * vector.Z,
                (h - k) * vector.X + (i + j) * vector.Y + (1.0f - d - e) * vector.Z);
        }

        /// <summary>
        /// Multiplication operator. Multiplies quaternions.
        /// </summary>
        /// <param name="a">The quaternion a.</param>
        /// <param name="b">The quaternion b.</param>
        public static Quaternion operator *(in Quaternion a, in Quaternion b)
        {
            var aybzMazby = a.Y * b.Z - a.Z * b.Y;
            var azbxMaxbz = a.Z * b.X - a.X * b.Z;
            var axbyMaybx = a.X * b.Y - a.Y * b.X;
            var axbxPaybyPazbz = a.X * b.X + a.Y * b.Y + a.Z * b.Z;
            return new Quaternion(
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
        public static Quaternion operator /(in Quaternion quaternion, float scalar)
        {
            return new Quaternion(quaternion.X / scalar, quaternion.Y / scalar, quaternion.Z / scalar, quaternion.W / scalar);
        }


        /// <summary>
        /// Creates a rotation which rotates angle [radians] around axis.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <param name="axis">The axis.</param>
        public static Quaternion AngleAxis(float angle, in Vector3 axis)
        {
            var magnitude = axis.Length;
            if (magnitude <= MathematicsConstants.Epsilon)
            {
                return Identity;
            }

            var halfAngle = angle * 0.5f;
            var sinPerMagnitude = MathF.Sin(halfAngle) / magnitude;
            return new Quaternion(
                axis.X * sinPerMagnitude,
                axis.Y * sinPerMagnitude,
                axis.Z * sinPerMagnitude,
                MathF.Cos(halfAngle));
        }

        /// <summary>
        /// Conjugates the quaternion.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quaternion Conjugate()
        {
            return new Quaternion(-X, -Y, -Z, W);
        }

        /// <summary>
        /// Performs the quaternion scalar dot product.
        /// </summary>
        /// <param name="a">The a quaternion value.</param>
        /// <param name="b">The b quaternion value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(in Quaternion a, in Quaternion b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
        }

        /// <summary>
        /// Performs the quaternion dot product.
        /// </summary>
        /// <param name="quaternion">The second quaternion value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Dot(in Quaternion quaternion)
        {
            return Dot(this, quaternion);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Quaternion other)
        {
            return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is Quaternion quaternion)
            {
                return Equals(quaternion);
            }

            return false;
        }

        /// <summary>
        /// Creates a quaternion from Euler angles [pitch, yaw, roll] [radians].
        /// </summary>
        /// <param name="euler">The euler angle vector value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion FromEulerAngles(in Vector3 euler)
        {
            return FromEulerAngles(euler.X, euler.Y, euler.Z);
        }

        /// <summary>
        /// Creates a quaternion from Euler angles [radians].
        /// </summary>
        /// <param name="pitch">The roll.</param>
        /// <param name="yaw">The yaw.</param>
        /// <param name="roll">The pitch.</param>
        public static Quaternion FromEulerAngles(float pitch, float yaw, float roll)
        {
            pitch *= 0.5f;
            var sp = MathF.Sin(pitch);
            var cp = MathF.Cos(pitch);

            yaw *= 0.5f;
            var sy = MathF.Sin(yaw);
            var cy = MathF.Cos(yaw);

            roll *= 0.5f;
            var sr = MathF.Sin(roll);
            var cr = MathF.Cos(roll);

            return new Quaternion(
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
        public static Quaternion FromToRotation(in Vector3 fromVector, in Vector3 toVector)
        {
            var fromDirection = fromVector.Normalize();
            var toDirection = toVector.Normalize();
            var dot = fromDirection.Dot(toDirection);

            Vector3 rotationAxis;
            if (dot < MinusOnePlusEpsilon)
            {
                rotationAxis = Vector3.PositiveX.Cross(fromDirection);
                if (rotationAxis.Length < MathematicsConstants.Epsilon)
                {
                    rotationAxis = Vector3.PositiveY.Cross(fromDirection);
                }

                return AngleAxis(MathF.PI, rotationAxis);
            }

            if (dot > OneMinusEpsilon)
            {
                return Identity;
            }

            rotationAxis = fromDirection.Cross(toDirection);
            var s = MathF.Sqrt(2.0f * (1.0f + dot));
            var invS = 1.0f / s;
            return new Quaternion(rotationAxis.X * invS, rotationAxis.Y * invS, rotationAxis.Z * invS, 0.5f * s);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z, W);
        }

        /// <summary>
        /// Calculates the inverse of the quaternion.
        /// </summary>
        public Quaternion Invert()
        {
            var squaredMagnitude = SquaredMagnitude;
            if (squaredMagnitude < MathematicsConstants.Epsilon)
            {
                return Zero;
            }

            return new Quaternion(-X / squaredMagnitude, -Y / squaredMagnitude, -Z / squaredMagnitude, W / squaredMagnitude);
        }

        /// <summary>
        /// Calculates the look rotation from forward and up vectors.
        /// </summary>
        /// <param name="eyePosition">The eye position.</param>
        /// <param name="targetPosition">The target position.</param>
        /// <param name="up">The up direction.</param>
        /// <param name="leftHanded">If true left-handed rotation is generated; otherwise right handed.</param>
        /// <returns>The look rotation quaternion.</returns>
        public static Quaternion LookRotation(in Vector3 eyePosition, in Vector3 targetPosition, in Vector3 up, bool leftHanded)
        {
            Vector3 forwardDirection;
            if (leftHanded)
            {
                forwardDirection = targetPosition - eyePosition;
            }
            else
            {
                forwardDirection = eyePosition - targetPosition;
            }

            forwardDirection = forwardDirection.Normalize();
            var forwardRotation = FromToRotation(Vector3.PositiveZ, forwardDirection);

            var rightDirection = forwardDirection.Cross(up.Normalize());
            var upDirection = rightDirection.Cross(forwardDirection);

            var newUpDirection = forwardRotation * Vector3.PositiveY;
            var upRotation = FromToRotation(newUpDirection, upDirection);

            return upRotation * forwardRotation;
        }

        /// <summary>
        /// Normalizes the Quaternion structure to have unit magnitude.
        /// </summary>
        public Quaternion Normalize()
        {
            var magnitude = Magnitude;
            if (magnitude < MathematicsConstants.Epsilon)
            {
                return Zero;
            }

            return new Quaternion(X / magnitude, Y / magnitude, Z / magnitude, W / magnitude);
        }

        /// <summary>
        /// Spherical linear interpolation between two quaternions.
        /// </summary>
        /// <param name="a">The quaternion a.</param>
        /// <param name="b">The quaternion b.</param>
        /// <param name="t">The interpolation factor.</param>
        public static Quaternion Slerp(in Quaternion a, in Quaternion b, float t)
        {
            // if either input is zero, return the other.
            if (a.SquaredMagnitude == 0.0f)
            {
                if (b.SquaredMagnitude == 0.0f)
                {
                    return Identity;
                }

                return b;
            }
            else if (b.SquaredMagnitude == 0.0f)
            {
                return a;
            }


            var sign = 1.0f;
            var cosHalfAngle = a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
            if (cosHalfAngle >= 1.0f || cosHalfAngle <= -1.0f)
            {
                // angle = 0.0f, so just return one input.
                return a;
            }
            else if (cosHalfAngle < 0.0f)
            {
                sign = -1.0f;
                cosHalfAngle = -cosHalfAngle;
            }

            float blendA;
            float blendB;
            if (cosHalfAngle < OneMinusEpsilon)
            {
                // do proper slerp for big angles
                var halfAngle = MathF.Acos(cosHalfAngle);
                var sinHalfAngle = MathF.Sin(halfAngle);
                var oneOverSinHalfAngle = 1.0f / sinHalfAngle;
                blendA = MathF.Sin(halfAngle * (1.0f - t)) * oneOverSinHalfAngle;
                blendB = sign * MathF.Sin(halfAngle * t) * oneOverSinHalfAngle;
            }
            else
            {
                // do lerp if angle is really small.
                blendA = 1.0f - t;
                blendB = sign * t;
            }

            var x = blendA * a.X + blendB * b.X;
            var y = blendA * a.Y + blendB * b.Y;
            var z = blendA * a.Z + blendB * b.Z;
            var w = blendA * a.W + blendB * b.W;
            var squaredMagnitude = x * x + y * y + z * z + w * w;
            if (squaredMagnitude > 0.0f)
            {
                return new Quaternion(x / squaredMagnitude, y / squaredMagnitude, z / squaredMagnitude, w / squaredMagnitude);
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
                return new Vector3(PIOver2, 2.0f * MathF.Atan2(Y, X), 0);
            }

            if (singularityTest < -SingularityThreshold * unit)
            {
                // singularity at south pole
                return new Vector3(-PIOver2, -2.0f * MathF.Atan2(Y, X), 0);
            }

            var pitch = MathF.Asin(2.0f * (W * X - Y * Z));
            var yaw = MathF.Atan2(2.0f * W * Y + 2.0f * Z * X, 1.0f - 2.0f * (X * X + Y * Y));
            var roll = MathF.Atan2(2.0f * W * Z + 2.0f * X * Y, 1.0f - 2.0f * (Z * Z + X * X));
            return new Vector3(pitch, yaw, roll);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[{X:0.##0}, {Y:0.##0}, {Z:0.##0}, {W:0.##0}]";
        }
    }
}