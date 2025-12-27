//-----------------------------------------------------------------------
// <copyright file="IAssetPackage.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Space.Core.Utilities;

namespace Quasar.Assets
{
    /// <summary>
    /// Represents an asset package which provides access and importing functionality of packed assets.
    /// </summary>
    /// <seealso cref="INameProvider" />
    /// <seealso cref="IDisposable" />
    public interface IAssetPackage : INameProvider, IDisposable
    {
        /// <summary>
        /// Gets the tag.
        /// </summary>
        string Tag { get; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        Version Version { get; }


        /// <summary>
        /// Enumerates the name of all the assets in the package.
        /// </summary>
        IEnumerable<string> EnumerateAssets();

        /// <summary>
        /// Enumerates the name of all custom the assets in the package.
        /// </summary>
        IEnumerable<string> EnumerateCustomAssets();

        /// <summary>
        /// Gets the asset stream by the specified name.
        /// </summary>
        /// <param name="assetName">The asset name.</param>
        /// <returns>The asset stream.</returns>
        Stream GetAssetStream(string assetName);

        /// <summary>
        /// Imports the assets from the package [NOTE: should be executed on the main thread].
        /// </summary>
        void ImportAssets();

        /// <summary>
        /// Imports all Quasar assets asynchronously from the package.
        /// </summary>
        /// <param name="progressUpdate">The progress update.</param>
        Task ImportAssetsAsync(Action<IProgressInformation> progressUpdate = null);
    }
}
