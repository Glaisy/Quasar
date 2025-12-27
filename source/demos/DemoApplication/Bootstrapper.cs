//-----------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar;
using Quasar.UI;

using Space.Core.DependencyInjection;

namespace DemoApplication
{
    /// <summary>
    /// Demo application's bootstrapper implementation.
    /// </summary>
    /// <seealso cref="IBootstrapper" />
    [Export]
    [Singleton]
    internal sealed class Bootstrapper : IBootstrapper
    {
        private readonly IApplicationWindow applicationWindow;
        private readonly IIconRepository iconRepository;
        private readonly ICursorRepository cursorRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="Bootstrapper" /> class.
        /// </summary>
        /// <param name="applicationWindow">The application window.</param>
        /// <param name="iconRepository">The icon repository.</param>
        /// <param name="cursorRepository">The cursor repository.</param>
        public Bootstrapper(
            IApplicationWindow applicationWindow,
            IIconRepository iconRepository,
            ICursorRepository cursorRepository)
        {
            this.applicationWindow = applicationWindow;
            this.iconRepository = iconRepository;
            this.cursorRepository = cursorRepository;
        }

        /// <inheritdoc/>
        public void Execute()
        {
            applicationWindow.Icon = iconRepository.Create("Demo", "./Contents/Logo.png");
            applicationWindow.Cursor = cursorRepository.DefaultCursor;
        }
    }
}
