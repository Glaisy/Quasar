//-----------------------------------------------------------------------
// <copyright file="PlaneDTests.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using NUnit.Framework;

using Quasar.Mathematics;
using Quasar.Tests.Extensions;

namespace Quasar.Tests.Mathematics
{
    [TestFixture]
    internal class EuclideanPlaneDTests
    {
        [Test]
        public void Constructor_Normal_D_Ok()
        {
            // arrange
            var expectedNormal = new Vector3D(1, 2, 3).Normalize();
            var expectedD = 4.56;

            // act
            var result = new EuclideanPlaneD(expectedNormal, expectedD);

            // assert
            Assert.That(AssertExtensions.EqualTo(result.Normal, expectedNormal), Is.True);
            Assert.That(AssertExtensions.EqualTo(result.D, expectedD), Is.True);
        }

        [Test]
        public void Constructor_Normal_Invalid()
        {
            // arrange, act, assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new EuclideanPlaneD(Vector3D.Zero, 1));
        }

        [Test]
        public void Constructor_Normal_Point_Ok()
        {
            // arrange
            var expectedNormal = Vector3D.PositiveY;
            var expectedD = 4.56;
            var point = new Vector3D(0.0, -expectedD, 0.0);

            // act
            var result = new EuclideanPlaneD(expectedNormal, point);

            // assert
            Assert.That(AssertExtensions.EqualTo(result.Normal, expectedNormal), Is.True);
            Assert.That(AssertExtensions.EqualTo(result.D, expectedD), Is.True);
        }

        [Test]
        [TestCaseSource("TestDataSource")]
        public void Distance_XYZ(Vector3D point, double expectedDistance)
        {
            // arrange 
            var normal = Vector3D.One.Normalize();
            var d = -Math.Sqrt(3.0);
            var sut = new EuclideanPlaneD(normal, d);

            // act
            var result = sut.Distance(point.X, point.Y, point.Z);

            // assert
            Assert.That(AssertExtensions.EqualTo(result, expectedDistance), Is.True);
        }

        [Test]
        public void Distance_Point_Wiki()
        {
            // arrange 
            var point = new Vector3D(1.0, 2.0, 3.0);
            var sut = new EuclideanPlaneD(new Vector3D(2.0, 3.0, 1.0), -5.0);
            var expectedResult = 6.0 * Math.Sqrt(14.0);

            // act
            var result = sut.Distance(point);

            // assert
            Assert.That(result, IsEx.NearlyEqualTo(expectedResult));
        }

        [Test]
        [TestCaseSource("TestDataSource")]
        public void Distance_Point(Vector3D point, double expectedDistance)
        {
            // arrange 
            var normal = Vector3D.One.Normalize();
            var d = -Math.Sqrt(3.0);
            var sut = new EuclideanPlaneD(normal, d);

            // act
            var result = sut.Distance(point);

            // assert
            Assert.That(AssertExtensions.EqualTo(result, expectedDistance), Is.True);
        }


        private static IEnumerable<TestCaseData> TestDataSource()
        {
            yield return new TestCaseData(Vector3D.Zero, -Math.Sqrt(3.0));
            yield return new TestCaseData(Vector3D.One, 0.0);
            yield return new TestCaseData(2.0 * Vector3D.One, Math.Sqrt(3.0));
        }
    }
}
