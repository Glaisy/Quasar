//-----------------------------------------------------------------------
// <copyright file="TutorialViewBase.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Microsoft.Extensions.DependencyInjection;

using Quasar.UI;
using Quasar.UI.Mvp;
using Quasar.UI.VisualElements;

namespace DemoApplication.Tutorials
{
    /// <summary>
    /// Abstract base class for tutorial view implementations.
    /// </summary>
    /// <seealso cref="VisualElement" />
    /// <seealso cref="IView" />
    internal abstract class TutorialViewBase : VisualElement, IView
    {
        private static IUIService uiService;
        private static Action<TutorialViewBase> onBack;


        /// <summary>
        /// Initializes a new instance of the <see cref="TutorialViewBase" /> class.
        /// </summary>
        protected TutorialViewBase()
        {
            TitlePanel = (TutorialTitlePanel)uiService.Load("TutorialTitlePanel");
            TitlePanel.TitleLabel.Text = Title;
            TitlePanel.BackButton.Click = button => onBack(this);
            Add(TitlePanel);
            AddToClassList("scene-view");
        }


        /// <summary>
        /// Gets the title.
        /// </summary>
        public abstract string Title { get; }


        /// <summary>
        /// Initializes the static services.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="onBack">The back action handler.</param>
        public static void InitializeStaticServices(IServiceProvider serviceProvider, Action<TutorialViewBase> onBack)
        {
            TutorialViewBase.onBack = onBack;
            uiService = serviceProvider.GetRequiredService<IUIService>();
        }


        /// <summary>
        /// Gets the title panel.
        /// </summary>
        protected TutorialTitlePanel TitlePanel { get; }


        /// <summary>
        /// Goes back to the tutorial selector.
        /// </summary>
        protected void GoBack()
        {
            onBack(this);
        }
    }
}
