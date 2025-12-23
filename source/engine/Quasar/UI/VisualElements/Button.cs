//-----------------------------------------------------------------------
// <copyright file="Button.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.UI.VisualElements
{
    /// <summary>
    /// General button implementation.
    /// </summary>
    /// <seealso cref="ButtonBase" />
    public class Button : ButtonBase
    {
        /// <inheritdoc/>
        protected override string TagSelector => TagSelectors.Button;
    }
}
