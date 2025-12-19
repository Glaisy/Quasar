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

using Microsoft.Extensions.DependencyInjection;

using NSubstitute;

using NUnit.Framework;

using Quasar.Graphics;
using Quasar.Rendering;
using Quasar.Rendering.Internals;

using Space.Core.DependencyInjection;

namespace Quasar.Tests.Rendering
{
    [TestFixture]
    [SingleThreaded]
    internal sealed class RenderBatchTests
    {
        static RenderBatchTests()
        {
            var dynamicServiceProvider = new DynamicServiceProvider();
            var serviceLoader = dynamicServiceProvider.GetRequiredService<IServiceLoader>();
            serviceLoader.AddExportedServices(typeof(RenderBatch).Assembly);
            serviceLoader.AddSingleton(Substitute.For<IMatrixFactory>());
            RenderModel.InitializeStaticServices(dynamicServiceProvider);
        }

        [Test]
        public void AddModel_BackCulled()
        {
            // arrange
            var renderModel = new RenderModel();
            var sut = new RenderBatch(null);

            // act
            sut.AddModel(renderModel);

            // assert
            Assert.That(sut.Models.Contains(renderModel), Is.True);
            Assert.That(sut.DoubleSidedModels.Contains(renderModel), Is.False);
            Assert.That(renderModel.State.RenderBatch, Is.EqualTo(sut));
        }

        [Test]
        public void AddModel_DoubleSided()
        {
            // arrange
            var renderModel = new RenderModel();
            renderModel.State.Flags |= RenderModelStateFlags.DoubleSided;
            var sut = new RenderBatch(null);

            // act
            sut.AddModel(renderModel);

            // assert
            Assert.That(sut.Models.Contains(renderModel), Is.False);
            Assert.That(sut.DoubleSidedModels.Contains(renderModel), Is.True);
            Assert.That(renderModel.State.RenderBatch, Is.EqualTo(sut));
        }

        [Test]
        public void Remove_BackCulled()
        {
            // arrange
            var renderModel = new RenderModel();
            var sut = new RenderBatch(null);
            sut.AddModel(renderModel);

            // act
            sut.RemoveModel(renderModel);

            // assert
            Assert.That(sut.Models.Count, Is.EqualTo(0));
            Assert.That(sut.DoubleSidedModels.Count, Is.EqualTo(0));
            Assert.That(renderModel.State.IsActive, Is.False);
            Assert.That(renderModel.State.RenderBatch, Is.Null);
        }

        [Test]
        public void Remove_DoubleSided()
        {
            // arrange
            var renderModel = new RenderModel();
            renderModel.State.Flags |= RenderModelStateFlags.DoubleSided;
            var sut = new RenderBatch(null);
            sut.AddModel(renderModel);

            // act
            sut.RemoveModel(renderModel);

            // assert
            Assert.That(sut.Models.Count, Is.EqualTo(0));
            Assert.That(sut.DoubleSidedModels.Count, Is.EqualTo(0));
            Assert.That(renderModel.State.IsActive, Is.False);
            Assert.That(renderModel.State.RenderBatch, Is.Null);
        }

        [Test]
        public void MoveModel_BackfaceCulled_DoubleSided()
        {
            // arrange
            var renderModel = new RenderModel();
            var sut = new RenderBatch(null);
            sut.AddModel(renderModel);

            // act
            renderModel.State.Flags |= RenderModelStateFlags.DoubleSided;
            sut.MoveModel(renderModel);

            // assert
            Assert.That(sut.Models.Contains(renderModel), Is.False);
            Assert.That(sut.DoubleSidedModels.Contains(renderModel), Is.True);
            Assert.That(renderModel.State.RenderBatch, Is.EqualTo(sut));
        }

        [Test]
        public void MoveModel_DoubleSided_BackfaceCulled()
        {
            // arrange
            var renderModel = new RenderModel();
            renderModel.State.Flags |= RenderModelStateFlags.DoubleSided;

            var sut = new RenderBatch(null);
            sut.AddModel(renderModel);

            // act
            renderModel.State.Flags &= ~RenderModelStateFlags.DoubleSided;
            sut.MoveModel(renderModel);

            // assert
            Assert.That(sut.Models.Contains(renderModel), Is.True);
            Assert.That(sut.DoubleSidedModels.Contains(renderModel), Is.False);
            Assert.That(renderModel.State.RenderBatch, Is.EqualTo(sut));
        }

        [Test]
        public void Clear()
        {
            // arrange
            var renderModel1 = new RenderModel();
            var renderModel2 = new RenderModel();
            renderModel2.State.Flags |= RenderModelStateFlags.DoubleSided;

            var sut = new RenderBatch(null);
            sut.AddModel(renderModel1);
            sut.AddModel(renderModel2);

            // act
            sut.Clear();

            // assert
            Assert.That(sut.Models.Count, Is.EqualTo(0));
            Assert.That(sut.DoubleSidedModels.Count, Is.EqualTo(0));
            Assert.That(renderModel1.State.IsActive, Is.False);
            Assert.That(renderModel1.State.RenderBatch, Is.Null);
            Assert.That(renderModel2.State.IsActive, Is.False);
            Assert.That(renderModel2.State.RenderBatch, Is.Null);
        }
    }
}
