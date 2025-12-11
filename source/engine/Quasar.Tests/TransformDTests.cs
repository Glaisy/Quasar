//-----------------------------------------------------------------------
// <copyright file="TransformDTests.cs" company="Space Development">
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

using Space.Core.Mathematics;

namespace Quasar.Tests
{
    [TestFixture]
    public class TransformDTests
    {
        [Test]
        public void TestRotationX()
        {
            // arrange
            var sut = new TransformD(null, "sut")
            {
                // act
                LocalRotation = QuaternionD.AngleAxis(90 * MathematicsConstants.DegreeToRadianD, Vector3D.PositiveX)
            };

            // assert
            var resultX = sut.PositiveX;
            var resultY = sut.PositiveY;
            var resultZ = sut.PositiveZ;

            Assert.That(resultX.EqualTo(Vector3D.PositiveX), Is.True);
            Assert.That(resultY.EqualTo(Vector3D.PositiveZ), Is.True);
            Assert.That(resultZ.EqualTo(Vector3D.NegativeY), Is.True);
        }

        [Test]
        public void TestRotationY()
        {
            // arrange
            var sut = new TransformD(null, "sut")
            {
                // act
                LocalRotation = QuaternionD.AngleAxis(90 * MathematicsConstants.DegreeToRadianD, Vector3D.PositiveY)
            };

            // assert
            var resultX = sut.PositiveX;
            var resultY = sut.PositiveY;
            var resultZ = sut.PositiveZ;

            Assert.That(resultX.EqualTo(Vector3D.NegativeZ), Is.True);
            Assert.That(resultY.EqualTo(Vector3D.PositiveY), Is.True);
            Assert.That(resultZ.EqualTo(Vector3D.PositiveX), Is.True);
        }

        [Test]
        public void TestRotationZ()
        {
            // arrange
            var sut = new TransformD(null, "sut")
            {
                // act
                LocalRotation = QuaternionD.AngleAxis(-90 * MathematicsConstants.DegreeToRadianD, Vector3D.PositiveZ)
            };

            // assert
            var resultX = sut.PositiveX;
            var resultY = sut.PositiveY;
            var resultZ = sut.PositiveZ;

            Assert.That(resultX.EqualTo(Vector3D.NegativeY), Is.True);
            Assert.That(resultY.EqualTo(Vector3D.PositiveX), Is.True);
            Assert.That(resultZ.EqualTo(Vector3D.PositiveZ), Is.True);
        }
    }
}
