//-----------------------------------------------------------------------
// <copyright file="AssetImportState.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Space.Core;

namespace Quasar.Assets.Importers.Internals
{
    /// <summary>
    /// Internal data structure to hold temporary asset importing state.
    /// </summary>
    /// <typeparam name="T">The custom asset data type.</typeparam>
    internal readonly struct AssetImportState<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetImportState{T}"/> struct.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="assetData">The asset data.</param>
        /// <param name="onCompleted">The completed event handler.</param>
        /// <param name="tag">The tag.</param>
        public AssetImportState(string id, T assetData, Action<string, bool> onCompleted, string tag)
        {
            Assertion.ThrowIfNullOrEmpty(id, nameof(id));
            Assertion.ThrowIfNull(onCompleted, nameof(onCompleted));

            Id = id;
            AssetData = assetData;
            OnCompleted = onCompleted;
            Tag = tag;
        }

        /// <summary>
        /// The asset data.
        /// </summary>
        public readonly T AssetData;

        /// <summary>
        /// The identifier.
        /// </summary>
        public readonly string Id;

        /// <summary>
        /// The completed event handler.
        /// </summary>
        public readonly Action<string, bool> OnCompleted;

        /// <summary>
        /// The tag.
        /// </summary>
        public readonly string Tag;
    }
}
