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

using Quasar.Core.IO;

namespace Quasar.Graphics.Internals.Factories
{
    /// <summary>
    /// Represents a render shader factory component.
    /// </summary>
    internal interface IShaderFactory
    {
        /// <summary>
        /// Creates a render shader by identifier and source code.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="source">The source code.</param>
        /// <returns>
        /// The created render shader instance.
        /// </returns>
        ShaderBase CreateShader(string id, in ShaderSource source);

        /// <summary>
        /// Loads the built-in render shaders.
        /// </summary>
        List<ShaderBase> LoadBuiltInShaders();

        /// <summary>
        /// Loads the shaders by resource provider and resource directory path.
        /// </summary>
        /// <param name="resourceProvider">The resource provider.</param>
        /// <param name="resourceDirectoryPath">The resource directory path.</param>
        List<ShaderBase> LoadShaders(IResourceProvider resourceProvider, string resourceDirectoryPath);
    }
}