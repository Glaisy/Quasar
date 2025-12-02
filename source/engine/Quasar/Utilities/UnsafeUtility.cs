//-----------------------------------------------------------------------
// <copyright file="UnsafeUtility.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Text;

namespace Quasar.Utilities
{
    /// <summary>
    /// Static method library for unsafe operations.
    /// </summary>
    public static unsafe class UnsafeUtility
    {
        /// <summary>
        /// Gets the string from the byte buffer by the specified endocing (default: ASCII).
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>
        /// The string value.
        /// </returns>
        public static unsafe string GetString(byte* bytes, Encoding encoding = null)
        {
            if (bytes == null)
            {
                return null;
            }

            // determine length
            var length = 0;
            var ptr = bytes;
            while (*ptr++ != 0)
            {
                length++;
            }

            // convert bytes to string
            encoding ??= Encoding.ASCII;
            return encoding.GetString(bytes, length);
        }
    }
}
