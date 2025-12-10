//-----------------------------------------------------------------------
// <copyright file="FontFamilyData.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.FontGenerator.Models
{
    /// <summary>
    /// Represents the font family data structure for the generator.
    /// </summary>
    internal readonly struct FontFamilyData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontFamilyData"/> struct.
        /// </summary>
        /// <param name="baseSize">Size of the base.</param>
        /// <param name="characterCount">The character count.</param>
        public FontFamilyData(int baseSize, int characterCount)
        {
            BaseSize = baseSize;
            CharacterCount = characterCount;
        }

        /// <summary>
        /// The base size.
        /// </summary>
        public readonly int BaseSize;

        /// <summary>
        /// The character count.
        /// </summary>
        public readonly int CharacterCount;
    }
}
