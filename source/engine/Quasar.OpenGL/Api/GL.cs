//-----------------------------------------------------------------------
// <copyright file="GL.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Diagnostics;

using Quasar.Utilities;

using Space.Core;

namespace Quasar.OpenGL.Api
{
    /// <summary>
    /// OpenGL function wrapper.
    /// </summary>
    internal static unsafe partial class GL
    {
        private static CreateContext createContext;
        private static MakeCurrent makeCurrent;

        private static glDeleteBuffers deleteBuffers;
        private static glDeleteFramebuffers deleteFrameBuffers;
        private static glDeleteRenderbuffers deleteRenderBuffers;
        private static glDeleteTextures deleteTextures;
        private static glDeleteVertexArrays deleteVertexArrays;
        private static glGenBuffers genBuffers;
        private static glGenFramebuffers genFrameBuffers;
        private static glGenRenderbuffers genRenderBuffers;
        private static glGenTextures genTextures;
        private static glGenVertexArrays genVertexArrays;
        private static glGetActiveUniform getActiveUniform;
        private static glGetShaderInfoLog getShaderInfoLog;
        private static glGetShaderiv getShaderiv;
        private static glGetString getString;
        private static glGetUniformfv getUniformfv;
        private static glGetUniformiv getUniformiv;
        private static glShaderSource shaderSource;
        private static glTexParameterfv texParameterVector4;


        public static DeleteContext DestroyContext;
        public static glAttachShader AttachShader;
        public static glActiveTexture ActiveTexture;
        public static glBindBuffer BindBuffer;
        public static glBindFramebuffer BindFrameBuffer;
        public static glBindRenderbuffer BindRenderBuffer;
        public static glBindTexture BindTexture;
        public static glBindVertexArray BindVertexArray;
        public static glBlendFunc BlendFunc;
        public static glBufferData BufferData;
        public static glClear Clear;
        public static glClearColor ClearColor;
        public static glCompileShader CompileShader;
        public static glCreateProgram CreateProgram;
        public static glCreateShader CreateShader;
        public static glCullFace CullFace;
        public static glDeleteProgram DeleteProgram;
        public static glDeleteShader DeleteShader;
        public static glDepthFunc DepthFunc;
        public static glDetachShader DetachShader;
        public static glDisable Disable;
        public static glDrawArrays DrawArrays;
        public static glDrawBuffer DrawBuffer;
        public static glDrawElements DrawElements;
        public static glEnable Enable;
        public static glEnableVertexAttribArray EnableVertexAttribArray;
        public static glFramebufferRenderbuffer FrameBufferRenderBuffer;
        public static glFramebufferTexture2D FrameBufferTexture2D;
        public static glFrontFace FrontFace;
        public static glGenerateMipmap GenerateMipmap;
        public static glGetBufferSubData GetBufferSubData;
        public static glGetError GetError;
        public static glGetFloatv GetFloat;
        public static glGetProgramiv GetProgram;
        public static glGetUniformLocation GetUniformLocation;
        public static glLineWidth LineWidth;
        public static glLinkProgram LinkProgram;
        public static glReadBuffer ReadBuffer;
        public static glRenderbufferStorage RenderBufferStorage;
        public static glSwapIntervalEXT SwapInterval;
        public static glTexImage2D TexImage2D;
        public static glTexParameteri TexParameterInteger;
        public static glTexParameterf TexParameterFloat;
        public static glUseProgram UseProgram;
        public static glUniform1f Uniform1f;
        public static glUniform2f Uniform2f;
        public static glUniform3f Uniform3f;
        public static glUniform4f Uniform4f;
        public static glUniform1i Uniform1i;
        public static glUniform2i Uniform2i;
        public static glUniform3i Uniform3i;
        public static glUniform4i Uniform4i;
        public static glUniformMatrix4fv UniformMatrix4;
        public static glVertexAttribPointer VertexAttribPointer;
        public static glViewport Viewport;


        /// <summary>
        /// Checks the error of the last function call.
        /// </summary>
        [Conditional("DEBUG")]
        public static void CheckErrors()
        {
            var errorCode = GetError();
            if (errorCode != 0)
            {
                throw new GLException($"OpenGL error occured: {errorCode}");
            }
        }

        /// <summary>
        /// Initializes the OpenGL function wrapper by the specified function provider.
        /// </summary>
        /// <param name="deviceContext">The device context.</param>
        /// <param name="interopFunctionProvider">The interop function provider.</param>
        public static void Initialize(IntPtr deviceContext, IInteropFunctionProvider interopFunctionProvider)
        {
            Assertion.ThrowIfNull(deviceContext, nameof(deviceContext));
            Assertion.ThrowIfNull(interopFunctionProvider, nameof(interopFunctionProvider));

            // initialize basic OpenGL functions
            createContext = interopFunctionProvider.GetFunction<CreateContext>();
            makeCurrent = interopFunctionProvider.GetFunction<MakeCurrent>();
            DestroyContext = interopFunctionProvider.GetFunction<DeleteContext>();

            // initialize render context
            var renderContext = createContext(deviceContext);
            if (renderContext == IntPtr.Zero)
            {
                throw new GLException("Unable to initialize render context");
            }

            var success = makeCurrent(deviceContext, renderContext);
            if (!success)
            {
                throw new GLException("Unable to set current render context");
            }

            // initialize rest of the OpenGL functions
            deleteBuffers = interopFunctionProvider.GetFunction<glDeleteBuffers>();
            deleteFrameBuffers = interopFunctionProvider.GetFunction<glDeleteFramebuffers>();
            deleteRenderBuffers = interopFunctionProvider.GetFunction<glDeleteRenderbuffers>();
            deleteTextures = interopFunctionProvider.GetFunction<glDeleteTextures>();
            deleteVertexArrays = interopFunctionProvider.GetFunction<glDeleteVertexArrays>();
            genBuffers = interopFunctionProvider.GetFunction<glGenBuffers>();
            genFrameBuffers = interopFunctionProvider.GetFunction<glGenFramebuffers>();
            genRenderBuffers = interopFunctionProvider.GetFunction<glGenRenderbuffers>();
            genTextures = interopFunctionProvider.GetFunction<glGenTextures>();
            genVertexArrays = interopFunctionProvider.GetFunction<glGenVertexArrays>();
            getActiveUniform = interopFunctionProvider.GetFunction<glGetActiveUniform>();
            getShaderInfoLog = interopFunctionProvider.GetFunction<glGetShaderInfoLog>();
            getShaderiv = interopFunctionProvider.GetFunction<glGetShaderiv>();
            getString = interopFunctionProvider.GetFunction<glGetString>();
            getUniformfv = interopFunctionProvider.GetFunction<glGetUniformfv>();
            getUniformiv = interopFunctionProvider.GetFunction<glGetUniformiv>();
            shaderSource = interopFunctionProvider.GetFunction<glShaderSource>();
            texParameterVector4 = interopFunctionProvider.GetFunction<glTexParameterfv>();

            AttachShader = interopFunctionProvider.GetFunction<glAttachShader>();
            ActiveTexture = interopFunctionProvider.GetFunction<glActiveTexture>();
            BindBuffer = interopFunctionProvider.GetFunction<glBindBuffer>();
            BindFrameBuffer = interopFunctionProvider.GetFunction<glBindFramebuffer>();
            BindRenderBuffer = interopFunctionProvider.GetFunction<glBindRenderbuffer>();
            BindTexture = interopFunctionProvider.GetFunction<glBindTexture>();
            BindVertexArray = interopFunctionProvider.GetFunction<glBindVertexArray>();
            BlendFunc = interopFunctionProvider.GetFunction<glBlendFunc>();
            BufferData = interopFunctionProvider.GetFunction<glBufferData>();
            Clear = interopFunctionProvider.GetFunction<glClear>();
            ClearColor = interopFunctionProvider.GetFunction<glClearColor>();
            CompileShader = interopFunctionProvider.GetFunction<glCompileShader>();
            CreateProgram = interopFunctionProvider.GetFunction<glCreateProgram>();
            CreateShader = interopFunctionProvider.GetFunction<glCreateShader>();
            CullFace = interopFunctionProvider.GetFunction<glCullFace>();
            DeleteProgram = interopFunctionProvider.GetFunction<glDeleteProgram>();
            DeleteShader = interopFunctionProvider.GetFunction<glDeleteShader>();
            DepthFunc = interopFunctionProvider.GetFunction<glDepthFunc>();
            DetachShader = interopFunctionProvider.GetFunction<glDetachShader>();
            Disable = interopFunctionProvider.GetFunction<glDisable>();
            DrawArrays = interopFunctionProvider.GetFunction<glDrawArrays>();
            DrawBuffer = interopFunctionProvider.GetFunction<glDrawBuffer>();
            DrawElements = interopFunctionProvider.GetFunction<glDrawElements>();
            Enable = interopFunctionProvider.GetFunction<glEnable>();
            EnableVertexAttribArray = interopFunctionProvider.GetFunction<glEnableVertexAttribArray>();
            FrameBufferRenderBuffer = interopFunctionProvider.GetFunction<glFramebufferRenderbuffer>();
            FrameBufferTexture2D = interopFunctionProvider.GetFunction<glFramebufferTexture2D>();
            FrontFace = interopFunctionProvider.GetFunction<glFrontFace>();
            GenerateMipmap = interopFunctionProvider.GetFunction<glGenerateMipmap>();
            GetBufferSubData = interopFunctionProvider.GetFunction<glGetBufferSubData>();
            GetError = interopFunctionProvider.GetFunction<glGetError>();
            GetFloat = interopFunctionProvider.GetFunction<glGetFloatv>();
            GetProgram = interopFunctionProvider.GetFunction<glGetProgramiv>();
            GetUniformLocation = interopFunctionProvider.GetFunction<glGetUniformLocation>();
            LineWidth = interopFunctionProvider.GetFunction<glLineWidth>();
            LinkProgram = interopFunctionProvider.GetFunction<glLinkProgram>();
            ReadBuffer = interopFunctionProvider.GetFunction<glReadBuffer>();
            RenderBufferStorage = interopFunctionProvider.GetFunction<glRenderbufferStorage>();
            SwapInterval = interopFunctionProvider.GetFunction<glSwapIntervalEXT>();
            TexImage2D = interopFunctionProvider.GetFunction<glTexImage2D>();
            TexParameterInteger = interopFunctionProvider.GetFunction<glTexParameteri>();
            TexParameterFloat = interopFunctionProvider.GetFunction<glTexParameterf>();
            Uniform1f = interopFunctionProvider.GetFunction<glUniform1f>();
            Uniform2f = interopFunctionProvider.GetFunction<glUniform2f>();
            Uniform3f = interopFunctionProvider.GetFunction<glUniform3f>();
            Uniform4f = interopFunctionProvider.GetFunction<glUniform4f>();
            Uniform1i = interopFunctionProvider.GetFunction<glUniform1i>();
            Uniform2i = interopFunctionProvider.GetFunction<glUniform2i>();
            Uniform3i = interopFunctionProvider.GetFunction<glUniform3i>();
            Uniform4i = interopFunctionProvider.GetFunction<glUniform4i>();
            UniformMatrix4 = interopFunctionProvider.GetFunction<glUniformMatrix4fv>();
            UseProgram = interopFunctionProvider.GetFunction<glUseProgram>();
            VertexAttribPointer = interopFunctionProvider.GetFunction<glVertexAttribPointer>();
            Viewport = interopFunctionProvider.GetFunction<glViewport>();
        }
    }
}
