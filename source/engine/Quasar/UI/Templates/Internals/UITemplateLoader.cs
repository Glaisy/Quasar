//-----------------------------------------------------------------------
// <copyright file="UITemplateLoader.cs" company="Space Development">
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
using System.IO;
using System.Reflection;
using System.Xml;

using Quasar.UI.VisualElements;
using Quasar.Utilities;

using Space.Core.DependencyInjection;
using Space.Core.Diagnostics;

namespace Quasar.UI.Templates.Internals
{
    /// <summary>
    /// Quasar UI tempplate loader implementation.
    /// </summary>
    /// <seealso cref="IUITemplateLoader" />
    /// <seealso cref="ITypeResolver" />
    [Export(typeof(IUITemplateLoader))]
    [Singleton]
    internal sealed class UITemplateLoader : IUITemplateLoader, ITypeResolver
    {
        private const string TemplateExtension = ".qxml";
        private const string TemplateNodeName = "QXml";
        private const string NamespacePrefix = "xmlns:";


        private readonly VisualElementFactory visualElementFactory;
        private readonly IUIContext context;
        private readonly ILogger logger;
        private readonly Dictionary<string, UITemplate> templates = new Dictionary<string, UITemplate>();
        private readonly Dictionary<string, Type> resolvedVisualElementTypes = new Dictionary<string, Type>();


        /// <summary>
        /// Initializes a new instance of the <see cref="UITemplateLoader" /> class.
        /// </summary>
        /// <param name="visualElementFactory">The visual element factory.</param>
        /// <param name="context">The UI context.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public UITemplateLoader(
            VisualElementFactory visualElementFactory,
            IUIContext context,
            ILoggerFactory loggerFactory)
        {
            this.visualElementFactory = visualElementFactory;
            this.context = context;

            logger = loggerFactory.Create<UITemplateLoader>();

            AddVisualElementTypes(GetType().Assembly);
        }


        /// <inheritdoc/>
        public VisualElement Load(string templatePath)
        {
            ArgumentException.ThrowIfNullOrEmpty(templatePath, nameof(templatePath));
            context.Validate();

            if (!templates.TryGetValue(templatePath, out var template))
            {
                throw new UITemplateException($"Template not found: '{templatePath}'");
            }

            return visualElementFactory.Create(template, this, this);
        }

        /// <inheritdoc/>
        public void Register(IResourceProvider resourceProvider, string templateBasePath, Assembly assembly)
        {
            ArgumentNullException.ThrowIfNull(resourceProvider, nameof(resourceProvider));
            ArgumentException.ThrowIfNullOrEmpty(templateBasePath, nameof(templateBasePath));
            context.Validate();

            // add visual element types from the assembly.
            AddVisualElementTypes(assembly);

            // load all templates
            var templatePaths = resourceProvider.EnumerateResources(templateBasePath, true, TemplateExtension);
            foreach (var templatePath in templatePaths)
            {
                Stream stream = null;
                try
                {
                    // load QXml document and parse namespaces
                    stream = resourceProvider.GetResourceStream(templatePath);
                    var qxml = LoadQXml(templatePath, stream);
                    var namespaces = ExtractNamespaces(qxml);

                    // calculate the relative template path
                    var pathIndex = templatePath.IndexOf(templateBasePath) + templateBasePath.Length + 1;
                    var pathLength = templatePath.Length - pathIndex - TemplateExtension.Length;
                    var relativeTemplatePath = templatePath.Substring(pathIndex, pathLength);

                    // add template
                    var uiTemplate = new UITemplate(relativeTemplatePath, qxml.DocumentElement.ChildNodes[0], namespaces);
                    templates.Add(relativeTemplatePath, uiTemplate);
                }
                catch (Exception exception)
                {
                    logger.Error(exception, $"Unable to load QXml template '{templatePath}'. Skipped.");
                }
                finally
                {
                    stream?.Dispose();
                }
            }
        }

        /// <inheritdoc/>
        Type ITypeResolver.Resolve(string typeName)
        {
            resolvedVisualElementTypes.TryGetValue(typeName, out var type);
            return type;
        }


        private void AddVisualElementTypes(Assembly referencedAssembly)
        {
            foreach (var type in referencedAssembly.GetTypes())
            {
                if (!typeof(VisualElement).IsAssignableFrom(type))
                {
                    continue;
                }

                resolvedVisualElementTypes.Add(type.FullName, type);
            }
        }

        private static Dictionary<string, string> ExtractNamespaces(XmlDocument xmlDocument)
        {
            var namespaces = new Dictionary<string, string>();
            foreach (XmlAttribute attribute in xmlDocument.DocumentElement.Attributes)
            {
                if (!attribute.Name.StartsWith(NamespacePrefix))
                {
                    continue;
                }

                var @namespace = attribute.Name.Substring(NamespacePrefix.Length);
                namespaces[@namespace] = attribute.Value;
            }

            return namespaces;
        }

        private static XmlDocument LoadQXml(string templatePath, Stream stream)
        {
            // load QXml from the stream
            XmlDocument qxml = null;
            try
            {
                qxml = new XmlDocument();
                qxml.Load(stream);
            }
            catch (Exception exception)
            {
                throw new UITemplateException($"Unable to load QXml template from '{templatePath}'.", exception);
            }

            // validate loaded xml document
            if (qxml.DocumentElement.Name != TemplateNodeName)
            {
                throw new UITemplateException($"The '{templatePath}' UI template resource must have the '{TemplateNodeName}' root node.");
            }

            if (qxml.DocumentElement.ChildNodes.Count != 1)
            {
                throw new UITemplateException($"The '{templatePath}' UI template must have exactly 1 root visual element.");
            }

            return qxml;
        }
    }
}
