//-----------------------------------------------------------------------
// <copyright file="StyleSheetParser.cs" company="Space Development">
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
using System.Text;

using Quasar.Utilities;

using Space.Core;
using Space.Core.Collections;
using Space.Core.DependencyInjection;
using Space.Core.Extensions;

namespace Quasar.UI.VisualElements.Styles.Internals.Parsers
{
    /// <summary>
    /// Quasar style sheet (qss) parser implementation.
    /// </summary>
    /// <seealso cref="IStyleSheetParser" />
    [Export(typeof(IStyleSheetParser))]
    [Singleton]
    internal sealed partial class StyleSheetParser : IStyleSheetParser
    {
        private const string CommentStartToken = "/*";
        private const string CommentEndToken = "*/";
        private const string ImportStartToken = "@import ";
        private const string ImportEndToken = ";";
        private const char PropertyEndToken = ';';
        private const string SingleLineCommentToken = "//";
        private const string StyleStartToken = "{";
        private const string StyleEndToken = "}";
        private const char KeyValueSeparator = ':';


        private readonly IPathResolver pathResolver;
        private readonly IStyleSheetValueParser valueParser;
        private readonly IStyleNameValidator nameValidator;
        private readonly IPool<List<string>> stringListPool;


        /// <summary>
        /// Initializes a new instance of the <see cref="StyleSheetParser" /> class.
        /// </summary>
        /// <param name="pathResolver">The path resolver.</param>
        /// <param name="valueParser">The value parser.</param>
        /// <param name="nameValidator">The name validator.</param>
        /// <param name="stringOperationContext">The string operation context.</param>
        public StyleSheetParser(
            IPathResolver pathResolver,
            IStyleSheetValueParser valueParser,
            IStyleNameValidator nameValidator,
            IStringOperationContext stringOperationContext)
        {
            this.pathResolver = pathResolver;
            this.valueParser = valueParser;
            this.nameValidator = nameValidator;

            stringListPool = stringOperationContext.ListPool;
        }


        /// <inheritdoc/>
        public StyleTable Parse(string path, IResourceProvider resourceProvider)
        {
            Assertion.ThrowIfNullOrEmpty(path, nameof(path));
            Assertion.ThrowIfNull(resourceProvider, nameof(resourceProvider));

            var rootResourcePath = resourceProvider.GetDirectoryPath(path);
            var context = new Context(resourceProvider, rootResourcePath)
            {
                ImportBaseUrl = null
            };

            ParseInternal(path, path, ref context);

            return context.Styles;
        }

        /// <inheritdoc/>
        public StyleProperties ParseInline(string inlineStyle, string templateId)
        {
            Assertion.ThrowIfNullOrEmpty(inlineStyle, nameof(inlineStyle));

            List<string> splitBuffer = null;
            try
            {
                var context = new Context
                {
                    StyleSheetUrl = templateId
                };

                var properties = new StyleProperties();
                splitBuffer = stringListPool.Allocate();
                inlineStyle.Split(splitBuffer, PropertyEndToken, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                foreach (var propertyToken in splitBuffer)
                {
                    var property = ParseProperty(propertyToken, context);
                    properties[property.Name] = property.Value;
                }

                return properties;
            }
            finally
            {
                stringListPool.Release(splitBuffer);
            }
        }


        private (string Name, string Value) ParseProperty(ReadOnlySpan<char> token, in Context context)
        {
            var separatorIndex = token.IndexOf(KeyValueSeparator);
            if (separatorIndex <= 0)
            {
                throw new StyleParserException("Unexpected property token", context.StyleSheetUrl, context.LineIndex);
            }

            var name = token.Slice(0, separatorIndex).Trim();
            if (!nameValidator.IsValidName(name))
            {
                throw new StyleParserException($"Invalid property name '{name}'", context.StyleSheetUrl, context.LineIndex);
            }

            var value = token.Slice(separatorIndex + 1).Trim();
            var trimmedValue = TrimQuotes(value, context);
            return (new string(name), new string(trimmedValue));
        }

        private void ParseInternal(string styleSheetUrl, string resourcePath, ref Context context)
        {
            Stream stream = null;
            StreamReader reader = null;
            try
            {
                stream = context.ResourceProvider.GetResourceStream(resourcePath);
                if (stream == null)
                {
                    throw new StyleParserException($"Unresolved style sheet: '{styleSheetUrl}'.", context.StyleSheetUrl, context.LineIndex);
                }

                reader = new StreamReader(stream, encoding: Encoding.UTF8, leaveOpen: true);

                context.StyleSheetUrl = styleSheetUrl;
                ParseInternal(reader, ref context);
            }
            finally
            {
                reader?.Dispose();
                stream?.Dispose();
            }
        }

        private void ParseInternal(StreamReader reader, ref Context context)
        {
            string line;
            context.Styles ??= new StyleTable();
            context.ParsedToken = ParsedToken.Default;
            context.StyleProperties = null;
            context.LineIndex = 0;
            while ((line = reader.ReadLine()) != null)
            {
                context.LineIndex++;
                var lineSpan = line.AsSpan().Trim();

                // skip empty lines and single line comments
                if (lineSpan.Length == 0 ||
                    lineSpan.StartsWith(SingleLineCommentToken))
                {
                    continue;
                }

                // import?
                if (lineSpan.StartsWith(ImportStartToken))
                {
                    ParseLineAsImport(lineSpan, ref context);
                    continue;
                }

                if (context.ParsedToken == ParsedToken.Default)
                {
                    ParseLine(lineSpan, ref context);
                    continue;
                }

                if (context.ParsedToken.HasFlag(ParsedToken.Comment))
                {
                    ParseLineAsComment(lineSpan, ref context);
                    continue;
                }

                ParseLineAsStyle(lineSpan, ref context);
            }
        }

        private void ParseLine(in ReadOnlySpan<char> line, ref Context context)
        {
            // new comment?
            if (line.StartsWith(CommentStartToken))
            {
                // multiline?
                var commentLine = line.Slice(CommentStartToken.Length);
                var commentEndTokenIndex = commentLine.IndexOf(CommentEndToken);
                if (commentEndTokenIndex < 0)
                {
                    // yes
                    context.ParsedToken = ParsedToken.Comment;
                    return;
                }

                // parse remaining characters
                ParseRemainingLine(commentLine, commentEndTokenIndex + CommentEndToken.Length, ref context);
                return;
            }

            // new style?
            var styleStartIndex = line.IndexOf(StyleStartToken);
            if (styleStartIndex >= 0)
            {
                // get selector
                var selector = new string(line.Slice(0, styleStartIndex).Trim());

                // get class attributes for the selector
                if (!context.Styles.TryGetValue(selector, out context.StyleProperties))
                {
                    context.StyleProperties = new StyleProperties();
                    context.Styles.Add(selector, context.StyleProperties);
                }

                context.ParsedToken = ParsedToken.Style;

                // parse remaining characters
                ParseRemainingLine(line, styleStartIndex + StyleStartToken.Length, ref context);
                return;
            }

            // unknown token
            throw new StyleParserException("Unexpected token", context.StyleSheetUrl, context.LineIndex);
        }

        private void ParseLineAsComment(in ReadOnlySpan<char> line, ref Context context)
        {
            // has comment ended?
            var commentEndTokenIndex = line.IndexOf(CommentEndToken);
            if (commentEndTokenIndex < 0)
            {
                // no, skip the line.
                return;
            }

            context.ParsedToken &= ~ParsedToken.Comment;

            // parse remaining characters
            ParseRemainingLine(line, commentEndTokenIndex + CommentEndToken.Length, ref context);
        }

        private void ParseLineAsImport(in ReadOnlySpan<char> line, ref Context context)
        {
            // parse import url
            var importEndTokenIndex = line.IndexOf(ImportEndToken);
            if (importEndTokenIndex < 0)
            {
                throw new StyleParserException("Incomplete import token", context.StyleSheetUrl, context.LineIndex);
            }

            var importUrlToken = new string(line.Slice(ImportStartToken.Length, importEndTokenIndex - ImportStartToken.Length));
            var importUrl = valueParser.ParseUrl(importUrlToken, null);

            var resolvedImportUrl = ResolveImportUrl(importUrl, context);

            var importBaseUrl = pathResolver.GetParentDirectoryPath(resolvedImportUrl);
            var importResourcePath = String.Concat(
                context.RootResourcePath,
                context.ResourceProvider.PathResolver.PathSeparator,
                resolvedImportUrl);

            // create new context
            var importContext = new Context(context)
            {
                ImportBaseUrl = importBaseUrl,
                Styles = context.Styles
            };

            // import the resource
            ParseInternal(resolvedImportUrl, importResourcePath, ref importContext);

            // parse remaining characters
            ParseRemainingLine(line, importEndTokenIndex + ImportEndToken.Length, ref context);
        }

        private void ParseLineAsStyle(in ReadOnlySpan<char> line, ref Context context)
        {
            // class end token?
            if (line.StartsWith(StyleEndToken))
            {
                // class completed
                context.ParsedToken = ParsedToken.Default;
                context.StyleProperties = null;

                // parse remaining characters
                ParseRemainingLine(line, StyleEndToken.Length, ref context);
                return;
            }

            // single line comment?
            if (line.StartsWith(SingleLineCommentToken))
            {
                // yes, skip the line
                return;
            }

            // new comment?
            if (line.StartsWith(CommentStartToken))
            {
                // multiline?
                var commentLine = line.Slice(CommentStartToken.Length);
                var commentEndTokenIndex = commentLine.IndexOf(CommentEndToken);
                if (commentEndTokenIndex < 0)
                {
                    // yes
                    context.ParsedToken |= ParsedToken.Comment;
                    return;
                }

                // parse remaining characters
                ParseRemainingLine(commentLine, commentEndTokenIndex + CommentEndToken.Length, ref context);
                return;
            }

            // parse next style property
            var propertyEndIndex = line.IndexOf(PropertyEndToken);
            if (propertyEndIndex <= 0)
            {
                throw new StyleParserException("Unexpected end of line", context.StyleSheetUrl, context.LineIndex);
            }

            // parse property
            var propertyToken = line.Slice(0, propertyEndIndex);
            var property = ParseProperty(propertyToken, context);
            context.StyleProperties[property.Name] = property.Value;

            // parse remaining characters
            ParseRemainingLine(line, propertyEndIndex + 1, ref context);
        }

        private void ParseRemainingLine(in ReadOnlySpan<char> line, int startIndex, ref Context context)
        {
            var remainingLine = line.Slice(startIndex).TrimStart();
            if (remainingLine.Length == 0)
            {
                return;
            }

            if (context.ParsedToken == ParsedToken.Style)
            {
                ParseLineAsStyle(remainingLine, ref context);
                return;
            }

            if (context.ParsedToken == ParsedToken.Comment)
            {
                ParseLineAsComment(remainingLine, ref context);
                return;
            }

            ParseLine(remainingLine, ref context);
        }

        private string ResolveImportUrl(in string importUrl, in Context context)
        {
            if (importUrl[0] == StyleConstants.RootUrlPrefix)
            {
                return importUrl.Substring(1);
            }

            var url = importUrl;
            if (importUrl.StartsWith(pathResolver.RelativePathToken))
            {
                url = importUrl.Substring(pathResolver.RelativePathToken.Length);
            }

            if (String.IsNullOrEmpty(context.ImportBaseUrl))
            {
                return url;
            }

            return String.Join(pathResolver.PathSeparator, context.ImportBaseUrl, url);
        }

        private static ReadOnlySpan<char> TrimQuotes(ReadOnlySpan<char> value, in Context context)
        {
            var first = value[0];
            var last = value[value.Length - 1];
            if (first != '\'' &&
                first != '"' &&
                last != '\'' &&
                last != '"')
            {
                return value;
            }

            // validate quotes
            if (last != first)
            {
                throw new StyleParserException("Invalid string quotes", context.StyleSheetUrl, context.LineIndex);
            }

            value = value.Slice(1, value.Length - 2);

            if (value.Contains('\'') ||
               value.Contains('"'))
            {
                throw new StyleParserException("Invalid string expression", context.StyleSheetUrl, context.LineIndex);
            }

            return value;
        }
    }
}
