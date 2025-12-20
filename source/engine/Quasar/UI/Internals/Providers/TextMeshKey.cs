//-----------------------------------------------------------------------
// <copyright file="TextMeshKey.cs" company="Space Development">
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

namespace Quasar.UI.Internals.Providers
{
    /// <summary>
    /// Represents an internal key for text meshes.
    /// </summary>
    /// <seealso cref="IEquatable{TextMeshKey}" />
    internal readonly struct TextMeshKey : IEquatable<TextMeshKey>
    {
        private readonly int hashCode;


        /// <summary>
        /// Initializes a new instance of the <see cref="TextMeshKey" /> struct.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <param name="text">The text.</param>
        /// <param name="start">The start.</param>
        /// <param name="length">The length.</param>
        public TextMeshKey(Font font, string text, int start, int length)
        {
            FontId = font.Id;
            Text = text;
            Start = start;
            Length = length;

            hashCode = HashCode.Combine(FontId, Text, start, length);
        }


        /// <summary>
        /// The font identifier.
        /// </summary>
        public readonly long FontId;

        /// <summary>
        /// The text.
        /// </summary>
        public readonly string Text;

        /// <summary>
        /// The start index.
        /// </summary>
        public readonly int Start;

        /// <summary>
        /// The length.
        /// </summary>
        public readonly int Length;


        /// <inheritdoc/>
        public bool Equals(TextMeshKey other)
        {
            return FontId == other.FontId && Text == other.Text && Start == other.Start && Length == other.Length;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is not TextMeshKey other)
            {
                return false;
            }

            return FontId == other.FontId && Text == other.Text && Start == other.Start && Length == other.Length;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return hashCode;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"({FontId}: '{Text}', [{Start}, {Length}])";
        }
    }
}
