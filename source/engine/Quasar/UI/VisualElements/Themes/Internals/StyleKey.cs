//-----------------------------------------------------------------------
// <copyright file="StyleKey.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Quasar.UI.VisualElements.Styles;

namespace Quasar.UI.VisualElements.Themes.Internals
{
    /// <summary>
    /// Style key data structure.
    /// </summary>
    /// <seealso cref="IEquatable{StyleKey}" />
    internal readonly struct StyleKey : IEquatable<StyleKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StyleKey"/> struct.
        /// </summary>
        /// <param name="selectorType">Type of the selector.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="pseudoClass">The pseudo class.</param>
        public StyleKey(StyleSelectorType selectorType, string selector, PseudoClass pseudoClass = PseudoClass.Default)
        {
            SelectorType = selectorType;
            Selector = selector;
            PseudoClass = pseudoClass;
        }


        /// <summary>
        /// The pseudo class.
        /// </summary>
        public readonly PseudoClass PseudoClass;

        /// <summary>
        /// The selector.
        /// </summary>
        public readonly string Selector;

        /// <summary>
        /// The selector type.
        /// </summary>
        public readonly StyleSelectorType SelectorType;


        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <inheritdoc/>
        public bool Equals(StyleKey other)
        {
            return SelectorType == other.SelectorType && Selector == other.Selector && PseudoClass == other.PseudoClass;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(SelectorType, Selector, PseudoClass);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[{SelectorType},{Selector},{PseudoClass}]";
        }
    }
}
