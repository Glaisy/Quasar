//-----------------------------------------------------------------------
// <copyright file="IShader.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Quasar.Graphics
{
    /// <summary>
    /// Represents a render shader program.
    /// </summary>
    /// <seealso cref="ICoreShader" />
    /// <seealso cref="IEquatable{IShader}" />
    public interface IShader : ICoreShader, IEquatable<IShader>
    {
        /// <summary>
        /// Gets the <see cref="ShaderProperty" /> with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        ShaderProperty this[string name] { get; }


        /// <summary>
        /// Gets the per draw properties.
        /// </summary>
        IReadOnlyList<ShaderProperty> DrawProperties { get; }

        /// <summary>
        /// Gets the per frame properties.
        /// </summary>
        IReadOnlyList<ShaderProperty> FrameProperties { get; }

        /// <summary>
        /// Gets the light properties.
        /// </summary>
        IReadOnlyList<ShaderProperty> LightProperties { get; }

        /// <summary>
        /// Gets the material properties.
        /// </summary>
        IReadOnlyList<ShaderProperty> MaterialProperties { get; }

        /// <summary>
        /// Gets the per vieww properties.
        /// </summary>
        IReadOnlyList<ShaderProperty> ViewProperties { get; }
    }
}
