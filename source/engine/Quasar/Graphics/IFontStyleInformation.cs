//-----------------------------------------------------------------------
// <copyright file="IFontStyleInformation.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;

using Space.Core;

namespace Quasar.Graphics
{
    /// <summary>
    /// Represents the style information of a Quasar font family.
    /// </summary>
    /// <seealso cref="IIdentifierProvider{FontStyle}" />
    public interface IFontStyleInformation : IIdentifierProvider<FontStyle>
    {
        /// <summary>
        /// Gets the ascent of the font.
        /// </summary>
        float Ascent { get; }

        /// <summary>
        /// Gets the descent of the font.
        /// </summary>
        float Descent { get; }

        /// <summary>
        /// Gets the line spacing of the font.
        /// </summary>
        float LineSpacing { get; }


        /// <summary>
        /// Gets the list of character widths.
        /// </summary>
        internal IReadOnlyList<float> CharacterWidths { get; }

        /// <summary>
        /// Gets the list of texture coordinates.
        /// </summary>
        internal IReadOnlyList<Vector2> TextureCoordinates { get; }
    }
}
