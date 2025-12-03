//-----------------------------------------------------------------------
// <copyright file="AssertExtensions.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Tests.Extensions
{
    /// <summary>
    /// Assert extension functions.
    /// </summary>
    public static class AssertExtensions
    {
        private const float Epsilon = 1E-4f;


        /// <summary>
        /// Equality check for matrices.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        public static unsafe bool EqualTo(this Matrix4 a, Matrix4 b)
        {
            var aCells = a.ToArray();
            var bCells = b.ToArray();
            for (var i = 0; i < 16; i++)
            {
                if (!EqualTo(aCells[i], bCells[i]))
                {
                    throw new Exception($"Matrices are not equal at cell {i}: '{aCells[i]}' vs '{bCells[i]}'.");
                }
            }

            return true;
        }

        /// <summary>
        /// Equality check for matrices.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        public static unsafe bool EqualTo(this Matrix4 a, System.Numerics.Matrix4x4 b)
        {
            var aCells = a.ToArray();
            var bCells = &b.M11;
            for (var i = 0; i < 16; i++)
            {
                if (!EqualTo(aCells[i], bCells[i]))
                {
                    throw new Exception($"Matrices are not equal at cell {i}: '{aCells[i]}' vs '{bCells[i]}'.");
                }
            }

            return true;
        }

        /// <summary>
        /// Equality check for matrices.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        public static unsafe bool EqualTo(this Matrix4D a, System.Numerics.Matrix4x4 b)
        {
            var aCells = a.ToArray();
            var bCells = &b.M11;
            for (var i = 0; i < 16; i++)
            {
                if (!EqualTo(aCells[i], bCells[i]))
                {
                    throw new Exception($"Matrices are not equal at cell {i}: '{aCells[i]}' vs '{bCells[i]}'.");
                }
            }

            return true;
        }

        /// <summary>
        /// Equality check for quaternions.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        public static bool EqualTo(this Quaternion a, Quaternion b)
        {
            return EqualTo(a.X, b.X) && EqualTo(a.Y, b.Y) && EqualTo(a.Z, b.Z) && EqualTo(a.W, b.W);
        }

        /// <summary>
        /// Equality check for quaternions.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        public static bool EqualTo(this QuaternionD a, Quaternion b)
        {
            return EqualTo(a.X, b.X) && EqualTo(a.Y, b.Y) && EqualTo(a.Z, b.Z) && EqualTo(a.W, b.W);
        }

        /// <summary>
        /// Equality check for quaternions.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        public static bool EqualTo(this Quaternion a, System.Numerics.Quaternion b)
        {
            return EqualTo(a.X, b.X) && EqualTo(a.Y, b.Y) && EqualTo(a.Z, b.Z) && EqualTo(a.W, b.W);
        }

        /// <summary>
        /// Equality check for quaternions.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        public static bool EqualTo(this QuaternionD a, System.Numerics.Quaternion b)
        {
            return EqualTo(a.X, b.X) && EqualTo(a.Y, b.Y) && EqualTo(a.Z, b.Z) && EqualTo(a.W, b.W);
        }

        /// <summary>
        /// Equality check for vectors.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        public static bool EqualTo(this Vector2 a, Vector2 b)
        {
            return EqualTo(a.X, b.X) && EqualTo(a.Y, b.Y);
        }

        /// <summary>
        /// Equality check for vectors.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        public static bool EqualTo(this Vector2D a, Vector2D b)
        {
            return EqualTo(a.X, b.X) && EqualTo(a.Y, b.Y);
        }

        /// <summary>
        /// Equality check for vectors.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        public static bool EqualTo(this Vector2 a, System.Numerics.Vector2 b)
        {
            return EqualTo(a.X, b.X) && EqualTo(a.Y, b.Y);
        }

        /// <summary>
        /// Equality check for vectors.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        public static bool EqualTo(this Vector2D a, System.Numerics.Vector2 b)
        {
            return EqualTo(a.X, b.X) && EqualTo(a.Y, b.Y);
        }

        /// <summary>
        /// Equality check for vectors.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        public static bool EqualTo(this Vector3 a, System.Numerics.Vector3 b)
        {
            return EqualTo(a.X, b.X) && EqualTo(a.Y, b.Y) && EqualTo(a.Z, b.Z);
        }

        /// <summary>
        /// Equality check for vectors.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        public static bool EqualTo(this Vector3 a, Vector3 b)
        {
            return EqualTo(a.X, b.X) && EqualTo(a.Y, b.Y) && EqualTo(a.Z, b.Z);
        }

        /// <summary>
        /// Equality check for vectors.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        public static bool EqualTo(this Vector3D a, Vector3D b)
        {
            return EqualTo(a.X, b.X) && EqualTo(a.Y, b.Y) && EqualTo(a.Z, b.Z);
        }

        /// <summary>
        /// Equality check for vectors.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        public static bool EqualTo(this Vector4 a, in Vector4 b)
        {
            return EqualTo(a.X, b.X) && EqualTo(a.Y, b.Y) && EqualTo(a.Z, b.Z) && EqualTo(a.W, b.W);
        }

        /// <summary>
        /// Equality check for vectors.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        public static bool EqualTo(this Vector4D a, in Vector4D b)
        {
            return EqualTo(a.X, b.X) && EqualTo(a.Y, b.Y) && EqualTo(a.Z, b.Z) && EqualTo(a.W, b.W);
        }

        /// <summary>
        /// Equality check for vectors.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        public static bool EqualTo(this Vector3D a, System.Numerics.Vector3 b)
        {
            return EqualTo(a.X, b.X) && EqualTo(a.Y, b.Y) && EqualTo(a.Z, b.Z);
        }

        /// <summary>
        /// Equality check for vectors.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        public static bool EqualTo(this Vector4 a, System.Numerics.Vector4 b)
        {
            return EqualTo(a.X, b.X) && EqualTo(a.Y, b.Y) && EqualTo(a.Z, b.Z) && EqualTo(a.W, b.W);
        }

        /// <summary>
        /// Equality check for vectors.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        public static bool EqualTo(this Vector4D a, System.Numerics.Vector4 b)
        {
            return EqualTo(a.X, b.X) && EqualTo(a.Y, b.Y) && EqualTo(a.Z, b.Z) && EqualTo(a.W, b.W);
        }

        /// <summary>
        /// Equality check for floats.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        public static bool EqualTo(float a, float b)
        {
            return MathF.Abs(a - b) < Epsilon;
        }

        /// <summary>
        /// Equality check for double and float.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        public static bool EqualTo(double a, float b)
        {
            return Math.Abs(a - b) < Epsilon;
        }

        /// <summary>
        /// Equality check for doubles.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        public static bool EqualTo(double a, double b)
        {
            return Math.Abs(a - b) < Epsilon;
        }

        /// <summary>
        /// Equality check for double arrays.
        /// </summary>
        /// <param name="a">The a array.</param>
        /// <param name="b">The b array.</param>
        public static bool EqualTo(double[] a, double[] b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }

            for (var i = 0; i < a.Length; i++)
            {
                if (Math.Abs(a[i] - b[i]) > Epsilon)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
