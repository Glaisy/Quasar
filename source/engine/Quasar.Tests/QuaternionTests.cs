//-----------------------------------------------------------------------
// <copyright file="QuaternionTests.cs" company="Space Development">
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
    internal sealed class QuaternionTests
    {
        [Test]
        public void AngleAxis_X()
        {
            // arrange
            var angle = 0.23f;
            var expectedValue = System.Numerics.Quaternion.CreateFromAxisAngle(System.Numerics.Vector3.UnitX, angle);

            // act
            var result = Quaternion.AngleAxis(angle, Vector3.PositiveX);

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
            var result = Quaternion.AngleAxis(angle, Vector3.PositiveY);

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
            var result = Quaternion.AngleAxis(angle, Vector3.PositiveZ);

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
            var sut = Quaternion.AngleAxis(angle, Vector3.PositiveZ);

            // act
            var result = sut * Vector3.PositiveX;

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
            var sut = Quaternion.AngleAxis(angle, Vector3.PositiveX);

            // act
            var result = sut * Vector3.PositiveY;

            // assert
            Assert.That(result.EqualTo(expectedValue), Is.True);
        }

        [Test]
        public void FromEulerAngles_Pitch_Yaw_Roll()
        {
            // arrange
            var expectedValue = System.Numerics.Quaternion.CreateFromYawPitchRoll(0.27f, 0.45f, 0.99f);

            // act
            var result = Quaternion.FromEulerAngles(0.45f, 0.27f, 0.99f);

            // assert
            Assert.That(result.EqualTo(expectedValue), Is.True);
        }

        [Test]
        public void FromEulerAngles_Vector3()
        {
            // arrange
            var expectedValue = System.Numerics.Quaternion.CreateFromYawPitchRoll(-0.22f, 0.54f, 1.22f);

            // act
            var result = Quaternion.FromEulerAngles(new Vector3(0.54f, -0.22f, 1.22f));

            // assert
            Assert.That(result.EqualTo(expectedValue), Is.True);
        }

        [Test]
        public void FromEulerAngles_ToEulerAngles()
        {
            // arrange
            var expectedValue = new Vector3(-0.73f, 1.01f, 1.53f).Normalize();

            // act
            var sut = Quaternion.FromEulerAngles(expectedValue);
            var result = sut.ToEulerAngles();

            // assert
            Assert.That(result.EqualTo(expectedValue), Is.True);
        }

        [Test]
        public void ToEulerAngles_FromEulerAngles()
        {
            // arrange
            var expectedValue = new Quaternion(0.54f, -0.22f, 1.22f, 1.0f).Normalize();

            // act
            var euler = expectedValue.ToEulerAngles();
            var result = Quaternion.FromEulerAngles(euler);

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
            var a = new Quaternion(1, 2, 3, 4).Normalize();
            var b = new Quaternion(5, 6, 7, 8).Normalize();
            var result = Quaternion.Slerp(a, b, 0.12345678f);

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

            var a = new Quaternion(1, 2, 3, 4);
            var b = new Quaternion(-5, 6, 1, -2);

            // act
            var result = a * b;

            Assert.That(AssertExtensions.EqualTo(result, expectedValue), Is.True);
        }

        [Test]
        public void FromToRotation()
        {
            // arrange
            var from = new Vector3(1, 2, 3).Normalize();
            var to = new Vector3(3, 1, -5).Normalize();
            var expectedResult = to;

            // act
            var result = Quaternion.FromToRotation(from, to) * from;

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

            var eye = new Vector3(0.05f, -12.3f, 2.13f);
            var target = new Vector3(-3.05f, 2.3f, -14.13f);
            var up = new Vector3(1.23f, -0.54f, 2.11f);

            // act
            var result = Quaternion.LookRotation(eye, target, up, true);

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

            var eye = new Vector3(0.05f, -12.3f, 2.13f);
            var target = new Vector3(-3.05f, 2.3f, -14.13f);
            var up = new Vector3(1.23f, -0.54f, 2.11f);

            // act
            var result = Quaternion.LookRotation(eye, target, up, false);

            Assert.That(result.EqualTo(expectedValue), Is.True);
        }

        [Test]
        public void LookRotation_XP_Z_YP()
        {
            // arrange
            var eye = Vector3.PositiveX;
            var target = Vector3.Zero;
            var up = Vector3.PositiveY;

            // act and assert
            LookRotationCase(eye, target, up);
        }

        [Test]
        public void LookRotation_XN_Z_YP()
        {
            // arrange
            var eye = Vector3.NegativeX;
            var target = Vector3.Zero;
            var up = Vector3.PositiveY;

            // act and assert
            LookRotationCase(eye, target, up);
        }

        [Test]
        public void LookRotation_ZP_Z_YP()
        {
            // arrange
            var eye = Vector3.PositiveZ;
            var target = Vector3.Zero;
            var up = Vector3.PositiveY;

            // act and assert
            LookRotationCase(eye, target, up);
        }

        [Test]
        public void LookRotation_ZN_Z_YP()
        {
            // arrange
            var eye = Vector3.NegativeZ;
            var target = Vector3.Zero;
            var up = Vector3.PositiveY;

            // act and assert
            LookRotationCase(eye, target, up);
        }

        [Test]
        public void LookRotation_YP_Z_XP()
        {
            // arrange
            var eye = Vector3.PositiveY;
            var target = Vector3.Zero;
            var up = Vector3.PositiveX;

            // act and assert
            LookRotationCase(eye, target, up);
        }

        [Test]
        public void LookRotation_YN_Z_XN()
        {
            // arrange
            var eye = Vector3.NegativeY;
            var target = Vector3.Zero;
            var up = Vector3.NegativeX;

            // act and assert
            LookRotationCase(eye, target, up);
        }


        private static void LookRotationCase(Vector3 eye, Vector3 target, Vector3 up)
        {
            // arrange
            var expectedForward = (target - eye).Normalize();
            var expectedUp = up.Normalize();
            var expectedRight = expectedUp.Cross(expectedForward);
            var sut = Quaternion.LookRotation(eye, target, up, true);

            // act and assert
            var resultX = sut * Vector3.PositiveX;
            var resultY = sut * Vector3.PositiveY;
            var resultZ = sut * Vector3.PositiveZ;
            Assert.That(AssertExtensions.EqualTo(resultX, expectedRight), Is.True);
            Assert.That(AssertExtensions.EqualTo(resultY, expectedUp), Is.True);
            Assert.That(AssertExtensions.EqualTo(resultZ, expectedForward), Is.True);
        }
    }
}
