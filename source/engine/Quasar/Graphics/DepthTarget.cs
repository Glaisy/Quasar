//-----------------------------------------------------------------------
// <copyright file="DepthTarget.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Graphics
{
    /// <summary>
    /// Frame buffer depth target enumeration.
    /// </summary>
    public enum DepthTarget
    {
        /// <summary>
        /// The not used.
        /// </summary>
        None,

        /// <summary>
        /// The texture target.
        /// </summary>
        Texture,

        /// <summary>
        /// The render buffer target.
        /// </summary>
        RenderBuffer
    }
}
