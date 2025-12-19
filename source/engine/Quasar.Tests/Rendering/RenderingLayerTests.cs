//-----------------------------------------------------------------------
// <copyright file="RenderBatchTests.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using NSubstitute;

using NUnit.Framework;

using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.Rendering;
using Quasar.Rendering.Internals;

namespace Quasar.Tests.Rendering
{
    [TestFixture]
    internal sealed class RenderingLayerTests
    {
        [Test]
        public void GetRenderBatch()
        {
            // arrange
            var sut = new RenderingLayer(Layer.Default);
            var shader = CreateShader(1);

            // act
            var result1 = sut.GetRenderBatch(shader);
            var result2 = sut.GetRenderBatch(shader);

            // assert
            Assert.That(result1, Is.Not.Null);
            Assert.That(result2, Is.SameAs(result1));
        }

        [Test]
        public void Clear()
        {
            // arrange
            var sut = new RenderingLayer(Layer.Default);
            sut.GetRenderBatch(CreateShader(1));
            sut.GetRenderBatch(CreateShader(2));

            // act
            sut.Clear();
            var result = sut.GetEnumerator().MoveNext();

            // assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void GetEnumerator()
        {
            // arrange
            var sut = new RenderingLayer(Layer.Default);
            sut.GetRenderBatch(CreateShader(1));
            sut.GetRenderBatch(CreateShader(2));
            sut.GetRenderBatch(CreateShader(3));

            // act
            var result = sut.GetEnumerator();
            var count = 0;
            while (result.MoveNext())
            {
                count++;
            }

            // assert
            Assert.That(count, Is.EqualTo(3));
        }

        private static ShaderBase CreateShader(int handle)
        {
            var shaderMock = Substitute.For<ShaderBase>(handle.ToString(), String.Empty, default(GraphicsResourceDescriptor));
            shaderMock.Handle.Returns(handle);
            return shaderMock;
        }
    }
}
