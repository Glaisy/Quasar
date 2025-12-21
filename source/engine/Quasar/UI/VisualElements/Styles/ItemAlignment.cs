//-----------------------------------------------------------------------
// <copyright file="ItemAlignment.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.UI.VisualElements.Styles
{
    /// <summary>
    /// Item alignment type enumeration.
    /// </summary>
    public enum ItemAlignment
    {
        /// <summary>
        /// Aligning items to the start of the available space (left of bottom).
        /// </summary>
        Start,

        /// <summary>
        /// Aligning items to the center of the available space.
        /// </summary>
        Center,

        /// <summary>
        /// Aligning items to the end of the available space (rignt or top).
        /// </summary>
        End,

        /// <summary>
        /// Aligning items streched into the available space.
        /// </summary>
        Stretched,

        /// <summary>
        /// Aligning items with even space between items.
        /// </summary>
        SpaceBetween
    }
}
