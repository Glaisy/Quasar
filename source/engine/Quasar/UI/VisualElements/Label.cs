//-----------------------------------------------------------------------
// <copyright file="Label.cs" company="Space Development">
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
    /// Generic label (string literal) rendering visual element.
    /// </summary>
    /// <seealso cref="VisualElement" />
    public class Label : VisualElement
    {
        /// <inheritdoc/>
        protected override string ElementTagSelector => ElementTagSelectors.Label;

        /// <inheritdoc/>
        protected override bool IsFocusable => false;
    }
}
