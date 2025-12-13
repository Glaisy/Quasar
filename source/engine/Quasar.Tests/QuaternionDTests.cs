//-----------------------------------------------------------------------
// <copyright file="QuaternionDTests.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using NUnit.Framework;

using Quasar.Tests.Extensions;

namespace Quasar.Tests
{
    [TestFixture]
    internal sealed class QuaternionDTests
    {
        [Test]
        public void AngleAxis_X()
        {
            // arrange
            var angle = 0.23f;
            var expectedValue = System.Numerics.Quaternion.CreateFromAxisAngle(System.Numerics.Vector3.UnitX, angle);

            // act
            var result = QuaternionD.AngleAxis(angle, Vector3D.PositiveX);

            // assert
            Assert.That(result.EqualTo(expectedValue), Is.True);
        }

        [Test]
        public void AngleAxis_Y()
        {
            // arrange
            var angle = 0.23f;
            var expectedValue = System.Numerics.Quaternion.CreateFromAxisAngle(System.Numerics.Vector3.UnitY, angle);

            // act
            var result = QuaternionD.AngleAxis(angle, Vector3D.PositiveY);

            // assert
            Assert.That(result.EqualTo(expectedValue), Is.True);
        }

        [Test]
        public void AngleAxis_Z()
        {
            // arrange
            var angle = 0.23f;
            var expectedValue = System.Numerics.Quaternion.CreateFromAxisAngle(System.Numerics.Vector3.UnitZ, angle);

            // act
            var result = QuaternionD.AngleAxis(angle, Vector3D.PositiveZ);

            // assert
            Assert.That(result.EqualTo(expectedValue), Is.True);
        }

        [Test]
        public void MultiplicationZ_VectorX()
        {
            // arrange
            var angle = 0.23f;
            var q = System.Numerics.Quaternion.CreateFromAxisAngle(System.Numerics.Vector3.UnitZ, angle);
            var v = new System.Numerics.Quaternion(System.Numerics.Vector3.UnitX, 0);
            var rotation = q * v * System.Numerics.Quaternion.Conjugate(q);
            var expectedValue = new System.Numerics.Vector3(rotation.X, rotation.Y, rotation.Z);
            var sut = QuaternionD.AngleAxis(angle, Vector3D.PositiveZ);

            // act
            var result = sut * Vector3D.PositiveX;

            // assert
            Assert.That(result.EqualTo(expectedValue), Is.True);
        }

        [Test]
        public void MultiplicationX_VectorY()
        {
            // arrange
            var angle = 0.23f;
            var q = System.Numerics.Quaternion.CreateFromAxisAngle(System.Numerics.Vector3.UnitX, angle);
            var v = new System.Numerics.Quaternion(System.Numerics.Vector3.UnitY, 0);
            var rotation = q * v * System.Numerics.Quaternion.Conjugate(q);
            var expectedValue = new System.Numerics.Vector3(rotation.X, rotation.Y, rotation.Z);
            var sut = QuaternionD.AngleAxis(angle, Vector3D.PositiveX);
            // act
            var result = sut * Vector3D.PositiveY;

            // assert
            Assert.That(result.EqualTo(expectedValue), Is.True);
        }

        [Test]
        public void FromEulerAngles_Pitch_Yaw_Roll()
        {
            // arrange
            var expectedValue = System.Numerics.Quaternion.CreateFromYawPitchRoll(0.27f, 0.45f, 0.99f);

            // act
            var result = QuaternionD.FromEulerAngles(0.45, 0.27, 0.99);

            // assert
            Assert.That(result.EqualTo(expectedValue), Is.True);
        }

        [Test]
        public void FromEulerAngles_Vector3()
        {
            // arrange
            var expectedValue = System.Numerics.Quaternion.CreateFromYawPitchRoll(-0.22f, 0.54f, 1.22f);

            // act
            var result = QuaternionD.FromEulerAngles(new Vector3D(0.54, -0.22, 1.22));

            // assert
            Assert.That(result.EqualTo(expectedValue), Is.True);
        }

        [Test]
        public void FromEulerAngles_ToEulerAngles()
        {
            // arrange
            var expectedValue = new Vector3D(-0.33, 0.11, 1.53).Normalize();

            // act
            var sut = QuaternionD.FromEulerAngles(expectedValue);
            var result = sut.ToEulerAngles();

            // assert
            Assert.That(result.EqualTo(expectedValue), Is.True);

        }

        [Test]
        public void ToEulerAngles_FromEulerAngles()
        {
            // arrange
            var expectedValue = new QuaternionD(0.54, -0.22, 1.22, 1.0).Normalize();

            // act
            var euler = expectedValue.ToEulerAngles();
            var result = QuaternionD.FromEulerAngles(euler);

            // assert
            Assert.That(result.EqualTo(expectedValue), Is.True);
        }

        [Test]
        public void Slerp()
        {
            // arrange
            var tkA = System.Numerics.Quaternion.Normalize(new System.Numerics.Quaternion(1, 2, 3, 4));
            var tkB = System.Numerics.Quaternion.Normalize(new System.Numerics.Quaternion(5, 6, 7, 8));
            var expectedValue = System.Numerics.Quaternion.Slerp(tkA, tkB, 0.12345678f);

            // act
            var a = new QuaternionD(1, 2, 3, 4).Normalize();
            var b = new QuaternionD(5, 6, 7, 8).Normalize();
            var result = QuaternionD.Slerp(a, b, 0.12345678);

            // assert
            Assert.That(result.EqualTo(expectedValue), Is.True);
        }

        [Test]
        public void Multiply()
        {
            // arrange
            var glA = new System.Numerics.Quaternion(1, 2, 3, 4);
            var glB = new System.Numerics.Quaternion(-5, 6, 1, -2);
            var expectedValue = System.Numerics.Quaternion.Multiply(glA, glB);

            var a = new QuaternionD(1, 2, 3, 4);
            var b = new QuaternionD(-5, 6, 1, -2);

            // act
            var result = a * b;

            Assert.That(AssertExtensions.EqualTo(result, expectedValue), Is.True);
        }

        [Test]
        public void FromToRotation()
        {
            // arrange
            var from = new Vector3D(1, 2, 3).Normalize();
            var to = new Vector3D(3, 1, -5).Normalize();
            var expectedResult = to;

            // act
            var result = QuaternionD.FromToRotation(from, to) * from;

            // assert
            Assert.That(AssertExtensions.EqualTo(result, expectedResult), Is.True);
        }

        [Test]
        public void LookRotation_LeftHanded()
        {
            // arrange
            var nmEye = new System.Numerics.Vector3(0.05f, -12.3f, 2.13f);
            var nmTarget = new System.Numerics.Vector3(-3.05f, 2.3f, -14.13f);
            var nmUp = new System.Numerics.Vector3(1.23f, -0.54f, 2.11f);
            var lookMatrix = System.Numerics.Matrix4x4.CreateLookAt(nmTarget, nmEye, nmUp);
            System.Numerics.Matrix4x4.Decompose(lookMatrix, out _, out var rotation, out _);
            var expectedValue = System.Numerics.Quaternion.Conjugate(rotation);

            var eye = new Vector3D(0.05, -12.3, 2.13);
            var target = new Vector3D(-3.05, 2.3, -14.13);
            var up = new Vector3D(1.23, -0.54, 2.11);

            // act
            var result = QuaternionD.LookRotation(eye, target, up, true);

            Assert.That(result.EqualTo(expectedValue), Is.True);
        }

        [Test]
        public void LookRotation_RightHanded()
        {
            // arrange
            var nmEye = new System.Numerics.Vector3(0.05f, -12.3f, 2.13f);
            var nmTarget = new System.Numerics.Vector3(-3.05f, 2.3f, -14.13f);
            var nmUp = new System.Numerics.Vector3(1.23f, -0.54f, 2.11f);
            var lookMatrix = System.Numerics.Matrix4x4.CreateLookAt(nmEye, nmTarget, nmUp);
            System.Numerics.Matrix4x4.Decompose(lookMatrix, out _, out var rotation, out _);
            var expectedValue = System.Numerics.Quaternion.Conjugate(rotation);

            var eye = new Vector3D(0.05, -12.3, 2.13);
            var target = new Vector3D(-3.05, 2.3, -14.13);
            var up = new Vector3D(1.23, -0.54, 2.11);

            // act
            var result = QuaternionD.LookRotation(eye, target, up, false);

            Assert.That(result.EqualTo(expectedValue), Is.True);
        }

        [Test]
        public void LookRotation_XP_Z_YP()
        {
            // arrange
            var eye = Vector3D.PositiveX;
            var target = Vector3D.Zero;
            var up = Vector3D.PositiveY;

            // act and assert
            LookRotationCase(eye, target, up);
        }

        [Test]
        public void LookRotation_XN_Z_YP()
        {
            // arrange
            var eye = Vector3D.NegativeX;
            var target = Vector3D.Zero;
            var up = Vector3D.PositiveY;

            // act and assert
            LookRotationCase(eye, target, up);
        }

        [Test]
        public void LookRotation_ZP_Z_YP()
        {
            // arrange
            var eye = Vector3D.PositiveZ;
            var target = Vector3D.Zero;
            var up = Vector3D.PositiveY;

            // act and assert
            LookRotationCase(eye, target, up);
        }

        [Test]
        public void LookRotation_ZN_Z_YP()
        {
            // arrange
            var eye = Vector3D.NegativeZ;
            var target = Vector3D.Zero;
            var up = Vector3D.PositiveY;

            // act and assert
            LookRotationCase(eye, target, up);
        }

        [Test]
        public void LookRotation_YP_Z_XP()
        {
            // arrange
            var eye = Vector3D.PositiveY;
            var target = Vector3D.Zero;
            var up = Vector3D.PositiveX;

            // act and assert
            LookRotationCase(eye, target, up);
        }

        [Test]
        public void LookRotation_YN_Z_XN()
        {
            // arrange
            var eye = Vector3D.NegativeY;
            var target = Vector3D.Zero;
            var up = Vector3D.NegativeX;

            // act and assert
            LookRotationCase(eye, target, up);
        }


        private static void LookRotationCase(Vector3D eye, Vector3D target, Vector3D up)
        {
            // arrange
            var expectedForward = (target - eye).Normalize();
            var expectedUp = up.Normalize();
            var expectedRight = expectedUp.Cross(expectedForward);
            var sut = QuaternionD.LookRotation(eye, target, up, true);

            // act and assert
            var resultX = sut * Vector3D.PositiveX;
            var resultY = sut * Vector3D.PositiveY;
            var resultZ = sut * Vector3D.PositiveZ;
            Assert.That(AssertExtensions.EqualTo(resultX, expectedRight), Is.True);
            Assert.That(AssertExtensions.EqualTo(resultY, expectedUp), Is.True);
            Assert.That(AssertExtensions.EqualTo(resultZ, expectedForward), Is.True);
        }
    }
}
