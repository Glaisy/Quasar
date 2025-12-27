//-----------------------------------------------------------------------
// <copyright file="IFontFamilyRepository.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;

using Quasar.Collections;
using Quasar.Utilities;

namespace Quasar.Graphics
{
    /// <summary>
    /// Represents an repository object for Font families.
    /// </summary>
    /// <seealso cref="IReadOnlyRepository{String,IFontFamily}" />
    public interface IFontFamilyRepository : IReadOnlyRepository<string, IFontFamily>
    {
        /// <summary>
        /// Gets the list of font families.
        /// </summary>
        /// <returns>The list of font families.</returns>
        List<IFontFamily> List();

        /// <summary>
        /// Creates the font family from the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>
        /// The loaded font family.
        /// </returns>
        IFontFamily Create(Stream stream);

        /// <summary>
        /// Loads the font family by the specified resource path.
        /// </summary>
        /// <param name="resourceProvider">The resource provider.</param>
        /// <param name="resourcePath">The resource path.</param>
        /// <returns>
        /// The loaded font family.
        /// </returns>
        IFontFamily Create(IResourceProvider resourceProvider, string resourcePath);


        /// <summary>
        /// Validates the built-in assets.
        /// </summary>
        internal void ValidateBuiltInAssets();
    }
}
