//-----------------------------------------------------------------------
// <copyright file="AssetPackageMetaData.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Text.Json.Serialization;

namespace Quasar.Assets.Internals
{
    /// <summary>
    /// Meta data structure for Quasar asset packages.
    /// </summary>
    internal struct AssetPackageMetaData
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonRequired]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public Version Version { get; set; }
    }
}
