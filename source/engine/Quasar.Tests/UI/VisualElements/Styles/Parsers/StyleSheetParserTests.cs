//-----------------------------------------------------------------------
// <copyright file="StyleSheetParserTests.cs" company="Space Development">
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
using System.IO;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using NUnit.Framework;

using Quasar.UI.VisualElements.Styles;
using Quasar.UI.VisualElements.Styles.Internals;
using Quasar.UI.VisualElements.Styles.Internals.Parsers;
using Quasar.UI.VisualElements.Themes;
using Quasar.Utilities;

using Space.Core.DependencyInjection;

namespace Quasar.Tests.UI.VisualElements.Styles.Parsers
{
    [TestFixture]
    internal class StyleSheetParserTests
    {
        private static readonly StyleTable variablesResult = new StyleTable
        {
            {
                ":root",
                new StyleProperties
                {
                    { "--Font-Family", "Conthrax" },
                    { "--Font-Size", "12px" },
                    { "--Color", "rgb(128, 224, 255)" },
                    { "--Color-Highlighted", "Color.White" },
                    { "--Color-Inverted", "rgba(#002440FF)" },
                    { "--Border-Width", "1px" },
                    { "--Padding", "5.2%" },
                    { "--Dummy", "Dummy" }
                }
            },
            {
                ".Dummy",
                new StyleProperties()
            }
        };

        private static readonly StyleTable stylesResult = new StyleTable
        {
            {
                "#name1",
                new StyleProperties
                {
                    { "name-attr1", "string" },
                    { "name-attr2", "20.1%" },
                    { "name-attr3", "var(--Font)" }
                }
            },
            {
                ".class1 > class1-child",
                new StyleProperties
                {
                    { "class1-atttr", "None" }
                }
            },
            {
                ".Dummy2",
                new StyleProperties()
            }
        };

        private static readonly StyleTable themeResult = new StyleTable
        {
            {
                ".class-theme1",
                new StyleProperties
                {
                    { "attr1", "2px" },
                    { "attr2", "Color.Red" },
                    { "attr3", "Hello World!" }
                }
            }
        };

        private static readonly Dictionary<string, string> testStyleSheets;


        private static readonly IServiceProvider serviceProvider;


        static StyleSheetParserTests()
        {
            serviceProvider = new DynamicServiceProvider();
            var serviceLoader = serviceProvider.GetRequiredService<IServiceLoader>();
            serviceLoader.AddExportedServices(typeof(IStringOperationContext).Assembly);
            serviceLoader.AddExportedServices(typeof(DynamicServiceProvider).Assembly);

            var resourceProviderFactory = serviceProvider.GetRequiredService<IResourceProviderFactory>();
            var resourceProvider = resourceProviderFactory.Create(Assembly.GetExecutingAssembly(), "Quasar/Tests/Resources/Styles");
            testStyleSheets = new Dictionary<string, string>();
            LoadStyleSheet(resourceProvider, ThemeConstants.ThemeRootStyleSheetPath, testStyleSheets);
            LoadStyleSheet(resourceProvider, "Styles.qss", testStyleSheets);
            LoadStyleSheet(resourceProvider, "Includes/Variables.qss", testStyleSheets);
            LoadStyleSheet(resourceProvider, "Includes/ImportDir/Dummy.qss", testStyleSheets);
            LoadStyleSheet(resourceProvider, "Includes/ImportDir/Dummy2.qss", testStyleSheets);
        }

        private static void LoadStyleSheet(IResourceProvider resourceProvider, string path, Dictionary<string, string> testStyleSheets)
        {
            using (var reader = new StreamReader(resourceProvider.GetResourceStream(path), leaveOpen: false))
            {
                var stylesheet = reader.ReadToEnd();
                testStyleSheets.Add(path, stylesheet);
            }
        }

        [Test]
        public void Parse_Args()
        {
            // arrange
            var sut = CreateSut();

            // act & assert
            Assert.Throws<ArgumentNullException>(() => sut.Parse(null, "something"));
            Assert.Throws<ArgumentNullException>(() => sut.Parse(new Dictionary<string, string>(), null));
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.Parse(new Dictionary<string, string>(), String.Empty));
        }

        [Test]
        [TestCaseSource(nameof(ParseTestCaseSource))]
        public void Parse(string path, StyleTable expectedResult)
        {
            // arrange
            var sut = CreateSut();

            // act
            var result = sut.Parse(testStyleSheets, path);

            // assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(expectedResult.Count));
            Assert.That(AreEqual(result, expectedResult), Is.True);
        }

        [Test]
        public void ParseInline_Ok()
        {
            // arrange
            var sut = CreateSut();
            var expectedResult = new StyleProperties
                {
                    { "attr1", "2px" },
                    { "attr2", "Color.Red" },
                    { "attr3", "\"Hello World!\"" }
                };

            // act
            var result = sut.ParseInline("attr1: 2px;attr2:Color.Red ; attr3:   \"Hello World!\"", "templatePath");
        }


        private static bool AreEqual(
            StyleTable result,
            StyleTable expectedResult)
        {
            foreach (var pairBySelector in result)
            {
                if (!expectedResult.TryGetValue(pairBySelector.Key, out var expectedSet))
                {
                    return false;
                }

                if (pairBySelector.Value.Count != expectedSet.Count)
                {
                    return false;
                }

                foreach (var keyValue in pairBySelector.Value)
                {
                    if (!expectedSet.TryGetValue(keyValue.Key, out var expectedValue))
                    {
                        return false;
                    }

                    if (expectedValue != keyValue.Value)
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        private IStyleSheetParser CreateSut()
        {
            return serviceProvider.GetRequiredService<IStyleSheetParser>();
        }

        private static object[] ParseTestCaseSource()
        {
            var combinedThemeResult = new StyleTable();
            foreach (var pair in variablesResult)
            {
                combinedThemeResult.Add(pair.Key, pair.Value);
            }

            foreach (var pair in stylesResult)
            {
                combinedThemeResult.Add(pair.Key, pair.Value);
            }

            foreach (var pair in themeResult)
            {
                combinedThemeResult.Add(pair.Key, pair.Value);
            }

            return
            [
                new object[] { "Includes/Variables.qss", variablesResult },
                new object[] { "Styles.qss", stylesResult },
                new object[] { ThemeConstants.ThemeRootStyleSheetPath, combinedThemeResult }
            ];
        }
    }
}
