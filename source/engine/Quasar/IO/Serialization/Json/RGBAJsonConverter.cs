//-----------------------------------------------------------------------
// <copyright file="RGBAJsonConverter.cs" company="Space Development">
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
    /// JSON converter for RGBA data structure.
    /// </summary>
    /// <seealso cref="JsonConverterBase{RGBA}" />
    public sealed class RGBAJsonConverter : JsonConverterBase<RGBA>
    {
        /// <inheritdoc/>
        public override RGBA Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                var serializedValue = reader.GetString();
                return new RGBA(UInt32.Parse(serializedValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture));
            }
            catch
            {
                return RGBA.Transparent;
            }
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, RGBA value, JsonSerializerOptions options)
        {
            var serializedValue = value.ToString();
            writer.WriteStringValue(serializedValue);
        }
    }
}
