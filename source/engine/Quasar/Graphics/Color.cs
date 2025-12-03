//-----------------------------------------------------------------------
// <copyright file="Color.cs" company="Space Development">
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
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

using Quasar.IO.Serialization.Json;

using Space.Core;

namespace Quasar.Graphics
{
    /// <summary>
    /// Float valued color representation (Immutable).
    /// </summary>
    /// <seealso cref="IEquatable{ColorF}" />
    [JsonConverter(typeof(ColorJsonConverter))]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct Color : IEquatable<Color>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public Color(string value)
            : this(UInt32.Parse(value, NumberStyles.HexNumber, CultureInfo.InvariantCulture))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct.
        /// </summary>
        /// <param name="rgba">The RRGGBBAA color representation.</param>
        public Color(uint rgba)
        {
            A = (rgba & 0xFF) / 255F;
            rgba >>= 8;
            B = (rgba & 0xFF) / 255F;
            rgba >>= 8;
            G = (rgba & 0xFF) / 255F;
            rgba >>= 8;
            R = (rgba & 0xFF) / 255F;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct.
        /// </summary>
        /// <param name="r">The red ccomponent.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        public Color(float r, float g, float b, float a = 1.0f)
        {
            R = Ranges.FloatUnit.Clamp(r);
            G = Ranges.FloatUnit.Clamp(g);
            B = Ranges.FloatUnit.Clamp(b);
            A = Ranges.FloatUnit.Clamp(a);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color" /> struct.
        /// </summary>
        /// <param name="value">The color value.</param>
        public Color(System.Drawing.Color value)
        {
            A = value.A / 255f;
            R = value.R / 255f;
            G = value.G / 255f;
            B = value.B / 255f;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color" /> struct.
        /// </summary>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        public Color(byte r, byte g, byte b, float a = 1.0f)
        {
            R = r / 255f;
            G = g / 255f;
            B = b / 255f;
            A = Ranges.FloatUnit.Clamp(a);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color" /> struct.
        /// </summary>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        public Color(byte r, byte g, byte b, byte a = 0xFF)
        {
            R = r / 255f;
            G = g / 255f;
            B = b / 255f;
            A = a / 255f;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Color" /> struct.
        /// </summary>
        /// <param name="value">The source color value.</param>
        /// <param name="a">The alpha component.</param>
        public Color(in Color value, float a)
        {
            R = value.R;
            G = value.G;
            B = value.B;
            A = Ranges.FloatUnit.Clamp(a);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct.
        /// </summary>
        /// <param name="rgba">The RGBA value.</param>
        public Color(in RGBA rgba)
        {
            R = rgba.R / 255f;
            G = rgba.G / 255f;
            B = rgba.B / 255f;
            A = rgba.A / 255f;
        }

        /// <summary>
        /// The read component.
        /// </summary>
        public readonly float R;

        /// <summary>
        /// The green component.
        /// </summary>
        public readonly float G;

        /// <summary>
        /// The blue component.
        /// </summary>
        public readonly float B;

        /// <summary>
        /// The alpha component.
        /// </summary>
        public readonly float A;


        /// <summary>
        /// The black color.
        /// </summary>
        public static readonly Color Black = new Color(0x000000FF);

        /// <summary>
        /// The blue color.
        /// </summary>
        public static readonly Color Blue = new Color(0x0000FFFF);

        /// <summary>
        /// The green color.
        /// </summary>
        public static readonly Color Green = new Color(0x00FF00FF);

        /// <summary>
        /// The magenta color.
        /// </summary>
        public static readonly Color Magenta = new Color(0xFF00FFFF);

        /// <summary>
        /// The red color.
        /// </summary>
        public static readonly Color Red = new Color(0xFF0000FF);

        /// <summary>
        /// The transparent color.
        /// </summary>
        public static readonly Color Transparent = new Color(0x000000);

        /// <summary>
        /// The yelllow color.
        /// </summary>
        public static readonly Color Yellow = new Color(0xFFFF00FF);

        /// <summary>
        /// The white color.
        /// </summary>
        public static readonly Color White = new Color(0xFFFFFFFF);


        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Drawing.Color"/> to <see cref="Color"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Color(in System.Drawing.Color value)
        {
            return new Color(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="RGBA"/> to <see cref="Color"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Color(in RGBA value)
        {
            return new Color(value);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        public static bool operator ==(in Color a, in Color b)
        {
            return a.R == b.R && a.G == b.G && a.B == b.B && a.A == b.A;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        public static bool operator !=(in Color a, in Color b)
        {
            return a.R != b.R || a.G != b.G || a.B != b.B || a.A != b.A;
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
            if (obj is Color other)
            {
                return R == other.R && G == other.G && B == other.B && A == other.A;
            }

            return false;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(Color other)
        {
            return R == other.R && G == other.G && B == other.B && A == other.A;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(R, G, B, A);
        }

        /// <summary>
        /// Calculates the linear interpolated value between a and b. The interpolation parameter is clamped to the [0...1] range.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <param name="t">The interpolation parameter.</param>
        public static Color Lerp(in Color a, in Color b, float t)
        {
            t = Ranges.FloatUnit.Clamp(t);
            return new Color(
                a.R + (b.R - a.R) * t,
                a.G + (b.G - a.G) * t,
                a.B + (b.B - a.B) * t,
                a.A + (b.A - a.A) * t);
        }

        /// <summary>
        /// Calculates the linear interpolated value between a and b. The interpolation parameter is not clamped to the [0...1] range.
        /// </summary>
        /// <param name="a">The a value.</param>
        /// <param name="b">The b value.</param>
        /// <param name="t">The interpolation parameter.</param>
        public static Color LerpUnclamped(in Color a, in Color b, float t)
        {
            return new Color(
                a.R + (b.R - a.R) * t,
                a.G + (b.G - a.G) * t,
                a.B + (b.B - a.B) * t,
                a.A + (b.A - a.A) * t);
        }


        /// <summary>
        /// Converts the color to RRGGBBAA integer representation.
        /// </summary>
        public uint ToRGBA()
        {
            var rgba = (uint)(R * 255f);
            rgba <<= 8;
            rgba |= (uint)(G * 255f);
            rgba <<= 8;
            rgba |= (uint)(B * 255f);
            rgba <<= 8;
            rgba |= (uint)(A * 255f);
            return rgba;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"({R}, {G}, {B}, {A})";
        }

        /// <summary>
        /// The well known colors.
        /// </summary>
        public static readonly Dictionary<string, Color> WellKnownColors = new Dictionary<string, Color>
        {
            { "Color.Black", Black },
            { "Color.Blue", Blue },
            { "Color.Green", Green },
            { "Color.Magenta", Magenta },
            { "Color.Red", Red },
            { "Color.Transparent", Transparent },
            { "Color.Yellow", Yellow },
            { "Color.White", White }
        };
    }
}
