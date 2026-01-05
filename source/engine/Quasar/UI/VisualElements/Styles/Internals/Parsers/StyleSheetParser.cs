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
        public StyleTable Parse(IReadOnlyDictionary<string, string> styleSheets, string rootStyleSheetId)
        {
            Assertion.ThrowIfNull(styleSheets, nameof(styleSheets));
            Assertion.ThrowIfNullOrEmpty(rootStyleSheetId, nameof(rootStyleSheetId));

            var context = new ParserContext(styleSheets)
            {
                StyleSheetUrl = rootStyleSheetId,
                ImportBaseUrl = pathResolver.GetParentDirectoryPath(rootStyleSheetId)
            };

            ParseInternal(rootStyleSheetId, ref context);

            return context.Styles;
        }

        /// <inheritdoc/>
        public StyleProperties ParseInline(string inlineStyle, string templateId)
        {
            Assertion.ThrowIfNullOrEmpty(inlineStyle, nameof(inlineStyle));

            List<string> splitBuffer = null;
            try
            {
                var context = new ParserContext
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


        private void ParseInternal(string styleSheetId, ref ParserContext context)
        {
            if (!context.StyleSheets.TryGetValue(styleSheetId, out var styleSheet))
            {
                throw new UIException($"Style sheet '{styleSheetId}' not found.");
            }

            ParseStyleSheet(styleSheet, ref context);
        }

        private ParsedProperty ParseProperty(ReadOnlySpan<char> token, in ParserContext context)
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
            return new ParsedProperty(new string(name), new string(trimmedValue));
        }

        private void ParseStyleSheet(string styleSheet, ref ParserContext context)
        {
            string line;
            context.Styles ??= new StyleTable();
            context.ParsedToken = ParsedToken.Default;
            context.StyleProperties = null;
            context.LineIndex = 0;
            using (var reader = new StringReader(styleSheet))
            {
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
        }

        private void ParseLine(in ReadOnlySpan<char> line, ref ParserContext context)
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

        private void ParseLineAsComment(in ReadOnlySpan<char> line, ref ParserContext context)
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

        private void ParseLineAsImport(in ReadOnlySpan<char> line, ref ParserContext context)
        {
            // parse import url
            var importEndTokenIndex = line.IndexOf(ImportEndToken);
            if (importEndTokenIndex < 0)
            {
                throw new StyleParserException("Incomplete import token", context.StyleSheetUrl, context.LineIndex);
            }

            var importUrlToken = new string(line.Slice(ImportStartToken.Length, importEndTokenIndex - ImportStartToken.Length));
            var importUrl = valueParser.ParseUrl(importUrlToken, null);
            var resolvedStyleSheetUrl = ResolveStyleShettUrl(importUrl, context);
            var importBaseUrl = pathResolver.GetParentDirectoryPath(resolvedStyleSheetUrl);

            // create new context
            var importContext = new ParserContext(context)
            {
                ImportBaseUrl = importBaseUrl,
                Styles = context.Styles,
                StyleSheetUrl = resolvedStyleSheetUrl
            };

            // import the resource
            ParseInternal(resolvedStyleSheetUrl, ref importContext);

            // parse remaining characters
            ParseRemainingLine(line, importEndTokenIndex + ImportEndToken.Length, ref context);
        }

        private string ResolveStyleShettUrl(string importUrl, in ParserContext context)
        {
            var url = pathResolver.Resolve(importUrl, context.ImportBaseUrl);
            if (url.StartsWith(StyleConstants.RootUrlPrefix))
            {
                return url.Substring(1);
            }

            if (importUrl.StartsWith(pathResolver.RelativePathToken))
            {
                return url;
            }

            return pathResolver.Combine(context.ImportBaseUrl, url);
        }

        private void ParseLineAsStyle(in ReadOnlySpan<char> line, ref ParserContext context)
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

        private void ParseRemainingLine(in ReadOnlySpan<char> line, int startIndex, ref ParserContext context)
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

        private static ReadOnlySpan<char> TrimQuotes(ReadOnlySpan<char> value, in ParserContext context)
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
