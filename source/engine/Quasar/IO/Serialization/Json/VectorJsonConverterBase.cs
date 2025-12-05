//-----------------------------------------------------------------------
// <copyright file="VectorJsonConverterBase.cs" company="Space Development">
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
using System.Text.Json;

using Microsoft.Extensions.DependencyInjection;

using Quasar.Utilities;

using Space.Core.Extensions;
using Space.Core.IO.Serialization.Json;

namespace Quasar.IO.Serialization.Json
{
    /// <summary>
    /// Abstract base class for Vector/Range JSON converters.
    /// </summary>
    /// <typeparam name="T">The converted data type.</typeparam>
    /// <seealso cref="JsonConverterBase{T}" />
    public abstract class VectorJsonConverterBase<T> : JsonConverterBase<T>
    {
        private static readonly IReadOnlyList<string> emptyComponents = Array.Empty<string>();


        /// <inheritdoc/>
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            List<string> components = null;
            try
            {
                var serializedValue = reader.GetString();
                if (String.IsNullOrEmpty(serializedValue))
                {
                    // default value if not exists in the JSON stream
                    return Deserialize(emptyComponents);
                }

                // deserialize from components
                components = StringOperationContext.ListPool.Allocate();
                serializedValue.Split(components, Separator, StringSplitOptions.None);
                return Deserialize(components);
            }
            finally
            {
                StringOperationContext.ListPool.Release(components);
            }
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            var serializedValue = Serialize(value);
            writer.WriteStringValue(serializedValue);
        }


        /// <summary>
        /// Gets the separator.
        /// </summary>
        protected virtual char Separator => ',';

        private static IStringOperationContext stringOperationContext;
        /// <summary>
        /// Gets the string operation context.
        /// </summary>
        protected IStringOperationContext StringOperationContext
        {
            get
            {
                stringOperationContext ??= ServiceProvider.GetRequiredService<IStringOperationContext>();
                return stringOperationContext;
            }
        }


        /// <summary>
        /// Deserializes the value from the components.
        /// </summary>
        /// <param name="components">The components.</param>
        protected abstract T Deserialize(IReadOnlyList<string> components);

        /// <summary>
        /// Serializes the specified value to string.
        /// </summary>
        /// <param name="value">The value.</param>
        protected abstract string Serialize(in T value);
    }
}
