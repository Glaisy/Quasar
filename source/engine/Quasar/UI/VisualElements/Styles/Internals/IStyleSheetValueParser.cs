//-----------------------------------------------------------------------
// <copyright file="IStyleSheetValueParser.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;

using Quasar.Graphics;

namespace Quasar.UI.VisualElements.Styles.Internals
{
    /// <summary>
    /// Represents the style sheet attribute value parser component.
    /// </summary>
    internal interface IStyleSheetValueParser
    {
        /// <summary>
        /// Parses the string value to border (only pixel values are allowed).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="variables">The variables.</param>
        Border ParseBorder(string value, IReadOnlyDictionary<string, string> variables);

        /// <summary>
        /// Parses the string value to border units.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="variables">The variables.</param>
        /// <param name="allowPercentages">Allow percentages flag. Percentage values are allowed when set to true; otherwise not.</param>
        (Unit Top, Unit Right, Unit Bottom, Unit Left) ParseBorderUnits(
            string value,
            IReadOnlyDictionary<string, string> variables,
            bool allowPercentages);

        /// <summary>
        /// Parses the string value to color.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="variables">The variables.</param>
        Color ParseColor(string value, IReadOnlyDictionary<string, string> variables);

        /// <summary>
        /// Parses the string value to enum.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The string value.</param>
        /// <param name="variables">The variables.</param>
        T ParseEnum<T>(string value, IReadOnlyDictionary<string, string> variables)
            where T : struct;

        /// <summary>
        /// Parses the string value to literal.
        /// Quotes are validated and stripped from the input value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="variables">The variables.</param>
        string ParseLiteral(string value, IReadOnlyDictionary<string, string> variables);

        /// <summary>
        /// Parses the string value to unit.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="variables">The variables.</param>
        /// <param name="allowPercentage">Allow percentage flag. Percentage value is allowed when set to true; otherwise not.</param>
        Unit ParseUnit(string value, IReadOnlyDictionary<string, string> variables, bool allowPercentage);

        /// <summary>
        /// Parses the url(string) value to URL string value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="variables">The variables.</param>
        string ParseUrl(string value, IReadOnlyDictionary<string, string> variables);
    }
}
