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
using System.Reflection;

using Quasar.UI.Templates.Internals;
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
        private readonly IUITemplateRepository templateRepository;
        private readonly IVisualElementEventProcessor visualElementEventProcessor;


        /// <summary>
        /// Initializes a new instance of the <see cref="UIService" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="templateRepository">The UI template repository.</param>
        /// <param name="visualElementEventProcessor">The visual element event processor.</param>
        public UIService(
            IUIContext context,
            IUITemplateRepository templateRepository,
            IVisualElementEventProcessor visualElementEventProcessor)
        {
            this.context = context;
            this.templateRepository = templateRepository;
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

            var visualElement = templateRepository.Instantiate(templatePath);
            SetRootVisualElement(visualElement);

            return visualElement;
        }

        /// <inheritdoc/>
        public void RegisterTemplatedVisualElements(Assembly assembly)
        {
            ArgumentNullException.ThrowIfNull(assembly, nameof(assembly));
            context.Validate();

            templateRepository.RegisterTemplatedVisualElements(assembly);
        }


        private void SetRootVisualElement(VisualElement visualElement)
        {
            rootVisualElement?.Dispose();
            rootVisualElement = visualElement;
            visualElementEventProcessor.ProcessRootVisualElementChanged(visualElement);
        }
    }
}
