//-----------------------------------------------------------------------
// <copyright file="Vector2JsonConverter.cs" company="Space Development">
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

namespace Quasar.IO.Serialization.Json
{
    /// <summary>
    /// JSON converter class for Vector2 structure.
    /// </summary>
    /// <seealso cref="VectorJsonConverterBase{Vector2}" />
    public sealed class Vector2JsonConverter : VectorJsonConverterBase<Vector2>
    {
        /// <inheritdoc/>
        protected override Vector2 Deserialize(IReadOnlyList<string> components)
        {
            try
            {
                if (components.Count != 2)
                {
                    return Vector2.Zero;
                }

                return new Vector2(
                    Single.Parse(components[0], CultureInfo.InvariantCulture),
                    Single.Parse(components[1], CultureInfo.InvariantCulture));
            }
            catch
            {
                return Vector2.Zero;
            }
        }

        /// <inheritdoc/>
        protected override string Serialize(in Vector2 value)
        {
            return $"{value.X.ToString(CultureInfo.InvariantCulture)}, {value.Y.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}
