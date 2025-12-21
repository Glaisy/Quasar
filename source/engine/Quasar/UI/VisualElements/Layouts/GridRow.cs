//-----------------------------------------------------------------------
// <copyright file="GridRow.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.UI.VisualElements.Layouts
{
    /// <summary>
    /// Represents the row information for grid layouts.
    /// </summary>
    public sealed class GridRow
    {
        /// <summary>
        /// Gets the defined height.
        /// </summary>
        public GridLength DefinedHeight { get; internal set; }

        /// <summary>
        /// Gets the height.
        /// </summary>
        public float Height { get; internal set; }


        /// <summary>
        /// Gets or sets the bottom side pixel value.
        /// </summary>
        internal float Bottom { get; set; }

        /// <summary>
        /// Gets or sets the estimated height.
        /// </summary>
        internal float EstimatedHeight { get; set; }

        /// <summary>
        /// Gets or sets the top side pixel value.
        /// </summary>
        internal float Top { get; set; }


        /// <summary>
        /// Clears the properties.
        /// </summary>
        internal void Clear()
        {
            DefinedHeight = default;
            EstimatedHeight = Height = Bottom = Top = 0.0f;
        }
    }
}
