//-----------------------------------------------------------------------
// <copyright file="CameraClearType.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Rendering
{
    /// <summary>
    /// Camera clear type enumeration.
    /// </summary>
    public enum CameraClearType
    {
        /// <summary>
        /// The none. No clear at all.
        /// </summary>
        None,

        /// <summary>
        /// The depth clear type.
        /// </summary>
        Depth,

        /// <summary>
        /// The solid color clear type.
        /// </summary>
        SolidColor,

        /// <summary>
        /// The skybox clear type.
        /// </summary>
        Skybox
    }
}
