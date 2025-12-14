//-----------------------------------------------------------------------
// <copyright file="DebugTextType.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

#if DEBUG
namespace Quasar.Diagnostics.Pipeline.Internals
{
    /// <summary>
    /// Debug text type enumeration.
    /// </summary>
    internal enum DebugTextType
    {
        /// <summary>
        /// The information debug text type.
        /// </summary>
        Info,

        /// <summary>
        /// The warning debug text type.
        /// </summary>
        Warning,

        /// <summary>
        /// The error debug text type.
        /// </summary>
        Error,
    }
}
#endif