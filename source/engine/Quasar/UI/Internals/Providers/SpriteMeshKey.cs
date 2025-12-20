//-----------------------------------------------------------------------
// <copyright file="SpriteMeshKey.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.UI.Internals.Providers
{
    /// <summary>
    /// Represents an internal key for sprite meshes.
    /// </summary>
    /// <seealso cref="IEquatable{SpriteMeshKey}" />
    internal readonly struct SpriteMeshKey : IEquatable<SpriteMeshKey>
    {
        private readonly int hashCode;


        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteMeshKey" /> struct.
        /// </summary>
        /// <param name="sprite">The sprite.</param>
        /// <param name="size">The size.</param>
        public SpriteMeshKey(in Sprite sprite, in Vector2 size)
        {
            BorderHashCode = sprite.Border.GetHashCode();
            SizeHashCode = size.GetHashCode();
            TextureSizeHashCode = sprite.Texture.GetHashCode();
            hashCode = HashCode.Combine(BorderHashCode, SizeHashCode, TextureSizeHashCode);
        }


        /// <summary>
        /// The border's hash code.
        /// </summary>
        public readonly int BorderHashCode;

        /// <summary>
        /// The the size's hash code.
        /// </summary>
        public readonly int SizeHashCode;

        /// <summary>
        /// The texture size's hashcode.
        /// </summary>
        public readonly int TextureSizeHashCode;


        /// <inheritdoc/>
        public bool Equals(SpriteMeshKey other)
        {
            return BorderHashCode == other.BorderHashCode &&
                SizeHashCode == other.SizeHashCode &&
                TextureSizeHashCode == other.TextureSizeHashCode;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is not SpriteMeshKey other)
            {
                return false;
            }

            return BorderHashCode == other.BorderHashCode &&
                SizeHashCode == other.SizeHashCode &&
                TextureSizeHashCode == other.TextureSizeHashCode;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return hashCode;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"({hashCode}])";
        }
    }
}
