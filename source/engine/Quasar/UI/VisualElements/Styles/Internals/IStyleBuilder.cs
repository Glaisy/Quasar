//-----------------------------------------------------------------------
// <copyright file="IStyleBuilder.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.UI.VisualElements.Themes;

namespace Quasar.UI.VisualElements.Styles.Internals
{
    /// <summary>
    /// Style builder interface defintion.
    /// </summary>
    internal interface IStyleBuilder
    {
        /// <summary>
        /// Copies style and custom properties from the specified source to the target.
        /// </summary>
        /// <param name="target">The target style.</param>
        /// <param name="source">The source style.</param>
        void Copy(Style target, IStyle source);

        /// <summary>
        /// Merges style and custom properties from the specified source into the target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        void Merge(Style target, IStyle source);

        /// <summary>
        /// Updates the style by the specified properties.
        /// </summary>
        /// <param name="style">The style.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="theme">The theme.</param>
        void Update(Style style, StyleProperties properties, ITheme theme);
    }
}