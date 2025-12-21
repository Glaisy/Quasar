//-----------------------------------------------------------------------
// <copyright file="PseudoClass.cs" company="Space Development">
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
    /// Pseudo class enumeration.
    /// </summary>
    public enum PseudoClass
    {
        /// <summary>
        /// The enabled state.
        /// </summary>
        Enabled = 0,

        /// <summary>
        /// The disabled state.
        /// </summary>
        Disabled = 1,

        /// <summary>
        /// The hover state.
        /// </summary>
        Hover = 2,

        /// <summary>
        /// The active state.
        /// </summary>
        Active = 3,

        /// <summary>
        /// The checked state.
        /// </summary>
        Checked = 4,

        /// <summary>
        /// The default state (alias for Enabled state).
        /// </summary>
        Default = Enabled
    }
}
