//-----------------------------------------------------------------------
// <copyright file="GLShaderException.cs" company="Space Development">
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
    /// <seealso cref="GLException" />
    internal sealed class GLShaderException : GLException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GLShaderException" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="shaderType">Type of the shader.</param>
        /// <param name="errorMessage">The error message.</param>
        public GLShaderException(string id, Api.ShaderType shaderType, string errorMessage)
            : base($"Unable to compile '{id}' {shaderType}: '{errorMessage}'. Skipped.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GLShaderException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public GLShaderException(string message)
            : base(message)
        {
        }
    }
}
