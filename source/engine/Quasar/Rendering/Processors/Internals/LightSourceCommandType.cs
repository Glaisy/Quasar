//-----------------------------------------------------------------------
// <copyright file="LightSourceCommandType.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Rendering.Processors.Internals
{
    /// <summary>
    /// Light source command type enumeration.
    /// </summary>
    internal enum LightSourceCommandType
    {
        /// <summary>
        /// The create command.
        /// </summary>
        Create,

        /// <summary>
        /// The enabled changed command.
        /// </summary>
        EnabledChanged
    }
}
