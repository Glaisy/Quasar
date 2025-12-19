using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using NSubstitute;

using NUnit.Framework;

using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.Rendering;
using Quasar.Rendering.Internals;
using Quasar.Rendering.Processors.Internals;

using Space.Core.DependencyInjection;

namespace Quasar.Tests.Rendering
{
    [TestFixture]
    [SingleThreaded]
    internal class RenderModelCommandProcessorTests
    {
        private static readonly IServiceProvider serviceProvider;
        static RenderModelCommandProcessorTests()
        {
            var dynamicServiceProvider = new DynamicServiceProvider();
            var serviceLoader = dynamicServiceProvider.GetRequiredService<IServiceLoader>();
            serviceLoader.AddExportedServices(typeof(RenderModelCommand).Assembly);
            serviceLoader.AddSingleton(Substitute.For<IMatrixFactory>());
            serviceProvider = dynamicServiceProvider;
        }


        [Test]
        [TestCaseSource(nameof(Boolean_DataSource))]
        public void CreateCommand(bool isModelEnabled)
        {
            // arrange
            var sut = CreateSutAndRawModel(isModelEnabled, out var model);

            // act
            sut.ExecuteCommands();
            var result = model.State;

            // assert
            Assert.That(result.RenderingLayer, Is.Not.Null);
            Assert.That(result.RenderingLayer.Layer, Is.EqualTo(model.Layer));
            Assert.That(result.Flags == (isModelEnabled ? RenderModelStateFlags.Enabled : RenderModelStateFlags.None), Is.True);
            Assert.That(result.RenderBatch, Is.Null);
            Assert.That(result.Material, Is.Null);
            Assert.That(result.Mesh, Is.Null);
        }

        [Test]
        [TestCaseSource(nameof(Boolean_Boolean_DataSource))]
        public void DoubleSidedChangedCommand_Inactive(bool isDoubleSided, bool isEnabled)
        {
            // arrange
            var sut = CreateSutAndRawModel(isEnabled, out var model);

            var expectedFlags = isEnabled ? RenderModelStateFlags.Enabled : RenderModelStateFlags.None;
            if (isDoubleSided)
            {
                expectedFlags |= RenderModelStateFlags.DoubleSided;
            }

            // act
            model.DoubleSided = isDoubleSided;
            sut.ExecuteCommands();
            var result = model.State;

            // assert
            Assert.That(result.RenderingLayer, Is.Not.Null);
            Assert.That(result.RenderingLayer.Layer, Is.EqualTo(model.Layer));
            Assert.That(result.Flags, Is.EqualTo(expectedFlags));
            Assert.That(result.RenderBatch, Is.Null);
            Assert.That(result.Material, Is.Null);
            Assert.That(result.Mesh, Is.Null);
        }

        [Test]
        [TestCaseSource(nameof(Boolean_DataSource))]
        public void DoubleSidedChangedCommand_Rendered(bool isDoubleSided)
        {
            // arrange
            var sut = CreateSutAndRenderedModel(out var model);

            // act
            model.DoubleSided = !isDoubleSided;
            model.DoubleSided = isDoubleSided;

            sut.ExecuteCommands();
            var result = model.State;

            // assert
            Assert.That(result.RenderingLayer, Is.Not.Null);
            Assert.That(result.RenderingLayer.Layer, Is.EqualTo(model.Layer));
            Assert.That(result.IsRendered, Is.EqualTo(true));
            Assert.That(result.RenderBatch, Is.Not.Null);

            if (isDoubleSided)
            {
                Assert.That(result.RenderBatch.DoubleSidedModels.Contains(model), Is.True);
            }
            else
            {
                Assert.That(result.RenderBatch.Models.Contains(model), Is.True);
            }
        }

        [Test]
        [TestCaseSource(nameof(Boolean_DataSource))]
        public void EnabledChangedCommand_Inactive(bool isEnabled)
        {
            // arrange
            var sut = CreateSutAndRawModel(!isEnabled, out var model);

            // act
            model.IsEnabled = isEnabled;
            sut.ExecuteCommands();
            var result = model.State;

            // assert
            Assert.That(result.RenderingLayer, Is.Not.Null);
            Assert.That(result.RenderingLayer.Layer, Is.EqualTo(model.Layer));
            Assert.That(result.IsEnabled, Is.EqualTo(isEnabled));
            Assert.That(result.RenderBatch, Is.Null);
            Assert.That(result.Material, Is.Null);
            Assert.That(result.Mesh, Is.Null);
        }

        [Test]
        [TestCaseSource(nameof(Boolean_DataSource))]
        public void EnabledChangedCommand_Rendered(bool isEnabled)
        {
            // arrange
            var sut = CreateSutAndRenderableByPropertiesModel(!isEnabled, out var model);

            // act
            model.IsEnabled = isEnabled;
            sut.ExecuteCommands();
            var result = model.State;

            // assert
            Assert.That(result.RenderingLayer, Is.Not.Null);
            Assert.That(result.RenderingLayer.Layer, Is.EqualTo(model.Layer));
            Assert.That(result.IsEnabled, Is.EqualTo(isEnabled));
            Assert.That(result.IsRendered, Is.EqualTo(isEnabled));

            if (isEnabled)
            {
                Assert.That(result.RenderBatch, Is.Not.Null);
            }
            else
            {
                Assert.That(result.RenderBatch, Is.Null);
            }
        }

        [Test]
        [TestCaseSource(nameof(Boolean_DataSource))]
        public void LayerChangedCommand_Inactive(bool isEnabled)
        {
            // arrange
            var sut = CreateSutAndRawModel(isEnabled, out var model);

            // act
            model.Layer = Layer.Transparent;
            sut.ExecuteCommands();
            var result = model.State;

            // assert
            Assert.That(result.RenderingLayer, Is.Not.Null);
            Assert.That(result.RenderingLayer.Layer, Is.EqualTo(model.Layer));
            Assert.That(result.IsRendered, Is.EqualTo(false));
            Assert.That(result.RenderBatch, Is.Null);
            Assert.That(result.Material, Is.Null);
            Assert.That(result.Mesh, Is.Null);
        }

        [Test]
        public void LayerChangedCommand_Rendered()
        {
            // arrange
            var sut = CreateSutAndRawModel(true, out var model);
            var shader = CreateShader(2);
            model.Material = new Material(shader);
            sut.ExecuteCommands();

            var mesh = CreateMesh(1);
            model.SetMesh(mesh, true);
            sut.ExecuteCommands();

            // act
            var previousRenderBatch = model.State.RenderBatch;
            Assert.That(previousRenderBatch, Is.Not.Null);
            Assert.That(previousRenderBatch.Models.Contains(model), Is.True);
            Assert.That(model.State.IsRendered, Is.EqualTo(true));

            model.Layer = Layer.Transparent;
            sut.ExecuteCommands();
            var result = model.State;

            // assert
            Assert.That(previousRenderBatch.Models.Contains(model), Is.False);

            Assert.That(result.RenderingLayer, Is.Not.Null);
            Assert.That(result.RenderingLayer.Layer, Is.EqualTo(model.Layer));
            Assert.That(result.IsRendered, Is.EqualTo(true));
            Assert.That(result.RenderBatch, Is.Not.Null);
            Assert.That(result.RenderBatch.Models.Contains(model), Is.True);
            Assert.That(result.Material, Is.SameAs(model.Material));
            Assert.That(result.Mesh, Is.SameAs(mesh));
        }

        [Test]
        [TestCaseSource(nameof(Boolean_DataSource))]
        public void MaterialChangedCommand_Inactive(bool isEnabled)
        {
            // arrange
            var sut = CreateSutAndRawModel(isEnabled, out var model);
            var shader = CreateShader(1);
            var material = new Material(shader);

            // act
            model.Material = material;
            sut.ExecuteCommands();
            var result = model.State;

            // assert
            Assert.That(result.IsRendered, Is.EqualTo(false));
            Assert.That(result.RenderBatch, Is.Null);
            Assert.That(result.Mesh, Is.Null);
            Assert.That(result.Material, Is.SameAs(material));
        }

        [Test]
        public void MaterialChangedCommand_Rendered_SetNull()
        {
            // arrange
            var sut = CreateSutAndRenderedModel(out var model);

            // act
            model.Material = null;
            sut.ExecuteCommands();
            var result = model.State;

            // assert
            Assert.That(result.IsRendered, Is.EqualTo(false));
            Assert.That(result.RenderBatch, Is.Null);
            Assert.That(result.Mesh, Is.Not.Null);
            Assert.That(result.Material, Is.Null);
        }

        [Test]
        public void MaterialChangedCommand_Rendered_Other()
        {
            // arrange
            var sut = CreateSutAndRenderedModel(out var model);
            var material = new Material(CreateShader(191));
            // act
            model.Material = material;
            sut.ExecuteCommands();
            var result = model.State;

            // assert
            Assert.That(result.IsRendered, Is.EqualTo(true));
            Assert.That(result.RenderBatch, Is.Not.Null);
            Assert.That(result.RenderBatch.Models.Contains(model), Is.True);
            Assert.That(result.Mesh, Is.Not.Null);
            Assert.That(result.Material, Is.SameAs(material));
        }

        [Test]
        [TestCaseSource(nameof(Boolean_DataSource))]
        public void MeshChangedCommand_Inactive(bool isEnabled)
        {
            // arrange
            var sut = CreateSutAndRawModel(isEnabled, out var model);
            var mesh = CreateMesh(1);

            // act
            model.SetMesh(mesh, true);
            sut.ExecuteCommands();
            var result = model.State;

            // assert
            Assert.That(result.IsRendered, Is.EqualTo(false));
            Assert.That(result.RenderBatch, Is.Null);
            Assert.That(result.Material, Is.Null);
            Assert.That(result.Mesh, Is.SameAs(mesh));
        }

        [Test]
        public void MeshChangedCommand_Rendered_SetNull()
        {
            // arrange
            var sut = CreateSutAndRenderedModel(out var model);

            // act
            model.SetMesh(null, false);
            sut.ExecuteCommands();
            var result = model.State;

            // assert
            Assert.That(result.IsRendered, Is.EqualTo(false));
            Assert.That(result.RenderBatch, Is.Null);
            Assert.That(result.Mesh, Is.Null);
        }

        [Test]
        public void MeshChangedCommand_Rendered_Other()
        {
            // arrange
            var sut = CreateSutAndRenderedModel(out var model);
            var mesh = CreateMesh(191);

            // act
            model.SetMesh(mesh, false);
            sut.ExecuteCommands();
            var result = model.State;

            // assert
            Assert.That(result.IsRendered, Is.EqualTo(true));
            Assert.That(result.RenderBatch, Is.Not.Null);
            Assert.That(result.RenderBatch.Models.Contains(model), Is.True);
            Assert.That(result.Mesh, Is.SameAs(mesh));
        }


        private static IEnumerable<object[]> Boolean_DataSource()
        {
            yield return [false];
            yield return [true];
        }

        private static IEnumerable<object[]> Boolean_Boolean_DataSource()
        {
            yield return [false, false];
            yield return [false, true];
            yield return [true, false];
            yield return [true, true];
        }


        private static ShaderBase CreateShader(int handle)
        {
            var shaderMock = Substitute.For<ShaderBase>(handle.ToString(), String.Empty, default(GraphicsResourceDescriptor));
            shaderMock.Handle.Returns(handle);
            return shaderMock;
        }

        private static IMesh CreateMesh(int handle)
        {
            var mesh = Substitute.For<IMesh>();
            mesh.Handle.Returns(handle);
            return mesh;
        }

        private static RenderModelCommandProcessor CreateSutAndRenderedModel(out RenderModel model)
        {
            return CreateSutAndRenderableByPropertiesModel(true, out model);
        }

        private static RenderModelCommandProcessor CreateSutAndRawModel(bool isModelEnabled, out RenderModel model)
        {
            var renderingCommandProcessor = serviceProvider.GetRequiredService<RenderModelCommandProcessor>();
            RenderModel.InitializeStaticServices(serviceProvider);

            model = new RenderModel(isModelEnabled);
            return renderingCommandProcessor;
        }

        private static RenderModelCommandProcessor CreateSutAndRenderableByPropertiesModel(bool isModelEnabled, out RenderModel model)
        {
            var renderingCommandProcessor = serviceProvider.GetRequiredService<RenderModelCommandProcessor>();
            RenderModel.InitializeStaticServices(serviceProvider);

            model = new RenderModel(isModelEnabled);
            model.SetMesh(CreateMesh(1), false);
            model.Material = new Material(CreateShader(2));
            renderingCommandProcessor.ExecuteCommands();

            Assert.That(model.State.IsEnabled, Is.EqualTo(isModelEnabled));
            Assert.That(model.State.IsRenderableByProperties, Is.EqualTo(isModelEnabled));

            return renderingCommandProcessor;
        }
    }
}
