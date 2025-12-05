//-----------------------------------------------------------------------
// <copyright file="ComputeShaderBase.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Graphics.Internals
{
    /// <summary>
    /// Abstract base class for compute shader program implementations.
    /// </summary>
    /// <seealso cref="CoreShaderBase" />
    /// <seealso cref="IComputeShader" />
    internal abstract class ComputeShaderBase : CoreShaderBase, IComputeShader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComputeShaderBase" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="descriptor">The graphic resource descriptor.</param>
        protected ComputeShaderBase(string id, in GraphicsResourceDescriptor descriptor)
            : base(ShaderType.Compute, id, descriptor)
        {
        }


        /// <inheritdoc/>
        public bool Equals(IComputeShader other)
        {
            return base.Equals(other);
        }
    }
}
