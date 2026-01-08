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

using System;
using System.IO;
using System.Reflection;

using Quasar;
using Quasar.Assets;
using Quasar.UI;
using Quasar.Utilities;

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
        private readonly IResourceProvider resourceProvider;
        private readonly IAssetPackageFactory assetPackageFactory;
        private readonly IUIService uiService;


        /// <summary>
        /// Initializes a new instance of the <see cref="Bootstrapper" /> class.
        /// </summary>
        /// <param name="applicationWindow">The application window.</param>
        /// <param name="iconRepository">The icon repository.</param>
        /// <param name="cursorRepository">The cursor repository.</param>
        /// <param name="resourceProviderFactory">The resource provider factory.</param>
        /// <param name="assetPackageFactory">The asset package factory.</param>
        /// <param name="uiService">The UI service.</param>
        public Bootstrapper(
            IApplicationWindow applicationWindow,
            IIconRepository iconRepository,
            ICursorRepository cursorRepository,
            IResourceProviderFactory resourceProviderFactory,
            IAssetPackageFactory assetPackageFactory,
            IUIService uiService)
        {
            this.applicationWindow = applicationWindow;
            this.iconRepository = iconRepository;
            this.cursorRepository = cursorRepository;
            this.assetPackageFactory = assetPackageFactory;
            this.uiService = uiService;

            resourceProvider = resourceProviderFactory.Create(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./Contents"));
        }

        /// <inheritdoc/>
        public void Execute()
        {
            var assetPackage = assetPackageFactory.Create(resourceProvider, "Demo.assets");
            assetPackage.ImportAssets();

            applicationWindow.Icon = iconRepository.Get("Logo");
            applicationWindow.Cursor = cursorRepository.DefaultCursor;

            uiService.RegisterVisualElementsForTemplates(Assembly.GetExecutingAssembly());
            uiService.RootVisualElement = uiService.Load("Views/TutorialSelectorView");
        }
    }
}
