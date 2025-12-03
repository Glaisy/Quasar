//-----------------------------------------------------------------------
// <copyright file="QuaternionDJsonConverter.cs" company="Space Development">
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
    /// JSON converter class for QuaternionD structure.
    /// </summary>
    /// <seealso cref="VectorJsonConverterBase{QuaternionD}" />
    public sealed class QuaternionDJsonConverter : VectorJsonConverterBase<QuaternionD>
    {
        /// <inheritdoc/>
        protected override QuaternionD Deserialize(IReadOnlyList<string> components)
        {
            try
            {
                if (components.Count != 4)
                {
                    return QuaternionD.Zero;
                }

                return new QuaternionD(
                    Double.Parse(components[0], CultureInfo.InvariantCulture),
                    Double.Parse(components[1], CultureInfo.InvariantCulture),
                    Double.Parse(components[2], CultureInfo.InvariantCulture),
                    Double.Parse(components[3], CultureInfo.InvariantCulture));
            }
            catch
            {
                return QuaternionD.Zero;
            }
        }

        /// <inheritdoc/>
        protected override string Serialize(in QuaternionD value)
        {
            return $"{value.X.ToString(CultureInfo.InvariantCulture)}, {value.Y.ToString(CultureInfo.InvariantCulture)}, " +
                $"{value.Z.ToString(CultureInfo.InvariantCulture)}, {value.W.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}
