//-----------------------------------------------------------------------
// <copyright file="TutorialSelectorView.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using DemoApplication.Tutorials.Objects;

using Microsoft.Extensions.DependencyInjection;

using Quasar.UI;
using Quasar.UI.Templates;
using Quasar.UI.VisualElements;

using Space.Core.DependencyInjection;

namespace DemoApplication.Tutorials
{
    /// <summary>
    /// Demo application's tutorial selector view implementation.
    /// </summary>
    [UITemplate("Views/TutorialSelectorView")]
    [Export]
    internal sealed class TutorialSelectorView : TemplatedVisualElementBase
    {
        private readonly IApplicationWindow applicationWindow;
        private readonly IUIService uiService;
        private readonly IServiceProvider serviceProvider;


        /// <summary>
        /// Initializes a new instance of the <see cref="TutorialSelectorView" /> class.
        /// </summary>
        /// <param name="applicationWindow">The application window.</param>
        /// <param name="uiService">The UI service.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public TutorialSelectorView(
            IApplicationWindow applicationWindow,
            IUIService uiService,
            IServiceProvider serviceProvider)
        {
            this.applicationWindow = applicationWindow;
            this.uiService = uiService;
            this.serviceProvider = serviceProvider;
        }



        /// <inheritdoc/>
        protected override void OnTemplateLoaded()
        {
            TutorialViewBase.InitializeStaticServices(serviceProvider, OnBack);

            var objectsButton = Q<Button>("objectsButton");
            objectsButton.Click = button => LoadTutorialView<ObjectsModelsAndSkyboxTutorialView>();
            var exitButton = Q<Button>("exitButton");
            exitButton.Click = button => applicationWindow.Close();
        }


        private void LoadTutorialView<TView>()
            where TView : TutorialViewBase
        {
            var tutorialView = serviceProvider.GetRequiredService<TView>();
            uiService.RootVisualElement = tutorialView;
        }

        private void OnBack(TutorialViewBase tutorialView)
        {
            uiService.RootVisualElement = this;
            tutorialView.Dispose();
        }
    }
}
