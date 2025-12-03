//-----------------------------------------------------------------------
// <copyright file="Vector2DJsonConverter.cs" company="Space Development">
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
    /// JSON converter class for Vector2D structure.
    /// </summary>
    /// <seealso cref="VectorJsonConverterBase{Vector2D}" />
    public sealed class Vector2DJsonConverter : VectorJsonConverterBase<Vector2D>
    {
        /// <inheritdoc/>
        protected override Vector2D Deserialize(IReadOnlyList<string> components)
        {
            try
            {
                if (components.Count != 2)
                {
                    return Vector2D.Zero;
                }

                return new Vector2D(
                    Double.Parse(components[0], CultureInfo.InvariantCulture),
                    Double.Parse(components[1], CultureInfo.InvariantCulture));
            }
            catch
            {
                return Vector2D.Zero;
            }
        }

        /// <inheritdoc/>
        protected override string Serialize(in Vector2D value)
        {
            return $"{value.X.ToString(CultureInfo.InvariantCulture)}, {value.Y.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}
