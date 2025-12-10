//-----------------------------------------------------------------------
// <copyright file="FontExtensions.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Drawing;

namespace Quasar.FontGenerator.Extensions
{
    /// <summary>
    /// Extension methods for System.Drawing.Font enumeration.
    /// </summary>
    internal static class FontExtensions
    {
        /// <summary>
        /// Converts the em value to pixels.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <param name="emFontHeight">The font's em height value.</param>
        /// <param name="emValue">The em value.</param>
        public static int ConvertEmToPixels(this Font font, int emFontHeight, int emValue)
        {
            switch (font.Unit)
            {
                case GraphicsUnit.Point:
                case GraphicsUnit.Pixel:
                    return (int)MathF.Ceiling(font.Size * emValue / emFontHeight);

                default:
                    throw new NotSupportedException($"GraphicsUnit '{font.Unit}' is not supported yet.");
            }
        }
    }
}
