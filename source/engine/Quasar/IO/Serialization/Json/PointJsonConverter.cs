//-----------------------------------------------------------------------
// <copyright file="PointJsonConverter.cs" company="Space Development">
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

using Quasar.Graphics;

namespace Quasar.IO.Serialization.Json
{
    /// <summary>
    /// JSON converter class for Size structure.
    /// </summary>
    /// <seealso cref="VectorJsonConverterBase{Point}" />
    public sealed class PointJsonConverter : VectorJsonConverterBase<Point>
    {
        /// <inheritdoc/>
        protected override Point Deserialize(IReadOnlyList<string> components)
        {
            try
            {
                switch (components.Count)
                {
                    case 1:
                        return new Point(Int32.Parse(components[0].Trim(), CultureInfo.InvariantCulture));
                    case 2:
                        return new Point(
                            Int32.Parse(components[0].Trim(), CultureInfo.InvariantCulture),
                            Int32.Parse(components[1].Trim(), CultureInfo.InvariantCulture));
                    default:
                        return Point.Empty;
                }
            }
            catch
            {
                return Point.Empty;
            }
        }

        /// <inheritdoc/>
        protected override string Serialize(in Point value)
        {
            return $"{value.X.ToString(CultureInfo.InvariantCulture)}, {value.Y.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}
