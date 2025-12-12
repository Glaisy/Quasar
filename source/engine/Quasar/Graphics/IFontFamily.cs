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

using Space.Core;

namespace Quasar.Graphics
{
    /// <summary>
    /// Represents a FontFamily object.
    /// </summary>
    /// <seealso cref="IIdentifierProvider{String}" />
    public interface IFontFamily : IIdentifierProvider<string>
    {
        /// <summary>
        /// Gets the <see cref="IFontStyleInformation" /> with the specified font style.
        /// </summary>
        /// <param name="fontStyle">The font style.</param>
        IFontStyleInformation this[FontStyle fontStyle] { get; }


        /// <summary>
        /// Gets the base size [pixels].
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
    }
}