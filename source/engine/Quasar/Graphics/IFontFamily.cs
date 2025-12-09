//-----------------------------------------------------------------------
// <copyright file="IFontFamily.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Graphics
{
    /// <summary>
    /// Font family interface definition.
    /// </summary>
    public interface IFontFamily
    {
        /// <summary>
        /// Gets the <see cref="IFontStyleInformation" /> with the specified font style.
        /// </summary>
        /// <param name="fontStyle">The font style.</param>
        /// <returns>
        /// The font style information.
        /// </returns>
        IFontStyleInformation this[FontStyle fontStyle] { get; }


        /// <summary>
        /// Gets the base size.
        /// </summary>
        float BaseSize { get; }

        /// <summary>
        /// Gets the character count.
        /// </summary>
        int CharacterCount { get; }

        /// <summary>
        /// Gets the character spacing.
        /// </summary>
        float CharacterSpacing { get; }

        /// <summary>
        /// Gets the fallback character.
        /// </summary>
        char FallbackCharacter { get; }

        /// <summary>
        /// Gets the first character.
        /// </summary>
        char FirstCharacter { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }
    }
}