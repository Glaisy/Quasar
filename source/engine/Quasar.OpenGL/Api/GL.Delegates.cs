//-----------------------------------------------------------------------
// <copyright file="GL.Delegates.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;

namespace Quasar.OpenGL.Api
{
    /// <summary>
    /// OpenGL function wrapper delegate types.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed.")]
    internal static unsafe partial class GL
    {
        private delegate IntPtr CreateContext(IntPtr deviceContext);
        private delegate IntPtr GetProcAddress(string name);
        private delegate bool MakeCurrent(IntPtr deviceContext, IntPtr renderContext);

        private delegate void glDeleteBuffers(int count, int* ids);
        private delegate void glDeleteFramebuffers(int count, int* ids);
        private delegate void glDeleteRenderbuffers(int count, int* ids);
        private delegate void glDeleteTextures(int count, int* ids);
        private delegate void glDeleteVertexArrays(int count, int* ids);
        private delegate void glGenBuffers(int count, int* ids);
        private delegate void glGenFramebuffers(int count, int* ids);
        private delegate void glGenRenderbuffers(int count, int* ids);
        private delegate void glGenTextures(int count, int* ids);
        private delegate void glGenVertexArrays(int count, int* ids);
        private delegate void glGetActiveUniform(
            int programId,
            int index,
            int bufferLength,
            out int nameLength,
            out int size,
            out ActiveUniformType uniformType,
            void* buffer);
        private delegate void glGetShaderInfoLog(int shaderId, int maxLength, int* length, byte* value);
        private delegate void glGetShaderiv(int shaderId, ShaderParameter parameter, int* value);
        private delegate byte* glGetString(StringType stringType);
        private delegate void glGetUniformiv(int programId, int location, int* value);
        private delegate void glGetUniformfv(int programId, int location, float* value);
        private delegate void glShaderSource(int shaderId, int count, string[] sources, int* lengths);

        public delegate IntPtr DeleteContext(IntPtr deviceContext);

        public delegate void glActiveTexture(TextureUnit textureUnit);
        public delegate void glAttachShader(int programId, int shaderId);
        public delegate void glBindBuffer(BufferTarget bufferTarget, int id);
        public delegate void glBindFramebuffer(FrameBufferTarget frameBufferTarget, int id);
        public delegate void glBindRenderbuffer(RenderBufferTarget renderBufferTarget, int id);
        public delegate void glBindVertexArray(int id);
        public delegate void glBufferData(BufferTarget bufferTarget, int size, IntPtr data, BufferUsageHint bufferUsageHint);
        public delegate void glBindTexture(TextureTarget target, int id);
        public delegate void glBlendFunc(BlendingFactor sourceFactor, BlendingFactor destinationFactor);
        public delegate void glClear(BufferClearMask clearMask);
        public delegate void glClearColor(float red, float green, float blue, float alpha);
        public delegate void glCompileShader(int shaderId);
        public delegate int glCreateProgram();
        public delegate int glCreateShader(ShaderType type);
        public delegate void glCullFace(CullFaceMode cullFaceMode);
        public delegate void glDeleteProgram(int programId);
        public delegate void glDeleteShader(int shaderId);
        public delegate void glDepthFunc(DepthFunction depthFunction);
        public delegate void glDetachShader(int programId, int shaderId);
        public delegate void glDisable(Capability capability);
        public delegate void glDrawArrays(int primitiveType, int start, int count);
        public delegate void glDrawBuffer(DrawBufferMode mode);
        public delegate void glDrawElements(int beginMode, int count, DrawElementsType elementsType, int indexLocation);
        public delegate void glEnable(Capability capability);
        public delegate void glEnableVertexAttribArray(int index);
        public delegate void glFramebufferRenderbuffer(
            FrameBufferTarget frameBufferTarget,
            FrameBufferAttachment frameBufferAttachment,
            RenderBufferTarget renderBufferTarget,
            int renderBufferId);
        public delegate void glFramebufferTexture2D(
            FrameBufferTarget frameBufferTarget,
            FrameBufferAttachment frmaeBufferAttachment,
            TextureTarget textureTarget,
            int textureId,
            int mipmapLevel);
        public delegate void glFrontFace(FrontFaceDirection frontFaceDirection);
        public delegate void glGenerateMipmap(GenerateMipmapTarget texture2D);
        public delegate void glGetBufferSubData(BufferTarget bufferTarget, int offset, int size, IntPtr data);
        public delegate int glGetError();
        public delegate void glGetFloatv(int parameterId, float* value);
        public delegate void glGetProgramiv(int programId, ProgramParameterName parameter, out int value);
        public delegate int glGetUniformLocation(int programId, string name);
        public delegate void glLineWidth(float width);
        public delegate void glLinkProgram(int programId);
        public delegate void glReadBuffer(ReadBufferMode mode);
        public delegate void glRenderbufferStorage(
            RenderBufferTarget renderBufferTarget,
            RenderBufferStorage renderBufferStorage,
            int width,
            int height);
        public delegate bool glSwapIntervalEXT(int interval);
        public delegate void glTexImage2D(
            TextureTarget target,
            int mipmapLevel,
            PixelInternalFormat internalFormat,
            int width,
            int height,
            int border,
            PixelFormat pixelFormat,
            PixelType pixelType,
            IntPtr data);
        public delegate void glTexParameteri(TextureTarget target, TextureParameterName textureParameterName, int value);
        public delegate void glTexParameterf(TextureTarget target, TextureParameterName textureParameterName, float value);
        public delegate void glTexParameterfv(TextureTarget target, TextureParameterName textureParameterName, float* values);

        public delegate void glUniform1f(int location, float x);
        public delegate void glUniform2f(int location, float x, float y);
        public delegate void glUniform3f(int location, float x, float y, float z);
        public delegate void glUniform4f(int location, float x, float y, float z, float w);
        public delegate void glUniformMatrix4fv(int location, int count, bool transpose, float* values);
        public delegate void glUniform1i(int location, int x);
        public delegate void glUniform2i(int location, int x, int y);
        public delegate void glUniform3i(int location, int x, int y, int z);
        public delegate void glUniform4i(int location, int x, int y, int z, int w);
        public delegate void glUseProgram(int programId);
        public delegate void glVertexAttribPointer(int index, int dimensions, VertexAttributePointerType type, bool isNormalized, int stride, int offset);
        public delegate void glViewport(int x, int y, int width, int height);
    }
}
