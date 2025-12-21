//-----------------------------------------------------------------------
// <copyright file="Alignment.cs" company="Space Development">
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
    /// Alignment type enumeration.
    /// </summary>
    public enum Alignment
    {
        /// <summary>
        /// Aligning content to the start of the available space (left of bottom).
        /// </summary>
        Start,

        /// <summary>
        /// Aligning content to the center of the available space.
        /// </summary>
        Center,

        /// <summary>
        /// Aligning content to the end of the available space (right or top).
        /// </summary>
        End,

        /// <summary>
        /// Content is streched into the available space.
        /// </summary>
        Stretched
    }
}
