//-----------------------------------------------------------------------
// <copyright file="FontMeasurementData.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace Quasar.FontGenerator.Models
{
    /// <summary>
    /// Represents the font measurement data structure for the generator.
    /// </summary>
    internal readonly struct FontMeasurementData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontMeasurementData" /> struct.
        /// </summary>
        /// <param name="widths">The widths.</param>
        /// <param name="maxWidth">The maximum width.</param>
        /// <param name="maxHeight">The maximum height.</param>
        public FontMeasurementData(List<float> widths, float maxWidth, float maxHeight)
        {
            Widths = widths;
            MaxWidth = maxWidth;
            MaxHeight = maxHeight;
        }


        /// <summary>
        /// The maximum width of glyphs in pixels.
        /// </summary>
        public readonly float MaxWidth;

        /// <summary>
        /// The maximum height of glyphs in pixels.
        /// </summary>
        public readonly float MaxHeight;

        /// <summary>
        /// The widths of glyphs.
        /// </summary>
        public readonly List<float> Widths;
    }
}
