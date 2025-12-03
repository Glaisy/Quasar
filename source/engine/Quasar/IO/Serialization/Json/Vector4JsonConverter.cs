//-----------------------------------------------------------------------
// <copyright file="Vector4JsonConverter.cs" company="Space Development">
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
    /// JSON converter class for Vector4 structure.
    /// </summary>
    /// <seealso cref="VectorJsonConverterBase{Vector4}" />
    public sealed class Vector4JsonConverter : VectorJsonConverterBase<Vector4>
    {
        /// <inheritdoc/>
        protected override Vector4 Deserialize(IReadOnlyList<string> components)
        {
            try
            {
                if (components.Count != 4)
                {
                    return Vector4.Zero;
                }

                return new Vector4(
                    Single.Parse(components[0].Trim(), CultureInfo.InvariantCulture),
                    Single.Parse(components[1].Trim(), CultureInfo.InvariantCulture),
                    Single.Parse(components[2].Trim(), CultureInfo.InvariantCulture),
                    Single.Parse(components[3].Trim(), CultureInfo.InvariantCulture));
            }
            catch
            {
                return Vector4.Zero;
            }
        }

        /// <inheritdoc/>
        protected override string Serialize(in Vector4 value)
        {
            return $"{value.X.ToString(CultureInfo.InvariantCulture)}, {value.Y.ToString(CultureInfo.InvariantCulture)}, " +
                $"{value.Z.ToString(CultureInfo.InvariantCulture)}, {value.W.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}
