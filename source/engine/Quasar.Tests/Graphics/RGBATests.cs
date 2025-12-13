//-----------------------------------------------------------------------
// <copyright file="RGBATests.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using NUnit.Framework;

using Quasar.Graphics;

namespace Quasar.Tests.Graphics
{
    [TestFixture]
    internal sealed class RGBATests
    {
        [TestCaseSource(nameof(Source_UInt32))]
        public void Constructor_UInt32(uint rgba, byte r, byte g, byte b, byte a)
        {
            // arrange and act
            var sut = new RGBA(rgba);

            // assert
            Assert.That(sut.A, Is.EqualTo(a));
            Assert.That(sut.R, Is.EqualTo(r));
            Assert.That(sut.G, Is.EqualTo(g));
            Assert.That(sut.B, Is.EqualTo(b));
        }

        [TestCaseSource(nameof(Source_Int32))]
        public void Constructor_Int32(int rgba, byte r, byte g, byte b, byte a)
        {
            // arrange and act
            var sut = new RGBA(rgba);

            // assert
            Assert.That(sut.A, Is.EqualTo(a));
            Assert.That(sut.R, Is.EqualTo(r));
            Assert.That(sut.G, Is.EqualTo(g));
            Assert.That(sut.B, Is.EqualTo(b));
        }

        [Test]
        public void ConvertToString()
        {
            // arrange
            var sut = new RGBA(0xAABBCCDD);

            // act
            var result = sut.ToString();

            // assert
            Assert.That(result, Is.EqualTo("AABBCCDD"));
        }

        [Test]
        public void ConvertToARGB()
        {
            // arrange
            var sut = new RGBA(0x12345678);

            // act
            var result = sut.ToRGBA();

            // assert
            Assert.That(result, Is.EqualTo(0x12345678));
        }

        [Test]
        public void TryParse_OK()
        {
            // arrange
            var expectedResult = new RGBA(0x226633DD);

            // act
            var result = RGBA.TryParse("226633DD", out var argb);

            // assert
            Assert.That(result, Is.True);
            Assert.That(argb, Is.EqualTo(expectedResult));
        }

        [Test]
        public void TryParse_Fail()
        {
            // arrange & act
            var result = RGBA.TryParse("xzasdqwq", out var argb);

            // assert
            Assert.That(result, Is.False);
            Assert.That(argb, Is.EqualTo(default(RGBA)));
        }


        private static object[] Source_UInt32 =
        {
            new object[] { 0xFFFFFFFF, (byte)0xFF, (byte)0xFF, (byte)0xFF, (byte)0xFF },
            new object[] { 0xFF000000, (byte)0xFF, (byte)0x00, (byte)0x00, (byte)0x00 },
            new object[] { (uint)0x00FF0000, (byte)0x00, (byte)0xFF, (byte)0x00, (byte)0x00 },
            new object[] { (uint)0x0000FF00, (byte)0x00, (byte)0x00, (byte)0xFF, (byte)0x00 },
            new object[] { (uint)0x000000FF, (byte)0x00, (byte)0x00, (byte)0x00, (byte)0xFF }
        };

        private static object[] Source_Int32 =
{
            new object[] { -1, (byte)0xFF, (byte)0xFF, (byte)0xFF, (byte)0xFF },
            new object[] { -16777216, (byte)0xFF, (byte)0x00, (byte)0x00, (byte)0x00 },
            new object[] { 0x00FF0000, (byte)0x00, (byte)0xFF, (byte)0x00, (byte)0x00 },
            new object[] { 0x0000FF00, (byte)0x00, (byte)0x00, (byte)0xFF, (byte)0x00 },
            new object[] { 0x000000FF, (byte)0x00, (byte)0x00, (byte)0x00, (byte)0xFF }
        };
    }
}
