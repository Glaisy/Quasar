//-----------------------------------------------------------------------
// <copyright file="IPlatformContext.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.UI.Internals;

namespace Quasar.Internals
{
    /// <summary>
    /// Represents an operating system platform sepcific context object.
    /// </summary>
    internal interface IPlatformContext
    {
        /// <summary>
        /// Gets the native message handler.
        /// </summary>
        INativeMessageHandler NativeMessageHandler { get; }

        /// <summary>
        /// Gets the native window factory.
        /// </summary>
        INativeWindowFactory NativeWindowFactory { get; }
    }
}
