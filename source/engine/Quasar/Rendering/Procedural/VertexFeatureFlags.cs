//-----------------------------------------------------------------------
// <copyright file="VertexFeatureFlags.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Rendering.Procedural
{
    /// <summary>
    /// Vertex feature flags enumeration type.
    /// </summary>
    [Flags]
    public enum VertexFeatureFlags
    {
        /// <summary>
        /// The none flag.
        /// </summary>
        None = 0,

        /// <summary>
        /// The normal flag.
        /// </summary>
        Normal = 1,

        /// <summary>
        /// The tangent flag.
        /// </summary>
        Tangent = 2,

        /// <summary>
        /// All flags.
        /// </summary>
        All = Normal | Tangent
    }
}
