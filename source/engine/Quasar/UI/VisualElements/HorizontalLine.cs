//-----------------------------------------------------------------------
// <copyright file="HorizontalLine.cs" company="Space Development">
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
    /// Horizontal line rendering visual element.
    /// </summary>
    /// <seealso cref="VisualElement" />
    public sealed class HorizontalLine : VisualElement
    {
        /// <inheritdoc/>
        protected override bool IsFocusable => false;

        /// <inheritdoc/>
        protected override string TagSelector => TagSelectors.HorizontalLine;
    }
}
