//-----------------------------------------------------------------------
// <copyright file="SizeJsonConverter.cs" company="Space Development">
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
    /// <seealso cref="VectorJsonConverterBase{Size}" />
    public sealed class SizeJsonConverter : VectorJsonConverterBase<Size>
    {
        /// <inheritdoc/>
        protected override Size Deserialize(IReadOnlyList<string> components)
        {
            try
            {
                switch (components.Count)
                {
                    case 1:
                        return new Size(Int32.Parse(components[0].Trim(), CultureInfo.InvariantCulture));
                    case 2:
                        return new Size(
                            Int32.Parse(components[0].Trim(), CultureInfo.InvariantCulture),
                            Int32.Parse(components[1].Trim(), CultureInfo.InvariantCulture));
                    default:
                        return Size.Empty;
                }
            }
            catch
            {
                return Size.Empty;
            }
        }

        /// <inheritdoc/>
        protected override string Serialize(in Size value)
        {
            return $"{value.Width.ToString(CultureInfo.InvariantCulture)}, {value.Height.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}
