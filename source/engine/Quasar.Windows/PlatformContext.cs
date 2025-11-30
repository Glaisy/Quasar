//-----------------------------------------------------------------------
// <copyright file="PlatformContext.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Internals;
using Quasar.UI.Internals;
using Quasar.Windows.UI;

using Space.Core.DependencyInjection;

namespace Quasar.Windows
{
    /// <summary>
    /// Windows specific platform context implementation.
    /// </summary>
    /// <seealso cref="IPlatformContext" />
    [Export(typeof(IPlatformContext))]
    [Singleton]
    internal sealed class PlatformContext : IPlatformContext
    {
        /// <inheritdoc/>
        public INativeMessageHandler NativeMessageHandler { get; } = new NativeMessageHandler();

        /// <inheritdoc/>
        public INativeWindowFactory NativeWindowFactory { get; } = new NativeWindowFactory();
    }
}
