//-----------------------------------------------------------------------
// <copyright file="ColorJsonConverter.cs" company="Space Development">
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
using System.Text.Json;

using Quasar.Graphics;

using Space.Core.IO.Serialization.Json;

namespace Quasar.IO.Serialization.Json
{
    /// <summary>
    /// JSON converter class for Color structure.
    /// </summary>
    /// <seealso cref="JsonConverterBase{Color}" />
    public class ColorJsonConverter : JsonConverterBase<Color>
    {
        /// <inheritdoc/>
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                var serializedValue = reader.GetString();
                var rgba = UInt32.Parse(serializedValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                return new Color(rgba);
            }
            catch
            {
                return Color.Transparent;
            }
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        {
            var serializedValue = value.ToRGBA().ToString("X8", CultureInfo.InvariantCulture);
            writer.WriteStringValue(serializedValue);
        }
    }
}
