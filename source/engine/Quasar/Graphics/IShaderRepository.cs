//-----------------------------------------------------------------------
// <copyright file="IShaderRepository.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Collections;
using Quasar.Graphics.Internals;

namespace Quasar.Graphics
{
    /// <summary>
    /// Render shader repository interface definition.
    /// </summary>
    /// <seealso cref="IRepository{String, IShader}" />
    public interface IShaderRepository : IResourceRepository<string, IShader>
    {
        /// <summary>
        /// Gets the fallback shader.
        /// </summary>
        IShader FallbackShader { get; }


        /// <summary>
        /// Creates a new render shader.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="source">The source.</param>
        /// <param name="tag">The custom tag value.</param>
        /// <returns>
        /// The create shader instance.
        /// </returns>
        IShader Create(string id, ShaderSource source, string tag = null);


        /// <summary>
        /// Gets the shader instance by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        internal ShaderBase GetShader(string id);

        /// <summary>
        /// Gets the shader instance by the specified handle.
        /// </summary>
        /// <param name="handle">The internal handle.</param>
        internal ShaderBase GetShader(int handle);

        /// <summary>
        /// Loads the built-in shaders.
        /// </summary>
        internal void LoadBuiltInShaders();
    }
}