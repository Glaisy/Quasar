//-----------------------------------------------------------------------
// <copyright file="ILayoutManager.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.UI.VisualElements.Layouts.Internals
{
    /// <summary>
    /// UI container control layout manager interface definition.
    /// </summary>
    public interface ILayoutManager
    {
        /// <summary>
        /// Gets the layout type.
        /// </summary>
        LayoutType LayoutType { get; }


        /// <summary>
        /// Arranges the children of the specified visual element.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        void Arrange(VisualElement visualElement);

        /// <summary>
        /// Calculates the preferred bounding box size of the specified visual element.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        Vector2 CalculatePreferredBoundingBoxSize(VisualElement visualElement);
    }
}
