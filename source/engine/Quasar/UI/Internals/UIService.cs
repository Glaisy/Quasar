//-----------------------------------------------------------------------
// <copyright file="UIService.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Quasar.UI.Templates;
using Quasar.UI.VisualElements;
using Quasar.UI.VisualElements.Internals;

using Space.Core.DependencyInjection;

namespace Quasar.UI.Internals
{
    /// <summary>
    /// UI service, provider and context validator implementation.
    /// </summary>
    /// <seealso cref="IUIService" />
    /// <seealso cref="IUIProvider" />
    [Export(typeof(IUIService))]
    [Export(typeof(IUIProvider))]
    [Singleton]
    internal sealed class UIService : IUIService, IUIProvider
    {
        private readonly IUIContext context;
        private readonly IUITemplateLoader templateLoader;
        private readonly IVisualElementEventProcessor visualElementEventProcessor;


        /// <summary>
        /// Initializes a new instance of the <see cref="UIService" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="templateLoader">The template loader.</param>
        /// <param name="visualElementEventProcessor">The visual element event processor.</param>
        public UIService(
            IUIContext context,
            IUITemplateLoader templateLoader,
            IVisualElementEventProcessor visualElementEventProcessor)
        {
            this.context = context;
            this.templateLoader = templateLoader;
            this.visualElementEventProcessor = visualElementEventProcessor;
        }


        private VisualElement rootVisualElement;
        /// <inheritdoc/>
        public VisualElement RootVisualElement
        {
            get => rootVisualElement;
            set
            {
                if (rootVisualElement == value)
                {
                    return;
                }

                context.Validate();
                SetRootVisualElement(value);
            }
        }


        /// <inheritdoc/>
        public VisualElement Load(string templatePath)
        {
            ArgumentException.ThrowIfNullOrEmpty(templatePath, nameof(templatePath));
            context.Validate();

            var visualElement = templateLoader.Load(templatePath);
            if (visualElement == null)
            {
                throw new UIException($"Unable to load the view by the '{templatePath}' template path.");
            }

            SetRootVisualElement(visualElement);

            return visualElement;
        }


        private void SetRootVisualElement(VisualElement visualElement)
        {
            rootVisualElement?.Dispose();
            rootVisualElement = visualElement;
            visualElementEventProcessor.ProcessRootVisualElementChanged(visualElement);
        }
    }
}
