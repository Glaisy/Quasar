//-----------------------------------------------------------------------
// <copyright file="Sprite.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Quasar.Graphics;

namespace Quasar.UI.VisualElements
{
    /// <summary>
    /// Represents a bordered texture wrapper structure for UI element rendering purposes (Immutable).
    /// </summary>
    /// <seealso cref="IEquatable{Sprite}" />
    public readonly struct Sprite : IEquatable<Sprite>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite" /> struct.
        /// </summary>
        /// <param name="texture">The texture.</param>
        public Sprite(ITexture texture)
        {
            if (texture == null)
            {
                return;
            }

            Size = new Vector2(texture.Size.Width, texture.Size.Height);
            Texture = texture;
            IsBorderless = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite" /> struct.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="border">The border.</param>
        public Sprite(ITexture texture, in Border border)
        {
            if (texture == null)
            {
                return;
            }

            Border = border;
            Size = new Vector2(texture.Size.Width, texture.Size.Height);
            Texture = texture;
            IsBorderless = border == Border.Empty;
        }


        /// <summary>
        /// Gets the border.
        /// </summary>
        public readonly Border Border;

        /// <summary>
        /// The borderless flag.
        /// </summary>
        public readonly bool IsBorderless;

        /// <summary>
        /// The size.
        /// </summary>
        public readonly Vector2 Size;

        /// <summary>
        /// The texture.
        /// </summary>
        public readonly ITexture Texture;


        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        public static bool operator ==(in Sprite left, in Sprite right)
        {
            var leftHandle = left.Texture?.Handle ?? 0;
            var rightHandle = right.Texture?.Handle ?? 0;
            return leftHandle == rightHandle && left.Border == right.Border;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        public static bool operator !=(in Sprite left, in Sprite right)
        {
            return !(left == right);
        }


        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is not Sprite other)
            {
                return false;
            }

            return this == other;
        }

        /// <inheritdoc/>
        public bool Equals(Sprite other)
        {
            return this == other;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Texture?.Handle ?? 0, Border);
        }
    }
}
