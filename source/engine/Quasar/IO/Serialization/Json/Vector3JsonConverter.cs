//-----------------------------------------------------------------------
// <copyright file="Vector3JsonConverter.cs" company="Space Development">
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
    /// JSON converter class for Vector3 structure.
    /// </summary>
    /// <seealso cref="VectorJsonConverterBase{Vector3}" />
    public sealed class Vector3JsonConverter : VectorJsonConverterBase<Vector3>
    {
        /// <inheritdoc/>
        protected override Vector3 Deserialize(IReadOnlyList<string> components)
        {
            try
            {
                if (components.Count != 3)
                {
                    return Vector3.Zero;
                }

                return new Vector3(
                    Single.Parse(components[0].Trim(), CultureInfo.InvariantCulture),
                    Single.Parse(components[1].Trim(), CultureInfo.InvariantCulture),
                    Single.Parse(components[2].Trim(), CultureInfo.InvariantCulture));
            }
            catch
            {
                return Vector3.Zero;
            }
        }

        /// <inheritdoc/>
        protected override string Serialize(in Vector3 value)
        {
            return $"{value.X.ToString(CultureInfo.InvariantCulture)}, {value.Y.ToString(CultureInfo.InvariantCulture)}, " +
                $"{value.Z.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}
