//-----------------------------------------------------------------------
// <copyright file="ApplicationWindowType.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.UI
{
    /// <summary>
    /// Application window type enumeration.
    /// </summary>
    public enum ApplicationWindowType
    {
        /// <summary>
        /// The default window type (has border, non-resizable).
        /// </summary>
        Default,

        /// <summary>
        /// The resizable window type (has border, resizable).
        /// </summary>
        Resizable,

        /// <summary>
        /// The borderless window type (no border, non-resizable).
        /// </summary>
        Borderless
    }
}
