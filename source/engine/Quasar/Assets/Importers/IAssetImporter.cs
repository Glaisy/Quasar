//-----------------------------------------------------------------------
// <copyright file="IAssetImporter.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.IO;

namespace Quasar.Assets.Importers
{
    /// <summary>
    /// Represents an asset importer for the built-in asset types.
    /// </summary>
    public interface IAssetImporter
    {
        /// <summary>
        /// Gets the imported asset type.
        /// </summary>
        AssetType Type { get; }


        /// <summary>
        /// Executes the importing process of an asset from the specified stream.
        /// </summary>
        /// <param name="id">The asset identifier.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="stream">The stream.</param>
        void Import(string id, string tag, Stream stream);
    }
}
