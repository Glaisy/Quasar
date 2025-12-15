//-----------------------------------------------------------------------
// <copyright file="ICriticalErrorHandler.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar
{
    /// <summary>
    /// Represents a critical error handler.
    /// This kind of errors can happen during application startup/shutdown process or
    /// by catching unhandled exceptions.
    /// </summary>
    public interface ICriticalErrorHandler
    {
        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="exception">The exception.</param>
        void Handle(string title, Exception exception);
    }
}
