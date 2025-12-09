//-----------------------------------------------------------------------
// <copyright file="SettingsSerializationEntry.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Text.Json.Serialization;

using Space.Core.Settings;

namespace Quasar.Settings.Internals
{
    /// <summary>
    /// Serialization structure for settings entry objects.
    /// </summary>
    [JsonConverter(typeof(SettingsSerializationEntryJsonConverter))]
    internal struct SettingsSerializationEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsSerializationEntry"/> struct.
        /// </summary>
        public SettingsSerializationEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsSerializationEntry" /> struct.
        /// </summary>
        /// <param name="typeName">The type name.</param>
        /// <param name="value">The settings value.</param>
        public SettingsSerializationEntry(string typeName, ISettings value)
        {
            TypeName = typeName;
            Value = value;
        }


        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [JsonPropertyOrder(0)]
        public string TypeName { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonPropertyOrder(1)]
        public ISettings Value { get; set; }
    }
}
