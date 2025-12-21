//-----------------------------------------------------------------------
// <copyright file="StyleTexture.cs" company="Space Development">
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
    /// Style texture property structure.
    /// </summary>
    /// <seealso cref="IStyleValue{ITexture}" />
    public struct StyleTexture : IStyleValue<ITexture>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StyleTexture"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public StyleTexture(ITexture value)
            : this(value, StyleFlag.Explicit)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleTexture"/> struct.
        /// </summary>
        /// <param name="flag">The flag.</param>
        public StyleTexture(StyleFlag flag)
            : this(default, flag)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleTexture"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="flag">The flag.</param>
        public StyleTexture(ITexture value, StyleFlag flag)
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
        public ITexture Value { get; }
    }
}
