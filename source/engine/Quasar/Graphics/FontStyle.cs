//-----------------------------------------------------------------------
// <copyright file="FontStyle.cs" company="Space Development">
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

namespace Quasar.Graphics
{
    /// <summary>
    /// Font style enumeration.
    /// </summary>
    [Flags]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FontStyle
    {
        /// <summary>
        /// Regular text.
        /// </summary>
        Regular = 0,

        /// <summary>
        /// Bold text.
        /// </summary>
        Bold = 1,

        /// <summary>
        /// Italic text.
        /// </summary>
        Italic = 2,

        /// <summary>
        /// Underlined text.
        /// </summary>
        Underline = 4,

        /// <summary>
        /// Text with a line through the middle.
        /// </summary>
        Strikeout = 8,

        /// <summary>
        /// All text styles.
        /// </summary>
        All = Regular | Bold | Italic | Underline | Strikeout
    }
}
