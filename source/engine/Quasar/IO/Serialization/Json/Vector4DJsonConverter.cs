//-----------------------------------------------------------------------
// <copyright file="Vector4DJsonConverter.cs" company="Space Development">
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
    /// JSON converter class for Vector4D structure.
    /// </summary>
    /// <seealso cref="VectorJsonConverterBase{Vector4D}" />
    public sealed class Vector4DJsonConverter : VectorJsonConverterBase<Vector4D>
    {
        /// <inheritdoc/>
        protected override Vector4D Deserialize(IReadOnlyList<string> components)
        {
            try
            {
                if (components.Count != 4)
                {
                    return Vector4D.Zero;
                }

                return new Vector4D(
                    Double.Parse(components[0], CultureInfo.InvariantCulture),
                    Double.Parse(components[1], CultureInfo.InvariantCulture),
                    Double.Parse(components[2], CultureInfo.InvariantCulture),
                    Double.Parse(components[3], CultureInfo.InvariantCulture));
            }
            catch
            {
                return Vector4D.Zero;
            }
        }

        /// <inheritdoc/>
        protected override string Serialize(in Vector4D value)
        {
            return $"{value.X.ToString(CultureInfo.InvariantCulture)}, {value.Y.ToString(CultureInfo.InvariantCulture)}, " +
                $"{value.Z.ToString(CultureInfo.InvariantCulture)}, {value.W.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}
