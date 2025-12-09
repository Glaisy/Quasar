//-----------------------------------------------------------------------
// <copyright file="FontStyleExtensions.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.FontGenerator.Extensions
{
    /// <summary>
    /// Extension methods for System.Drawing.FontStyle enumeration.
    /// </summary>
    internal static class FontStyleExtensions
    {
        /// <summary>
        /// Converts System.Drawing font style value to Quasar.Graphics.FontStyle value.
        /// </summary>
        /// <param name="fontStyle">The font style.</param>
        public static Graphics.FontStyle ToFontStyle(this System.Drawing.FontStyle fontStyle)
        {
            return (Graphics.FontStyle)(int)fontStyle;
        }
    }
}
