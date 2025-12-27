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
        /// <param name="templateId">The template identifier.</param>
        public UITemplateAttribute(string templateId)
        {
            ArgumentException.ThrowIfNullOrEmpty(templateId, nameof(templateId));

            TemplateId = templateId;
        }


        /// <summary>
        /// The template identifier.
        /// </summary>
        public readonly string TemplateId;
    }
}