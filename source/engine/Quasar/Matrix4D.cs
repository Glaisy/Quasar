//-----------------------------------------------------------------------
// <copyright file="Matrix4D.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

using Space.Core.Mathematics;

namespace Quasar
{
    /// <summary>
    /// Single precision 4x4 matrix structure (row major representation).
    /// </summary>
    /// <seealso cref="IEquatable{Matrix4Dx4}" />
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Matrix4D : IEquatable<Matrix4D>
    {
        private const int N = 4;
        private const int N2 = N * N;


        private fixed double cells[N2];


        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix4D"/> struct.
        /// </summary>
        /// <param name="row0">The row 0.</param>
        /// <param name="row1">The row 1.</param>
        /// <param name="row2">The row 2.</param>
        /// <param name="row3">The row 3.</param>
        public Matrix4D(in Vector4D row0, in Vector4D row1, in Vector4D row2, in Vector4D row3)
        {
            cells[0] = row0.X;
            cells[1] = row0.Y;
            cells[2] = row0.Z;
            cells[3] = row0.W;

            cells[4] = row1.X;
            cells[5] = row1.Y;
            cells[6] = row1.Z;
            cells[7] = row1.W;

            cells[8] = row2.X;
            cells[9] = row2.Y;
            cells[10] = row2.Z;
            cells[11] = row2.W;

            cells[12] = row3.X;
            cells[13] = row3.Y;
            cells[14] = row3.Z;
            cells[15] = row3.W;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix4D"/> struct.
        /// </summary>
        /// <param name="m00">The M00.</param>
        /// <param name="m01">The M01.</param>
        /// <param name="m02">The M02.</param>
        /// <param name="m03">The M03.</param>
        /// <param name="m10">The M10.</param>
        /// <param name="m11">The M11.</param>
        /// <param name="m12">The M12.</param>
        /// <param name="m13">The M13.</param>
        /// <param name="m20">The M20.</param>
        /// <param name="m21">The M21.</param>
        /// <param name="m22">The M22.</param>
        /// <param name="m23">The M23.</param>
        /// <param name="m30">The M30.</param>
        /// <param name="m31">The M31.</param>
        /// <param name="m32">The M32.</param>
        /// <param name="m33">The M33.</param>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1117:Parameters should be on same line or separate lines", Justification = "Reviewed.")]
        public Matrix4D(
            double m00, double m01, double m02, double m03,
            double m10, double m11, double m12, double m13,
            double m20, double m21, double m22, double m23,
            double m30, double m31, double m32, double m33)
        {
            cells[0] = m00;
            cells[1] = m01;
            cells[2] = m02;
            cells[3] = m03;

            cells[4] = m10;
            cells[5] = m11;
            cells[6] = m12;
            cells[7] = m13;

            cells[8] = m20;
            cells[9] = m21;
            cells[10] = m22;
            cells[11] = m23;

            cells[12] = m30;
            cells[13] = m31;
            cells[14] = m32;
            cells[15] = m33;
        }


        /// <summary>
        /// The zero matrix.
        /// </summary>
        public static readonly Matrix4D Zero = default;

        /// <summary>
        /// The identity matrix.
        /// </summary>
        public static readonly Matrix4D Identity = new Matrix4D(
            new Vector4D(1.0, 0.0, 0.0, 0.0),
            new Vector4D(0.0, 1.0, 0.0, 0.0),
            new Vector4D(0.0, 0.0, 1.0, 0.0),
            new Vector4D(0.0, 0.0, 0.0, 1.0));


        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="a">The a matrix.</param>
        /// <param name="b">The b matrix.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(in Matrix4D a, in Matrix4D b)
        {
            if (a.GetHashCode() != b.GetHashCode())
            {
                return false;
            }

            for (var i = 0; i < N2; i++)
            {
                if (a.cells[i] != b.cells[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="a">The a matrix.</param>
        /// <param name="b">The b matrix.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(in Matrix4D a, in Matrix4D b)
        {
            if (a.GetHashCode() != b.GetHashCode())
            {
                return true;
            }

            for (var i = 0; i < N2; i++)
            {
                if (a.cells[i] != b.cells[i])
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="a">The a matrix.</param>
        /// <param name="b">The b matrix.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4D operator +(in Matrix4D a, in Matrix4D b)
        {
            Matrix4D result;
            for (var i = 0; i < N2; i++)
            {
                result.cells[i] = a.cells[i] + b.cells[i];
            }

            return result;
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="a">The a matrix.</param>
        /// <param name="b">The b matrix.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4D operator -(in Matrix4D a, in Matrix4D b)
        {
            Matrix4D result;
            for (var i = 0; i < N2; i++)
            {
                result.cells[i] = a.cells[i] - b.cells[i];
            }

            return result;
        }

        /// <summary>
        /// Implements the operator * with scalar.
        /// </summary>
        /// <param name="a">The a matrix.</param>
        /// <param name="b">The b matrix.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4D operator *(in Matrix4D a, double b)
        {
            Matrix4D result;
            for (var i = 0; i < N2; i++)
            {
                result.cells[i] = a.cells[i] * b;
            }

            return result;
        }

        /// <summary>
        /// Implements the operator / with scalar.
        /// </summary>
        /// <param name="a">The a matrix.</param>
        /// <param name="b">The b matrix.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4D operator /(in Matrix4D a, double b)
        {
            Matrix4D result;
            b = 1.0 / b;
            for (var i = 0; i < N2; i++)
            {
                result.cells[i] = a.cells[i] * b;
            }

            return result;
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="a">The a matrix.</param>
        /// <param name="b">The b matrix.</param>
        public static Matrix4D operator *(in Matrix4D a, in Matrix4D b)
        {
            Matrix4D result;
            Multiply(a, b, ref result);
            return result;
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="a">The a matrix.</param>
        /// <param name="b">The b matrix.</param>
        public static Vector4D operator *(in Matrix4D a, in Vector4D b)
        {
            return new Vector4D(
                a.cells[0] * b.X + a.cells[1] * b.Y + a.cells[2] * b.Z + a.cells[3] * b.W,
                a.cells[4] * b.X + a.cells[5] * b.Y + a.cells[6] * b.Z + a.cells[7] * b.W,
                a.cells[8] * b.X + a.cells[9] * b.Y + a.cells[10] * b.Z + a.cells[11] * b.W,
                a.cells[12] * b.X + a.cells[12] * b.Y + a.cells[14] * b.Z + a.cells[15] * b.W);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Matrix4D other)
        {
            for (var i = 0; i < N2; i++)
            {
                if (cells[i] != other.cells[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj == null ||
                GetHashCode() != obj.GetHashCode())
            {
                return false;
            }

            if (obj is Matrix4D other)
            {
                return Equals(other);
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hash = 0;
            for (var i = 0; i < N2; i++)
            {
                hash = HashCode.Combine(hash, cells[i]);
            }

            return hash;
        }

        /// <summary>
        /// Initializes the matrix from the quaternion.
        /// </summary>
        /// <param name="quaternion">The quaternion.</param>
        /// <param name="rowMajor">Row major matrix is generated if true; otherwise column major matrix will be created.</param>
        public void FromQuaternion(in QuaternionD quaternion, bool rowMajor)
        {
            if (rowMajor)
            {
                FromQuaternionRowMajor(quaternion);
            }
            else
            {
                FromQuaternionColumnMajor(quaternion);
            }
        }

        /// <summary>
        /// Initializes the matrix as a scale matrix from the scale vector.
        /// </summary>
        /// <param name="scale">The scale vector.</param>
        public void FromScale(in Vector3 scale)
        {
            CopyCells(Zero);

            cells[0] = scale.X;
            cells[5] = scale.Y;
            cells[10] = scale.Z;
            cells[15] = 1.0;
        }

        /// <summary>
        /// Initializes the matrix from the translation vector.
        /// </summary>
        /// <param name="translation">The translation vector.</param>
        /// <param name="rowMajor">Row major matrix is generated if true; otherwise column major matrix will be created.</param>
        public void FromTranslation(in Vector3 translation, bool rowMajor)
        {
            CopyCells(Identity);

            if (rowMajor)
            {
                cells[3] = translation.X;
                cells[7] = translation.Y;
                cells[11] = translation.Z;
            }
            else
            {
                cells[12] = translation.X;
                cells[13] = translation.Y;
                cells[14] = translation.Z;
            }
        }

        /// <summary>
        /// Calculates the inverse of the matrix.
        /// </summary>
        /// <param name="result">The result.</param>
        public void Invert(ref Matrix4D result)
        {
            double a = cells[0], b = cells[4], c = cells[8], d = cells[12];
            double e = cells[1], f = cells[5], g = cells[9], h = cells[13];
            double i = cells[2], j = cells[6], k = cells[10], l = cells[14];
            double m = cells[3], n = cells[7], o = cells[11], p = cells[15];

            var kp_lo = k * p - l * o;
            var jp_ln = j * p - l * n;
            var jo_kn = j * o - k * n;
            var ip_lm = i * p - l * m;
            var io_km = i * o - k * m;
            var in_jm = i * n - j * m;

            var a11 = +(f * kp_lo - g * jp_ln + h * jo_kn);
            var a12 = -(e * kp_lo - g * ip_lm + h * io_km);
            var a13 = +(e * jp_ln - f * ip_lm + h * in_jm);
            var a14 = -(e * jo_kn - f * io_km + g * in_jm);

            var determinant = a * a11 + b * a12 + c * a13 + d * a14;

            if (Math.Abs(determinant) < MathematicsConstants.EpsilonD)
            {
                throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
            }

            var oneOverDeterminant = 1.0f / determinant;

            result.cells[0] = a11 * oneOverDeterminant;
            result.cells[1] = a12 * oneOverDeterminant;
            result.cells[2] = a13 * oneOverDeterminant;
            result.cells[3] = a14 * oneOverDeterminant;

            result.cells[4] = -(b * kp_lo - c * jp_ln + d * jo_kn) * oneOverDeterminant;
            result.cells[5] = (a * kp_lo - c * ip_lm + d * io_km) * oneOverDeterminant;
            result.cells[6] = -(a * jp_ln - b * ip_lm + d * in_jm) * oneOverDeterminant;
            result.cells[7] = (a * jo_kn - b * io_km + c * in_jm) * oneOverDeterminant;

            var gp_ho = g * p - h * o;
            var fp_hn = f * p - h * n;
            var fo_gn = f * o - g * n;
            var ep_hm = e * p - h * m;
            var eo_gm = e * o - g * m;
            var en_fm = e * n - f * m;

            result.cells[8] = (b * gp_ho - c * fp_hn + d * fo_gn) * oneOverDeterminant;
            result.cells[9] = -(a * gp_ho - c * ep_hm + d * eo_gm) * oneOverDeterminant;
            result.cells[10] = (a * fp_hn - b * ep_hm + d * en_fm) * oneOverDeterminant;
            result.cells[11] = -(a * fo_gn - b * eo_gm + c * en_fm) * oneOverDeterminant;

            var gl_hk = g * l - h * k;
            var fl_hj = f * l - h * j;
            var fk_gj = f * k - g * j;
            var el_hi = e * l - h * i;
            var ek_gi = e * k - g * i;
            var ej_fi = e * j - f * i;

            result.cells[12] = -(b * gl_hk - c * fl_hj + d * fk_gj) * oneOverDeterminant;
            result.cells[13] = (a * gl_hk - c * el_hi + d * ek_gi) * oneOverDeterminant;
            result.cells[14] = -(a * fl_hj - b * el_hi + d * ej_fi) * oneOverDeterminant;
            result.cells[15] = (a * fk_gj - b * ek_gi + c * ej_fi) * oneOverDeterminant;
        }

        /// <summary>
        /// Multiplies the lhs matrix with the rhs matrix.
        /// </summary>
        /// <param name="left">The left hand-side matrix.</param>
        /// <param name="right">The right hand-size matrix.</param>
        /// <param name="result">The result matrix.</param>
        public static void Multiply(in Matrix4D left, in Matrix4D right, ref Matrix4D result)
        {
            var leftM11 = left.cells[0];
            var leftM12 = left.cells[1];
            var leftM13 = left.cells[2];
            var leftM14 = left.cells[3];
            var leftM21 = left.cells[4];
            var leftM22 = left.cells[5];
            var leftM23 = left.cells[6];
            var leftM24 = left.cells[7];
            var leftM31 = left.cells[8];
            var leftM32 = left.cells[9];
            var leftM33 = left.cells[10];
            var leftM34 = left.cells[11];
            var leftM41 = left.cells[12];
            var leftM42 = left.cells[13];
            var leftM43 = left.cells[14];
            var leftM44 = left.cells[15];
            var rightM11 = right.cells[0];
            var rightM12 = right.cells[1];
            var rightM13 = right.cells[2];
            var rightM14 = right.cells[3];
            var rightM21 = right.cells[4];
            var rightM22 = right.cells[5];
            var rightM23 = right.cells[6];
            var rightM24 = right.cells[7];
            var rightM31 = right.cells[8];
            var rightM32 = right.cells[9];
            var rightM33 = right.cells[10];
            var rightM34 = right.cells[11];
            var rightM41 = right.cells[12];
            var rightM42 = right.cells[13];
            var rightM43 = right.cells[14];
            var rightM44 = right.cells[15];

            result.cells[0] = leftM11 * rightM11 + leftM12 * rightM21 + leftM13 * rightM31 + leftM14 * rightM41;
            result.cells[1] = leftM11 * rightM12 + leftM12 * rightM22 + leftM13 * rightM32 + leftM14 * rightM42;
            result.cells[2] = leftM11 * rightM13 + leftM12 * rightM23 + leftM13 * rightM33 + leftM14 * rightM43;
            result.cells[3] = leftM11 * rightM14 + leftM12 * rightM24 + leftM13 * rightM34 + leftM14 * rightM44;
            result.cells[4] = leftM21 * rightM11 + leftM22 * rightM21 + leftM23 * rightM31 + leftM24 * rightM41;
            result.cells[5] = leftM21 * rightM12 + leftM22 * rightM22 + leftM23 * rightM32 + leftM24 * rightM42;
            result.cells[6] = leftM21 * rightM13 + leftM22 * rightM23 + leftM23 * rightM33 + leftM24 * rightM43;
            result.cells[7] = leftM21 * rightM14 + leftM22 * rightM24 + leftM23 * rightM34 + leftM24 * rightM44;
            result.cells[8] = leftM31 * rightM11 + leftM32 * rightM21 + leftM33 * rightM31 + leftM34 * rightM41;
            result.cells[9] = leftM31 * rightM12 + leftM32 * rightM22 + leftM33 * rightM32 + leftM34 * rightM42;
            result.cells[10] = leftM31 * rightM13 + leftM32 * rightM23 + leftM33 * rightM33 + leftM34 * rightM43;
            result.cells[11] = leftM31 * rightM14 + leftM32 * rightM24 + leftM33 * rightM34 + leftM34 * rightM44;
            result.cells[12] = leftM41 * rightM11 + leftM42 * rightM21 + leftM43 * rightM31 + leftM44 * rightM41;
            result.cells[13] = leftM41 * rightM12 + leftM42 * rightM22 + leftM43 * rightM32 + leftM44 * rightM42;
            result.cells[14] = leftM41 * rightM13 + leftM42 * rightM23 + leftM43 * rightM33 + leftM44 * rightM43;
            result.cells[15] = leftM41 * rightM14 + leftM42 * rightM24 + leftM43 * rightM34 + leftM44 * rightM44;
        }

        /// <summary>
        /// Transposes this Matrix4x4 instance.
        /// </summary>
        /// <param name="result">The transposed matrix.</param>
        public void Transpose(ref Matrix4D result)
        {
            var k = 0;
            for (var i = 0; i < N; i++)
            {
                for (var j = 0; j < N; j++, k++)
                {
                    result.cells[j * N + i] = cells[k];
                }
            }
        }

        /// <summary>
        /// Converts to array.
        /// </summary>
        public double[] ToArray()
        {
            var result = new double[N2];
            for (var i = 0; i < N2; i++)
            {
                result[i] = cells[i];
            }

            return result;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            var sb = new StringBuilder();
            fixed (double* pointer = &cells[0])
            {
                var ptr = pointer;
                for (var i = 0; i < N2; i++)
                {
                    if (i % N == 0)
                    {
                        sb.Append('(');
                    }

                    sb.Append(*ptr++);
                    if (i % N == N - 1)
                    {
                        sb.Append(")\n");
                    }
                    else
                    {
                        sb.Append(", ");
                    }
                }
            }

            return sb.ToString();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe void CopyCells(in Matrix4D source)
        {
            fixed (double* targetPointer = cells)
            {
                fixed (double* sourcePointer = source.cells)
                {
                    Unsafe.CopyBlock(targetPointer, sourcePointer, N2 * sizeof(double));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe void FromQuaternionColumnMajor(in QuaternionD quaternion)
        {
            var xx = quaternion.X * quaternion.X;
            var yy = quaternion.Y * quaternion.Y;
            var zz = quaternion.Z * quaternion.Z;
            var xy = quaternion.X * quaternion.Y;
            var xz = quaternion.Z * quaternion.X;
            var yz = quaternion.Y * quaternion.Z;
            var wz = quaternion.Z * quaternion.W;
            var wy = quaternion.Y * quaternion.W;
            var wx = quaternion.X * quaternion.W;

            cells[0] = 1.0 - 2.0 * (yy + zz);
            cells[1] = 2.0 * (xy + wz);
            cells[2] = 2.0 * (xz - wy);
            cells[3] = 0.0;

            cells[4] = 2.0 * (xy - wz);
            cells[5] = 1.0 - 2.0 * (zz + xx);
            cells[6] = 2.0 * (yz + wx);
            cells[7] = 0.0;

            cells[8] = 2.0 * (xz + wy);
            cells[9] = 2.0 * (yz - wx);
            cells[10] = 1.0 - 2.0 * (yy + xx);
            cells[11] = 0.0;

            cells[12] = 0.0;
            cells[13] = 0.0;
            cells[14] = 0.0;
            cells[15] = 1.0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe void FromQuaternionRowMajor(in QuaternionD quaternion)
        {
            var xx = quaternion.X * quaternion.X;
            var yy = quaternion.Y * quaternion.Y;
            var zz = quaternion.Z * quaternion.Z;
            var xy = quaternion.X * quaternion.Y;
            var xz = quaternion.Z * quaternion.X;
            var yz = quaternion.Y * quaternion.Z;
            var wz = quaternion.Z * quaternion.W;
            var wy = quaternion.Y * quaternion.W;
            var wx = quaternion.X * quaternion.W;

            cells[0] = 1.0 - 2.0 * (yy + zz);
            cells[4] = 2.0 * (xy + wz);
            cells[8] = 2.0 * (xz - wy);
            cells[12] = 0.0;

            cells[1] = 2.0 * (xy - wz);
            cells[5] = 1.0 - 2.0 * (zz + xx);
            cells[9] = 2.0 * (yz + wx);
            cells[13] = 0.0;

            cells[2] = 2.0 * (xz + wy);
            cells[6] = 2.0 * (yz - wx);
            cells[10] = 1.0 - 2.0 * (yy + xx);
            cells[14] = 0.0;

            cells[3] = 0.0;
            cells[7] = 0.0;
            cells[11] = 0.0;
            cells[15] = 1.0;
        }
    }
}
