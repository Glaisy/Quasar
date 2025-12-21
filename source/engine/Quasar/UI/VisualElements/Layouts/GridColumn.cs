//-----------------------------------------------------------------------
// <copyright file="GridColumn.cs" company="Space Development">
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
    /// Represents the column information for grid layouts.
    /// </summary>
    public sealed class GridColumn
    {
        /// <summary>
        /// Gets the defined width.
        /// </summary>
        public GridLength DefinedWidth { get; internal set; }

        /// <summary>
        /// Gets the width.
        /// </summary>
        public float Width { get; internal set; }


        /// <summary>
        /// Gets or sets the estimated width.
        /// </summary>
        internal float EstimatedWidth { get; set; }

        /// <summary>
        /// Gets or sets the left side pixel value.
        /// </summary>
        internal float Left { get; set; }

        /// <summary>
        /// Gets or sets the right side pixel value.
        /// </summary>
        internal float Right { get; set; }


        /// <summary>
        /// Clears the properties.
        /// </summary>
        internal void Clear()
        {
            DefinedWidth = default;
            EstimatedWidth = Width = Left = Right = 0.0f;
        }
    }
}
