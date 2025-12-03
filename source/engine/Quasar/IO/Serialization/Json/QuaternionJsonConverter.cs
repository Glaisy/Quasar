//-----------------------------------------------------------------------
// <copyright file="QuaternionJsonConverter.cs" company="Space Development">
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
    /// JSON converter class for Quaternion structure.
    /// </summary>
    /// <seealso cref="VectorJsonConverterBase{Quaternion}" />
    public sealed class QuaternionJsonConverter : VectorJsonConverterBase<Quaternion>
    {
        /// <inheritdoc/>
        protected override Quaternion Deserialize(IReadOnlyList<string> components)
        {
            try
            {
                if (components.Count != 4)
                {
                    return Quaternion.Zero;
                }

                return new Quaternion(
                    Single.Parse(components[0].Trim(), CultureInfo.InvariantCulture),
                    Single.Parse(components[1].Trim(), CultureInfo.InvariantCulture),
                    Single.Parse(components[2].Trim(), CultureInfo.InvariantCulture),
                    Single.Parse(components[3].Trim(), CultureInfo.InvariantCulture));
            }
            catch
            {
                return Quaternion.Zero;
            }
        }

        /// <inheritdoc/>
        protected override string Serialize(in Quaternion value)
        {
            return $"{value.X.ToString(CultureInfo.InvariantCulture)}, {value.Y.ToString(CultureInfo.InvariantCulture)}, " +
                $"{value.Z.ToString(CultureInfo.InvariantCulture)}, {value.W.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}
