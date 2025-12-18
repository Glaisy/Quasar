//-----------------------------------------------------------------------
// <copyright file="RenderBatch.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;

using Quasar.Graphics.Internals;

namespace Quasar.Rendering.Internals
{
    /// <summary>
    /// Render batch object.
    /// </summary>
    internal sealed class RenderBatch
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderBatch" /> class.
        /// </summary>
        /// <param name="shader">The shader.</param>
        public RenderBatch(ShaderBase shader)
        {
            Shader = shader;
        }


        /// <summary>
        /// The double sided render model objects.
        /// </summary>
        public readonly HashSet<RenderModel> DoubleSidedModels = new HashSet<RenderModel>();

        /// <summary>
        /// The render model objects.
        /// </summary>
        public readonly HashSet<RenderModel> Models = new HashSet<RenderModel>();

        /// <summary>
        /// The shader.
        /// </summary>
        public readonly ShaderBase Shader;
    }
}
