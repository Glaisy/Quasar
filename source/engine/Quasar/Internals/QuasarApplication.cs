//-----------------------------------------------------------------------
// <copyright file="QuasarApplication.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.UI.Internals;

using Space.Core;
using Space.Core.DependencyInjection;

namespace Quasar.Internals
{
    /// <summary>
    /// Quasar application implementation.
    /// </summary>
    /// <seealso cref="DisposableBase" />
    /// <seealso cref="IQuasarApplication" />
    [Export]
    [Singleton]
    internal sealed class QuasarApplication : DisposableBase, IQuasarApplication
    {
        private readonly INativeMessageHandler nativeMessageHandler;
        private readonly INativeWindowFactory nativeWindowFactory;


        /// <summary>
        /// Initializes a new instance of the <see cref="QuasarApplication" /> class.
        /// </summary>
        /// <param name="platformContext">The platform context.</param>
        public QuasarApplication(IPlatformContext platformContext)
        {
            nativeMessageHandler = platformContext.NativeMessageHandler;
            nativeWindowFactory = platformContext.NativeWindowFactory;
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
        }


        /// <inheritdoc/>
        public void Run()
        {
            var applicationWindow = nativeWindowFactory.CreateApplicationWindow();
            while (applicationWindow.Visible)
            {
                nativeMessageHandler.ProcessMessages();
            }
        }
    }
}
