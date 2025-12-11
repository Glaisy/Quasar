//-----------------------------------------------------------------------
// <copyright file="PlaneTests.cs" company="Space Development">
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
    internal class EuclideanPlaneTests
    {
        [Test]
        public void Constructor_Normal_D_Ok()
        {
            // arrange
            var expectedNormal = new Vector3(1, 2, 3).Normalize();
            var expectedD = 4.56f;

            // act
            var result = new EuclideanPlane(expectedNormal, expectedD);

            // assert
            Assert.That(AssertExtensions.EqualTo(result.Normal, expectedNormal), Is.True);
            Assert.That(result.D, IsEx.NearlyEqualTo(expectedD));
        }

        [Test]
        public void Constructor_Normal_Point_Ok()
        {
            // arrange
            var expectedNormal = Vector3.PositiveY;
            var expectedD = 4.56f;
            var point = new Vector3(0.0f, -expectedD, 0.0f);

            // act
            var result = new EuclideanPlane(expectedNormal, point);

            // assert
            Assert.That(AssertExtensions.EqualTo(result.Normal, expectedNormal), Is.True);
            Assert.That(result.D, IsEx.NearlyEqualTo(expectedD));
        }

        [Test]
        public void Constructor_Normal_Invalid()
        {
            // arrange, act, assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new EuclideanPlane(Vector3.Zero, 1));
        }

        [Test]
        [TestCaseSource(nameof(TestDataSource))]
        public void Distance_XYZ(Vector3 point, float expectedDistance)
        {
            // arrange 
            var normal = Vector3.One.Normalize();
            var d = -MathF.Sqrt(3.0f);
            var sut = new EuclideanPlane(normal, d);

            // act
            var result = sut.Distance(point.X, point.Y, point.Z);

            // assert
            Assert.That(result, IsEx.NearlyEqualTo(expectedDistance));
        }

        [Test]
        [TestCaseSource(nameof(TestDataSource))]
        public void Distance_Point(Vector3 point, float expectedDistance)
        {
            // arrange 
            var normal = Vector3.One.Normalize();
            var d = -MathF.Sqrt(3.0f);
            var sut = new EuclideanPlane(normal, d);

            // act
            var result = sut.Distance(point);

            // assert
            Assert.That(result, IsEx.NearlyEqualTo(expectedDistance));
        }

        [Test]
        [TestCaseSource(nameof(TestDataSource_By_Point))]
        public void Distance_Point_By_Point(Vector3 point)
        {
            // arrange 
            var normal = Vector3.One.Normalize();
            var sut = new EuclideanPlane(normal, point);

            // act
            var result = sut.Distance(point);

            // assert
            Assert.That(result, IsEx.NearlyEqualTo(0.0f));
        }


        private static IEnumerable<TestCaseData> TestDataSource()
        {
            yield return new TestCaseData(Vector3.Zero, -MathF.Sqrt(3.0f));
            yield return new TestCaseData(Vector3.One, 0.0f);
            yield return new TestCaseData(2.0f * Vector3.One, MathF.Sqrt(3.0f));
        }

        private static IEnumerable<TestCaseData> TestDataSource_By_Point()
        {
            yield return new TestCaseData(Vector3.Zero);
            yield return new TestCaseData(Vector3.One);
            yield return new TestCaseData(2.0f * Vector3.One);
        }
    }
}
