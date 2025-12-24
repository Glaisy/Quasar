//-----------------------------------------------------------------------
// <copyright file="IAssetPackageFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.IO;

using Quasar.Utilities;

namespace Quasar.Assets
{
    /// <summary>
    /// Represents a factory component for asset packages.
    /// </summary>
    public interface IAssetPackageFactory
    {
        /// <summary>
        /// Creates an asset package by the specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>
        /// The created asset package instance.
        /// </returns>
        IAssetPackage Create(string filePath);

        /// <summary>
        /// Creates an asset package the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="leaveOpen">The stream is left open when the package is disposed if this flag is true; otherwise stream is disposed.</param>
        /// <returns>
        /// The created asset package instance.
        /// </returns>
        IAssetPackage Create(Stream stream, bool leaveOpen = true);

        /// <summary>
        /// Creates an asset package the specified resource provider and resource path.
        /// </summary>
        /// <param name="resourceProvider">The resource provider.</param>
        /// <param name="resourcePath">The resource path.</param>
        /// <returns>
        /// The created asset package instance.
        /// </returns>
        IAssetPackage Create(IResourceProvider resourceProvider, string resourcePath);
    }
}
