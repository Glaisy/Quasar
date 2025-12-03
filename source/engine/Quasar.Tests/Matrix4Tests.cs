//-----------------------------------------------------------------------
// <copyright file="Matrix4Tests.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using NUnit.Framework;
using NUnit.Framework.Internal;

using Quasar.Tests.Extensions;

namespace Quasar.Tests
{
    [TestFixture]
    internal class Matrix4Tests
    {
        [Test]
        public void FromQuaternion_RowMajor()
        {
            // arrange
            var nmQuaternion = new System.Numerics.Quaternion(1, 2, 3, 4);
            var expectedValue = System.Numerics.Matrix4x4.Transpose(System.Numerics.Matrix4x4.CreateFromQuaternion(nmQuaternion));
            var quaternion = new Quaternion(1, 2, 3, 4);

            // act
            Matrix4 result;
            result.FromQuaternion(quaternion, true);

            // assert
            Assert.That(result.EqualTo(expectedValue));
        }

        [Test]
        public void FromQuaternion_ColumnMajor()
        {
            // arrange
            var nmQuaternion = new System.Numerics.Quaternion(1, 2, 3, 4);
            var expectedValue = System.Numerics.Matrix4x4.CreateFromQuaternion(nmQuaternion);
            var quaternion = new Quaternion(1, 2, 3, 4);

            // act
            Matrix4 result;
            result.FromQuaternion(quaternion, false);

            // assert
            Assert.That(result.EqualTo(expectedValue));
        }

        [Test]
        public void FromScale()
        {
            // arrange
            var nmScale = new System.Numerics.Vector3(1, 2, 3);
            var expectedValue = System.Numerics.Matrix4x4.CreateScale(nmScale);
            var scale = new Vector3(1, 2, 3);

            // act
            Matrix4 result;
            result.FromScale(scale);

            // assert
            Assert.That(result.EqualTo(expectedValue));
        }

        [Test]
        public void FromTranslation_RowMajor()
        {
            // arrange
            var nmTranslation = new System.Numerics.Vector3(1, -2, -3);
            var expectedValue = System.Numerics.Matrix4x4.Transpose(System.Numerics.Matrix4x4.CreateTranslation(nmTranslation));
            var translation = new Vector3(1, -2, -3);

            // act
            Matrix4 result;
            result.FromTranslation(translation, true);

            // assert
            Assert.That(result.EqualTo(expectedValue));
        }

        [Test]
        public void FromTranslation_ColumnMajor()
        {
            // arrange
            var nmTranslation = new System.Numerics.Vector3(1, -2, -3);
            var expectedValue = System.Numerics.Matrix4x4.CreateTranslation(nmTranslation);
            var translation = new Vector3(1, -2, -3);

            // act
            Matrix4 result;
            result.FromTranslation(translation, false);

            // assert
            Assert.That(result.EqualTo(expectedValue));
        }

        [Test]
        public void MultiplicationOperator()
        {
            //arrange
            var nmMatrixA = new System.Numerics.Matrix4x4(1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8);
            var nmMatrixB = new System.Numerics.Matrix4x4(8, 7, 6, 5, 4, 3, 2, 1, 8, 7, 6, 5, 4, 3, 2, 1);
            var nmMatrixC = new System.Numerics.Matrix4x4(1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2);
            var expectedValue = nmMatrixA * nmMatrixB * nmMatrixC;

            var matrixA = new Matrix4(1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8);
            var matrixB = new Matrix4(8, 7, 6, 5, 4, 3, 2, 1, 8, 7, 6, 5, 4, 3, 2, 1);
            var matrixC = new Matrix4(1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2);

            // act
            var result = matrixA * matrixB * matrixC;

            // assert 
            Assert.That(result.EqualTo(expectedValue));
        }

        [Test]
        public void Multiply()
        {
            //arrange
            var nmMatrixA = new System.Numerics.Matrix4x4(1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8);
            var nmMatrixB = new System.Numerics.Matrix4x4(8, 7, 6, 5, 4, 3, 2, 1, 8, 7, 6, 5, 4, 3, 2, 1);
            var expectedValue = nmMatrixA * nmMatrixB;

            var matrixA = new Matrix4(1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8);
            var matrixB = new Matrix4(8, 7, 6, 5, 4, 3, 2, 1, 8, 7, 6, 5, 4, 3, 2, 1);

            // act
            Matrix4 result;
            Matrix4.Multiply(matrixA, matrixB, ref result);

            // assert 
            Assert.That(result.EqualTo(expectedValue));
        }

        [Test]
        public void Inverse()
        {
            // arrange
            var nmMatrix = new System.Numerics.Matrix4x4(1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 14);
            var isInverted = System.Numerics.Matrix4x4.Invert(nmMatrix, out var expectedValue);
            var sut = new Matrix4(1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 14);

            // act
            Matrix4 result;
            sut.Invert(ref result);

            // assert
            Assert.That(result.EqualTo(expectedValue));
        }

        [Test]
        public void Transpose()
        {
            // arrange
            var source = new System.Numerics.Matrix4x4(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
            var expectedValue = System.Numerics.Matrix4x4.Transpose(source);
            var sut = new Matrix4(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

            // act
            Matrix4 result;
            sut.Transpose(ref result);

            // assert
            Assert.That(result.EqualTo(expectedValue));
        }
    }
}
