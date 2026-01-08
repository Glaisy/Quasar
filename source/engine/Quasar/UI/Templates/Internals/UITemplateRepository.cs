//-----------------------------------------------------------------------
// <copyright file="UITemplateRepository.cs" company="Space Development">
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

using Quasar.Collections;
using Quasar.UI.VisualElements;

using Space.Core.DependencyInjection;
using Space.Core.Diagnostics;

namespace Quasar.UI.Templates.Internals
{
    /// <summary>
    /// Quasar UI template repository implementation.
    /// </summary>
    /// <seealso cref="IUITemplateRepository" />
    /// <seealso cref="ITypeResolver" />
    [Export(typeof(IUITemplateRepository))]
    [Singleton]
    internal sealed class UITemplateRepository : RepositoryBase<string, UITemplate, UITemplate>, IUITemplateRepository, ITypeResolver
    {
        private const string TemplateNodeName = "QXml";
        private const string NamespacePrefix = "xmlns:";


        private readonly IUIContext uiContext;
        private readonly ILogger logger;
        private readonly TemplatedVisualElementFactory templatedVisualElementFactory;
        private readonly Dictionary<string, Type> resolvedVisualElementTypes = new Dictionary<string, Type>();


        /// <summary>
        /// Initializes a new instance of the <see cref="UITemplateRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="uiContext">The UI context.</param>
        /// <param name="templatedVisualElementFactory">The templated visual element factory.</param>
        public UITemplateRepository(
            IQuasarContext context,
            IUIContext uiContext,
            TemplatedVisualElementFactory templatedVisualElementFactory)
            : base(true)
        {
            this.templatedVisualElementFactory = templatedVisualElementFactory;
            this.uiContext = uiContext;

            logger = context.Logger;
        }


        /// <inheritdoc/>
        public void Create(Stream stream, string templateId, bool leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(stream, nameof(stream));
            ArgumentException.ThrowIfNullOrEmpty(templateId, nameof(templateId));
            uiContext.Validate();

            try
            {
                RepositoryLock.EnterWriteLock();

                var qxml = LoadQXml(templateId, stream);
                var namespaces = ExtractNamespaces(qxml);

                var uiTemplate = new UITemplate(templateId, qxml.DocumentElement.ChildNodes[0], namespaces);
                AddItem(uiTemplate);
            }
            catch (Exception exception)
            {
                logger.Error(exception, $"Unable to load QXml template '{templateId}'. Skipped.");
            }
            finally
            {
                if (!leaveOpen)
                {
                    stream.Dispose();
                }

                RepositoryLock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        public TemplatedVisualElementBase Instantiate(string templateId)
        {
            try
            {
                RepositoryLock.EnterReadLock();

                var uiTemplate = GetItemById(templateId);
                if (uiTemplate == null)
                {
                    throw new UIException($"Unable to load the UI template by the id: '{templateId}'.");
                }

                return templatedVisualElementFactory.Create(uiTemplate, this, this);
            }
            finally
            {
                RepositoryLock.ExitReadLock();
            }
        }

        /// <inheritdoc/>
        public void RegisterVisualElementsForTemplates(Assembly assembly)
        {
            ArgumentNullException.ThrowIfNull(assembly, nameof(assembly));

            try
            {
                RepositoryLock.EnterWriteLock();

                foreach (var type in assembly.GetTypes())
                {
                    if (!typeof(VisualElement).IsAssignableFrom(type))
                    {
                        continue;
                    }

                    resolvedVisualElementTypes.Add(type.FullName, type);
                }
            }
            finally
            {
                RepositoryLock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        Type ITypeResolver.Resolve(string typeName)
        {
            var type = Type.GetType(typeName);
            if (type != null)
            {
                return type;
            }

            resolvedVisualElementTypes.TryGetValue(typeName, out type);
            return type;
        }


        /// <inheritdoc/>
        protected override void ValidateIdentifier(string id)
        {
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));
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

        private static XmlDocument LoadQXml(string templateId, Stream stream)
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
                throw new UITemplateException($"Unable to load QXml template '{templateId}'.", exception);
            }

            // validate loaded xml document
            if (qxml.DocumentElement.Name != TemplateNodeName)
            {
                throw new UITemplateException($"The '{templateId}' UI template resource must have the '{TemplateNodeName}' root node.");
            }

            if (qxml.DocumentElement.ChildNodes.Count != 1)
            {
                throw new UITemplateException($"The '{templateId}' UI template must have exactly 1 root visual element.");
            }

            return qxml;
        }
    }
}
