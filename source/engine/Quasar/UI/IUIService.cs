//-----------------------------------------------------------------------
// <copyright file="IUIService.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Reflection;

using Quasar.UI.VisualElements;

namespace Quasar.UI
{
    /// <summary>
    /// Represents the UI service to manipulate the visual element hierarchy.
    /// </summary>
    public interface IUIService
    {
        /// <summary>
        /// Gets or sets the root visual element.
        /// </summary>
        VisualElement RootVisualElement { get; set; }


        /// <summary>
        /// Loads the root visual element from the template path.
        /// </summary>
        /// <param name="templatePath">The template path.</param>
        /// <returns>
        /// The loaded visual elements.
        /// </returns>
        VisualElement Load(string templatePath);

        /// <summary>
        /// Registers the templated visual element types from the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        void RegisterTemplatedVisualElements(Assembly assembly);
    }
}
