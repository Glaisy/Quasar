//-----------------------------------------------------------------------
// <copyright file="UITemplateException.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.UI.Templates.Internals
{
    /// <summary>
    /// Exception base class for Quasar UITemplate related exceptions.
    /// </summary>
    /// <seealso cref="UIException" />
    internal sealed class UITemplateException : UIException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UITemplateException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
        public UITemplateException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }
    }
}
