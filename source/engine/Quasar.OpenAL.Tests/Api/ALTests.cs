//-----------------------------------------------------------------------
// <copyright file="ALTests.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

using NUnit.Framework;

using Quasar.OpenAL.Api;
using Quasar.Windows.OpenAL;

namespace Quasar.OpenAL.Tests.Api
{
    [TestFixture]
    internal class ALTests
    {
        [Test]
        [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Reviewed.")]
        public void Initialize()
        {
            // arrange
            var functionProvider = new OpenALInteropFunctionProvider();

            // act
            AL.Initialize(functionProvider);

            // assert
            Assert.That(AL.GetError, Is.Not.Null);
        }
    }
}
