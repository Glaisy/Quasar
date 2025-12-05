//-----------------------------------------------------------------------
// <copyright file="OpenGLShaderException.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.OpenGL.Graphics
{
    /// <summary>
    /// OpenGL shader exception.
    /// </summary>
    /// <seealso cref="OpenGLException" />
    internal sealed class OpenGLShaderException : OpenGLException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGLShaderException" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="shaderType">Type of the shader.</param>
        /// <param name="errorMessage">The error message.</param>
        public OpenGLShaderException(string id, Api.ShaderType shaderType, string errorMessage)
            : base($"Unable to compile '{id}' {shaderType}: '{errorMessage}'. Skipped.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGLShaderException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public OpenGLShaderException(string message)
            : base(message)
        {
        }
    }
}
