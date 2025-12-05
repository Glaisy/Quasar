//-----------------------------------------------------------------------
// <copyright file="GLEnumExtensons.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Runtime.CompilerServices;

using Quasar.OpenGL.Api;

namespace Quasar.OpenGL.Extensions
{
    /// <summary>
    /// OpenGL enumeration type extension methods.
    /// </summary>
    internal static class GLEnumExtensons
    {
        private static readonly BeginMode[] beginModes =
        {
            // Point
            BeginMode.Points,

            // Line
            BeginMode.Lines,

            // LinesAdjacency
            BeginMode.LinesAdjacency,

            // LineStrip
            BeginMode.LineStrip,

            // LineStripAdjacency
            BeginMode.LineStripAdjacency,

            // LineLoop
            BeginMode.LineLoop,

            // Triangle
            BeginMode.Triangles,

            // TriangleStrip
            BeginMode.TriangleStrip,
        };

        private static readonly BufferTarget[] bufferTargets =
        {
            // None
            BufferTarget.ArrayBuffer,

            // VertexBuffer
            BufferTarget.ArrayBuffer,

            // IndexBuffer
            BufferTarget.ElementArrayBuffer
        };

        private static readonly BufferUsageHint[] bufferUsageHints =
        {
            // None
            BufferUsageHint.StaticDraw,

            // Default
            BufferUsageHint.StaticDraw,

            // Immutable
            BufferUsageHint.StaticDraw,

            // Dynamic
            BufferUsageHint.DynamicDraw,

            // Staging
            BufferUsageHint.DynamicCopy,
        };

        private static readonly TextureTarget[] cubeMapTargets =
        {
            // Positive X face.
            TextureTarget.TextureCubeMapPositiveX,

            // Negative X face.
            TextureTarget.TextureCubeMapNegativeX,

            // Negative Y face (reversed Y due to LH-RH conversion).
            TextureTarget.TextureCubeMapNegativeY,

            // Positive Y face (reversed Y due to LH-RH conversion).
            TextureTarget.TextureCubeMapPositiveY,

            // Positive Z face.
            TextureTarget.TextureCubeMapPositiveZ,

            // Negative Z face.
            TextureTarget.TextureCubeMapNegativeZ
        };

        private static readonly DepthFunction[] depthFunctions =
        {
            // None
            DepthFunction.Always,

            // Always
            DepthFunction.Always,

            // Never
            DepthFunction.Never,

            // Less
            DepthFunction.Less,

            // LessOrEqual
            DepthFunction.Lequal,

            // Greater
            DepthFunction.Greater,
        };

        private static readonly PrimitiveType[] primitiveTypes =
        {
            // Point
            PrimitiveType.Points,

            // Line
            PrimitiveType.Lines,

            // LinesAdjacency
            PrimitiveType.LinesAdjacency,

            // LineStrip
            PrimitiveType.LineStrip,

            // LineStripAdjacency
            PrimitiveType.LineStripAdjacency,

            // LineLoop
            PrimitiveType.LineLoop,

            // Triangle
            PrimitiveType.Triangles,

            // TriangleStrip
            PrimitiveType.TriangleStrip,
        };

        private static readonly TextureWrapMode[] textureWrapModes =
        {
            // Clamped
            TextureWrapMode.ClampToEdge,

            // Repeat
            TextureWrapMode.Repeat,

            // Mirror
            TextureWrapMode.MirroredRepeat
        };


        /// <summary>
        /// Converts the primitive type repeat mode to OpenGL primitive type.
        /// </summary>
        /// <param name="primitiveType">Type of the primitive.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BeginMode ToBeginMode(this Quasar.Graphics.PrimitiveType primitiveType)
        {
            return beginModes[(int)primitiveType];
        }

        /// <summary>
        /// Converts the graphics buffer type to OpenGL buffer target.
        /// </summary>
        /// <param name="graphicsBufferType">The graphics buffer type.</param>
        /// <returns>The converted value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BufferTarget ToBufferTarget(this Quasar.Graphics.GraphicsBufferType graphicsBufferType)
        {
            return bufferTargets[(int)graphicsBufferType];
        }

        /// <summary>
        /// Converts the graphics resource usage to OpenGL buffer usage hint.
        /// </summary>
        /// <param name="graphicsResourceUsage">The graphics resource usage.</param>
        /// <returns>The converted value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BufferUsageHint ToBufferUsageHint(this Quasar.Graphics.GraphicsResourceUsage graphicsResourceUsage)
        {
            return bufferUsageHints[(int)graphicsResourceUsage];
        }

        /// <summary>
        /// Converts the depth test mode to OpenGL depth function.
        /// </summary>
        /// <param name="depthTestMode">The depth test mode.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DepthFunction ToDepthFunction(this Quasar.Graphics.DepthTestMode depthTestMode)
        {
            return depthFunctions[(int)depthTestMode];
        }

        /// <summary>
        /// Converts the primitive type repeat mode to OpenGL primitive type.
        /// </summary>
        /// <param name="primitiveType">Type of the primitive.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PrimitiveType ToPrimitiveType(this Quasar.Graphics.PrimitiveType primitiveType)
        {
            return primitiveTypes[(int)primitiveType];
        }

        /// <summary>
        /// Converts the cube map face to OpenGL texture target.
        /// </summary>
        /// <param name="cubeMapFace">Cube map face.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TextureTarget ToCubeMapTextureTarget(this Quasar.Graphics.CubeMapFace cubeMapFace)
        {
            return cubeMapTargets[(int)cubeMapFace];
        }

        /// <summary>
        /// Converts the texture repeat mode to OpenGL texture wrap mode.
        /// </summary>
        /// <param name="textureRepeatMode">The texture repeat mode.</param>
        /// <returns>The converted value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TextureWrapMode ToTextureWrapMode(this Quasar.Graphics.TextureRepeatMode textureRepeatMode)
        {
            return textureWrapModes[(int)textureRepeatMode];
        }
    }
}
