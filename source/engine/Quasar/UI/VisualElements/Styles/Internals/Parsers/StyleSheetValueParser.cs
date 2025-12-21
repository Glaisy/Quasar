//-----------------------------------------------------------------------
// <copyright file="StyleSheetValueParser.cs" company="Space Development">
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
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

using Quasar.Graphics;
using Quasar.Utilities;

using Space.Core.Collections;
using Space.Core.DependencyInjection;
using Space.Core.Extensions;

namespace Quasar.UI.VisualElements.Styles.Internals.Parsers
{
    /// <summary>
    /// Quasar style sheet (qss) value parser implementation.
    /// </summary>
    /// <seealso cref="IStyleSheetValueParser" />
    [Export(typeof(IStyleSheetValueParser))]
    [Singleton]
    internal class StyleSheetValueParser : IStyleSheetValueParser
    {
        private const string RgbFunctionStartToken = "rgb(";
        private const string RgbaFunctionStartToken = "rgba(";
        private const string UrlFunctionStartToken = "url(";
        private static readonly Regex variablesRegex = new Regex(@"var\((--[\w-]+)\)");

        private const char FunctionEndToken = ')';
        private const char PercentageSuffix = '%';
        private const string PixelSuffix = "px";
        private const char HexColorPrefix = '#';
        private const string WellKnownColorPrefix = "Color.";
        private readonly IPool<StringBuilder> stringBuilderPool;
        private readonly IPool<List<string>> stringListPool;


        /// <summary>
        /// Initializes a new instance of the <see cref="StyleSheetValueParser" /> class.
        /// </summary>
        /// <param name="stringOperationContext">The string operation context.</param>
        public StyleSheetValueParser(IStringOperationContext stringOperationContext)
        {
            stringBuilderPool = stringOperationContext.BuilderPool;
            stringListPool = stringOperationContext.ListPool;
        }


        /// <inheritdoc/>
        public Border ParseBorder(string value, IReadOnlyDictionary<string, string> variables)
        {
            ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));

            List<string> splitBuffer = null;
            try
            {
                var resolvedValue = ResolveVariables(value, variables);
                resolvedValue = ValidateAndTrimLiteral(resolvedValue);

                splitBuffer = stringListPool.Allocate();
                resolvedValue.Split(splitBuffer, ' ', StringSplitOptions.TrimEntries);
                if (splitBuffer.Count > 4 ||
                    splitBuffer.Count == 0)
                {
                    throw CreateInvalidBorderError(resolvedValue);
                }

                // 1 value (top | top, bottom | top, right, bottom, left)
                if (!TryParsePixelValue(splitBuffer[0], out var pixel0))
                {
                    throw CreateInvalidBorderError(resolvedValue);
                }

                if (splitBuffer.Count == 1)
                {
                    // left, top, right, bottom
                    return new Border(pixel0);
                }

                // 2 values (right | left, right)
                if (!TryParsePixelValue(splitBuffer[1], out var pixel1))
                {
                    throw CreateInvalidBorderError(resolvedValue);
                }

                if (splitBuffer.Count == 2)
                {
                    // width, height
                    return new Border(pixel1, pixel0);
                }

                // 3 values (bottom)
                if (!TryParsePixelValue(splitBuffer[2], out var pixel2))
                {
                    throw CreateInvalidBorderError(resolvedValue);
                }

                if (splitBuffer.Count == 3)
                {
                    // left, top, right, bottom
                    return new Border(pixel1, pixel0, pixel1, pixel2);
                }

                // 4 values (left)
                if (!TryParsePixelValue(splitBuffer[3], out var pixel3))
                {
                    throw CreateInvalidBorderError(resolvedValue);
                }

                return new Border(pixel3, pixel0, pixel1, pixel2);
            }
            finally
            {
                stringListPool.Release(splitBuffer);
            }
        }

        /// <inheritdoc/>
        public (Unit Top, Unit Right, Unit Bottom, Unit Left) ParseBorderUnits(
            string value,
            IReadOnlyDictionary<string, string> variables,
            bool allowPercentages)
        {
            ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));

            List<string> splitBuffer = null;
            try
            {
                var resolvedValue = ResolveVariables(value, variables);
                resolvedValue = ValidateAndTrimLiteral(resolvedValue);
                splitBuffer = stringListPool.Allocate();
                resolvedValue.Split(splitBuffer, ' ', StringSplitOptions.TrimEntries);
                if (splitBuffer.Count > 4 ||
                    splitBuffer.Count == 0)
                {
                    throw CreateInvalidBorderError(resolvedValue);
                }

                // 1 value
                if (!TryParseUnit(splitBuffer[0], allowPercentages, out var unit0))
                {
                    throw CreateInvalidBorderError(resolvedValue);
                }

                if (splitBuffer.Count == 1)
                {
                    return (unit0, unit0, unit0, unit0);
                }

                // 2 values
                if (!TryParseUnit(splitBuffer[1], allowPercentages, out var unit1))
                {
                    throw CreateInvalidBorderError(resolvedValue);
                }

                if (splitBuffer.Count == 2)
                {
                    return (unit0, unit1, unit0, unit1);
                }

                // 3 values
                if (!TryParseUnit(splitBuffer[2], allowPercentages, out var unit2))
                {
                    throw CreateInvalidBorderError(resolvedValue);
                }

                if (splitBuffer.Count == 3)
                {
                    return (unit0, unit1, unit2, unit1);
                }

                // 4 values
                if (!TryParseUnit(splitBuffer[3], allowPercentages, out var unit3))
                {
                    throw CreateInvalidBorderError(resolvedValue);
                }

                return (unit0, unit1, unit2, unit3);
            }
            finally
            {
                stringListPool.Release(splitBuffer);
            }
        }

        /// <inheritdoc/>
        public Color ParseColor(string value, IReadOnlyDictionary<string, string> variables)
        {
            ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));

            var resolvedValue = ResolveVariables(value, variables);
            resolvedValue = ValidateAndTrimLiteral(resolvedValue);
            if (resolvedValue.StartsWith(HexColorPrefix))
            {
                return ParseHexColor(resolvedValue);
            }

            if (resolvedValue.StartsWith(WellKnownColorPrefix))
            {
                return ResolveWellKnownColor(resolvedValue);
            }

            if (TryParseFunction(RgbaFunctionStartToken, resolvedValue, out var colorLiteral))
            {
                return ParseRGBA(colorLiteral, resolvedValue);
            }

            if (TryParseFunction(RgbFunctionStartToken, resolvedValue, out colorLiteral))
            {
                return ParseRGB(colorLiteral, resolvedValue);
            }

            throw CreateInvalidColorError(resolvedValue);
        }

        /// <inheritdoc/>
        public T ParseEnum<T>(string value, IReadOnlyDictionary<string, string> variables)
            where T : struct
        {
            ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));

            var resolvedValue = ResolveVariables(value, variables);
            if (!Enum.TryParse<T>(resolvedValue, true, out var enumValue))
            {
                throw new StyleParserException($"Invalid {typeof(T).FullName} enum value: {resolvedValue}");
            }

            return enumValue;
        }

        /// <inheritdoc/>
        public string ParseLiteral(string value, IReadOnlyDictionary<string, string> variables)
        {
            ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));

            var resolvedValue = ResolveVariables(value, variables);
            return ValidateAndTrimLiteral(resolvedValue);
        }

        /// <inheritdoc/>
        public Unit ParseUnit(string value, IReadOnlyDictionary<string, string> variables, bool allowPercentage)
        {
            ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));

            var resolvedValue = ResolveVariables(value, variables);
            var literalValue = ValidateAndTrimLiteral(resolvedValue).AsSpan();
            if (!TryParseUnit(literalValue, allowPercentage, out var unit))
            {
                throw new StyleParserException($"Invalid unit value: {resolvedValue}");
            }

            return unit;
        }

        /// <inheritdoc/>
        public string ParseUrl(string value, IReadOnlyDictionary<string, string> variables)
        {
            ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));

            var resolvedValue = ResolveVariables(value, variables);
            if (!TryParseFunction(UrlFunctionStartToken, resolvedValue, out var urlLiteral))
            {
                throw new StyleParserException($"Invalid url value: '{resolvedValue}'.");
            }

            return ValidateAndTrimLiteral(urlLiteral);
        }


        private static StyleParserException CreateInvalidBorderError(string resolvedValue)
        {
            return new StyleParserException($"Invalid border value: {resolvedValue}");
        }

        private static StyleParserException CreateInvalidColorError(string resolvedValue)
        {
            return new StyleParserException($"Invalid color value: {resolvedValue}");
        }

        private Color ParseRGB(string rgb, string resolvedValue)
        {
            List<string> splitBuffer = null;
            try
            {
                splitBuffer = stringListPool.Allocate();
                rgb.Split(splitBuffer, ',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (splitBuffer.Count != 3)
                {
                    throw CreateInvalidColorError(resolvedValue);
                }

                var r = Byte.Parse(splitBuffer[0], CultureInfo.InvariantCulture);
                var g = Byte.Parse(splitBuffer[1], CultureInfo.InvariantCulture);
                var b = Byte.Parse(splitBuffer[2], CultureInfo.InvariantCulture);
                return new Color(r, g, b, 1.0f);
            }
            catch
            {
                throw CreateInvalidColorError(resolvedValue);
            }
            finally
            {
                stringListPool.Release(splitBuffer);
            }
        }

        private Color ParseRGBA(string rgba, string resolvedValue)
        {
            List<string> splitBuffer = null;
            try
            {
                splitBuffer = stringListPool.Allocate();
                rgba.Split(splitBuffer, ',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (splitBuffer.Count != 4)
                {
                    throw CreateInvalidColorError(resolvedValue);
                }

                var r = Byte.Parse(splitBuffer[0], CultureInfo.InvariantCulture);
                var g = Byte.Parse(splitBuffer[1], CultureInfo.InvariantCulture);
                var b = Byte.Parse(splitBuffer[2], CultureInfo.InvariantCulture);
                var alpha = Single.Parse(splitBuffer[3], CultureInfo.InvariantCulture);
                return new Color(r, g, b, alpha);
            }
            catch
            {
                throw CreateInvalidColorError(resolvedValue);
            }
            finally
            {
                stringListPool.Release(splitBuffer);
            }
        }

        private static Color ParseHexColor(string hexColor)
        {
            hexColor = hexColor.Substring(1);
            if (hexColor.Length == 8 || hexColor.Length == 6)
            {
                if (UInt32.TryParse(hexColor, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var uintColor))
                {
                    if (hexColor.Length == 6)
                    {
                        uintColor = uintColor << 8 | 0xFF;
                    }

                    return new Color(uintColor);
                }
            }

            throw CreateInvalidColorError(hexColor);
        }

        private string ResolveVariables(string value, IReadOnlyDictionary<string, string> variables)
        {
            var matches = variablesRegex.Matches(value);
            var count = matches.Count;
            if (count == 0)
            {
                return value;
            }

            if (variables == null || variables.Count == 0)
            {
                throw new StyleParserException($"Unable to resolve variables in value '{value}'.");
            }

            StringBuilder builder = null;
            try
            {
                var index = 0;
                builder = stringBuilderPool.Allocate();
                for (var i = 0; i < count; i++)
                {
                    var match = matches[i];
                    var variableName = match.Groups[1].Value;
                    if (!variables.TryGetValue(variableName, out var variableValue))
                    {
                        throw new StyleParserException($"Unable to resolve variable: '{match.Value}'.");
                    }

                    builder.Append(value, index, match.Index - index);
                    builder.Append(variableValue);
                    index = match.Index + match.Length;
                }

                if (index < value.Length)
                {
                    builder.Append(value, index, value.Length - index);
                }

                return builder.ToString();
            }
            finally
            {
                if (builder != null)
                {
                    stringBuilderPool.Release(builder);
                }
            }
        }

        private static Color ResolveWellKnownColor(string wellKnownColorName)
        {
            if (!Color.WellKnownColors.TryGetValue(wellKnownColorName, out var color))
            {
                throw new StyleParserException($"Invalid color value: {wellKnownColorName}");
            }

            return color;
        }

        private static bool TryParseFunction(string functionStartToken, string value, out string argument)
        {
            argument = String.Empty;
            if (value.Length < functionStartToken.Length + 1 ||
                !value.StartsWith(functionStartToken) ||
                value[value.Length - 1] != FunctionEndToken)
            {
                return false;
            }

            argument = value.Substring(functionStartToken.Length, value.Length - 1 - functionStartToken.Length);
            return true;
        }

        private static bool TryParsePixelValue(ReadOnlySpan<char> value, out float pixelValue)
        {
            if (value.EndsWith(PixelSuffix))
            {
                value = value.Slice(0, value.Length - PixelSuffix.Length).TrimStart();
            }

            return Single.TryParse(value, CultureInfo.InvariantCulture, out pixelValue);
        }

        private static bool TryParseUnit(ReadOnlySpan<char> value, bool allowPercentage, out Unit unit)
        {
            unit = default;
            if (allowPercentage && value[value.Length - 1] == PercentageSuffix)
            {
                value = value.Slice(0, value.Length - 1).TrimStart();
                if (Single.TryParse(value, CultureInfo.InvariantCulture, out var percentageValue))
                {
                    unit = new Unit(percentageValue, UnitType.Percentage);
                    return true;
                }

                return false;
            }

            if (value.EndsWith(PixelSuffix))
            {
                value = value.Slice(0, value.Length - PixelSuffix.Length).TrimStart();
            }

            if (Int32.TryParse(value, CultureInfo.InvariantCulture, out var pixelValue))
            {
                unit = new Unit(pixelValue, UnitType.Pixel);
                return true;
            }

            return false;
        }

        private static string ValidateAndTrimLiteral(string value)
        {
            var first = value[0];
            if (first == '\'' || first == '"')
            {
                var last = value[value.Length - 1];
                if (last != first)
                {
                    throw new StyleParserException($"Invalid string literal: {value}");
                }

                value = value.Substring(1, value.Length - 2);
            }

            if (value.Contains('\'') || value.Contains('"'))
            {
                throw new StyleParserException($"Invalid string literal: {value}");
            }

            return value;
        }
    }
}
