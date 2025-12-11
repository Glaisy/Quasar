//-----------------------------------------------------------------------
// <copyright file="TransformTests.cs" company="Space Development">
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
    public class TransformTests
    {
        [Test]
        public void TestRotationX()
        {
            // arrange
            var sut = new Transform(null, "sut")
            {
                // act
                LocalRotation = Quaternion.AngleAxis(90 * MathematicsConstants.DegreeToRadian, Vector3.PositiveX)
            };

            // assert
            var resultX = sut.PositiveX;
            var resultY = sut.PositiveY;
            var resultZ = sut.PositiveZ;

            Assert.That(resultX.EqualTo(Vector3.PositiveX), Is.True);
            Assert.That(resultY.EqualTo(Vector3.PositiveZ), Is.True);
            Assert.That(resultZ.EqualTo(Vector3.NegativeY), Is.True);
        }

        [Test]
        public void TestRotationY()
        {
            // arrange
            var sut = new Transform(null, "sut")
            {
                // act
                LocalRotation = Quaternion.AngleAxis(90 * MathematicsConstants.DegreeToRadian, Vector3.PositiveY)
            };

            // assert
            var resultX = sut.PositiveX;
            var resultY = sut.PositiveY;
            var resultZ = sut.PositiveZ;

            Assert.That(resultX.EqualTo(Vector3.NegativeZ), Is.True);
            Assert.That(resultY.EqualTo(Vector3.PositiveY), Is.True);
            Assert.That(resultZ.EqualTo(Vector3.PositiveX), Is.True);
        }

        [Test]
        public void TestRotationZ()
        {
            // arrange
            var sut = new Transform(null, "sut")
            {
                // act
                LocalRotation = Quaternion.AngleAxis(-90 * MathematicsConstants.DegreeToRadian, Vector3.PositiveZ)
            };

            // assert
            var resultX = sut.PositiveX;
            var resultY = sut.PositiveY;
            var resultZ = sut.PositiveZ;

            Assert.That(resultX.EqualTo(Vector3.NegativeY), Is.True);
            Assert.That(resultY.EqualTo(Vector3.PositiveX), Is.True);
            Assert.That(resultZ.EqualTo(Vector3.PositiveZ), Is.True);
        }
    }
}
