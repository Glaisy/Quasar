//-----------------------------------------------------------------------
// <copyright file="ShaderProperty.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Graphics
{
    /// <summary>
    /// Shader property structure.
    /// </summary>
    public sealed class ShaderProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShaderProperty" /> class.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="textureUnit">The texture unit.</param>
        internal ShaderProperty(int index, string name, ShaderPropertyType type, int textureUnit)
        {
            Index = index;
            Name = name;
            Type = type;
            TextureUnit = textureUnit;
        }


        /// <summary>
        /// The index.
        /// </summary>
        public readonly int Index;

        /// <summary>
        /// The name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The type.
        /// </summary>
        public readonly ShaderPropertyType Type;

        /// <summary>
        /// The texture unit.
        /// </summary>
        public readonly int TextureUnit;
    }
}
