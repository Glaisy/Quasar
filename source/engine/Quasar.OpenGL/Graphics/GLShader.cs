//-----------------------------------------------------------------------
// <copyright file="GLShader.cs" company="Space Development">
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

using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.OpenGL.Api;

namespace Quasar.OpenGL.Graphics
{
    /// <summary>
    ///  OpenGL render shader program implementation.
    /// </summary>
    /// <seealso cref="ShaderBase" />
    internal sealed unsafe class GLShader : ShaderBase
    {
        private delegate void PropertySetter(ShaderProperty shaderProperty, byte* buffer);

        private static readonly PropertySetter[] propertySetters =
        [
            null,
            PropertySetterColor,
            PropertySetterCubeMapTexture,
            PropertySetterFloat,
            PropertySetterInteger,
            PropertySetterMatrix4,
            PropertySetterTexture,
            PropertySetterTexture,
            PropertySetterVector2,
            PropertySetterVector3,
            PropertySetterVector4,
        ];

        private static readonly Dictionary<string, ShaderProperty> emptyProperties = new Dictionary<string, ShaderProperty>();


        /// <summary>
        /// Initializes a new instance of the <see cref="GLShader" /> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="tag">The tag value.</param>
        /// <param name="descriptor">The descriptor.</param>
        public GLShader(
            int handle,
            string id,
            string tag,
            in GraphicsResourceDescriptor descriptor)
            : base(id, tag, descriptor)
        {
            this.handle = handle;
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (handle != 0)
            {
                GL.DeleteProgram(handle);
                handle = 0;
            }

            base.Dispose(disposing);
        }


        private int handle;
        /// <inheritdoc/>
        public override int Handle => handle;


        /// <inheritdoc/>
        internal override void Activate()
        {
            GL.UseProgram(handle);
        }

        /// <inheritdoc/>
        internal override void Deactivate()
        {
            GL.UseProgram(0);
        }


        /// <inheritdoc/>
        internal override unsafe void SetProperty(ShaderProperty shaderProperty, byte* buffer)
        {
            propertySetters[(int)shaderProperty.Type](shaderProperty, buffer);
        }

        /// <inheritdoc/>
        internal override void SetColor(int index, in Color value)
        {
            GL.Uniform4f(index, value.R, value.G, value.B, value.A);
        }

        /// <inheritdoc/>
        internal override void SetCubeMapTexture(int index, ICubeMapTexture value)
        {
            var textureUnit = Properties[index].TextureUnit;
            GL.ActiveTexture(TextureUnit.Texture0 + textureUnit);
            GL.BindTexture(TextureTarget.TextureCubeMap, value.Handle);
            GL.Uniform1i(index, textureUnit);
        }

        /// <inheritdoc/>
        internal override void SetFloat(int index, float value)
        {
            GL.Uniform1f(index, value);
        }

        /// <inheritdoc/>
        internal override void SetInteger(int index, int value)
        {
            GL.Uniform1i(index, value);
        }

        /// <inheritdoc/>
        internal unsafe override void SetMatrix(int index, in Matrix4 value)
        {
            fixed (Matrix4* pointer = &value)
            {
                GL.UniformMatrix4(index, 1, false, (float*)pointer);
            }
        }

        /// <inheritdoc/>
        internal override void SetTexture(int index, ITexture value)
        {
            var textureUnit = Properties[index].TextureUnit;
            GL.ActiveTexture(TextureUnit.Texture0 + textureUnit);
            GL.BindTexture(TextureTarget.Texture2D, value.Handle);
            GL.Uniform1i(index, textureUnit);
        }

        /// <inheritdoc/>
        internal override void SetTexture(int index, int value)
        {
            var textureUnit = Properties[index].TextureUnit;
            GL.ActiveTexture(TextureUnit.Texture0 + textureUnit);
            GL.BindTexture(TextureTarget.Texture2D, value);
            GL.Uniform1i(index, textureUnit);
        }

        /// <inheritdoc/>
        internal override void SetVector2(int index, in Vector2 value)
        {
            GL.Uniform2f(index, value.X, value.Y);
        }

        /// <inheritdoc/>
        internal override void SetVector3(int index, in Vector3 value)
        {
            GL.Uniform3f(index, value.X, value.Y, value.Z);
        }

        /// <inheritdoc/>
        internal override void SetVector4(int index, in Vector4 value)
        {
            GL.Uniform4f(index, value.X, value.Y, value.Z, value.W);
        }


        /// <inheritdoc/>
        protected override Dictionary<string, ShaderProperty> EnumerateProperties()
        {
            // get property count
            GL.GetProgram(handle, ProgramParameterName.ActiveUniforms, out var propertyCount);
            if (propertyCount <= 0)
            {
                return emptyProperties;
            }

            // iterate properties
            var textureUnit = 0;
            var properties = new Dictionary<string, ShaderProperty>(propertyCount);
            for (var i = 0; i < propertyCount; i++)
            {
                var uniformName = GL.GetActiveUniform(handle, i, out var _, out var uniformType);
                var propertyType = MapUniformToShaderPropertyType(uniformName, uniformType);
                var locationIndex = GL.GetUniformLocation(handle, uniformName);

                // create shader property
                ShaderProperty property;
                switch (propertyType)
                {
                    case ShaderPropertyType.CubeMapTexture:
                    case ShaderPropertyType.NormalMapTexture:
                    case ShaderPropertyType.Texture:
                        property = new ShaderProperty(locationIndex, uniformName, propertyType, textureUnit);
                        textureUnit++;
                        break;
                    default:
                        property = new ShaderProperty(locationIndex, uniformName, propertyType, 0);
                        break;
                }

                properties.Add(property.Name, property);
            }

            return properties;
        }


        private ShaderPropertyType MapUniformToShaderPropertyType(string name, ActiveUniformType uniformType)
        {
            switch (uniformType)
            {
                case ActiveUniformType.Float:
                    return ShaderPropertyType.Float;
                case ActiveUniformType.FloatMat4:
                    return ShaderPropertyType.Matrix4;
                case ActiveUniformType.Int:
                    return ShaderPropertyType.Integer;
                case ActiveUniformType.FloatVec2:
                    return ShaderPropertyType.Vector2;
                case ActiveUniformType.FloatVec3:
                    return ShaderPropertyType.Vector3;
                case ActiveUniformType.FloatVec4:
                    if (name.EndsWith(ShaderConstants.ColorPropertyNameSuffix))
                    {
                        return ShaderPropertyType.Color;
                    }

                    return ShaderPropertyType.Vector4;
                case ActiveUniformType.Sampler2D:
                    if (name.EndsWith(ShaderConstants.NormalMapPropertyNameSuffix))
                    {
                        return ShaderPropertyType.NormalMapTexture;
                    }

                    return ShaderPropertyType.Texture;
                case ActiveUniformType.SamplerCube:
                    return ShaderPropertyType.CubeMapTexture;
                default:
                    throw new NotSupportedException($"GL property type '{uniformType}' in shader '{Id}' at '{name}'.");
            }
        }

        private static void PropertySetterColor(ShaderProperty shaderProperty, byte* value)
        {
            var color = (Color*)value;
            GL.Uniform4f(shaderProperty.Index, color->R, color->G, color->B, color->A);
        }

        private static void PropertySetterCubeMapTexture(ShaderProperty shaderProperty, byte* value)
        {
            GL.ActiveTexture(TextureUnit.Texture0 + shaderProperty.TextureUnit);
            GL.BindTexture(TextureTarget.TextureCubeMap, *(int*)value);
            GL.Uniform1i(shaderProperty.Index, shaderProperty.TextureUnit);
        }

        private static void PropertySetterFloat(ShaderProperty shaderProperty, byte* value)
        {
            GL.Uniform1f(shaderProperty.Index, *(float*)value);
        }

        private static void PropertySetterInteger(ShaderProperty shaderProperty, byte* value)
        {
            GL.Uniform1f(shaderProperty.Index, *(int*)value);
        }

        private static void PropertySetterMatrix4(ShaderProperty shaderProperty, byte* value)
        {
            GL.UniformMatrix4(shaderProperty.Index, 1, false, (float*)value);
        }

        private static void PropertySetterTexture(ShaderProperty shaderProperty, byte* value)
        {
            GL.ActiveTexture(TextureUnit.Texture0 + shaderProperty.TextureUnit);
            GL.BindTexture(TextureTarget.Texture2D, *(int*)value);
            GL.Uniform1i(shaderProperty.Index, shaderProperty.TextureUnit);
        }

        private static void PropertySetterVector2(ShaderProperty shaderProperty, byte* value)
        {
            var vector2 = (Vector2*)value;
            GL.Uniform2f(shaderProperty.Index, vector2->X, vector2->Y);
        }

        private static void PropertySetterVector3(ShaderProperty shaderProperty, byte* value)
        {
            var vector3 = (Vector3*)value;
            GL.Uniform3f(shaderProperty.Index, vector3->X, vector3->Y, vector3->Z);
        }

        private static void PropertySetterVector4(ShaderProperty shaderProperty, byte* value)
        {
            var vector4 = (Vector4*)value;
            GL.Uniform4f(shaderProperty.Index, vector4->X, vector4->Y, vector4->Z, vector4->W);
        }
    }
}
