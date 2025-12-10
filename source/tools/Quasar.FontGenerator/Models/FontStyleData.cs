//-----------------------------------------------------------------------
// <copyright file="FontStyleData.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;

using Quasar.Graphics;

namespace Quasar.FontGenerator.Models
{
    /// <summary>
    /// Font style data structure.
    /// </summary>
    internal struct FontStyleData
    {
        /// <summary>
        /// The ascent value.
        /// </summary>
        public int Ascent;

        /// <summary>
        /// The cell distance.
        /// </summary>
        public Size CellDistance;

        /// <summary>
        /// The cell size.
        /// </summary>
        public Size CellSize;

        /// <summary>
        /// The characters widths.
        /// </summary>
        public List<float> CharacterWidths;

        /// <summary>
        /// The column count.
        /// </summary>
        public int ColumnCount;

        /// <summary>
        /// The descent value.
        /// </summary>
        public int Descent;

        /// <summary>
        /// The line spacing value.
        /// </summary>
        public int LineSpacing;

        /// <summary>
        /// The texture size.
        /// </summary>
        public Size TextureSize;
    }
}
