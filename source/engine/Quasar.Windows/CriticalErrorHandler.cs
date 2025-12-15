//-----------------------------------------------------------------------
// <copyright file="CriticalErrorHandler.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Windows.Forms;

using Space.Core.DependencyInjection;

namespace Quasar.Windows
{
    /// <summary>
    /// Critical error handler implementation for Windows platform.
    /// </summary>
    [Export(typeof(ICriticalErrorHandler))]
    [Singleton]
    internal class CriticalErrorHandler : ICriticalErrorHandler
    {
        /// <inheritdoc/>
        public void Handle(string title, Exception exception)
        {
#if DEBUG
            var errorMessage = exception.ToString();
#else
            var errorMessage = exception.Message;
#endif

            MessageBox.Show(null, errorMessage, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(exception.HResult);
        }
    }
}
