//-----------------------------------------------------------------------
// <copyright file="Vector3DJsonConverter.cs" company="Space Development">
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
    /// JSON converter class for Vector3D structure.
    /// </summary>
    /// <seealso cref="VectorJsonConverterBase{Vector3D}" />
    public sealed class Vector3DJsonConverter : VectorJsonConverterBase<Vector3D>
    {
        /// <inheritdoc/>
        protected override Vector3D Deserialize(IReadOnlyList<string> components)
        {
            try
            {
                if (components.Count != 3)
                {
                    return Vector3D.Zero;
                }

                return new Vector3D(
                    Double.Parse(components[0], CultureInfo.InvariantCulture),
                    Double.Parse(components[1], CultureInfo.InvariantCulture),
                    Double.Parse(components[2], CultureInfo.InvariantCulture));
            }
            catch
            {
                return Vector3D.Zero;
            }
        }

        /// <inheritdoc/>
        protected override string Serialize(in Vector3D value)
        {
            return $"{value.X.ToString(CultureInfo.InvariantCulture)}, {value.Y.ToString(CultureInfo.InvariantCulture)}, " +
                $"{value.Z.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}
