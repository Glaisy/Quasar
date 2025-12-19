//-----------------------------------------------------------------------
// <copyright file="RenderModelStateFlags.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Rendering.Internals
{
    /// <summary>
    /// Render model state flag enumeration.
    /// </summary>
    [Flags]
    internal enum RenderModelStateFlags
    {
        /// <summary>
        /// The none flag.
        /// </summary>
        None = 0,

        /// <summary>
        /// The enabled flag.
        /// </summary>
        Enabled = 1,

        /// <summary>
        /// The double sided flag.
        /// </summary>
        DoubleSided = 2,

        /// <summary>
        /// The shared mesh flag.
        /// </summary>
        SharedMesh = 4,

        /// <summary>
        /// The renderable flag.
        /// </summary>
        Renderable = 8
    }
}
