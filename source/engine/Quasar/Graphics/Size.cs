//-----------------------------------------------------------------------
// <copyright file="Size.cs" company="Space Development">
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
    /// 2D size structure for integer operations (Immutable).
    /// </summary>
    /// <seealso cref="IEquatable{Size}" />
    [JsonConverter(typeof(SizeJsonConverter))]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct Size : IEquatable<Size>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Size"/> struct.
        /// </summary>
        /// <param name="width">The width value.</param>
        /// <param name="height">The height value.</param>
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public Size(int value)
        {
            Width = Height = value;
        }


        /// <summary>
        /// The width value.
        /// </summary>
        public readonly int Width;

        /// <summary>
        /// The height value.
        /// </summary>
        public readonly int Height;

        /// <summary>
        /// The empty value.
        /// </summary>
        public static readonly Size Empty;

        /// <summary>
        /// The maximum value.
        /// </summary>
        public static readonly Size Maximum = new Size(Int32.MaxValue, Int32.MaxValue);

        /// <summary>
        /// The minimum value.
        /// </summary>
        public static readonly Size Minimum = new Size(Int32.MinValue, Int32.MinValue);


        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(in Size a, in Size b)
        {
            return a.Width == b.Width && a.Height == b.Height;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(in Size a, in Size b)
        {
            return a.Width != b.Width || a.Height != b.Height;
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Size operator +(in Size a, in Size b)
        {
            return new Size(a.Width + b.Width, a.Height + b.Height);
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Size operator +(in Size a, int b)
        {
            return new Size(a.Width + b, a.Height + b);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Size operator -(in Size a, in Size b)
        {
            return new Size(a.Width - b.Width, a.Height - b.Height);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Size operator -(in Size a, int b)
        {
            return new Size(a.Width - b, a.Height - b);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Size"/> to <see cref="System.Drawing.Size"/>.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator System.Drawing.Size(in Size size)
        {
            return new System.Drawing.Size(size.Width, size.Height);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Drawing.Size"/> to <see cref="Size"/>.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Size(in System.Drawing.Size size)
        {
            return new Size(size.Width, size.Height);
        }


        /// <summary>
        /// Adds the specified value to this instance.
        /// </summary>
        /// <param name="size">The size value.</param>
        /// <returns>The result of operation.</returns>
        public Size Add(in Size size)
        {
            return new Size(Width + size.Width, Height + size.Height);
        }

        /// <summary>
        /// Adds the specified value to this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of operation.</returns>
        public Size Add(int value)
        {
            return new Size(Width + value, Height + value);
        }

        /// <inheritdoc/>
        public bool Equals(Size other)
        {
            return Width == other.Width && Height == other.Height;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is Size size && Equals(size);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Width, Height);
        }

        /// <summary>
        /// Sustracts the specified value from this instance.
        /// </summary>
        /// <param name="size">The size value.</param>
        /// <returns>The result of operation.</returns>
        public Size Substract(in Size size)
        {
            return new Size(Width - size.Width, Height - size.Height);
        }

        /// <summary>
        /// Sustracts the specified value from this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of operation.</returns>
        public Size Substract(int value)
        {
            return new Size(Width - value, Height - value);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"(W:{Width}, H:{Height})";
        }
    }
}
