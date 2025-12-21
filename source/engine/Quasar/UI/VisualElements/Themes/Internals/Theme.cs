//-----------------------------------------------------------------------
// <copyright file="Theme.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Quasar.UI.VisualElements.Styles;
using Quasar.UI.VisualElements.Styles.Internals;

using Space.Core;

namespace Quasar.UI.VisualElements.Themes.Internals
{
    /// <summary>
    /// UI theme object implementation.
    /// </summary>
    /// <seealso cref="ITheme" />
    internal sealed class Theme : ITheme
    {
        private readonly Style rootStyle;
        private readonly Dictionary<StyleKey, Style> styles = new Dictionary<StyleKey, Style>();
        private readonly Dictionary<string, Style> stylesByRawSelector = new Dictionary<string, Style>();


        /// <summary>
        /// Initializes a new instance of the <see cref="Theme" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="rootStyle">The root style.</param>
        public Theme(string id, string name, Style rootStyle)
        {
            Assertion.ThrowIfNullOrEmpty(id, nameof(id));

            Id = id;
            Name = String.IsNullOrEmpty(name) ? id : name;

            this.rootStyle = rootStyle;
            stylesByRawSelector.Add(rootStyle.Selector, rootStyle);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Theme" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="baseTheme">The base theme.</param>
        /// <param name="styleFactory">The style factory.</param>
        public Theme(string id, string name, Theme baseTheme, IStyleFactory styleFactory)
        {
            Assertion.ThrowIfNullOrEmpty(id, nameof(id));

            Id = id;
            Name = String.IsNullOrEmpty(name) ? id : name;

            rootStyle = styleFactory.Create(baseTheme.rootStyle.Selector, baseTheme.rootStyle);
            stylesByRawSelector.Add(rootStyle.Selector, rootStyle);

            foreach (var styleKeyValue in baseTheme.styles)
            {
                var baseStyle = styleKeyValue.Value;
                var style = styleFactory.Create(baseStyle.Selector, baseStyle);

                styles.Add(styleKeyValue.Key, style);
                stylesByRawSelector.Add(style.Selector, style);
            }
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the root style.
        /// </summary>
        public IStyle RootStyle => rootStyle;


        /// <inheritdoc/>
        public IStyle GetStyleByClass(string selector, PseudoClass pseudoClass)
        {
            ArgumentException.ThrowIfNullOrEmpty(selector, nameof(selector));

            var key = new StyleKey(StyleSelectorType.Class, selector, pseudoClass);
            styles.TryGetValue(key, out var style);
            return style;
        }

        /// <inheritdoc/>
        public IStyle GetStyleByName(string selector, PseudoClass pseudoClass)
        {
            ArgumentException.ThrowIfNullOrEmpty(selector, nameof(selector));

            var key = new StyleKey(StyleSelectorType.Name, selector, pseudoClass);
            styles.TryGetValue(key, out var style);
            return style;
        }

        /// <inheritdoc/>
        public IStyle GetStyleByTag(string selector, PseudoClass pseudoClass)
        {
            ArgumentException.ThrowIfNullOrEmpty(selector, nameof(selector));

            var key = new StyleKey(StyleSelectorType.Tag, selector, pseudoClass);
            styles.TryGetValue(key, out var style);
            return style;
        }

        /// <summary>
        /// Adds the style to the theme.
        /// </summary>
        /// <param name="style">The style.</param>
        internal void AddStyle(Style style)
        {
            var selector = style.Selector;

            // add by raw selector
            stylesByRawSelector.Add(selector, style);

            // determine style selector type and selector start index
            var startIndex = 0;
            var selectorType = StyleSelectorType.Tag;
            if (selector[0] == StyleConstants.ClassPrefix)
            {
                startIndex = 1;
                selectorType = StyleSelectorType.Class;
            }
            else if (selector[0] == StyleConstants.NamePrefix)
            {
                startIndex = 1;
                selectorType = StyleSelectorType.Name;
            }

            // parse pseudo class
            var pseudoClass = PseudoClass.Default;
            var endIndex = selector.IndexOf(':', 1);
            if (endIndex < 0 || endIndex >= selector.Length - 1)
            {
                endIndex = selector.Length;
            }
            else
            {
                var pseudoClassSpan = selector.AsSpan().Slice(endIndex + 1);
                if (!Enum.TryParse(pseudoClassSpan, true, out pseudoClass))
                {
                    pseudoClass = PseudoClass.Default;
                }
            }

            // get final selector value
            if (startIndex > 0 || endIndex < selector.Length)
            {
                selector = selector.Substring(startIndex, endIndex - startIndex);
            }

            // add style
            var styleKey = new StyleKey(selectorType, selector, pseudoClass);
            styles.Add(styleKey, style);
        }

        /// <summary>
        /// Tries to get the style by the specified selector.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="style">The style.</param>
        /// <returns>True if the style exists; otherwise false.</returns>
        internal bool TryGetStyle(string selector, out Style style)
        {
            return stylesByRawSelector.TryGetValue(selector, out style);
        }
    }
}
