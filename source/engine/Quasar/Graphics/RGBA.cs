//-----------------------------------------------------------------------
// <copyright file="RGBA.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

using Quasar.IO.Serialization.Json;

using Space.Core;

namespace Quasar.Graphics
{
    /// <summary>
    /// Interger valued color representation (Immutable).
    /// </summary>
    /// <seealso cref="IEquatable{RGBA}" />
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [JsonConverter(typeof(RGBAJsonConverter))]
    public readonly struct RGBA : IEquatable<RGBA>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RGBA"/> struct.
        /// </summary>
        /// <param name="rgba">The RGBA unsigned integer representation.</param>
        public RGBA(uint rgba)
        {
            A = (byte)rgba;
            rgba >>= 8;
            B = (byte)rgba;
            rgba >>= 8;
            G = (byte)rgba;
            rgba >>= 8;
            R = (byte)rgba;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RGBA"/> struct.
        /// </summary>
        /// <param name="rgba">The RGBA integer representation.</param>
        public RGBA(int rgba)
            : this((uint)rgba)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RGBA"/> struct.
        /// </summary>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="alpha">The alpha.</param>
        public RGBA(byte red, byte green, byte blue, byte alpha)
        {
            R = red;
            G = green;
            B = blue;
            A = alpha;
        }


        /// <summary>
        /// The blue component.
        /// </summary>
        public readonly byte B;

        /// <summary>
        /// The green component.
        /// </summary>
        public readonly byte G;

        /// <summary>
        /// The red component.
        /// </summary>
        public readonly byte R;

        /// <summary>
        /// The alpha component.
        /// </summary>
        public readonly byte A;


        /// <summary>
        /// The black color.
        /// </summary>
        public static readonly RGBA Black = new RGBA(0x000000FF);

        /// <summary>
        /// The blue color.
        /// </summary>
        public static readonly RGBA Blue = new RGBA(0x0000FFFF);

        /// <summary>
        /// The green color.
        /// </summary>
        public static readonly RGBA Green = new RGBA(0x00FF00FF);

        /// <summary>
        /// The red color.
        /// </summary>
        public static readonly RGBA Red = new RGBA(0xFF0000FF);

        /// <summary>
        /// The transparent color.
        /// </summary>
        public static readonly RGBA Transparent = new RGBA(0);

        /// <summary>
        /// The white color.
        /// </summary>
        public static readonly RGBA White = new RGBA(0xFFFFFFFF);


        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(RGBA other)
        {
            return R == other.R && G == other.G && B == other.B && A == other.A;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is not RGBA other)
            {
                return false;
            }

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
        /// Calculates the linear interpolated value between a and b colors.
        /// The interpolation parameter is clamped to the [0...1] range.
        /// </summary>
        /// <param name="rgbaA">The a RGBA value.</param>
        /// <param name="rgbaB">The b RGBA value.</param>
        /// <param name="t">The interpolation parameter.</param>
        public static RGBA Lerp(RGBA rgbaA, RGBA rgbaB, float t)
        {
            t = Ranges.FloatUnit.Clamp(t);
            return LerpUnclamped(rgbaA, rgbaB, t);
        }

        /// <summary>
        /// Calculates the linear interpolated value between a and b colors.
        /// </summary>
        /// <param name="rgbaA">The a RGBA value.</param>
        /// <param name="rgbaB">The b RGBA value.</param>
        /// <param name="t">The interpolation parameter.</param>
        public static RGBA LerpUnclamped(RGBA rgbaA, RGBA rgbaB, float t)
        {
            var oneMinusT = 1.0f - t;
            return new RGBA(
                (byte)(rgbaA.R * t + rgbaB.R * oneMinusT),
                (byte)(rgbaA.G * t + rgbaB.G * oneMinusT),
                (byte)(rgbaA.B * t + rgbaB.B * oneMinusT),
                (byte)(rgbaA.A * t + rgbaB.A * oneMinusT));
        }

        /// <summary>
        /// Tries to parse the string value into ARGB.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="result">The result.</param>
        /// <returns>True if parsing was successful; otherwise false.</returns>
        public static bool TryParse(string value, out RGBA result)
        {
            if (UInt32.TryParse(value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var argb))
            {
                result = new RGBA(argb);
                return true;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Converts the RGBA structure into an UInt32 rgba value.
        /// </summary>
        public uint ToRGBA()
        {
            var argb = (uint)R << 8;
            argb |= G;
            argb <<= 8;
            argb |= B;
            argb <<= 8;
            argb |= A;
            return argb;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        public override string ToString()
        {
            var rgba = ToRGBA();
            return rgba.ToString("X8", CultureInfo.InvariantCulture);
        }
    }
}
