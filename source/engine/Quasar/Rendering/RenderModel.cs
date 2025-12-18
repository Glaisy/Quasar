//-----------------------------------------------------------------------
// <copyright file="RenderModel.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;
using System.Threading;

using Microsoft.Extensions.DependencyInjection;

using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.Rendering.Processors.Internals;

using Space.Core;

namespace Quasar.Rendering
{
    /// <summary>
    /// General render model object (mesh + material + transform).
    /// </summary>
    /// <seealso cref="InvalidatableBase" />
    /// <seealso cref="IIdentifierProvider{Int32}" />
    /// <seealso cref="INameProvider" />
    /// <seealso cref="IEquatable{RenderObject}" />
    /// <seealso cref="IDisposable" />
    public sealed partial class RenderModel : InvalidatableBase,
        IIdentifierProvider<int>,
        INameProvider,
        IEquatable<RenderModel>,
        IDisposable
    {
        private static IMatrixFactory matrixFactory;
        private static IRenderingContext renderingContext;
        private static RenderModelCommandProcessor commandProcessor;
        private static int lastIdentifier;
        private int transformationTimestamp;
        private bool isDisposed;


        /// <summary>
        /// Initializes a new instance of the <see cref="RenderModel"/> class.
        /// </summary>
        /// <param name="enabled">The initial value of the Enabled property.</param>
        public RenderModel(bool enabled = true)
        {
            Id = Interlocked.Increment(ref lastIdentifier);
            this.enabled = enabled;
            transformationTimestamp = Transform.Timestamp;

            SendCreateCommand();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="RenderModel"/> class.
        /// </summary>
        ~RenderModel()
        {
            DisposeInternal();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            DisposeInternal();
            GC.SuppressFinalize(this);
        }

        private void DisposeInternal()
        {
            IsEnabled = false;
            isDisposed = true;
        }


        private BoundingBox boundingBox = default;
        /// <summary>
        /// Gets the render model object's bounding box in world space.
        /// </summary>
        public ref BoundingBox BoundingBox
        {
            get
            {
                EnsureTransformationHasNotChanged();
                if (HasInvalidationFlags(InvalidationFlags.BoundingBox))
                {
                    UpdateBoundingBox();
                    ClearInvalidationFlags(InvalidationFlags.BoundingBox);
                }

                return ref boundingBox;
            }
        }

        private bool doubleSided = false;
        /// <summary>
        /// Gets or sets a value indicating whether this object is rendered without backface culling.
        /// </summary>
        public bool DoubleSided
        {
            get => doubleSided;
            set
            {
                EnsureIsNotDisposed();

                if (doubleSided == value)
                {
                    return;
                }

                doubleSided = value;
                SendDoubleSidedChangedCommand(value);
            }
        }

        /// <inheritdoc/>
        public int Id { get; }

        private bool enabled;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get => enabled;
            set
            {
                EnsureIsNotDisposed();

                if (enabled == value)
                {
                    return;
                }

                enabled = value;
                SendEnabledChangedCommand(value);
            }
        }

        private Layer layer = Layer.Default;
        /// <summary>
        /// Gets or sets the layer.
        /// </summary>
        public Layer Layer
        {
            get => layer;
            set
            {
                EnsureIsNotDisposed();

                if (layer == value)
                {
                    return;
                }

                layer = value;
                SendLayerChangedCommand(value);
            }
        }

        private Material material;
        /// <summary>
        /// Gets or sets the material.
        /// </summary>
        public Material Material
        {
            get => material;
            set
            {
                EnsureIsNotDisposed();

                if (material == value)
                {
                    return;
                }

                // update material
                var oldShader = material?.GetShader();
                var newShader = value?.GetShader();
                material = value;

                if (newShader == oldShader)
                {
                    return;
                }

                SendShaderChangedCommand(newShader);
            }
        }

        /// <summary>
        /// Gets the mesh.
        /// </summary>
        public IMesh Mesh { get; private set; }

        private Matrix4 modelMatrix;
        /// <summary>
        /// Gets the model matrix.
        /// </summary>
        public ref Matrix4 ModelMatrix
        {
            get
            {
                EnsureTransformationHasNotChanged();
                if (HasInvalidationFlags(InvalidationFlags.ModelMatrix))
                {
                    matrixFactory.CreateModelMatrix(Transform, ref modelMatrix);
                    ClearInvalidationFlags(InvalidationFlags.ModelMatrix);
                }

                return ref modelMatrix;
            }
        }

        private string name;
        /// <inheritdoc/>
        public string Name
        {
            get => name;
            set
            {
                name = value;
                Transform.Name = value;
            }
        }

        /// <summary>
        /// The transform.
        /// </summary>
        public readonly Transform Transform = new Transform();


        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is not RenderModel renderObject)
            {
                return false;
            }

            return Id == renderObject.Id;
        }

        /// <inheritdoc/>
        public bool Equals(RenderModel other)
        {
            if (other == null)
            {
                return false;
            }

            return Id == other.Id;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Id;
        }

        /// <summary>
        /// Sets the mesh.
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        /// <param name="shared">Shared mesh flag. Shared mesh is not disposed when the render model object is disposed.</param>
        public void SetMesh(IMesh mesh, bool shared)
        {
            EnsureIsNotDisposed();

            if (Mesh == mesh)
            {
                return;
            }

            Mesh = mesh;
            SendMeshChangedCommand(mesh, shared);
            Invalidate(InvalidationFlags.BoundingBox);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            if (String.IsNullOrEmpty(name))
            {
                return name;
            }

            return Id.ToString();
        }


        /// <summary>
        /// The internal state used by the renderer.
        /// </summary>
        internal RenderModelState State;


        /// <summary>
        /// Initializes static the services.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        internal static void InitializeStaticServices(IServiceProvider serviceProvider)
        {
            matrixFactory = serviceProvider.GetRequiredService<IMatrixFactory>();
            renderingContext = serviceProvider.GetRequiredService<IRenderingContext>();
            commandProcessor = serviceProvider.GetRequiredService<RenderModelCommandProcessor>();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnsureIsNotDisposed()
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException($"{GetType().FullName} - [#{Id}:{Name}].");
            }
        }

        private void EnsureTransformationHasNotChanged()
        {
            if (transformationTimestamp == Transform.Timestamp)
            {
                return;
            }

            Invalidate(InvalidationFlags.All);
            transformationTimestamp = Transform.Timestamp;
        }

        private void SendCreateCommand()
        {
            commandProcessor.Add(new RenderModelCommand(this, RenderModelCommandType.Create)
            {
                Value = enabled,
                Layer = layer
            });
        }

        private void SendDoubleSidedChangedCommand(bool doubleSided)
        {
            commandProcessor.Add(new RenderModelCommand(this, RenderModelCommandType.DoubleSidedChanged)
            {
                Value = doubleSided
            });
        }

        private void SendEnabledChangedCommand(bool enabled)
        {
            commandProcessor.Add(new RenderModelCommand(this, RenderModelCommandType.EnabledChanged)
            {
                Value = enabled
            });
        }

        private void SendLayerChangedCommand(Layer layer)
        {
            commandProcessor.Add(new RenderModelCommand(this, RenderModelCommandType.LayerChanged)
            {
                Layer = layer
            });
        }

        private void SendMeshChangedCommand(IMesh mesh, bool shared)
        {
            commandProcessor.Add(new RenderModelCommand(this, RenderModelCommandType.MeshChanged)
            {
                Mesh = mesh,
                Value = shared
            });
        }

        private void SendShaderChangedCommand(ShaderBase shader)
        {
            commandProcessor.Add(new RenderModelCommand(this, RenderModelCommandType.ShaderChanged)
            {
                Shader = shader
            });
        }

        private void UpdateBoundingBox()
        {
            if (Mesh == null)
            {
                boundingBox = default;
                return;
            }

            // convert the mesh's bounding box to world space.
            var min = Transform.ToWorldPosition(Mesh.BoundingBox.Min);
            var max = Transform.ToWorldPosition(Mesh.BoundingBox.Max);

            boundingBox = BoundingBox.Create(min, max);
        }
    }
}
