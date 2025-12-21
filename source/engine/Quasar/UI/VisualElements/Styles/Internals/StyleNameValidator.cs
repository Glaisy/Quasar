//-----------------------------------------------------------------------
// <copyright file="StyleNameValidator.cs" company="Space Development">
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

using Space.Core.DependencyInjection;

namespace Quasar.UI.VisualElements.Styles.Internals
{
    /// <summary>
    /// Style name validator implementation.
    /// </summary>
    /// <seealso cref="IStyleNameValidator" />
    [Export(typeof(IStyleNameValidator))]
    [Singleton]
    internal class StyleNameValidator : IStyleNameValidator
    {
        private static readonly List<char> ValidSpecialCharacters = new List<char>
        {
            '-', '_'
        };


        /// <inheritdoc/>
        public bool IsValidName(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return false;
            }

            return IsValidInternal(value, 0, value.Length);
        }

        /// <inheritdoc/>
        public bool IsValidName(string value, int startIndex)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex, nameof(startIndex));

            if (String.IsNullOrEmpty(value))
            {
                return false;
            }

            return IsValidInternal(value, startIndex, value.Length - startIndex);
        }

        /// <inheritdoc/>
        public bool IsValidName(string value, int startIndex, int length)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex, nameof(startIndex));
            ArgumentOutOfRangeException.ThrowIfNegative(length, nameof(length));

            if (String.IsNullOrEmpty(value))
            {
                return false;
            }

            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex + length, value.Length, nameof(startIndex));

            return IsValidInternal(value, startIndex, length);
        }

        /// <inheritdoc/>
        public bool IsValidName(in ReadOnlySpan<char> value)
        {
            if (value.Length == 0)
            {
                return false;
            }

            return IsValidInternal(value, 0, value.Length);
        }

        /// <inheritdoc/>
        public bool IsValidName(in ReadOnlySpan<char> value, int startIndex)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex, nameof(startIndex));

            if (value.Length == 0)
            {
                return false;
            }

            return IsValidInternal(value, startIndex, value.Length - startIndex);
        }

        /// <inheritdoc/>
        public bool IsValidName(in ReadOnlySpan<char> value, int startIndex, int length)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex, nameof(startIndex));
            ArgumentOutOfRangeException.ThrowIfNegative(length, nameof(length));

            if (value.Length == 0)
            {
                return false;
            }

            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex + length, value.Length, nameof(startIndex));

            return IsValidInternal(value, startIndex, length);
        }


        private static bool IsValidInternal(in ReadOnlySpan<char> value, int startIndex, int length)
        {
            if (length == 1)
            {
                return Char.IsAsciiLetter(value[startIndex]);
            }

            if (!IsValidFirstCharacter(value[startIndex]))
            {
                return false;
            }

            var index = startIndex + 1;
            for (var i = 2; i < length; i++, index++)
            {
                if (!IsValidCharacter(value[index]))
                {
                    return false;
                }
            }

            return IsValidLastCharacter(value[index]);
        }

        private static bool IsValidCharacter(char character)
        {
            return Char.IsAsciiLetterOrDigit(character) || ValidSpecialCharacters.Contains(character);
        }

        private static bool IsValidFirstCharacter(char character)
        {
            return Char.IsAsciiLetter(character) || ValidSpecialCharacters.Contains(character);
        }

        private static bool IsValidLastCharacter(char character)
        {
            return Char.IsAsciiLetterOrDigit(character);
        }
    }
}
