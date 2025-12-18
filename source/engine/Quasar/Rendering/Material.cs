//-----------------------------------------------------------------------
// <copyright file="Material.cs" company="Space Development">
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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

using Microsoft.Extensions.DependencyInjection;

using Quasar.Graphics;
using Quasar.Graphics.Internals;

namespace Quasar.Rendering
{
    /// <summary>
    /// Represents a rendering material.
    /// </summary>
    /// <seealso cref="INameProvider" />
    public unsafe class Material : INameProvider
    {
        private static readonly Color DefaultColor = Color.Magenta;


        private static readonly ReaderWriterLockSlim defaultMaterialsLock = new ReaderWriterLockSlim();
        private static readonly Dictionary<int, Material> defaultMaterials = new Dictionary<int, Material>();
        private static IShaderRepository shaderRepository;
        private static ITextureRepository textureRepository;
        private static ICubeMapTextureRepository cubeMapTextureRepository;
        private readonly ShaderBase shader;
        private int[] offsets;
        private byte[] propertyValueBuffer;
        private Dictionary<string, int> materialPropertyIndices;
        private static int lastTimestamp;


        /// <summary>
        /// Initializes a new instance of the <see cref="Material" /> class.
        /// </summary>
        /// <param name="shaderId">The shader identifier.</param>
        /// <param name="name">The material name.</param>
        public Material(string shaderId, string name = null)
        {
            ArgumentException.ThrowIfNullOrEmpty(shaderId, nameof(shaderId));

            shader = shaderRepository.GetShader(shaderId);
            InitializeFromDefaults(name);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Material"/> class.
        /// </summary>
        /// <param name="shader">The shader.</param>
        /// <param name="name">The name.</param>
        public Material(IShader shader, string name = null)
        {
            ArgumentNullException.ThrowIfNull(shader, nameof(shader));
            if (shader is not ShaderBase shaderBase)
            {
                throw new ArgumentOutOfRangeException($"Not supported shader implementation: {shader.GetType()}");
            }

            this.shader = shaderBase;
            InitializeFromDefaults(name);
        }

        private Material(ShaderBase shader)
        {
            this.shader = shader;
            InitializeDefaultMaterial();
        }


        /// <inheritdoc/>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the timestamp.
        /// </summary>
        public int Timestamp { get; private set; }


        /// <summary>
        /// Sets the color property value by the specified name.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <param name="value">The value.</param>
        public void SetColor(string name, in Color value)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

            if (!TryGetPropertyOffset(name, ShaderPropertyType.Color, out var offset))
            {
                return;
            }

            fixed (byte* buffer = propertyValueBuffer)
            {
                *(Color*)(buffer + offset) = value;
            }

            UpdateTimestamp();
        }

        /// <summary>
        /// Sets the cube map texture property value by the specified name.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <param name="value">The value.</param>
        public void SetCubeMapTexture(string name, in ICubeMapTexture value)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentNullException.ThrowIfNull(value, nameof(value));


            if (!TryGetPropertyOffset(name, ShaderPropertyType.CubeMapTexture, out var offset))
            {
                return;
            }

            fixed (byte* buffer = propertyValueBuffer)
            {
                *(int*)(buffer + offset) = value.Handle;
            }

            UpdateTimestamp();
        }

        /// <summary>
        /// Sets the cube map texture property value by the specified name.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <param name="cubeMapTextureId">The cube map texture identifier.</param>
        public void SetCubeMapTexture(string name, in string cubeMapTextureId)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(cubeMapTextureId, nameof(cubeMapTextureId));


            if (!TryGetPropertyOffset(name, ShaderPropertyType.CubeMapTexture, out var offset))
            {
                return;
            }

            var cubeMapTexture = cubeMapTextureRepository.Get(cubeMapTextureId) ?? cubeMapTextureRepository.FallbackTexture;
            fixed (byte* buffer = propertyValueBuffer)
            {
                *(int*)(buffer + offset) = cubeMapTexture.Handle;
            }

            UpdateTimestamp();
        }

        /// <summary>
        /// Sets the float property value by the specified name.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <param name="value">The value.</param>
        public void SetFloat(string name, float value)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

            if (!TryGetPropertyOffset(name, ShaderPropertyType.Float, out var offset))
            {
                return;
            }

            fixed (byte* buffer = propertyValueBuffer)
            {
                *(float*)(buffer + offset) = value;
            }

            UpdateTimestamp();
        }

        /// <summary>
        /// Sets the integer property value by the specified name.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <param name="value">The value.</param>
        public void SetInteger(string name, int value)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

            if (!TryGetPropertyOffset(name, ShaderPropertyType.Integer, out var offset))
            {
                return;
            }

            fixed (byte* buffer = propertyValueBuffer)
            {
                *(int*)(buffer + offset) = value;
            }

            UpdateTimestamp();
        }

        /// <summary>
        /// Sets the Matrix4 property value by the specified name.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <param name="value">The value.</param>
        public void SetMatrix4(string name, in Matrix4 value)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

            if (!TryGetPropertyOffset(name, ShaderPropertyType.Matrix4, out var offset))
            {
                return;
            }

            fixed (byte* buffer = propertyValueBuffer)
            {
                *(Matrix4*)(buffer + offset) = value;
            }

            UpdateTimestamp();
        }

        /// <summary>
        /// Sets the normal map texture property value by the specified name.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <param name="value">The value.</param>
        public void SetNormalMapTexture(string name, in ITexture value)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentNullException.ThrowIfNull(value, nameof(value));


            if (!TryGetPropertyOffset(name, ShaderPropertyType.NormalMapTexture, out var offset))
            {
                return;
            }

            fixed (byte* buffer = propertyValueBuffer)
            {
                *(int*)(buffer + offset) = value.Handle;
            }

            UpdateTimestamp();
        }

        /// <summary>
        /// Sets the normal map texture property value by the specified name.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <param name="textureId">The texture identifier.</param>
        public void SetNormalMapTexture(string name, in string textureId)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(textureId, nameof(textureId));


            if (!TryGetPropertyOffset(name, ShaderPropertyType.NormalMapTexture, out var offset))
            {
                return;
            }

            var texture = textureRepository.Get(textureId) ?? textureRepository.FallbackNormalMapTexture;
            fixed (byte* buffer = propertyValueBuffer)
            {
                *(int*)(buffer + offset) = texture.Handle;
            }

            UpdateTimestamp();
        }

        /// <summary>
        /// Sets the texture property value by the specified name.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <param name="value">The value.</param>
        public void SetTexture(string name, in ITexture value)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentNullException.ThrowIfNull(value, nameof(value));


            if (!TryGetPropertyOffset(name, ShaderPropertyType.Texture, out var offset))
            {
                return;
            }

            fixed (byte* buffer = propertyValueBuffer)
            {
                *(int*)(buffer + offset) = value.Handle;
            }

            UpdateTimestamp();
        }

        /// <summary>
        /// Sets the texture property value by the specified name.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <param name="textureId">The texture identifier.</param>
        public void SetTexture(string name, in string textureId)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(textureId, nameof(textureId));


            if (!TryGetPropertyOffset(name, ShaderPropertyType.Texture, out var offset))
            {
                return;
            }

            var texture = textureRepository.Get(textureId) ?? textureRepository.FallbackTexture;
            fixed (byte* buffer = propertyValueBuffer)
            {
                *(int*)(buffer + offset) = texture.Handle;
            }

            UpdateTimestamp();
        }

        /// <summary>
        /// Sets the Vector2 property value by the specified name.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <param name="value">The value.</param>
        public void SetVector2(string name, in Vector2 value)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

            if (!TryGetPropertyOffset(name, ShaderPropertyType.Vector2, out var offset))
            {
                return;
            }

            fixed (byte* buffer = propertyValueBuffer)
            {
                *(Vector2*)(buffer + offset) = value;
            }

            UpdateTimestamp();
        }

        /// <summary>
        /// Sets the Matrix4 property value by the specified name.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <param name="value">The value.</param>
        public void SetVector3(string name, in Vector3 value)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

            if (!TryGetPropertyOffset(name, ShaderPropertyType.Vector3, out var offset))
            {
                return;
            }

            fixed (byte* buffer = propertyValueBuffer)
            {
                *(Vector3*)(buffer + offset) = value;
            }

            UpdateTimestamp();
        }

        /// <summary>
        /// Sets the Vector4 property value by the specified name.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <param name="value">The value.</param>
        public void SetVector4(string name, in Vector4 value)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

            if (!TryGetPropertyOffset(name, ShaderPropertyType.Vector4, out var offset))
            {
                return;
            }

            fixed (byte* buffer = propertyValueBuffer)
            {
                *(Vector4*)(buffer + offset) = value;
            }

            UpdateTimestamp();
        }


        /// <summary>
        /// Gets the underlying shader.
        /// </summary>
        internal ShaderBase GetShader()
        {
            return shader;
        }

        /// <summary>
        /// Initializes the static services.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        internal static void InitializeStaticServices(IServiceProvider serviceProvider)
        {
            shaderRepository = serviceProvider.GetRequiredService<IShaderRepository>();
            textureRepository = serviceProvider.GetRequiredService<ITextureRepository>();
            cubeMapTextureRepository = serviceProvider.GetRequiredService<ICubeMapTextureRepository>();
        }

        /// <summary>
        /// Transfers the material properties to the underlying shader.
        /// </summary>
        internal void TransferToShader()
        {
            fixed (byte* pointer = propertyValueBuffer)
            {
                var index = 0;
                foreach (var shaderProperty in shader.MaterialProperties)
                {
                    var property = pointer + offsets[index];
                    shader.SetProperty(shaderProperty, property);
                    index++;
                }
            }
        }


        private static Material GetDefaultMaterial(ShaderBase shader)
        {
            try
            {
                defaultMaterialsLock.EnterUpgradeableReadLock();

                if (!defaultMaterials.TryGetValue(shader.Handle, out var defaultMaterial))
                {
                    try
                    {
                        defaultMaterialsLock.EnterWriteLock();
                        defaultMaterial = new Material(shader);
                        defaultMaterials.Add(shader.Handle, defaultMaterial);
                    }
                    finally
                    {
                        defaultMaterialsLock.ExitWriteLock();
                    }
                }

                return defaultMaterial;
            }
            finally
            {
                defaultMaterialsLock.ExitUpgradeableReadLock();
            }
        }

        private void InitializeDefaultMaterial()
        {
            // initialize offsets and property mapping
            offsets = new int[shader.MaterialProperties.Count];
            materialPropertyIndices = new Dictionary<string, int>(offsets.Length);

            var offset = 0;
            for (var i = 0; i < offsets.Length; i++)
            {
                var shaderProperty = shader.MaterialProperties[i];
                materialPropertyIndices.Add(shaderProperty.Name, i);

                offsets[i] = offset;
                switch (shaderProperty.Type)
                {
                    case ShaderPropertyType.Color:
                        offset += Marshal.SizeOf<Color>();
                        break;
                    case ShaderPropertyType.Integer:
                    case ShaderPropertyType.CubeMapTexture:
                    case ShaderPropertyType.NormalMapTexture:
                    case ShaderPropertyType.Texture:
                        offset += sizeof(int);
                        break;
                    case ShaderPropertyType.Float:
                        offset += sizeof(float);
                        break;
                    case ShaderPropertyType.Matrix4:
                        offset += Marshal.SizeOf<Matrix4>();
                        break;
                    case ShaderPropertyType.Vector2:
                        offset += Marshal.SizeOf<Vector2>();
                        break;
                    case ShaderPropertyType.Vector3:
                        offset += Marshal.SizeOf<Vector3>();
                        break;
                    case ShaderPropertyType.Vector4:
                        offset += Marshal.SizeOf<Vector4>();
                        break;

                    default:
                        throw new NotSupportedException($"Shader property type: {shaderProperty.Type}");
                }
            }

            // initialize property storage with defaults
            propertyValueBuffer = new byte[offset];
            fixed (byte* pointer = propertyValueBuffer)
            {
                for (var i = 0; i < offsets.Length; i++)
                {
                    var shaderPropertyType = shader.MaterialProperties[i].Type;
                    var property = pointer + offsets[i];
                    switch (shaderPropertyType)
                    {
                        case ShaderPropertyType.Color:
                            *(Color*)property = DefaultColor;
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        private void InitializeFromDefaults(string name)
        {
            var defaultMaterial = GetDefaultMaterial(shader);
            offsets = defaultMaterial.offsets;
            materialPropertyIndices = defaultMaterial.materialPropertyIndices;

            propertyValueBuffer = new byte[defaultMaterial.propertyValueBuffer.Length];
            Array.Copy(defaultMaterial.propertyValueBuffer, propertyValueBuffer, propertyValueBuffer.Length);

            Name = name;
            UpdateTimestamp();
        }

        private bool TryGetPropertyOffset(string name, ShaderPropertyType shaderPropertyType, out int offset)
        {
            if (!materialPropertyIndices.TryGetValue(name, out var materialPropertyIndex) ||
                shader.MaterialProperties[materialPropertyIndex].Type != shaderPropertyType)
            {
                offset = 0;
                return false;
            }

            offset = offsets[materialPropertyIndex];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateTimestamp()
        {
            Timestamp = Interlocked.Increment(ref lastTimestamp);
        }
    }
}
