//-----------------------------------------------------------------------
// <copyright file="RangeInt32Converter.cs" company="Space Development">
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

using Space.Core;

namespace Quasar.IO.Serialization.Json
{
    /// <summary>
    /// JSON converter class for Range{int} structure.
    /// </summary>
    public sealed class RangeInt32Converter : VectorJsonConverterBase<Range<int>>
    {
        /// <inheritdoc/>
        protected override Range<int> Deserialize(IReadOnlyList<string> components)
        {
            try
            {
                if (components.Count != 2)
                {
                    return Ranges.IntZero;
                }

                return new Range<int>(
                    Int32.Parse(components[0].Trim(), CultureInfo.InvariantCulture),
                    Int32.Parse(components[1].Trim(), CultureInfo.InvariantCulture));
            }
            catch
            {
                return Ranges.IntZero;
            }
        }

        /// <inheritdoc/>
        protected override string Serialize(in Range<int> value)
        {
            return $"{value.Minimum.ToString(CultureInfo.InvariantCulture)}, {value.Maximum.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}
