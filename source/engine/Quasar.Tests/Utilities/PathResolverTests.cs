//-----------------------------------------------------------------------
// <copyright file="PathResolverTests.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Microsoft.Extensions.DependencyInjection;

using NUnit.Framework;

using Quasar.Utilities;

using Space.Core.DependencyInjection;

namespace Quasar.Tests.Utilities
{
    [TestFixture]
    internal class PathResolverTests
    {
        private static readonly IPathResolver sut;


        static PathResolverTests()
        {
            var serviceProvider = new DynamicServiceProvider();
            var serviceLoader = serviceProvider.GetRequiredService<IServiceLoader>();
            serviceLoader.AddExportedServices(typeof(DynamicServiceProvider).Assembly);
            serviceLoader.AddExportedServices(typeof(IQuasarApplication).Assembly);
            sut = serviceProvider.GetRequiredService<IPathResolver>();
        }


        [Test]
        [TestCaseSource("GetDirectoryPathTestCaseSource")]
        public void GetDirectoryPath(string path, string expectedResult)
        {
            // act
            var result = sut.GetParentDirectoryPath(path);

            // assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCaseSource("ResolveTestCaseSource")]
        public void Resolve(string basePath, string path, string expectedResult)
        {
            // act
            var result = sut.Resolve(path, basePath);

            // assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCaseSource("TrimEndTestCaseSource")]
        public void TrimEnd(string path, string expectedResult)
        {
            // act
            var result = sut.TrimEnd(path);

            // assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }


        private static object[] ResolveTestCaseSource()
        {
            return
            [
                new object[]{ String.Empty, null, String.Empty },
                new object[]{ null, String.Empty, String.Empty },
                new object[]{ null, "relativePath/resource.ext", "relativePath/resource.ext" },
                new object[]{ null, "./relativePath/resource.ext", "relativePath/resource.ext" },
                new object[]{ null, "../../relativePath/resource.ext", "relativePath/resource.ext" },
                new object[]{ String.Empty, "relativePath/resource.ext", "relativePath/resource.ext" },
                new object[]{ String.Empty, "./relativePath/resource.ext", "relativePath/resource.ext" },
                new object[]{ String.Empty, "../../relativePath/resource.ext", "relativePath/resource.ext" },
                new object[]{ "basePath", "relativePath/resource.ext", "relativePath/resource.ext" },
                new object[]{ "./path/../basePath/", "./relativePath/resource.ext", "basePath/relativePath/resource.ext" },
                new object[]{ "basePath", "./relativePath/resource.ext", "basePath/relativePath/resource.ext" },
                new object[]{ "/basePath/", "./relativePath/resource.ext", "basePath/relativePath/resource.ext" },
                new object[]{ "basePath/dir1/dir2/dir3", "./../../relativePath/resource.ext", "basePath/dir1/relativePath/resource.ext" },
                new object[]{ "/basePath/dir1/dir2/dir3", "./.././relativePath/resource.ext", "basePath/dir1/dir2/relativePath/resource.ext" },
                new object[]{ "basePath/dir1/dir2/dir3", "./../relativePath/resource.ext", "basePath/dir1/dir2/relativePath/resource.ext" }
            ];
        }

        private static object[] GetDirectoryPathTestCaseSource()
        {
            return
            [
                new object[]{ null, String.Empty },
                new object[]{ String.Empty, String.Empty },
                new object[]{ "relativePath/resource.ext", "relativePath" },
                new object[]{ "relativePath/dir1/dir2/resource.ext", "relativePath/dir1/dir2" },
                new object[]{ "/resource.ext", String.Empty },
                new object[]{ "/dir1/resource.ext", "/dir1" },
                new object[]{ "resource.ext", String.Empty },
            ];
        }

        private static object[] TrimEndTestCaseSource()
        {
            return
            [
                new object[]{ "path/path2/", "path/path2" },
                new object[]{ "path/path2\\\\", "path/path2" },
                new object[]{ "path/path2//", "path/path2" },
                new object[]{ "path/path2", "path/path2" },
            ];
        }
    }
}
