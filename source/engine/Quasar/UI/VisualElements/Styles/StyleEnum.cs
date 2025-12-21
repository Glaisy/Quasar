//-----------------------------------------------------------------------
// <copyright file="StyleEnum.cs" company="Space Development">
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
    /// Style enum property structure.
    /// </summary>
    /// <typeparam name="T">The enumeration type.</typeparam>
    /// <seealso cref="IStyleValue{T}" />
    public struct StyleEnum<T> : IStyleValue<T>
        where T : struct, IConvertible
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StyleEnum{T}"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public StyleEnum(T value)
            : this(value, StyleFlag.Explicit)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleEnum{T}"/> struct.
        /// </summary>
        /// <param name="flag">The flag.</param>
        public StyleEnum(StyleFlag flag)
            : this(default, flag)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleEnum{T}"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="flag">The flag.</param>
        public StyleEnum(T value, StyleFlag flag)
        {
            Value = value;
            Flag = flag;
        }


        /// <summary>
        /// Gets the flag.
        /// </summary>
        public StyleFlag Flag { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public T Value { get; }
    }
}
