//-----------------------------------------------------------------------
// <copyright file="IUIProvider.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.UI.VisualElements;

namespace Quasar.UI
{
    /// <summary>
    /// Represents the UI provider component to access to root visual element.
    /// </summary>
    public interface IUIProvider
    {
        /// <summary>
        /// Gets the root visual element.
        /// </summary>
        VisualElement RootVisualElement { get; }
    }
}
