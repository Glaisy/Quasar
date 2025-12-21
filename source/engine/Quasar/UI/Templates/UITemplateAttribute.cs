//-----------------------------------------------------------------------
// <copyright file="UITemplateAttribute.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.UI.Templates
{
    /// <summary>
    /// UI template attribute.
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class UITemplateAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UITemplateAttribute" /> class.
        /// </summary>
        /// <param name="templatePath">The template path.</param>
        public UITemplateAttribute(string templatePath)
        {
            ArgumentException.ThrowIfNullOrEmpty(templatePath, nameof(templatePath));

            TemplatePath = templatePath;
        }


        /// <summary>
        /// The template path.
        /// </summary>
        public readonly string TemplatePath;
    }
}