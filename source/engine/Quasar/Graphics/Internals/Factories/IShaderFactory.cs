//-----------------------------------------------------------------------
// <copyright file="IShaderFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;

using Quasar.Utilities;

namespace Quasar.Graphics.Internals.Factories
{
    /// <summary>
    /// Represents a render shader factory component.
    /// </summary>
    internal interface IShaderFactory
    {
        /// <summary>
        /// Creates a compute shader by identifier and source code.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="source">The source code.</param>
        /// <param name="tag">The tag value.</param>
        /// <returns>
        /// The created compute shader instance.
        /// </returns>
        ComputeShaderBase CreateComputeShader(string id, string source, string tag);

        /// <summary>
        /// Creates a render shader by identifier and source code.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="source">The source code.</param>
        /// <param name="tag">The tag value.</param>
        /// <returns>
        /// The created render shader instance.
        /// </returns>
        ShaderBase CreateShader(string id, in ShaderSource source, string tag);

        /// <summary>
        /// Loads the built-in render shaders.
        /// </summary>
        IEnumerable<ShaderBase> LoadBuiltInShaders();

        /// <summary>
        /// Loads the shaders by resource provider and resource directory path.
        /// </summary>
        /// <param name="resourceProvider">The resource provider.</param>
        /// <param name="searchPath">The resource search path.</param>
        /// <param name="tag">The tag.</param>
        IEnumerable<ShaderBase> LoadShaders(
            IResourceProvider resourceProvider,
            string searchPath,
            string tag);
    }
}