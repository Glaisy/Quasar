//-----------------------------------------------------------------------
// <copyright file="StyleColor.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics;

namespace Quasar.UI.VisualElements.Styles
{
    /// <summary>
    /// Style color property structure.
    /// </summary>
    /// <seealso cref="IStyleValue{Color}" />
    public struct StyleColor : IStyleValue<Color>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StyleColor"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public StyleColor(Color value)
            : this(value, StyleFlag.Explicit)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleColor"/> struct.
        /// </summary>
        /// <param name="flag">The flag.</param>
        public StyleColor(StyleFlag flag)
            : this(Color.Transparent, flag)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleColor"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="flag">The flag.</param>
        public StyleColor(Color value, StyleFlag flag)
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
        public Color Value { get; }
    }
}
