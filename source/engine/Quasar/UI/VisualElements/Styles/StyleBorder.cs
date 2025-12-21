//-----------------------------------------------------------------------
// <copyright file="StyleBorder.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.UI.VisualElements.Styles
{
    /// <summary>
    /// Style border property structure.
    /// </summary>
    /// <seealso cref="IStyleValue{Border}" />
    public struct StyleBorder : IStyleValue<Border>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StyleBorder"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public StyleBorder(Border value)
            : this(value, StyleFlag.Explicit)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleBorder"/> struct.
        /// </summary>
        /// <param name="flag">The flag.</param>
        public StyleBorder(StyleFlag flag)
            : this(default, flag)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleBorder"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="flag">The flag.</param>
        public StyleBorder(Border value, StyleFlag flag)
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
        public Border Value { get; }
    }
}
