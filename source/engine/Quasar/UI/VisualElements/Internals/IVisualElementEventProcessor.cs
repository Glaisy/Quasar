//-----------------------------------------------------------------------
// <copyright file="IVisualElementEventProcessor.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.UI.VisualElements.Internals
{
    /// <summary>
    /// Represents the visual element event processor component.
    /// </summary>
    internal interface IVisualElementEventProcessor
    {
        /// <summary>
        /// Adds the visual element to the load list.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        void AddToLoadList(VisualElement visualElement);

        /// <summary>
        /// Processes the focus changed event.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        void ProcessFocusChanged(VisualElement visualElement);

        /// <summary>
        /// Processes the root visual element changed event.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        void ProcessRootVisualElementChanged(VisualElement visualElement);

        /// <summary>
        /// Removes the visual element from the load list.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        void RemoveFromLoadList(VisualElement visualElement);
    }
}
