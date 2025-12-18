//-----------------------------------------------------------------------
// <copyright file="RenderModelCommand.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics;
using Quasar.Graphics.Internals;

namespace Quasar.Rendering.Processors.Internals
{
    /// <summary>
    /// Render model's command structure.
    /// </summary>
    internal struct RenderModelCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderModelCommand"/> struct.
        /// </summary>
        /// <param name="renderModel">The render model.</param>
        /// <param name="type">The type.</param>
        public RenderModelCommand(RenderModel renderModel, RenderModelCommandType type)
        {
            RenderModel = renderModel;
            Type = type;
        }


        /// <summary>
        /// The render model.
        /// </summary>
        public readonly RenderModel RenderModel;

        /// <summary>
        /// The command type.
        /// </summary>
        public readonly RenderModelCommandType Type;


        /// <summary>
        /// The layer.
        /// </summary>
        public Layer Layer;

        /// <summary>
        /// The mesh.
        /// </summary>
        public IMesh Mesh;

        /// <summary>
        /// The shader.
        /// </summary>
        public ShaderBase Shader;

        /// <summary>
        /// The value flag.
        /// </summary>
        public bool Value;
    }
}
