//-----------------------------------------------------------------------
// <copyright file="StyleFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.UI.VisualElements.Internals;
using Quasar.UI.VisualElements.Themes;

using Space.Core;
using Space.Core.Collections;
using Space.Core.DependencyInjection;

namespace Quasar.UI.VisualElements.Styles.Internals
{
    /// <summary>
    /// Style factory implementation.
    /// </summary>
    /// <seealso cref="IStyleFactory" />
    [Export(typeof(IStyleFactory))]
    [Singleton]
    internal sealed class StyleFactory : IStyleFactory
    {
        private readonly IStyleBuilder styleBuilder;
        private readonly IPool<Style> stylePool;


        /// <summary>
        /// Initializes a new instance of the <see cref="StyleFactory" /> class.
        /// </summary>
        /// <param name="visualElementContext">The visual element context.</param>
        /// <param name="styleBuilder">The style builder.</param>
        public StyleFactory(
            VisualElementContext visualElementContext,
            IStyleBuilder styleBuilder)
        {
            this.styleBuilder = styleBuilder;
            stylePool = visualElementContext.StylePool;
        }


        /// <summary>
        /// Create and initializes a style instance by the specified selector and inherited style.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="inheritedStyle">The inherited style.</param>
        public Style Create(string selector, IStyle inheritedStyle)
        {
            Assertion.ThrowIfNullOrEmpty(selector, nameof(selector));

            return CreateInternal(selector, inheritedStyle);
        }

        /// <summary>
        /// Creates an inline style instance by the specifed properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <param name="theme">The theme.</param>
        public Style CreateInline(StyleProperties properties, ITheme theme)
        {
            var style = CreateInternal(null, null);
            styleBuilder.Update(style, properties, theme);

            return style;
        }

        /// <summary>
        /// Creates and initializes a root style instance.
        /// </summary>
        public Style CreateRoot()
        {
            return CreateInternal(StyleConstants.RootStyleName, null);
        }

        /// <summary>
        /// Releases the specified style instance for later re-use.
        /// </summary>
        /// <param name="style">The style.</param>
        public void Release(Style style)
        {
            Assertion.ThrowIfNull(style, nameof(style));
            stylePool.Release(style);
        }


        private Style CreateInternal(string selector, IStyle inheritedStyle)
        {
            Style style = null;
            try
            {
                style = stylePool.Allocate();
                style.Selector = selector;
                if (inheritedStyle != null)
                {
                    styleBuilder.Copy(style, inheritedStyle);
                }

                return style;
            }
            catch
            {
                stylePool.Release(style);

                throw;
            }
        }
    }
}
