//-----------------------------------------------------------------------
// <copyright file="PointerButton.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Inputs
{
    /// <summary>
    /// Pointer button enumeration.
    /// </summary>
    public enum PointerButton : short
    {
        /// <summary>
        /// The empty pointer button.
        /// </summary>
        None = 0,

        /// <summary>
        /// The left pointer button.
        /// </summary>
        Left = 1,

        /// <summary>
        /// The right pointer button.
        /// </summary>
        Right = 2,

        /// <summary>
        /// The middle pointer button.
        /// </summary>
        Middle = 3,

        /// <summary>
        /// The button count value.
        /// </summary>
        ButtonCount = 3
    }
}
