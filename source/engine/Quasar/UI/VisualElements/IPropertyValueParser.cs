//-----------------------------------------------------------------------
// <copyright file="IPropertyValueParser.cs" company="Space Development">
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

using Quasar.Graphics;
using Quasar.UI.VisualElements.Layouts;

namespace Quasar.UI.VisualElements
{
    /// <summary>
    /// Represents a style property value parser.
    /// </summary>
    public interface IPropertyValueParser
    {
        /// <summary>
        /// Parses the boolean value from the input string value.
        /// </summary>
        /// <param name="value">The value.</param>
        bool ParseBoolean(string value);

        /// <summary>
        /// Parses the color value from the input string value.
        /// </summary>
        /// <param name="value">The value.</param>
        Color ParseColor(string value);

        /// <summary>
        /// Parses the double precision floating point value from the input string value.
        /// </summary>
        /// <param name="value">The value.</param>
        double ParseDouble(string value);

        /// <summary>
        /// Parses the enumeration value from the input string value.
        /// </summary>
        /// <typeparam name="T">The enumeration type.</typeparam>
        /// <param name="value">The value.</param>
        T ParseEnum<T>(string value)
            where T : struct, IConvertible;

        /// <summary>
        /// Parses the grid column values from the string value to the grid column list.
        /// </summary>
        /// <param name="gridColumns">The grid columns.</param>
        /// <param name="value">The string value.</param>
        void ParseGridColumns(List<GridColumn> gridColumns, string value);

        /// <summary>
        /// Parses the grid row values from the string value to the grid row list.
        /// </summary>
        /// <param name="gridRows">The grid rows.</param>
        /// <param name="value">The string value.</param>
        void ParseGridRows(List<GridRow> gridRows, string value);

        /// <summary>
        /// Parses the Int16 value from the input string value.
        /// </summary>
        /// <param name="value">The value.</param>
        short ParseInt16(string value);

        /// <summary>
        /// Parses the Int32 value from the input string value.
        /// </summary>
        /// <param name="value">The value.</param>
        int ParseInt32(string value);

        /// <summary>
        /// Parses the Int64 value from the input string value.
        /// </summary>
        /// <param name="value">The value.</param>
        long ParseInt64(string value);

        /// <summary>
        /// Parses the single precision floating point value from the input string value.
        /// </summary>
        /// <param name="value">The value.</param>
        float ParseSingle(string value);

        /// <summary>
        /// Parses the unit value from the input string value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="allowPercentage">Allow percentage flag. Percentage value is allowed when set to true; otherwise not.</param>
        Unit ParseUnit(string value, bool allowPercentage);
    }
}