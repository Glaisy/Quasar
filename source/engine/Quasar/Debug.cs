//-----------------------------------------------------------------------
// <copyright file="Debug.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Diagnostics;

using Microsoft.Extensions.DependencyInjection;


#if DEBUG
using Quasar.Diagnostics.Pipeline.Internals;
#endif

namespace Quasar
{
    /// <summary>
    /// Debug text rendering helper class.
    /// </summary>
    public static class Debug
    {
#if DEBUG
        private static DebugTextService debugTextService;
#endif

        /// <summary>
        /// Logs an information text onto the screen.
        /// </summary>
        /// <param name="info">The information text.</param>
        /// <param name="arguments">The arguments.</param>
        [Conditional("DEBUG")]
        public static void Info(string info, params object[] arguments)
        {
#if DEBUG
            if (String.IsNullOrEmpty(info))
            {
                return;
            }

            var message = FormatMessage(info, arguments);
            debugTextService.Add(DebugTextType.Info, message);
#endif
        }

        /// <summary>
        /// Logs an exception and error message onto the screen.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="arguments">The arguments.</param>
        [Conditional("DEBUG")]
        public static void Error(Exception exception, string errorMessage, params object[] arguments)
        {
#if DEBUG
            if (exception == null)
            {
                return;
            }

            errorMessage ??= String.Empty;
            errorMessage = FormatMessage(errorMessage, arguments);
            errorMessage = String.Concat(errorMessage, "\n", exception.ToString());
            debugTextService.Add(DebugTextType.Error, errorMessage);
#endif
        }

        /// <summary>
        /// Logs a warning text onto the screen.
        /// </summary>
        /// <param name="warning">The warning text.</param>
        /// <param name="arguments">The arguments.</param>
        [Conditional("DEBUG")]
        public static void Warning(string warning, params object[] arguments)
        {
#if DEBUG
            if (String.IsNullOrEmpty(warning))
            {
                return;
            }

            var message = FormatMessage(warning, arguments);
            debugTextService.Add(DebugTextType.Warning, message);
#endif
        }


        /// <summary>
        /// Initializes the static services.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        [Conditional("DEBUG")]
        internal static void InitializeStaticServices(IServiceProvider serviceProvider)
        {
#if DEBUG
            debugTextService = serviceProvider.GetRequiredService<DebugTextService>();
            debugTextService.Initialize();
#endif
        }


#if DEBUG
        private static string FormatMessage(string message, object[] arguments)
        {
            if (String.IsNullOrEmpty(message) || arguments.Length == 0)
            {
                return message;
            }

            return String.Format(message, arguments);
        }
#endif
    }
}
