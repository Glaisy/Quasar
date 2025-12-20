//-----------------------------------------------------------------------
// <copyright file="BorderJsonConverter.cs" company="Space Development">
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

using Quasar.UI;

namespace Quasar.IO.Serialization.Json
{
    /// <summary>
    /// JSON converter class for Border structure.
    /// </summary>
    /// <seealso cref="VectorJsonConverterBase{Border}" />
    public class BorderJsonConverter : VectorJsonConverterBase<Border>
    {
        /// <inheritdoc/>
        protected override Border Deserialize(IReadOnlyList<string> components)
        {
            try
            {
                switch (components.Count)
                {
                    case 1:
                        return new Border(Single.Parse(components[0].Trim(), CultureInfo.InvariantCulture));
                    case 2:
                        return new Border(
                            Single.Parse(components[0].Trim(), CultureInfo.InvariantCulture),
                            Single.Parse(components[1].Trim(), CultureInfo.InvariantCulture));
                    case 4:
                        return new Border(
                            Single.Parse(components[0].Trim(), CultureInfo.InvariantCulture),
                            Single.Parse(components[1].Trim(), CultureInfo.InvariantCulture),
                            Single.Parse(components[2].Trim(), CultureInfo.InvariantCulture),
                            Single.Parse(components[3].Trim(), CultureInfo.InvariantCulture));
                    default:
                        return Border.Empty;
                }
            }
            catch
            {
                return Border.Empty;
            }
        }

        /// <inheritdoc/>
        protected override string Serialize(in Border value)
        {
            if (value.Left == value.Right &&
                value.Top == value.Bottom)
            {
                if (value.Left == value.Top)
                {
                    return $"{value.Left.ToString(CultureInfo.InvariantCulture)}";
                }

                return $"{value.Left.ToString(CultureInfo.InvariantCulture)}, {value.Top.ToString(CultureInfo.InvariantCulture)}";
            }

            return $"{value.Left.ToString(CultureInfo.InvariantCulture)}, {value.Top.ToString(CultureInfo.InvariantCulture)}, " +
                $"{value.Right.ToString(CultureInfo.InvariantCulture)}, {value.Bottom.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}
