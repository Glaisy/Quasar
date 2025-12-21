//-----------------------------------------------------------------------
// <copyright file="StyleParserException.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.UI.VisualElements.Styles.Internals.Parsers
{
    /// <summary>
    /// General syle parser exception type.
    /// </summary>
    /// <seealso cref="StyleException" />
    internal sealed class StyleParserException : StyleException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StyleParserException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public StyleParserException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleParserException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="styleSheetUrl">The style sheet URL.</param>
        /// <param name="lineIndex">Index of the line.</param>
        public StyleParserException(string message, string styleSheetUrl, int lineIndex)
            : base(message + $" in '{styleSheetUrl}' at line {lineIndex}.")
        {
        }
    }
}
