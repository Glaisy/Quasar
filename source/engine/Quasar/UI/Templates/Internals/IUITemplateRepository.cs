//-----------------------------------------------------------------------
// <copyright file="IUITemplateRepository.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.IO;
using System.Reflection;

using Quasar.Collections;
using Quasar.UI.VisualElements;

namespace Quasar.UI.Templates.Internals
{
    /// <summary>
    /// Quasar UI template loader interface definition.
    /// </summary>
    /// <seealso cref="IReadOnlyRepository{String, VisualElement}" />
    internal interface IUITemplateRepository : IReadOnlyRepository<string, UITemplate>
    {
        /// <summary>
        /// Create the UI template from the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="templateId">The template identifier.</param>
        /// <param name="leaveOpen">The leave open flag for the stream.</param>
        void Create(Stream stream, string templateId, bool leaveOpen = false);

        /// <summary>
        /// Creates a templated visual element instance from the specified template identifier.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        TemplatedVisualElementBase Instantiate(string templateId);

        /// <summary>
        /// Registers the visual element types for UI templates from the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        void RegisterVisualElementsForTemplates(Assembly assembly);
    }
}
