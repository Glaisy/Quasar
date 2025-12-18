//-----------------------------------------------------------------------
// <copyright file="RenderModelCommandType.cs" company="Space Development">
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
    /// Render model command type enumeration.
    /// </summary>
    internal enum RenderModelCommandType
    {
        /// <summary>
        /// The create command.
        /// </summary>
        Create,

        /// <summary>
        /// The DoubleSided property changed command.
        /// </summary>
        DoubleSidedChanged,

        /// <summary>
        /// The Enabled property changed command.
        /// </summary>
        EnabledChanged,

        /// <summary>
        /// The Layer property changed command.
        /// </summary>
        LayerChanged,

        /// <summary>
        /// The Mesh property changed command.
        /// </summary>
        MeshChanged,

        /// <summary>
        /// The shader changed command.
        /// </summary>
        ShaderChanged
    }
}
