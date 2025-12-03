//-----------------------------------------------------------------------
// <copyright file="GraphicsException.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Graphics
{
    /// <summary>
    /// Base class for graphics related exceptions.
    /// </summary>
    /// <seealso cref="Exception" />
    public class GraphicsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
        public GraphicsException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }
    }
}
