//-----------------------------------------------------------------------
// <copyright file="GLMatrixFactoryTests.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using NUnit.Framework;

using Quasar.OpenGL.Graphics.Factories;
using Quasar.Tests.Extensions;

using Space.Core.Mathematics;

namespace Quasar.OpenGL.Tests.Graphics.Factories
{
    [TestFixture]
    internal class GLMatrixFactoryTests
    {
        [Test]
        public void CreateModelMatrix()
        {
            var sut = new GLMatrixFactory();

            var scaleMatrix = System.Numerics.Matrix4x4.CreateScale(new System.Numerics.Vector3(1, 2, 3));
            var rotation = System.Numerics.Quaternion.Normalize(new System.Numerics.Quaternion(4, 5, 6, 7));
            var rotationMatrix = System.Numerics.Matrix4x4.CreateFromQuaternion(rotation);
            var translationMatrix = System.Numerics.Matrix4x4.CreateTranslation(new System.Numerics.Vector3(8, 9, 10));
            var expectedValue = scaleMatrix * rotationMatrix * translationMatrix;

            var transform = new Transform
            {
                LocalScale = new Vector3(1, 2, 3),
                LocalRotation = new Quaternion(4, 5, 6, 7).Normalize(),
                LocalPosition = new Vector3(8, 9, 10)
            };

            // act
            Matrix4 result;
            sut.CreateModelMatrix(transform, ref result);

            // assert
            Assert.That(result.EqualTo(expectedValue), Is.True);
        }

        [Test]
        public void CreateOrthographicProjectionMatrix()
        {
            // arrange
            const float zNear = 0.5f;
            const float zFar = 350.0f;
            const int width = 1000;
            const int height = 800;

            var expectedValue = System.Numerics.Matrix4x4.CreateOrthographic(width, height, zNear, zFar);
            expectedValue.M33 *= 2f;
            var sut = new GLMatrixFactory();

            // act
            var result = new Matrix4();
            sut.CreateOrthographicProjectionMatrix(width, height, zNear, zFar, ref result);

            // assert
            Assert.That(result.EqualTo(expectedValue));
        }

        [Test]
        public void CreatePerspectiveProjectionMatrix()
        {
            // arrange
            const float zNear = 0.5f;
            const float zFar = 350.0f;
            const float fov = 60;
            const int width = 1000;
            const int height = 800;
            var aspectRatio = width / (float)height;

            var expectedValue = System.Numerics.Matrix4x4.CreatePerspectiveFieldOfView(
                MathematicsConstants.DegreeToRadian * fov,
                aspectRatio,
                zNear,
                zFar);

            var sut = new GLMatrixFactory();

            // act
            var result = new Matrix4();
            sut.CreatePerspectiveProjectionMatrix(aspectRatio, fov, zNear, zFar, ref result);

            // assert
            Assert.That(result.EqualTo(expectedValue));
        }

        [Test]
        [Ignore("Check handedness and other factors with System.Numerics check.")]
        public void CreateViewMatrix()
        {
            // arrange
            var sut = new GLMatrixFactory();
            var cameraPosition = new Vector3(-1.0f, 2.0f, 3.0f);
            var transform = new Transform
            {
                LocalPosition = cameraPosition,
                LocalRotation = Quaternion.LookRotation(cameraPosition, Vector3.Zero, Vector3.PositiveY, false)
            };

            var nmPosition = new System.Numerics.Vector3(transform.Position.X, transform.Position.Y, transform.Position.Z);
            var expectedValue = System.Numerics.Matrix4x4.CreateLookAt(nmPosition, System.Numerics.Vector3.Zero, System.Numerics.Vector3.UnitY);

            // act
            Matrix4 result;
            sut.CreateViewMatrix(transform, ref result);

            // assert
            Assert.That(result.EqualTo(expectedValue));
        }

        [Test]
        public void CreateViewRotationMatrix()
        {
            // arrange
            var sut = new GLMatrixFactory();
            var cameraPosition = new Vector3(-1.0f, 2.0f, 3.0f);
            var transform = new Transform
            {
                LocalPosition = cameraPosition,
                LocalRotation = Quaternion.LookRotation(cameraPosition, Vector3.Zero, Vector3.PositiveY, false)
            };

            var rotationTransform = new Transform
            {
                LocalRotation = transform.Rotation
            };

            Matrix4 viewMatrix;
            sut.CreateViewMatrix(transform, ref viewMatrix);
            Matrix4 expectedValue;
            sut.CreateViewMatrix(rotationTransform, ref expectedValue);

            // act
            Matrix4 result;
            sut.CreateViewRotationMatrix(viewMatrix, ref result);

            // assert
            Assert.That(result.EqualTo(expectedValue));

        }
    }
}