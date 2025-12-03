//-----------------------------------------------------------------------
// <copyright file="GL.CustomFunctions.cs" company="Space Development">
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
using System.Text;

using Quasar.Graphics;
using Quasar.Utilities;

namespace Quasar.OpenGL.Api
{
    /// <summary>
    /// OpenGL custom functions.
    /// </summary>
    internal static unsafe partial class GL
    {
        private const int nameBufferLength = 128;
        private static readonly string[] shaderSourceStrings = new string[1];

        /// <summary>
        /// Deletes a buffer.
        /// </summary>
        /// <param name="id">The buffer identifier.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteBuffer(int id)
        {
            deleteBuffers(1, &id);
        }

        /// <summary>
        /// Deletes a frame buffer.
        /// </summary>
        /// <param name="id">The render buffer identifier.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteFrameBuffer(int id)
        {
            deleteFrameBuffers(1, &id);
        }

        /// <summary>
        /// Deletes a render buffer.
        /// </summary>
        /// <param name="id">The render buffer identifier.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteRenderBuffer(int id)
        {
            deleteRenderBuffers(1, &id);
        }

        /// <summary>
        /// Deletes the texture by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteTexture(int id)
        {
            deleteTextures(1, &id);
        }

        /// <summary>
        /// Deletes the vertex array by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteVertexArray(int id)
        {
            deleteVertexArrays(1, &id);
        }

        /// <summary>
        /// Creates a buffer.
        /// </summary>
        /// <returns>The buffer identifier.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GenBuffer()
        {
            int id;
            genBuffers(1, &id);

            return id;
        }

        /// <summary>
        /// Creates a frame buffer.
        /// </summary>
        /// <returns>The frame buffer identifier.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GenFrameBuffer()
        {
            int id;
            genFrameBuffers(1, &id);

            return id;
        }

        /// <summary>
        /// Creates a render buffer.
        /// </summary>
        /// <returns>The frame buffer identifier.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GenRenderBuffer()
        {
            int id;
            genRenderBuffers(1, &id);

            return id;
        }

        /// <summary>
        /// Creates a texture.
        /// </summary>
        /// <returns>The frame buffer identifier.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GenTexture()
        {
            int id;
            genTextures(1, &id);

            return id;
        }

        /// <summary>
        /// Creates a vertex array.
        /// </summary>
        /// <returns>The frame buffer identifier.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GenVertexArray()
        {
            int id;
            genVertexArrays(1, &id);

            return id;
        }

        /// <summary>
        /// Get the active uniform type and name.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="location">The location.</param>
        /// <param name="size">The size of the uniform.</param>
        /// <param name="uniformType">Type of the uniform.</param>
        /// <returns>The name of the uniform.</returns>
        public static string GetActiveUniform(int programId, int location, out int size, out ActiveUniformType uniformType)
        {
            var nameBuffer = stackalloc byte[nameBufferLength];
            getActiveUniform(programId, location, nameBufferLength, out var length, out size, out uniformType, nameBuffer);

            return Encoding.ASCII.GetString(nameBuffer, length);
        }

        /// <summary>
        /// Gets the shader information log.
        /// </summary>
        /// <param name="shaderId">The shader identifier.</param>
        /// <returns>The info log string.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetShaderInformationLog(int shaderId)
        {
            var length = GetShaderParameter(shaderId, ShaderParameter.InfoLogLength);
            if (length == 0)
            {
                return String.Empty;
            }

            var buffer = stackalloc byte[length];
            getShaderInfoLog(shaderId, length, &length, buffer);

            return Encoding.ASCII.GetString(buffer, length);
        }

        /// <summary>
        /// Gets the integer shader parameter.
        /// </summary>
        /// <param name="shaderId">The shader identifier.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The shader parameter value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetShaderParameter(int shaderId, ShaderParameter parameter)
        {
            int id;
            getShaderiv(shaderId, parameter, &id);

            return id;
        }

        /// <summary>
        /// Gets the string by the specified type.
        /// </summary>
        /// <param name="stringType">The internal OpenGL string type.</param>
        /// <returns>The string.</returns>
        public static string GetString(StringType stringType)
        {
            var bytes = getString(stringType);
            return UnsafeUtility.GetString(bytes);
        }

        /// <summary>
        /// Gets the uniform Color value.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="location">The location.</param>
        /// <returns>The uniform Color value.</returns>
        public static Color GetUniformColor(int programId, int location)
        {
            var dataBuffer = stackalloc float[4];
            getUniformfv(programId, location, dataBuffer);

            return new Color(dataBuffer[0], dataBuffer[1], dataBuffer[2], dataBuffer[3]);
        }

        /// <summary>
        /// Gets the uniform integer value.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="location">The location.</param>
        /// <returns>The uniform value.</returns>
        public static int GetUniformInteger(int programId, int location)
        {
            var dataBuffer = stackalloc int[1];
            getUniformiv(programId, location, dataBuffer);

            return dataBuffer[0];
        }

        /// <summary>
        /// Gets the uniform float value.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="location">The location.</param>
        /// <returns>The uniform value.</returns>
        public static float GetUniformFloat(int programId, int location)
        {
            var dataBuffer = stackalloc float[1];
            getUniformfv(programId, location, dataBuffer);

            return dataBuffer[0];
        }

        /// <summary>
        /// Gets the uniform Matrix4 value.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="location">The location.</param>
        /// <param name="result">The matrix result.</param>
        public static void GetUniformMatrix4(int programId, int location, Matrix4* result)
        {
            getUniformfv(programId, location, (float*)result);
        }

        /// <summary>
        /// Gets the uniform Vector2 value.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="location">The location.</param>
        /// <returns>The uniform Vector2 value.</returns>
        public static Vector2 GetUniformVector2(int programId, int location)
        {
            var dataBuffer = stackalloc float[2];
            getUniformfv(programId, location, dataBuffer);

            return new Vector2(dataBuffer[0], dataBuffer[1]);
        }

        /// <summary>
        /// Gets the uniform Vector3 value.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="location">The location.</param>
        /// <returns>The uniform Vector3 value.</returns>
        public static Vector3 GetUniformVector3(int programId, int location)
        {
            var dataBuffer = stackalloc float[3];
            getUniformfv(programId, location, dataBuffer);

            return new Vector3(dataBuffer[0], dataBuffer[1], dataBuffer[2]);
        }

        /// <summary>
        /// Gets the uniform Vector4 value.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="location">The location.</param>
        /// <returns>The uniform Vector4 value.</returns>
        public static Vector4 GetUniformVector4(int programId, int location)
        {
            var dataBuffer = stackalloc float[4];
            getUniformfv(programId, location, dataBuffer);

            return new Vector4(dataBuffer[0], dataBuffer[1], dataBuffer[2], dataBuffer[3]);
        }

        /// <summary>
        /// Sets the shaders' the source string.
        /// </summary>
        /// <param name="shaderId">The shader identifier.</param>
        /// <param name="source">The source.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ShaderSource(int shaderId, string source)
        {
            var lengths = source.Length;
            shaderSourceStrings[0] = source;
            shaderSource(shaderId, 1, shaderSourceStrings, &lengths);
        }

        /// <summary>
        /// Sets the Vector4 texture parameter.
        /// </summary>
        /// <param name="target">The texture target.</param>
        /// <param name="parameterName">The texture parameter name.</param>
        /// <param name="value">The value.</param>
        public static void TexParameterVector4(TextureTarget target, TextureParameterName parameterName, in Vector4 value)
        {
            fixed (float* values = &value.X)
            {
                texParameterVector4(target, parameterName, values);
            }
        }
    }
}
