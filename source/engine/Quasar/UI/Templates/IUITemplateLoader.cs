//-----------------------------------------------------------------------
// <copyright file="IUITemplateLoader.cs" company="Space Development">
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
using Quasar.Utilities;

namespace Quasar.UI.Templates
{
    /// <summary>
    /// Quasar UI template loader interface definition.
    /// </summary>
    public interface IUITemplateLoader
    {
        /// <summary>
        /// Loads the visual element by the template path.
        /// </summary>
        /// <param name="templatePath">The template path.</param>
        VisualElement Load(string templatePath);

        /// <summary>
        /// Registers the UI template resources by the resource provider and assembly.
        /// The assembly should contain the custom visual element implementations.
        /// </summary>
        /// <param name="resourceProvider">The resource provider.</param>
        /// <param name="templateBasePath">The template base path.</param>
        /// <param name="assembly">The assembly with the custom visual elements [optional].</param>
        void Register(IResourceProvider resourceProvider, string templateBasePath, Assembly assembly = null);
    }
}
