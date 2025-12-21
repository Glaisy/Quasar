//-----------------------------------------------------------------------
// <copyright file="IStyleNameValidator.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.UI.VisualElements.Styles
{
    /// <summary>
    /// Represents a style name validator component.
    /// </summary>
    public interface IStyleNameValidator
    {
        /// <summary>
        /// Determines whether the specified string value is a valid style class/attribute/variable name, identifier etc.
        /// </summary>
        /// <param name="value">The value.</param>
        bool IsValidName(string value);

        /// <summary>
        /// Determines whether the specified string value from the start index is a valid style class/attribute/variable name, identifier etc.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="startIndex">The start index.</param>
        bool IsValidName(string value, int startIndex);

        /// <summary>
        /// Determines whether the specified string value's range is a valid style class/attribute/variable name, identifier etc.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="length">The length.</param>
        bool IsValidName(string value, int startIndex, int length);

        /// <summary>
        /// Determines whether the specified span of characters is a valid style class/attribute/variable name, identifier etc.
        /// </summary>
        /// <param name="value">The value.</param>
        bool IsValidName(in ReadOnlySpan<char> value);

        /// <summary>
        ///  Determines whether the specified span of characters from the start index is a valid style class/attribute/variable name, identifier etc.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="startIndex">The start index.</param>
        bool IsValidName(in ReadOnlySpan<char> value, int startIndex);

        /// <summary>
        /// Determines whether the specified span of characters' range is a valid style class/attribute/variable name, identifier etc.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="length">The length.</param>
        bool IsValidName(in ReadOnlySpan<char> value, int startIndex, int length);
    }
}
