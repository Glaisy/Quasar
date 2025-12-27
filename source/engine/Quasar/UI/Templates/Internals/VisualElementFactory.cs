//-----------------------------------------------------------------------
// <copyright file="VisualElementFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

using Quasar.UI.VisualElements;
using Quasar.UI.VisualElements.Styles.Internals;
using Quasar.UI.VisualElements.Themes;
using Quasar.Utilities;

using Space.Core;
using Space.Core.Collections;
using Space.Core.DependencyInjection;
using Space.Core.Extensions;

namespace Quasar.UI.Templates.Internals
{
    /// <summary>
    /// Visual element factory implementation.
    /// </summary>
    [Export]
    [Singleton]
    internal sealed class VisualElementFactory
    {
        private readonly IStyleFactory styleFactory;
        private readonly IStyleSheetParser styleSheetParser;
        private readonly IThemeProvider themeProvider;
        private readonly IPool<List<string>> stringListPool;
        private readonly IServiceProvider serviceProvider;


        /// <summary>
        /// Initializes a new instance of the <see cref="VisualElementFactory" /> class.
        /// </summary>
        /// <param name="styleFactory">The style factory.</param>
        /// <param name="styleSheetParser">The style sheet parser.</param>
        /// <param name="themeProvider">The theme provider.</param>
        /// <param name="stringOperationContext">The string operation context.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public VisualElementFactory(
            IStyleFactory styleFactory,
            IStyleSheetParser styleSheetParser,
            IThemeProvider themeProvider,
            IStringOperationContext stringOperationContext,
            IServiceProvider serviceProvider)
        {
            this.styleFactory = styleFactory;
            this.styleSheetParser = styleSheetParser;
            this.themeProvider = themeProvider;
            this.serviceProvider = serviceProvider;

            stringListPool = stringOperationContext.ListPool;
        }


        /// <summary>
        /// Creates and initializes a visual element by the template.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="templateRepository">The template reposiory.</param>
        /// <param name="typeResolver">The type resolver.</param>
        /// <returns>
        /// The created visual element (and child hierarchy).
        /// </returns>
        public VisualElement Create(in UITemplate template, IUITemplateRepository templateRepository, ITypeResolver typeResolver)
        {
            Assertion.ThrowIfNull(templateRepository, nameof(templateRepository));

            return CreateVisualElement(template.RootNode, template, templateRepository, typeResolver);
        }


        private VisualElement CreateInstance(Type visualElementType)
        {
            try
            {
                return (VisualElement)Activator.CreateInstance(visualElementType);
            }
            catch
            {
                // regular instancing does not work, try to get as exported service
                var @object = serviceProvider.GetService(visualElementType);
                if (@object == null)
                {
                    throw new UITemplateException($"Unable to instantiate visual element: '{visualElementType.FullName}'");
                }

                return (VisualElement)@object;
            }
        }

        private VisualElement CreateVisualElement(
            XmlNode node,
            in UITemplate uiTemplate,
            IUITemplateRepository templateRepository,
            ITypeResolver typeResolver)
        {
            // create visual element instance
            VisualElement visualElement;
            UITemplateAttribute templateAttribute;
            var visualElementType = GetVisualElementType(node, uiTemplate, typeResolver);

            if (node != uiTemplate.RootNode &&
                (templateAttribute = visualElementType.GetCustomAttribute<UITemplateAttribute>()) != null)
            {
                // yes, load from the template
                var nestedUITemplate = templateRepository.Get(templateAttribute.TemplateId);
                visualElement = CreateVisualElement(nestedUITemplate.RootNode, nestedUITemplate, templateRepository, typeResolver);
            }
            else
            {
                // no, simply create instance.
                visualElement = CreateInstance(visualElementType);
            }

            // initialize visual element
            foreach (XmlAttribute attribute in node.Attributes)
            {
                switch (attribute.Name)
                {
                    case nameof(VisualElement.Name):
                        visualElement.Name = attribute.Value;
                        break;

                    case "Style":
                        ProcessStyleAttribute(visualElement, attribute.Value, uiTemplate.Id);
                        break;

                    case "Class":
                        ProcessClassAttribute(visualElement, attribute.Value);
                        break;

                    default:
                        visualElement.SetProperty(attribute.Name, attribute.Value);
                        break;
                }
            }

            // create child visual elements
            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode is XmlComment)
                {
                    continue;
                }

                var childVisualElement = CreateVisualElement(childNode, uiTemplate, templateRepository, typeResolver);
                visualElement.Add(childVisualElement);
            }

            return visualElement;
        }

        private static Type GetVisualElementType(XmlNode node, in UITemplate template, ITypeResolver typeResolver)
        {
            string typeName;
            var namespaceSeparatorIndex = node.Name.IndexOf(':');
            if (namespaceSeparatorIndex > 0)
            {
                var namespaceName = node.Name.Substring(0, namespaceSeparatorIndex);
                if (!template.Namespaces.TryGetValue(namespaceName, out var @namespace))
                {
                    throw new UITemplateException($"Unknown namespace '{namespaceName}' in '{template.Id}' template.");
                }

                var className = node.Name.Substring(namespaceSeparatorIndex + 1);
                typeName = String.Join('.', @namespace, className);
            }
            else
            {
                typeName = node.Name;
            }

            var type = typeResolver.Resolve(typeName);
            if (type == null)
            {
                throw new UITemplateException($"Unresolved type '{typeName}' in {template.Id}' template.");
            }

            if (!typeof(VisualElement).IsAssignableFrom(type))
            {
                throw new UITemplateException($"'{typeName}' type must be a descendant of '{typeof(VisualElement).FullName}' in '{template.Id}' template.");
            }

            return type;
        }

        private void ProcessClassAttribute(VisualElement visualElement, string classValue)
        {
            if (String.IsNullOrEmpty(classValue))
            {
                return;
            }

            List<string> splitBuffer = null;
            try
            {
                splitBuffer = stringListPool.Allocate();
                classValue.Split(splitBuffer, ' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                foreach (var @class in splitBuffer)
                {
                    visualElement.AddToClassList(@class);
                }
            }
            finally
            {
                stringListPool.Release(splitBuffer);
            }
        }

        private void ProcessStyleAttribute(VisualElement visualElement, string styleValue, string templateId)
        {
            var properties = styleSheetParser.ParseInline(styleValue, templateId);
            var inlineStyle = styleFactory.CreateInline(properties, themeProvider.CurrentTheme);
            visualElement.SetInlineStyle(inlineStyle);
        }
    }
}
