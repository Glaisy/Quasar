//-----------------------------------------------------------------------
// <copyright file="CoreShaderBase.cs" company="Space Development">
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
    /// Abstract base class for core shader program implementations.
    /// </summary>
    /// <seealso cref="GraphicsResourceBase" />
    /// <seealso cref="IShader" />
    internal abstract class CoreShaderBase : GraphicsResourceBase, ICoreShader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoreShaderBase" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="descriptor">The graphic resource descriptor.</param>
        protected CoreShaderBase(ShaderType type, string id, in GraphicsResourceDescriptor descriptor)
            : base(descriptor)
        {
            Type = type;
            Id = id;
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            Id = null;
            Type = ShaderType.Unknown;
        }

        /// <inheritdoc/>
        public string Id { get; private set; }

        /// <inheritdoc/>
        public ShaderType Type { get; private set; }


        /// <inheritdoc/>
        public bool Equals(ICoreShader other)
        {
            if (other == null)
            {
                return false;
            }

            return Handle == other.Handle;
        }
    }
}
