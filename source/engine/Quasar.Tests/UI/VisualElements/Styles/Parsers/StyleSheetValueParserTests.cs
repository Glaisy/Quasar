//-----------------------------------------------------------------------
// <copyright file="StyleSheetValueParserTests.cs" company="Space Development">
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
using System.Runtime.InteropServices;

using Microsoft.Extensions.DependencyInjection;

using NUnit.Framework;

using Quasar.Graphics;
using Quasar.UI.VisualElements;
using Quasar.UI.VisualElements.Styles.Internals.Parsers;
using Quasar.Utilities;

using Space.Core.DependencyInjection;

namespace Quasar.Tests.UI.VisualElements.Styles.Parsers
{
    [TestFixture]
    internal class StyleSheetValueParserTests
    {
        private static IStringOperationContext stringOperationContext;

        /// <summary>
        /// Initializes the <see cref="StyleSheetValueParserTests"/> class.
        /// </summary>
        static StyleSheetValueParserTests()
        {
            var serviceProvider = new DynamicServiceProvider();
            var serviceLoader = serviceProvider.GetRequiredService<IServiceLoader>();
            serviceLoader.AddExportedServices(typeof(IStringOperationContext).Assembly);
            serviceLoader.AddExportedServices(typeof(DynamicServiceProvider).Assembly);
            stringOperationContext = serviceProvider.GetRequiredService<IStringOperationContext>();
        }


        #region ParseBorder
        [Test]
        public void ParseBorder_OK()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);
            var variables = new Dictionary<string, string>
            {
                { "--vari1", "1px" },
                { "--vari2", "2px 4" },
                { "--vari3", "4 2 3px" },
                { "--vari4", "1px 2px 3px 4" },
            };

            // act & assert 1
            var result = sut.ParseBorder("\"var(--vari1)\"", variables);
            Assert.That(result.Top, Is.EqualTo(1));
            Assert.That(result.Right, Is.EqualTo(1));
            Assert.That(result.Bottom, Is.EqualTo(1));
            Assert.That(result.Left, Is.EqualTo(1));

            // act & assert 2
            result = sut.ParseBorder("\"var(--vari2)\"", variables);
            Assert.That(result.Top, Is.EqualTo(2));
            Assert.That(result.Right, Is.EqualTo(4));
            Assert.That(result.Bottom, Is.EqualTo(2));
            Assert.That(result.Left, Is.EqualTo(4));

            // act & assert 3
            result = sut.ParseBorder("\"var(--vari3)\"", variables);
            Assert.That(result.Top, Is.EqualTo(4));
            Assert.That(result.Right, Is.EqualTo(2));
            Assert.That(result.Bottom, Is.EqualTo(3));
            Assert.That(result.Left, Is.EqualTo(2));

            // act & assert 4
            result = sut.ParseBorder("\"var(--vari4)\"", variables);
            Assert.That(result.Top, Is.EqualTo(1));
            Assert.That(result.Right, Is.EqualTo(2));
            Assert.That(result.Bottom, Is.EqualTo(3));
            Assert.That(result.Left, Is.EqualTo(4));

        }
        #endregion

        #region ParseBorderUnits
        [Test]
        public void ParseBorderUnits_Number_Pixel_Ok()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);
            var variables = new Dictionary<string, string>
            {
                { "--vari1", "1px" },
                { "--vari2", "2px 4" },
                { "--vari3", "4 2 3px" },
                { "--vari4", "1px 2px 3px 4" },
            };

            // act & assert 1
            var result = sut.ParseBorderUnits("\"var(--vari1)\"", variables, false);
            Assert.That(result.Top, Is.EqualTo(new Unit(1, UnitType.Pixel)));
            Assert.That(result.Right, Is.EqualTo(new Unit(1, UnitType.Pixel)));
            Assert.That(result.Bottom, Is.EqualTo(new Unit(1, UnitType.Pixel)));
            Assert.That(result.Left, Is.EqualTo(new Unit(1, UnitType.Pixel)));

            // act & assert 2
            result = sut.ParseBorderUnits("\"var(--vari2)\"", variables, false);
            Assert.That(result.Top, Is.EqualTo(new Unit(2, UnitType.Pixel)));
            Assert.That(result.Right, Is.EqualTo(new Unit(4, UnitType.Pixel)));
            Assert.That(result.Bottom, Is.EqualTo(new Unit(2, UnitType.Pixel)));
            Assert.That(result.Left, Is.EqualTo(new Unit(4, UnitType.Pixel)));

            // act & assert 3
            result = sut.ParseBorderUnits("\"var(--vari3)\"", variables, false);
            Assert.That(result.Top, Is.EqualTo(new Unit(4, UnitType.Pixel)));
            Assert.That(result.Right, Is.EqualTo(new Unit(2, UnitType.Pixel)));
            Assert.That(result.Bottom, Is.EqualTo(new Unit(3, UnitType.Pixel)));
            Assert.That(result.Left, Is.EqualTo(new Unit(2, UnitType.Pixel)));

            // act & assert 4
            result = sut.ParseBorderUnits("\"var(--vari4)\"", variables, false);
            Assert.That(result.Top, Is.EqualTo(new Unit(1, UnitType.Pixel)));
            Assert.That(result.Right, Is.EqualTo(new Unit(2, UnitType.Pixel)));
            Assert.That(result.Bottom, Is.EqualTo(new Unit(3, UnitType.Pixel)));
            Assert.That(result.Left, Is.EqualTo(new Unit(4, UnitType.Pixel)));
        }

        [Test]
        public void ParseBorderUnits_Number_Percentage_Ok()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);
            var variables = new Dictionary<string, string>
            {
                { "--vari1", "1%" },
                { "--vari2", "2px 4%" },
                { "--vari3", "4% 2 3px" },
                { "--vari4", "1px 2 3% 4%" },
            };

            // act & assert 1
            var result = sut.ParseBorderUnits("\"var(--vari1)\"", variables, true);
            Assert.That(result.Top, Is.EqualTo(new Unit(1, UnitType.Percentage)));
            Assert.That(result.Right, Is.EqualTo(new Unit(1, UnitType.Percentage)));
            Assert.That(result.Bottom, Is.EqualTo(new Unit(1, UnitType.Percentage)));
            Assert.That(result.Left, Is.EqualTo(new Unit(1, UnitType.Percentage)));

            // act & assert 2
            result = sut.ParseBorderUnits("\"var(--vari2)\"", variables, true);
            Assert.That(result.Top, Is.EqualTo(new Unit(2, UnitType.Pixel)));
            Assert.That(result.Right, Is.EqualTo(new Unit(4, UnitType.Percentage)));
            Assert.That(result.Bottom, Is.EqualTo(new Unit(2, UnitType.Pixel)));
            Assert.That(result.Left, Is.EqualTo(new Unit(4, UnitType.Percentage)));

            // act & assert 3
            result = sut.ParseBorderUnits("\"var(--vari3)\"", variables, true);
            Assert.That(result.Top, Is.EqualTo(new Unit(4, UnitType.Percentage)));
            Assert.That(result.Right, Is.EqualTo(new Unit(2, UnitType.Pixel)));
            Assert.That(result.Bottom, Is.EqualTo(new Unit(3, UnitType.Pixel)));
            Assert.That(result.Left, Is.EqualTo(new Unit(2, UnitType.Pixel)));

            // act & assert 4
            result = sut.ParseBorderUnits("\"var(--vari4)\"", variables, true);
            Assert.That(result.Top, Is.EqualTo(new Unit(1, UnitType.Pixel)));
            Assert.That(result.Right, Is.EqualTo(new Unit(2, UnitType.Pixel)));
            Assert.That(result.Bottom, Is.EqualTo(new Unit(3, UnitType.Percentage)));
            Assert.That(result.Left, Is.EqualTo(new Unit(4, UnitType.Percentage)));
        }

        [Test]
        public void ParseBorderUnits_Number_Percentage_NotAllowed_Fail()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);
            var variables = new Dictionary<string, string>
            {
                { "--vari1", "1%" },
                { "--vari2", "2px 4%" },
                { "--vari3", "4% 2 3px" },
                { "--vari4", "1px 2 3% 4%" },
            };

            // act & assert 1
            var result = Assert.Throws<StyleParserException>(() => sut.ParseBorderUnits("\"var(--vari1)\"", variables, false));
            Assert.That(result.Message.Contains("1%"), Is.True);

            // act & assert 2
            result = Assert.Throws<StyleParserException>(() => sut.ParseBorderUnits("\"var(--vari2)\"", variables, false));
            Assert.That(result.Message.Contains("2px 4%"), Is.True);

            // act & assert 3
            result = Assert.Throws<StyleParserException>(() => sut.ParseBorderUnits("\"var(--vari3)\"", variables, false));
            Assert.That(result.Message.Contains("4% 2 3px"), Is.True);

            // act & assert 4
            result = Assert.Throws<StyleParserException>(() => sut.ParseBorderUnits("\"var(--vari4)\"", variables, false));
            Assert.That(result.Message.Contains("1px 2 3% 4%"), Is.True);
        }
        #endregion

        #region ParseColor
        [Test]
        public void ParseColor_Null_Fail()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);

            // act & assert
            Assert.Throws<ArgumentNullException>(() => sut.ParseColor(null, null));
        }

        [Test]
        public void ParseColor_Empty_Fail()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);

            // act & assert
            Assert.Throws<ArgumentException>(() => sut.ParseColor(String.Empty, null));
        }

        [Test]
        public void ParseColor_RGB_Ok()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);
            var expectedResult = new Color(1, 2, 3, 255);
            var variables = new Dictionary<string, string>
            {
                { "--variable", "rgb(1,  2, 3)"}
            };

            // act
            var result = sut.ParseColor("var(--variable)", variables);

            // assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ParseColor_RGBA_Ok()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);
            var expectedResult = new Color(1, 2, 3, 0.5f);
            var variables = new Dictionary<string, string>
            {
                { "--variable", "rgba(1, 2, 3, 0.5)"}
            };

            // act
            var result = sut.ParseColor("var(--variable)", variables);

            // assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ParseColor_Hex8_Ok()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);
            var expectedResult = new Color(0xAA, 0xBB, 0xCC, 0xDD);

            // act
            var result = sut.ParseColor("#AABBCCDD", null);

            // assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ParseColor_Hex6_Ok()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);
            var expectedResult = new Color(0xAA, 0xBB, 0xCC, 0xFF);

            // act
            var result = sut.ParseColor("#AABBCC", null);

            // assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ParseColor_WellKnown_Ok()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);
            var expectedResult = Color.Magenta;

            // act
            var result = sut.ParseColor("Color.Magenta", null);

            // assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ParseColor_WellKnown_Fail()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);

            // act
            var result = Assert.Throws<StyleParserException>(() => sut.ParseColor("Color.Purple", null));

            // assert
            Assert.That(result.Message.Contains("Color.Purple"), Is.True);
        }
        #endregion

        #region ParseEnum
        [Test]
        public void ParseEnum_Null_Fail()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);

            // act & assert
            Assert.Throws<ArgumentNullException>(() => sut.ParseEnum<LayoutKind>(null, null));
        }

        [Test]
        public void ParseEnum_Empty_Fail()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);

            // act & assert
            Assert.Throws<ArgumentException>(() => sut.ParseEnum<LayoutKind>(String.Empty, null));
        }

        [Test]
        public void ParseEnum_NoVariables_Ok()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);
            var expectedResult = LayoutKind.Sequential;

            // act
            var result = sut.ParseEnum<LayoutKind>("Sequential", null);

            // assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ParseEnum_NoVariables_Fail()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);

            // act
            var result = Assert.Throws<StyleParserException>(() => sut.ParseEnum<LayoutKind>("Sequential__", null));

            // assert
            Assert.That(result.Message.Contains("Sequential__"), Is.True);
        }

        [Test]
        public void ParseEnum_Variables_Ok()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);
            var expectedResult = LayoutKind.Sequential;
            var variables = new Dictionary<string, string>
            {
                { "--variable1", "Seq"},
                { "--variable2", "i"}
            };

            // act
            var result = sut.ParseEnum<LayoutKind>("var(--variable1)uentvar(--variable2)al", variables);

            // assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ParseEnum_Variables_Null()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);

            // act
            var result = Assert.Throws<StyleParserException>(() => sut.ParseEnum<LayoutKind>("var(--variable)", null));

            // assert
            Assert.That(result.Message.Contains("--variable"), Is.True);
        }

        [Test]
        public void ParseEnum_Variables_Not_Existings()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);
            var variables = new Dictionary<string, string>();

            // act
            var result = Assert.Throws<StyleParserException>(() => sut.ParseEnum<LayoutKind>("var(--variable)", variables));

            // assert
            Assert.That(result.Message.Contains("--variable"), Is.True);
        }

        [Test]
        public void ParseEnum_Variables_Fail()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);
            var variables = new Dictionary<string, string>
            {
                { "--variable", "invalid"}
            };

            // act
            var result = Assert.Throws<StyleParserException>(() => sut.ParseEnum<LayoutKind>("var(--variable)", variables));

            // assert
            Assert.That(result.Message.Contains("invalid"), Is.True);
        }
        #endregion

        #region ParseLiteral
        [Test]
        public void ParseLiteral_OK()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);
            var variables = new Dictionary<string, string>
            {
                { "--vari1", "literal1" },
                { "--vari2", "'literal2'" },
                { "--vari3", "\"literal3\"" },
            };

            // act & assert 1
            var result = sut.ParseLiteral("\"var(--vari1)\"", variables);
            Assert.That(result, Is.EqualTo("literal1"));

            // act & assert 2
            result = sut.ParseLiteral("var(--vari2)", variables);
            Assert.That(result, Is.EqualTo("literal2"));

            // act & assert 3
            result = sut.ParseLiteral("var(--vari3)", variables);
            Assert.That(result, Is.EqualTo("literal3"));
        }
        #endregion

        #region ParseUnit
        [Test]
        public void ParseUnit_Null_Fail()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);

            // act & assert
            Assert.Throws<ArgumentNullException>(() => sut.ParseUnit(null, null, false));
        }

        [Test]
        public void ParseUnit_Empty_Fail()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);

            // act & assert
            Assert.Throws<ArgumentException>(() => sut.ParseUnit(String.Empty, null, true));
        }

        [Test]
        public void ParseUnit_Number_Fail()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);

            // act
            var result = Assert.Throws<StyleParserException>(() => sut.ParseUnit("12.2a4234", null, true));

            // assert
            Assert.That(result.Message.Contains("12.2a4234"), Is.True);
        }

        [Test]
        public void ParseUnit_Number_Ok()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);
            var expectedResult = new Unit(123, UnitType.Pixel);
            var variables = new Dictionary<string, string>
            {
                { "--variable", "123"}
            };

            // act
            var result = sut.ParseUnit("var(--variable)", variables, false);

            // assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ParseUnit_Percentage_NotAllowed_Fail()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);

            // act
            var result = Assert.Throws<StyleParserException>(() => sut.ParseUnit("12.25%", null, false));

            // assert
            Assert.That(result.Message.Contains("12.25%"), Is.True);
        }

        [Test]
        public void ParseUnit_Percentage_Format_Fail()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);

            // act
            var result = Assert.Throws<StyleParserException>(() => sut.ParseUnit("12._25%", null, true));

            // assert
            Assert.That(result.Message.Contains("12._25%"), Is.True);
        }

        [Test]
        public void ParsUnit_Percentage_Ok()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);
            var expectedResult = new Unit(123.456f, UnitType.Percentage);
            var variables = new Dictionary<string, string>
            {
                { "--variable", "123.456%"}
            };

            // act
            var result = sut.ParseUnit("var(--variable)", variables, true);

            // assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }
        #endregion

        #region ParseUrl
        [Test]
        public void ParseUrl_Ok()
        {
            // arrange
            var sut = new StyleSheetValueParser(stringOperationContext);
            var expectedResult = "http://url/res.ext";
            var variables = new Dictionary<string, string>
            {
                { "--vari1", expectedResult }
            };

            // act & assert 1
            var result = sut.ParseUrl("url(\"var(--vari1)\")", variables);
            Assert.That(result, Is.EqualTo(expectedResult));

            // act & assert 2
            result = sut.ParseUrl("url(\'var(--vari1)\')", variables);
            Assert.That(result, Is.EqualTo(expectedResult));

            // act & assert 3
            result = sut.ParseUrl("url(var(--vari1))", variables);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
        #endregion
    }
}
