//-----------------------------------------------------------------------
// <copyright file="PropertyValueParser.cs" company="Space Development">
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

using Quasar.Graphics;
using Quasar.UI.VisualElements.Layouts;
using Quasar.Utilities;

using Space.Core.Collections;
using Space.Core.DependencyInjection;
using Space.Core.Extensions;

namespace Quasar.UI.VisualElements.Internals
{
    /// <summary>
    /// Property value parser implementation.
    /// </summary>
    [Export(typeof(IPropertyValueParser))]
    [Singleton]
    internal sealed class PropertyValueParser : IPropertyValueParser
    {
        private const char PercentageSuffix = '%';
        private const string PixelSuffix = "px";
        private const char GridDefinitionSeparator = ';';
        private const char GridLengthStarSuffix = '*';


        private readonly IPool<List<string>> stringListPool;
        private readonly IPool<GridColumn> gridColumnPool;
        private readonly IPool<GridRow> gridRowPool;


        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyValueParser" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="stringOperationContext">The string operation context.</param>
        public PropertyValueParser(
            VisualElementContext context,
            IStringOperationContext stringOperationContext)
        {
            gridColumnPool = context.GridColumnPool;
            gridRowPool = context.GridRowPool;
            stringListPool = stringOperationContext.ListPool;
        }


        /// <inheritdoc/>
        public bool ParseBoolean(string value)
        {
            ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));
            if (value == "0")
            {
                return false;
            }

            if (value == "1")
            {
                return true;
            }

            return Boolean.Parse(value);
        }

        /// <inheritdoc/>
        public Color ParseColor(string value)
        {
            ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));

            var span = value.AsSpan();
            if (value[0] == '#')
            {
                span = span.Slice(1);
            }

            var rgba = UInt32.Parse(span, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            return new Color(rgba);
        }

        /// <inheritdoc/>
        public double ParseDouble(string value)
        {
            ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));
            return Double.Parse(value, CultureInfo.InvariantCulture);
        }

        /// <inheritdoc/>
        public T ParseEnum<T>(string value)
            where T : struct, IConvertible
        {
            ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));
            return Enum.Parse<T>(value, false);
        }

        /// <inheritdoc/>
        public void ParseGridColumns(List<GridColumn> gridColumns, string value)
        {
            ArgumentNullException.ThrowIfNull(gridColumns, nameof(gridColumns));

            gridColumns.Clear();
            if (String.IsNullOrEmpty(value))
            {
                return;
            }

            List<string> splitBuffer = null;
            try
            {
                splitBuffer = stringListPool.Allocate();
                value.Split(splitBuffer, GridDefinitionSeparator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                foreach (var gridLengthString in splitBuffer)
                {
                    var gridLength = ParseGridLengthInternal(gridLengthString);
                    var gridColumn = gridColumnPool.Allocate();
                    gridColumn.DefinedWidth = gridLength;
                    gridColumns.Add(gridColumn);
                }
            }
            catch
            {
                // rollback on error
                foreach (var gridColumn in gridColumns)
                {
                    gridColumnPool.Release(gridColumn);
                }

                gridColumns.Clear();

                throw;
            }
            finally
            {
                stringListPool.Release(splitBuffer);
            }
        }

        /// <inheritdoc/>
        public void ParseGridRows(List<GridRow> gridRows, string value)
        {
            gridRows.Clear();
            if (String.IsNullOrEmpty(value))
            {
                return;
            }

            List<string> splitBuffer = null;
            try
            {
                splitBuffer = stringListPool.Allocate();
                value.Split(splitBuffer, GridDefinitionSeparator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                foreach (var gridLengthString in splitBuffer)
                {
                    var gridLength = ParseGridLengthInternal(gridLengthString);
                    var gridRow = gridRowPool.Allocate();
                    gridRow.DefinedHeight = gridLength;
                    gridRows.Add(gridRow);
                }
            }
            catch
            {
                // rollback on error
                foreach (var gridRow in gridRows)
                {
                    gridRowPool.Release(gridRow);
                }

                gridRows.Clear();

                throw;
            }
            finally
            {
                stringListPool.Release(splitBuffer);
            }
        }

        /// <inheritdoc/>
        public short ParseInt16(string value)
        {
            ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));
            return Int16.Parse(value, CultureInfo.InvariantCulture);
        }

        /// <inheritdoc/>
        public int ParseInt32(string value)
        {
            ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));
            return Int32.Parse(value, CultureInfo.InvariantCulture);
        }

        /// <inheritdoc/>
        public long ParseInt64(string value)
        {
            ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));
            return Int64.Parse(value, CultureInfo.InvariantCulture);
        }

        /// <inheritdoc/>
        public float ParseSingle(string value)
        {
            ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));
            return Single.Parse(value, CultureInfo.InvariantCulture);
        }

        /// <inheritdoc/>
        public Unit ParseUnit(string value, bool allowPercentage)
        {
            ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));

            var valueSpan = value.AsSpan();
            if (allowPercentage && value[value.Length - 1] == PercentageSuffix)
            {
                valueSpan = valueSpan.Slice(0, value.Length - 1);
                var percentage = Single.Parse(valueSpan, CultureInfo.InvariantCulture);
                return new Unit(percentage, UnitType.Percentage);
            }

            if (valueSpan.EndsWith(PixelSuffix))
            {
                valueSpan = valueSpan.Slice(0, value.Length - PixelSuffix.Length);
            }

            var floatValue = Single.Parse(valueSpan, CultureInfo.InvariantCulture);
            return new Unit(floatValue, UnitType.Pixel);
        }


        private static GridLength ParseGridLengthInternal(string value)
        {
            GridLengthType type;
            var valueSpan = value.AsSpan();
            if (value[^1] == GridLengthStarSuffix)
            {
                type = GridLengthType.Star;
                valueSpan = valueSpan.Slice(0, value.Length - 1).TrimEnd();
            }
            else
            {
                type = GridLengthType.Pixel;
                if (valueSpan.EndsWith(PixelSuffix))
                {
                    valueSpan = valueSpan.Slice(0, value.Length - PixelSuffix.Length).TrimEnd();
                }
            }

            var length = Single.Parse(valueSpan, CultureInfo.InvariantCulture);
            return new GridLength(length, type);
        }
    }
}
