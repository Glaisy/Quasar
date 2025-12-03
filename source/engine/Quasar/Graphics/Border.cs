//-----------------------------------------------------------------------
// <copyright file="Border.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------


using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

using Quasar.IO.Serialization.Json;

namespace Quasar.Graphics
{
    /// <summary>
    /// The border data structure (Immutable).
    /// </summary>
    [JsonConverter(typeof(BorderJsonConverter))]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct Border
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Border" /> struct.
        /// </summary>
        /// <param name="top">The top width.</param>
        /// <param name="right">The right width.</param>
        /// <param name="bottom">The bottom width.</param>
        /// <param name="left">The left width.</param>
        public Border(float left, float top, float right, float bottom)
        {
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Border" /> struct.
        /// </summary>
        /// <param name="width">The width in every directions.</param>
        public Border(float width)
        {
            Left = Top = Right = Bottom = width;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Border" /> struct.
        /// </summary>
        /// <param name="width">The width in horizontal directions.</param>
        /// <param name="height">The height in vertical directions.</param>
        public Border(float width, float height)
        {
            Left = Right = Bottom = width;
            Top = Bottom = height;
        }


        /// <summary>
        /// The left border.
        /// </summary>
        public readonly float Left;

        /// <summary>
        /// The top border.
        /// </summary>
        public readonly float Top;

        /// <summary>
        /// The right border.
        /// </summary>
        public readonly float Right;

        /// <summary>
        /// The top border.
        /// </summary>
        public readonly float Bottom;

        /// <summary>
        /// The bottom border.
        /// </summary>
        public static readonly Border Empty;

        /// <summary>
        /// Gets the height (top + bottom).
        /// </summary>
        public float Height => Top + Bottom;

        /// <summary>
        /// Gets the width (left + right).
        /// </summary>
        public float Width => Left + Right;


        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        public static bool operator ==(in Border a, in Border b)
        {
            return a.Left == b.Left && a.Top == b.Top && a.Right == b.Right && a.Bottom == b.Bottom;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        public static bool operator !=(in Border a, in Border b)
        {
            return a.Left != b.Left || a.Top != b.Top || a.Right != b.Right || a.Bottom != b.Bottom;
        }


        /// <summary>
        /// Determines whether the specified <see cref="Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is not Border other)
            {
                return false;
            }

            return this == other;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(Left, Top, Right, Bottom);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"(L:{Left}, T:{Top}, R:{Right}, B:{Bottom})";
        }
    }
}
