//-----------------------------------------------------------------------
// <copyright file="SettingsSerializationEntryJsonConverter.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Quasar.Settings.Internals
{
    /// <summary>
    /// Polymorphic JSON converter for SettingsBase class.
    /// </summary>
    /// <seealso cref="JsonConverter" />
    internal sealed class SettingsSerializationEntryJsonConverter : JsonConverter<SettingsSerializationEntry>
    {
        /// <inheritdoc/>
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(SettingsSerializationEntry).IsAssignableFrom(typeToConvert);
        }


        /// <inheritdoc/>
        public override SettingsSerializationEntry Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string type = null;
            ISettings settings = null;
            Type settingsType = null;
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    continue;
                }

                var propertyName = reader.GetString();
                switch (propertyName)
                {
                    case nameof(SettingsSerializationEntry.TypeName):
                        if (!reader.Read())
                        {
                            break;
                        }

                        type = reader.GetString();
                        settingsType = Type.GetType(type);
                        if (settingsType == null ||
                            !typeof(ISettings).IsAssignableFrom(settingsType))
                        {
                            return default;
                        }

                        break;
                    case nameof(SettingsSerializationEntry.Value):
                        var typeInfo = options.GetTypeInfo(settingsType);
                        settings = (ISettings)JsonSerializer.Deserialize(ref reader, typeInfo);
                        break;
                }
            }

            return new SettingsSerializationEntry(type, settings);
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, SettingsSerializationEntry value, JsonSerializerOptions options)
        {
            if (value.Value == null)
            {
                return;
            }

            writer.WriteStartObject();
            writer.WriteString(nameof(SettingsSerializationEntry.TypeName), value.TypeName);
            writer.WritePropertyName(nameof(SettingsSerializationEntry.Value));
            var settingsValue = value.Value;
            JsonSerializer.Serialize(writer, settingsValue, settingsValue.GetType(), options);
            writer.WriteEndObject();
        }
    }
}
