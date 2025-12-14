//-----------------------------------------------------------------------
// <copyright file="DebugTextEntry.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

#if DEBUG
using Quasar.Graphics;

namespace Quasar.Diagnostics.Pipeline.Internals
{
    /// <summary>
    /// Debug text entry object.
    /// </summary>
    internal sealed class DebugTextEntry
    {
        /// <summary>
        /// The color.
        /// </summary>
        public Color Color;

        /// <summary>
        /// The height.
        /// </summary>
        public float Height;

        /// <summary>
        /// The text.
        /// </summary>
        public string Text;

        /// <summary>
        /// The timestamp.
        /// </summary>
        public float Timestamp;
    }
}
#endif