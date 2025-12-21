//-----------------------------------------------------------------------
// <copyright file="StyleNameValidatorTests.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using Quasar.UI.VisualElements.Styles.Internals;

namespace Quasar.Tests.UI.VisualElements.Styles
{
    [TestFixture]
    internal class StyleNameValidatorTests
    {
        [Test]
        public void IsValidName_String_Args()
        {
            // arrange 
            var sut = new StyleNameValidator();

            // act & assert
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.IsValidName("text", -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.IsValidName("text", 5, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.IsValidName("text", 0, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.IsValidName("text", 1, 4));
        }

        [Test]
        public void IsValidName_Span_Args()
        {
            // arrange 
            var sut = new StyleNameValidator();

            // act & assert
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.IsValidName("text".AsSpan(), -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.IsValidName("text".AsSpan(), 5, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.IsValidName("text".AsSpan(), 0, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.IsValidName("text".AsSpan(), 1, 4));
        }

        [Test]
        [TestCaseSource(nameof(TestCaseSourceFull))]
        public void IsValidName_String_Full(string text, bool expectedValue)
        {
            // arrange
            var sut = new StyleNameValidator();
            // act
            var result = sut.IsValidName(text);

            // assert
            Assert.That(result, Is.EqualTo(expectedValue));
        }

        [Test]
        [TestCaseSource(nameof(TestCaseSourceStart))]
        public void IsValidName_String_Start(string text, int startIndex, bool expectedValue)
        {
            // arrange
            var sut = new StyleNameValidator();

            // act
            var result = sut.IsValidName(text, startIndex);

            // assert
            Assert.That(result, Is.EqualTo(expectedValue));
        }

        [Test]
        [TestCaseSource(nameof(TestCaseSourceRange))]
        public void IsValidName_String_Range(string text, int startIndex, int length, bool expectedValue)
        {
            // arrange
            var sut = new StyleNameValidator();

            // act
            var result = sut.IsValidName(text, startIndex, length);

            // assert
            Assert.That(result, Is.EqualTo(expectedValue));
        }

        [Test]
        [TestCaseSource(nameof(TestCaseSourceFull))]
        public void IsValidName_Span_Full(string text, bool expectedValue)
        {
            // arrange
            var sut = new StyleNameValidator();

            // act
            var result = sut.IsValidName(text.AsSpan());

            // assert
            Assert.That(result, Is.EqualTo(expectedValue));
        }

        [Test]
        [TestCaseSource(nameof(TestCaseSourceStart))]
        public void IsValidName_Span_Start(string text, int startIndex, bool expectedValue)
        {
            // arrange
            var sut = new StyleNameValidator();

            // act
            var result = sut.IsValidName(text.AsSpan(), startIndex);

            // assert
            Assert.That(result, Is.EqualTo(expectedValue));
        }

        [Test]
        [TestCaseSource(nameof(TestCaseSourceRange))]
        public void IsValidName_Span_Range(string text, int startIndex, int length, bool expectedValue)
        {
            // arrange
            var sut = new StyleNameValidator();

            // act
            var result = sut.IsValidName(text.AsSpan(), startIndex, length);

            // assert
            Assert.That(result, Is.EqualTo(expectedValue));
        }


        private static object[] TestCaseSourceFull()
        {
            return
            [
                new object[] { default(string), false },
                new object[] { String.Empty, false },
                new object[] { "validName1", true },
                new object[] { "Valid-Name2", true },
                new object[] { "-_Valid3", true },
                new object[] { "--Valid4", true },
                new object[] { "1invalid1", false },
                new object[] { "invalid%2", false },
                new object[] { "inva-#lid3", false },
                new object[] { "inválid3", false }
            ];
        }

        private static object[] TestCaseSourceStart()
        {
            return
            [
                new object[] { default(string), 0, false },
                new object[] { String.Empty, 1, false },
                new object[] { "validName1", 4, true },
                new object[] { "Valid-Name2", 2, true },
                new object[] { "-_Valid3", 1, true },
                new object[] { "--Valid4", 0, true },
                new object[] { "1invalid1", 3, true },
                new object[] { "invalid%2", 5, false },
                new object[] { "inva-#lid3", 6, true },
                new object[] { "inválid3", 3, false },
                new object[] { "inválid3", 4, true }
            ];
        }

        private static object[] TestCaseSourceRange()
        {
            return
            [
                new object[] { default(string), 0, 1, false },
                new object[] { String.Empty, 0, 3, false },
                new object[] { "validName1", 4, 3, true },
                new object[] { "Valid-Name2", 2, 4, false },
                new object[] { "-_Val1id3", 5, 4, false },
                new object[] { "--1Valid4", 3, 6, true },
                new object[] { "1invalid1", 0, 3, false},
                new object[] { "invalid%2", 4, 5, false },
                new object[] { "inva-#lid3", 0, 6, false },
                new object[] { "inválid3", 4, 3, true },
                new object[] { "inválid3", 2, 4, false },
                new object[] { "inv-lid3", 3, 1, false },
                new object[] { "inv-lid3", 3, 2, true },
                new object[] { "inv-lid3", 2, 2, false },
                new object[] { "1in2valid1", 3, 1, false}
            ];
        }
    }
}
