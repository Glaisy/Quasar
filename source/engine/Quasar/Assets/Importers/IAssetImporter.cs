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

using System;
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
        /// Begins the importing process of the asset from the specified stream.
        /// The completed event handler action is called when the importing is finished.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="id">The asset identifier.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="onCompleted">The completed event handler action.</param>
        void BeginImport(Stream stream, string id, string tag, Action<string, bool> onCompleted);
    }
}
